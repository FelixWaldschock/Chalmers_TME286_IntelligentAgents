using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // For the Path class; see LoadDataSet()
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLP;
using NLP.TextClassification;
using NLP.Tokenization;

namespace BayesianClassifierApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILE_FILTER = "Text files (*.txt)|*.txt";
        private List<string> abbreviations = new List<string> { "i.e.", "e.g.", "etc.", "mr.", "mrs." };

        private TextClassificationDataSet trainingSet = null;
        private TextClassificationDataSet testSet = null;
        private Tokenizer tokenizer;

        private List<Token> listOfTrainingTokens;
        private List<Token> listOfTestTokens;
        private List<Sentence> listOfTestSentences;

        private List<TokenData> BagOfWords;
        private BayesianClassifier bayesianClassifier;

        public MainForm()
        {
            InitializeComponent();
        }

        private TextClassificationDataSet LoadDataSet()
        {
            TextClassificationDataSet dataSet = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILE_FILTER;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataSet = new TextClassificationDataSet();
                    StreamReader dataReader = new StreamReader(openFileDialog.FileName);
                    while (!dataReader.EndOfStream)
                    {
                        string line = dataReader.ReadLine();
                        List<string> lineSplit = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        TextClassificationDataItem item = new TextClassificationDataItem();
                        item.Text = lineSplit[0].ToLower();
                        item.ClassLabel = int.Parse(lineSplit[1]);
                        dataSet.ItemList.Add(item);
                    }
                    dataReader.Close();
                    int count0 = dataSet.ItemList.Count(i => i.ClassLabel == 0);
                    int count1 = dataSet.ItemList.Count(i => i.ClassLabel == 1);
                    string fileName = Path.GetFileName(openFileDialog.FileName); // File name without the file path.
                    progressListBox.Items.Add("Loaded data file \"" + fileName + "\" with " + count0.ToString() +
                        " negative reviews and " + count1.ToString() + " positive reviews.");
                    dataSet.numberOfDocumentsOfClass0 = count0;
                    dataSet.numberOfDocumentsOfClass1 = count1;
                }
            }
            return dataSet;
        }

        private void loadTrainingSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainingSet = LoadDataSet();
            if ((trainingSet != null) && (testSet != null)) { tokenizeButton.Enabled = true; }
            loadTrainingSetToolStripMenuItem.Enabled = false; // To avoid accidentally reloading the training set instead of the validation set...
        }

        private void loadTestSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testSet = LoadDataSet();
            if ((trainingSet != null) && (testSet != null)) { tokenizeButton.Enabled = true; }
            loadTestSetToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tokenizeButton_Click(object sender, EventArgs e)
        {
            // init the tokenize
            tokenizer = new Tokenizer(abbreviations);


            // init a list of tokens for the trainingSet
            listOfTrainingTokens = new List<Token>();

            // loop over the trainingSet
            foreach (TextClassificationDataItem item in trainingSet.ItemList)
            {
                List<Token> tokens = tokenizer.Tokenize(item.Text, item.ClassLabel);
                listOfTrainingTokens.AddRange(tokens);

            }

            // tokenize the test set
            listOfTestSentences = new List<Sentence>();
            listOfTestTokens = new List<Token>();

            // loop over the testset
            foreach (TextClassificationDataItem item in testSet.ItemList)
            {
                List<Token> tokensList = tokenizer.Tokenize(item.Text, item.ClassLabel);
                Sentence newSentence = new Sentence();
                newSentence.TokenList = tokensList;
                newSentence.ClassLabel = item.ClassLabel;
                listOfTestSentences.Add(newSentence);
                listOfTestTokens.AddRange(tokensList);

            }
            progressListBox.Items.Add("Tokenized: " + (listOfTrainingTokens.Count + listOfTestTokens.Count).ToString() + " tokens");
            generateBagOfWordsButton.Enabled = true;
            
        }

        private void createBagOfWords()
        {
            // Use the training set to create the bag of words
            // the bag of words should be a list of TokenData objects
            // when creating the bag of words, the frequency of each word should be counted and stored
            // in the TokenData.Count variable, and the frequency of each word in each class should be
            // counted and stored in the TokenData.Class0Count and TokenData.Class1Count variables.

            // Create the bag of words
            BagOfWords = new List<TokenData>();

            // loop over the listOfTrainingTokens and count the frequency of each word
            foreach (Token token in listOfTrainingTokens)
            {
                // check if the spelling of the token already exists in the bag of words
                TokenData tokenData = BagOfWords.Find(t => t.Token.Spelling.Equals(token.Spelling));

                // if the token is not in the bag of words, add it
                if (tokenData == null)
                {
                    tokenData = new TokenData(token);
                    BagOfWords.Add(tokenData);
                }
                else
                {
                    tokenData.Count++;
                }

                // count the frequency of each word in each class
                if (token.ClassLabel == 0)
                {
                    tokenData.Class0Count++;
                }
                else
                {
                    tokenData.Class1Count++;
                }
            }

        }

        private void generateBagOfWordsButton_Click(object sender, EventArgs e)
        {
            createBagOfWords();
            progressListBox.Items.Add("Generated Bag of Words with: " + BagOfWords.Count() + " unique words");
            initClassifierButton.Enabled = true;
        }

        private void initClassifierButton_Click(object sender, EventArgs e)
        {
            bayesianClassifier = new BayesianClassifier();
            bayesianClassifier.Initialize(trainingSet, BagOfWords);
            progressListBox.Items.Add("Classifier created");
            classifyTestSetButton.Enabled = true;
        }

        private void classifyTestSetButton_Click(object sender, EventArgs e)
        {
            int counterCorrectClassified = 0;
            int counterTotalClassified = 0;

            int TPCounter = 0;
            int TNCounter = 0;
            int FPCounter = 0;
            int FNCounter = 0;

            foreach (Sentence sentence in listOfTestSentences)
            {
                int designatedLabel = sentence.ClassLabel;
                int predictedLabel = bayesianClassifier.Classify(sentence.TokenList);

                if (designatedLabel == predictedLabel)
                {
                    counterCorrectClassified++;
                }
                counterTotalClassified++;

                if (designatedLabel == 1) { 
                    if (predictedLabel == 1)
                    {
                        TPCounter++;
                    }
                    else { 
                        FNCounter++; 
                    }
                }

                else
                {
                    if (predictedLabel == 0)
                    {
                        TNCounter++;
                    }
                    else
                    {
                        FPCounter++;
                    }
                }   


            }
            

            double precision = (double)TPCounter / (TPCounter + FPCounter);                                         // (eq. 4.29)
            double recall = (double)TPCounter / (TPCounter + FNCounter);                                            // (eq. 4.30)
            double accuracy = (double)(TPCounter + TNCounter) / (TPCounter + FNCounter + TNCounter + FPCounter);    // (eq. 4.31)
            double F1value = (double)2 * precision * recall / (precision + recall);                                 // (eq. 4.32)

            progressListBox.Items.Add("Classifier properties: ");
            progressListBox.Items.Add("Precision: \t" + precision.ToString("F6"));
            progressListBox.Items.Add("Recall: \t" + recall.ToString("F6"));
            progressListBox.Items.Add("Accuracy: \t" + accuracy.ToString("F6"));
            progressListBox.Items.Add("F1 value: \t" + F1value.ToString("F6"));
        }
    }
}
