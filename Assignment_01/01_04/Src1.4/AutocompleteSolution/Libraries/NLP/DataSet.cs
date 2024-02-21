using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLP.NGrams;
using NLP.Tokenization;

namespace NLP.NGramDataSet
{
    public class NGramDataSet
    {
        List<string> listOfWordsInDataSet;

        List<List<string>> listOfUnigrams;
        List<List<string>> listOfBigrams;
        List<List<string>> listOfTrigrams;

        public Dictionary<string, NGram> unigrams;
        public Dictionary<string, NGram> bigrams;
        public Dictionary<string, NGram> trigrams;

        // Constructor
        public NGramDataSet(List<string> tokenizedDataSet)
        {
            listOfWordsInDataSet = new List<string>();
            foreach(string word in tokenizedDataSet)
            {
                listOfWordsInDataSet.Add(word);
            }

        }
        public void computeTheListsOfNGrams()
        {
            listOfUnigrams = computeListOfUnigrams();
            listOfBigrams = computeListOfBigrams();
            listOfTrigrams = computeListOfTrigrams();
        }

        private List<List<string>> computeListOfUnigrams()
        {
            List<List<string>> unigramsList = new List<List<string>>();
            for (int i = 0; i < listOfWordsInDataSet.Count; i++)
            {
                List<string> unigram = new List<string>();
                unigram.Add(listOfWordsInDataSet[i]);
                unigramsList.Add(unigram);
            }
            return unigramsList;
        }

        private List<List<string>> computeListOfBigrams()
        {
            List<List<string>> bigrams = new List<List<string>>();
            for (int i = 0; i < listOfWordsInDataSet.Count - 1; i++)
            {
                List<string> bigram = new List<string>();
                bigram.Add(listOfWordsInDataSet[i]);
                bigram.Add(listOfWordsInDataSet[i + 1]);
                bigrams.Add(bigram);
            }
            return bigrams;
        }

        private List<List<string>> computeListOfTrigrams()
        {
            List<List<string>> trigrams = new List<List<string>>();
            for (int i = 0; i < listOfWordsInDataSet.Count - 2; i++)
            {
                List<string> trigram = new List<string>();
                trigram.Add(listOfWordsInDataSet[i]);
                trigram.Add(listOfWordsInDataSet[i + 1]);
                trigram.Add(listOfWordsInDataSet[i + 2]);
                trigrams.Add(trigram);
            }
            return trigrams;
        }

        public void computeNGrams()
        {
            
            // Unigrams

            unigrams = new Dictionary<string, NGram>();
            Dictionary<string, int> unigramsCounter = new Dictionary<string, int>();

            foreach (List<string> TokenList in listOfUnigrams)
            {
                string identifier = string.Join(" ", TokenList);
                if (unigrams.ContainsKey(identifier))
                {
                    unigramsCounter[identifier]++;
                }
                else
                {
                    // create a new NGram
                    NGram nGram = new NGram(identifier);
                    nGram.TokenList = TokenList;
                    unigrams.Add(identifier, nGram);
                    unigramsCounter.Add(identifier, 1);
                }
            }

            // add the frequency per million to the NGrams
            int totalNumberOfOccurencesOfAllUnigrams = unigramsCounter.Values.Sum();
            foreach (KeyValuePair<string, NGram> entry in unigrams)
            {


                entry.Value.FrequencyPerMillionInstances = 1000000 * (double)unigramsCounter[entry.Key] / totalNumberOfOccurencesOfAllUnigrams;
            }

            // Bigrams
            bigrams = new Dictionary<string, NGram>();
            Dictionary<string, int> bigramsCounter = new Dictionary<string, int>();
            for (int i = 0; i < listOfBigrams.Count; i++)
            {
                string identifier = string.Join(" ", listOfBigrams[i]);
                if (bigrams.ContainsKey(identifier))
                {
                    bigramsCounter[identifier]++;
                }
                else
                {
                    NGram nGram = new NGram(identifier);
                    nGram.TokenList = listOfBigrams[i];
                    bigrams.Add(identifier, nGram);
                    bigramsCounter.Add(identifier, 1);
                }
            }

            // add the frequency per million to the NGrams
            int totalNumberOfOccurencesOfAllBigrams = bigramsCounter.Values.Sum();
            foreach (KeyValuePair<string, NGram> entry in bigrams)
            {
                entry.Value.FrequencyPerMillionInstances = 1000000 * (double)bigramsCounter[entry.Key] / totalNumberOfOccurencesOfAllBigrams;
            }
            

            // Trigrams
            trigrams = new Dictionary<string, NGram>();
            Dictionary<string, int> trigramsCounter = new Dictionary<string, int>();
            for (int i = 0; i < listOfTrigrams.Count; i++)
            {
                string identifier = string.Join(" ", listOfTrigrams[i]);
                if (trigrams.ContainsKey(identifier))
                {
                    trigramsCounter[identifier]++;
                }
                else
                {
                    NGram nGram = new NGram(identifier);
                    nGram.TokenList = listOfTrigrams[i];
                    trigrams.Add(identifier, nGram);
                    trigramsCounter.Add(identifier, 1);
                }
            }

            // add the frequency per million to the NGrams
            int totalNumberOfOccurencesOfAllTrigrams = trigramsCounter.Values.Sum();
            foreach (KeyValuePair<string, NGram> entry in trigrams)
            {
                entry.Value.FrequencyPerMillionInstances = 1000000 * (double)trigramsCounter[entry.Key] / totalNumberOfOccurencesOfAllTrigrams;
            }

            // Print the 5 most frequent Uni, Bi and Trigrams
            
            // sort the ungirams, bigrams and trigrams by frequency

            var sortedUnigrams = unigrams.OrderByDescending(kv => kv.Value.FrequencyPerMillionInstances).ToDictionary(kv => kv.Key, kv => kv.Value).Take(5);
            var sortedBigrams = bigrams.OrderByDescending(kv => kv.Value.FrequencyPerMillionInstances).ToDictionary(kv => kv.Key, kv => kv.Value).Take(5);
            var sortedTrigrams = trigrams.OrderByDescending(kv => kv.Value.FrequencyPerMillionInstances).ToDictionary(kv => kv.Key, kv => kv.Value).Take(5);

            Console.WriteLine("The 5 most frequent Unigrams are:");
            foreach (KeyValuePair<string, NGram> entry in sortedUnigrams)
            {
                Console.WriteLine(entry.Key + "\t" + (int)entry.Value.FrequencyPerMillionInstances);
            }

            Console.WriteLine("The 5 most frequent Bigrams are:");
            foreach (KeyValuePair<string, NGram> entry in sortedBigrams)
            {
                Console.WriteLine(entry.Key + "\t" + (int)entry.Value.FrequencyPerMillionInstances);
            }

            Console.WriteLine("The 5 most frequent Trigrams are:");
            foreach (KeyValuePair<string, NGram> entry in sortedTrigrams)
            {
                Console.WriteLine(entry.Key + "\t" + (int)entry.Value.FrequencyPerMillionInstances);
            }


        }


    }
}
