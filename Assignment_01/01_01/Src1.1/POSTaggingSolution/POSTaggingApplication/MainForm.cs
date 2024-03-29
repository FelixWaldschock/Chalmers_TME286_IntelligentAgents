﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLP;
using NLP.POS;
using NLP.POS.Taggers;


namespace POSTaggingApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILE_FILTER = "Text files (*.txt)|*.txt";
        private POSDataSet completeDataSet = null;
        private POSDataSet trainingDataSet = null;
        private POSDataSet testDataSet = null;
        private List<TokenData> vocabulary = null;
        private UnigramTagger unigramTagger;

        // add the POSTagConverter class
        private POSTagConverter BrownToUniTagConverter = null;
        private Dictionary<string, WordData> wordDataDictionary;

        public MainForm()
        {
            InitializeComponent();
            resultsListBox.Font = new Font("Aptos", 9);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Note: 
        // The Brown corpus is available on the Canvas web page.
        // It can also be obtained at http://www.sls.hawaii.edu/bley-vroman/brown_corpus.html
        // in the file "browntag_nolines.txt: Corpus in one file, tagged, no line numbers, each sentence is one line"
        private void loadPOSCorpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILE_FILTER;
                int tokenCount = 0;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader fileReader = new StreamReader(openFileDialog.FileName);
                    completeDataSet = new POSDataSet();
                    while (!fileReader.EndOfStream)
                    {
                        string line = fileReader.ReadLine();
                        if (line != "")
                        {
                            List<string> lineSplit = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            List<TokenData> tokenDataList = new List<TokenData>();
                            Sentence sentence = new Sentence();
                            foreach (string lineSplitItem in lineSplit)
                            {
                                List<string> spellingAndTag = lineSplitItem.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                Token token = new Token();
                                if (spellingAndTag.Count == 2) // Needed in order to ignore the very last line that just contains "_.".
                                {
                                    token.Spelling = spellingAndTag[0].ToLower().Trim(); // Convert all words to lowercase.
                                    token.POSTag = spellingAndTag[1].Trim();

                                    
                                }
                                TokenData tokenData = new TokenData(token);
                                if (token.POSTag.Length == 1 || token.POSTag[1] != '|') // A somewhat ugly fix, needed to remove some junk from the data ...
                                {
                                    tokenDataList.Add(tokenData);
                                    tokenCount++;
                                }
                            }
                            sentence.TokenDataList = tokenDataList;
                            completeDataSet.SentenceList.Add(sentence);
                        }
                    }
                    fileReader.Close();
                    resultsListBox.Items.Add("Loaded the Brown corpus with " + completeDataSet.SentenceList.Count.ToString()
                        + " sentences and " + tokenCount.ToString() + " tokens.");
    
                }

            }
        }

        private List<TokenData> GenerateVocabulary(POSDataSet dataSet)
        {
            List<TokenData> tmpTokenDataList = new List<TokenData>();
            foreach (Sentence sentence in dataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    tmpTokenDataList.Add(tokenData);
                }
            }
            // Sort in alphabetical order, then by tag (also in alphabetical order)...
            // This takes a few seconds to run: It would have been more elegant (and easy) to put the
            // computation in a separate thread, but I didn't bother to do that here, as it would make
            // the code slightly more complex. Here, it is OK that the application freezes for a few
            // seconds while it is sorting the data.
            tmpTokenDataList = tmpTokenDataList.OrderBy(t => t.Token.Spelling).ThenBy(t => t.Token.POSTag).ToList();
            // ... then merge
            List<TokenData> tokenDataList = MergeTokens(tmpTokenDataList);
            return tokenDataList;
        }

        private List<TokenData> MergeTokens(List<TokenData> unmergedDataSet)
        {
            List<TokenData> mergedDataSet = new List<TokenData>();
            if (unmergedDataSet.Count > 0)
            {
                int index = 0;
                Token currentToken = new Token();
                currentToken.Spelling = unmergedDataSet[index].Token.Spelling;
                currentToken.POSTag = unmergedDataSet[index].Token.POSTag;
                TokenData currentTokenData = new TokenData(currentToken);
                index++;
                while (index < unmergedDataSet.Count)
                {
                    Token nextToken = unmergedDataSet[index].Token;
                    if ((nextToken.Spelling == currentToken.Spelling) && (nextToken.POSTag == currentToken.POSTag))
                    {
                        currentTokenData.Count += 1;
                    }
                    else
                    {
                        mergedDataSet.Add(currentTokenData);
                        currentToken = new Token();
                        currentToken.Spelling = unmergedDataSet[index].Token.Spelling;
                        currentToken.POSTag = unmergedDataSet[index].Token.POSTag;
                        currentTokenData = new TokenData(currentToken);
                    }
                    index++;
                }
                mergedDataSet.Add(currentTokenData); // Add the final element as well ...
            }
            return mergedDataSet;
        }


        private void loadTagConversionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Write code here: This method should load the tag conversion file. You must
            // represent the mappings somehow, so that the various tags in Brown set
            // can be mapped to the universal tag set. For example, one can maintain
            // a list with rows containing two elements (e.g. List<string[2]>, or List<List<string>>, where
            // the inner list should then contain two elements): The Brown tag and the corresponding
            // Universal tag, e.g.,
            //
            // VBZ -> VERB (verb in 3rd person present tense)
            // VB  -> VERB (verb in infinitive form)
            // NN  -> NOUN (noun in singular form)
            // NNS -> NOUN (noun in plural form) 
            // 
            // ...and so on. 
            // An even more elegant (and more re-usable) way is to define a class called, for example, POSTagConverter,
            // with a Convert method that takes a tag as input and outputs the converted tags.
            // Note that, since the data sets are not very large, you don't need to care much about the speed of the code.
            // Thus when searching for an input tag, you can use the Find() method instead of, say, a binary search 
            // or a Dictionary (but it's up to you).
            // --------------------------------------------------------------------------------

      
            // init the 2D list to store the Brown and Universal tags
            List<List<string>> BrownAndUniList = new List<List<string>>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILE_FILTER;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader fileReader = new StreamReader(openFileDialog.FileName);  

    

                    while (!fileReader.EndOfStream)
                    {
                        string line = fileReader.ReadLine();
                        // Process data here ...

                        // split the line where a space is 
                        List<string> lineSplit = line.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();


                        foreach (string lineSplitItem in lineSplit)
                        { 
                            // split the line where a \t is
                            List<string> BrownAndUni = lineSplitItem.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            
                            // Access the BrownTag
                            string BrownTag = BrownAndUni[0].Trim();
                            string UniTag = BrownAndUni[1].Trim();

                            // Add the Brown and Universal tags to the 2D list
                            BrownAndUniList.Add(new List<string> { BrownTag, UniTag });

                            // DEBUG

                        // Process data stop
                        }
                    }
                }
            }

            // Keep these lines: They will activate the conversion button, provided that the
            // Brown data set has been loaded first:
            if (completeDataSet != null)
            {
                if (completeDataSet.SentenceList.Count > 0)
                {
                    convertPOSTagsButton.Enabled = true;
                }
            }

            // Create the POSTagConverter object
            BrownToUniTagConverter = new POSTagConverter(BrownAndUniList);

            BrownToUniTagConverter.showConverter();

            // Print to the resultsListBox
            resultsListBox.Items.Add("Loaded the Brown Corpus and the tag conversion data");
            resultsListBox.Items.Add("Size of the BrownAndUniList: " + BrownToUniTagConverter.getConverterSize());


        }

        private void convertPOSTagsButton_Click(object sender, EventArgs e)
        {
            // Write code here, such that the Brown tags are mapped to the 
            // Universal tags (for the complete data set), using the representation described above 
            // After running this method, all the tokens should be assigned
            // one of the 12 Universal tags.
            // 

            // --------------------------------------------------------------------------------

            // loop over the entire dataSet

            resultsListBox.Items.Add("Conversion starts (window might freeze)"); // Keep this line.

            int conversionCounter = 0;

 

            foreach (Sentence sentence in completeDataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    

                    // get the Brown tag
                    string BrownTag = tokenData.Token.POSTag;

                    // convert the Brown tag to the Universal tag
                    string UniTag = BrownToUniTagConverter.getUniversalTag(BrownTag);

                    // Increment the posTagCounter in the BrownToUniTagConverter
                    BrownToUniTagConverter.updatePOSCounters(UniTag);


                    // set the Universal tag
                    tokenData.Token.BrownTag = BrownTag;
                    tokenData.Token.POSTag = UniTag;

                    conversionCounter++;


                }
            }

            resultsListBox.Items.Add("Converted " + conversionCounter + " tags from Brown to Universal");

            // --------------------------------------------------------------------------------

            // Method call: 
            // completeDataSet.ConvertPOSTags(... <suitable input, namely the tag conversion data> ...); // this you have to write ...

            // Next, build the vocabulary, using the 12 universal tags (this method you get for free! :) )
            vocabulary = GenerateVocabulary(completeDataSet);


            // Keep this line: It will activate the split button.
            splitDataSetButton.Enabled = true;
            convertPOSTagsButton.Enabled = false;
        }

        private void splitDataSetButton_Click(object sender, EventArgs e)
        {
            // Split the data set into a training set and a test set (a validation
            // set is not needed here, since no optimization is carried out - the
            // unigram tagger is as it is - no optimization required or possible).
            // The result should be 
            //
            //  trainingDataSet (containing, by default, 80% of the sentences)
            //
            //  testDataSet (contaning the remaining 20% of the sentences)
            //
            double splitFraction;
            Boolean splitFractionOK = double.TryParse(splitFractionTextBox.Text, out splitFraction);
            if (splitFractionOK && splitFraction > 0 && splitFraction <= 1)
            {
                // NOTE: The most elegant way to do this is to write a static method in the POSDataSet class,
                // such as, POSDataSet.Split(POSDataSet completeDataSet, double splitFraction).
                // One should always strive to put methods *where they naturally belong*. In this case,
                // the split method belongs with the POSDataSet. One can also, of course,
                // just write the code here (in this method), instantiating the trainingDataSet and
                // the testSet, and then just adding sentences, but the most elegant way is
                // to define a method in the POSDataSet class. You can read about static
                // methods on MSDN or StackOverflow, for example

                // FXW Start

                (trainingDataSet, testDataSet) = POSDataSet.Split(completeDataSet, splitFraction);

                // Give user feedback
                // show size of the training and test data set
                resultsListBox.Items.Add("Size of the training data set: " + trainingDataSet.SentenceList.Count +" senteces");
                resultsListBox.Items.Add("Size of the test data set: " + testDataSet.SentenceList.Count +" senteces");



                // FXW Stop

                // Keep these lines: It will activate the statistics generation button and the unigram tagger generation button,
                // once the data set has been split.
                generateStatisticsButton.Enabled = true;
                generateUnigramTaggerButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Incorrectly specified split fraction", "Error", MessageBoxButtons.OK);
            }
        }

        private void generateStatisticsButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear(); // Keep this line.

            // Write code here for carrying out all the steps described in 
            // Assignment 1.1a, using the (note!) training set

            // Put different parts of the assignment (different subtasks) in
            // separate methods, in order to keep the code neat and tidy.

            // All the results should be printed *neatly* to the screen, in the
            // associated listBox (resultsListBox), after first clearing it.
            // To add things to the result listbox, one uses the command:
            // resultListBox.Items.Add(...);
            // where ... is a text string. Include empty lines with
            // resultListBox.Items.Add(" ");
            // where appropriate (e.g., between different
            // subtasks, to make the output more readable).

            // --------------------------------------------------------------------------------

            // (i)

            // init a List of PosTagData objects 
            List<POSTagData> POSTagDataList = new List<POSTagData>();
            Dictionary<string, POSTagData> tagDictionary = new Dictionary<string, POSTagData>();
            int sumOfTokens = 0;

            // loop over the entire trainingDataSet
            foreach (Sentence sentence in trainingDataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    // get the tag
                    string tag = tokenData.Token.POSTag;

                    // check if the tag is already in the dictionary
                    if (tagDictionary.TryGetValue(tag, out var existingTagData))
                    {
                        // increment the count of the tag
                        existingTagData.count++;
                    }
                    else
                    {
                        // create a new POSTagData object
                        POSTagData newPOSTagData = new POSTagData(tag, 1);

                        // add the new POSTagData object to the list and dictionary
                        POSTagDataList.Add(newPOSTagData);
                        tagDictionary.Add(tag, newPOSTagData);
                    }

                    sumOfTokens++;
                }
            }



            // compute the fractions
            foreach (POSTagData POSTagData in POSTagDataList)
            {
                POSTagData.computeFraction(sumOfTokens);
            }

            // sort the list by count descending
            POSTagDataList = POSTagDataList.OrderByDescending(x => x.count).ToList();

            // ToDo insert the statistics from the BrownToUniTagConverter to the POSTagDataList
            List<Statistics> statisticsList = new List<Statistics>();
            int sumOfAllTags = 0;

            for(int i = 0; i < POSTagDataList.Count; i++)
            {
                string tag = POSTagDataList[i].name;
                int count = POSTagDataList[i].count;
                double fraction = POSTagDataList[i].fraction;
                sumOfAllTags += count;

                Statistics newStatistic = new Statistics(tag, count, fraction);
                statisticsList.Add(newStatistic);
            }

            // sort the list by count descending
            statisticsList = statisticsList.OrderByDescending(x => x.Count).ToList();

            // print to user
            resultsListBox.Items.Add("Most frequent tags:");
            resultsListBox.Items.Add("Tag\tCount\tFraction");

            for (int i = 0; i < statisticsList.Count; i++)
            {
                string output = ($"{statisticsList[i].Name}:\t{statisticsList[i].Count}\t{statisticsList[i].Fraction.ToString("F5")}");
                resultsListBox.Items.Add(output);
            }

            // print the sum of the fractions
            double sumOfFractions = 0;  
            foreach (POSTagData POSTagData in POSTagDataList)
            {
                sumOfFractions += POSTagData.fraction;
            }
            
            // sum of all statistics.Counts
            int sumOfAllTokens = statisticsList.Sum(x => x.Count);

            resultsListBox.Items.Add("Total: \t" + sumOfAllTokens + "\t" + sumOfFractions);
            //Console.WriteLine("Total: \t" + sumOfAllTokens + "\t" + sumOfFractions);
            
            // --------
            // compute how many words have n-tags
            wordDataDictionary = new Dictionary<string, WordData>();

            // Loop over the entire trainingDataSet
            foreach (Sentence sentence in trainingDataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    // Get the word and tag
                    string word = tokenData.Token.Spelling;

                    // Check if the word is already in the dictionary
                    if (wordDataDictionary.TryGetValue(word, out WordData existingWordData))
                    {
                        // Add the new tag to the existing WordData object
                        existingWordData.AddTag(tokenData.Token.POSTag);
                    }
                    else
                    {
                        // Create a new WordData object
                        WordData newWordData = new WordData(word, tokenData.Token.POSTag);

                        // Add the new WordData object to the dictionary
                        wordDataDictionary.Add(word, newWordData);
                    }
                }
            }

            // Convert dictionary to list
            List<WordData> WordDataList = wordDataDictionary.Values.ToList();

            Dictionary<int, int> tagCountDistribution = new Dictionary<int, int>();

            // Count the number of words for each tag count
            foreach (WordData word in WordDataList)
            {
                int tagCount = word.TagsCount.Count;

                if (tagCountDistribution.TryGetValue(tagCount, out int count))
                {
                    tagCountDistribution[tagCount] = count + 1;
                }
                else
                {
                    tagCountDistribution.Add(tagCount, 1);
                }
            }



            // Calculate the likelihood for each tag count
            List<string> likelihoodLines = new List<string>();
            int totalWords = WordDataList.Count;
            sumOfFractions = 0;

            for (int i = 1; i <= 12; i++)
            {
                int countForTag = tagCountDistribution.TryGetValue(i, out int count) ? count : 0;
                double likelihood = (double)countForTag / totalWords;
                sumOfFractions += likelihood;

                likelihoodLines.Add($"{i}:\t{countForTag}\t{likelihood.ToString("F5")}");
            }

            // Print the likelihood lines to the resultsListBox
            resultsListBox.Items.Add("");
            resultsListBox.Items.Add("\nDistribution of n-tags per spelling");
            resultsListBox.Items.Add("#Tags\tCount\tFraction");

            foreach (string line in likelihoodLines)
            {
                resultsListBox.Items.Add(line);

            }
            resultsListBox.Items.Add("Total:\t" + totalWords.ToString() + "\t" + sumOfFractions.ToString("F5"));


        }

        private void generateUnigramTaggerButton_Click(object sender, EventArgs e)
        {
            // Write code here for generating a unigram tagger, again using the *training* set;
            // Here, you *should* Define a class Unigram tagger derived from (inheriting) the base class
            // POSTagger in the NLP library included in this solution.


            // --------------------------------------------------------------------------------

            // -------
                // create a vocabulary of the trainingDataSet
            List<TokenData> vocabularyTrainingDataSet = GenerateVocabulary(trainingDataSet);

            // unique spellings in the vocabularyTrainingDataSet
            List<string> uniqueSpellings = vocabularyTrainingDataSet.Select(x => x.Token.Spelling).Distinct().ToList();

            Dictionary<string, TokenData> UnigramTaggerWordDataDictionary = new Dictionary<string, TokenData>();

            // loop over the unique spellings
            foreach(string spelling in uniqueSpellings)
            {
                // find the spelling in the wordDataDictionary and get the most frequent tag
                if (wordDataDictionary.TryGetValue(spelling, out WordData wordData))
                {
                    string mostFrequentTag = wordData.GetMostFrequentTag();

                    // create a new TokenData object with the spelling and most frequent tag
                    Token newToken = new Token();
                    newToken.Spelling = spelling;
                    newToken.POSTag = mostFrequentTag;

                    TokenData newTokenData = new TokenData(newToken);
                    
                    // add this pair to the UnigramTaggerWordDataDictionary
                    UnigramTaggerWordDataDictionary[spelling] = newTokenData;
                }

            }





            // create the Unigram Tagger
            unigramTagger = new UnigramTagger(UnigramTaggerWordDataDictionary);

            resultsListBox.Items.Add("Unigram tagger generated!");

           

            // --------------------------------------------------------------------------------

            // For the actual tagging (once the unigram tagger has been generated)
            // you must override the Tag() method in the base class (POSTagger)).

            // Note that, for most POS taggers, it matters whether or not a word is
            // (say) the first word or the last word of a sentence, but not for the
            // unigram tagger, so it is easy to write the Tag() method - it need not
            // take into account the position of the word in the sentence.

            // Keep this line: It will activate the evaluation button for the unigram tagger
            runUnigramTaggerButton.Enabled = true;
        }

        private void runUnigramTaggerButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear(); // Keep this line.

            // convert the testDataSet to a list of tokens
            List<TokenData> testSet = new List<TokenData>();
            List<string> testSetTags = new List<string>();

            int debug = 0;

            // loop over the entire testDataSet
            foreach (Sentence sentence in testDataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    // add the token to the testSet
                    testSet.Add(tokenData);
                    testSetTags.Add(tokenData.Token.POSTag);
                    debug++;
                }
            }

            //Console.WriteLine(testSet.Count);

            // Run Unigram Tagger over the token set
            Sentence testSentence = new Sentence();
            testSentence.TokenDataList = testSet;


            List<string> tags = unigramTagger.Tag(testSentence);

            //Console.WriteLine("Number of tags: " + tags.Count);
            //Console.WriteLine("Number of testSetTags: " + testSetTags.Count);

            float accuray;
            int counter = 0;
            int correctClassifiedTags = 0;

            for(int i = 0; i < testSetTags.Count; i++)
            {
                if (tags[i] == "UNKNOWNSPELLING")
                {
                    continue;
                }

                if(testSetTags[i] == tags[i])
                {
                    correctClassifiedTags++;
                }
                /*
                else
                {
                    //Console.WriteLine("Should:\t" + testSetTags[i] + "\tPredict:\t" + tags[i] + "\tSpelling:\t" + testSentence.TokenDataList[i].Token.Spelling);
                }
                */
                counter++;
            }
            accuray = (float)correctClassifiedTags / counter;

            resultsListBox.Items.Add("Total number of tags: " + counter);
            resultsListBox.Items.Add("Correctly classified tags: " + correctClassifiedTags);
            resultsListBox.Items.Add("Accuracy: " + accuray);



            // Write code here for running the unigram tagger over the test set.
            // All the results should be printed *neatly* to the screen, in the
            // associated listBox (resultsListBox), after first clearing it.

            // Note again that, for most POS taggers, it matters whether or not a word is
            // (say) the first word or the last word of a sentence, but not for the
            // unigram tagger. Thus, when you run the unigram tagger the "sentence"
            // that goes into the Tag() method can simply be the entire list of
            // tokens in the test set (which you must extract from the testSet -
            // remember that the test set is a list of TokenCount, whereas you
            // here need a list of Token (it is easy to do so`)).
        }
    }
}
