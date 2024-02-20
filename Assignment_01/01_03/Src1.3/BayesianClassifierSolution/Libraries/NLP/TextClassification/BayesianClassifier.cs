using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class BayesianClassifier: TextClassifier
    {

        private int numberOfWordsInClass0 = -1;
        private int numberOfWordsInClass1 = -1; 
        private int numberOfClasses = 2;

        private List<double> prior;
        private List<TokenData> BagOfWords;

        List<string> wordsOfInterestForReport = new List<string> { "friendly", "perfectly", "horrible", "poor" };

        public override void Initialize(int numberOfFeatures) { 
            // not used -> only for compiler
        }

        public override int Classify(List<int> tokenIndexList) {
            // not used -> only for compiler
            return 0;
        }

        public override void Initialize(TextClassificationDataSet trainingDataSet, List<TokenData> BagOfWords)
        {
                prior = ComputePrior(trainingDataSet);
                this.BagOfWords = BagOfWords;
        }
 
        public override int Classify(List<Token> ListOfTokens)
        {
            // as in Lecture described use LOG as numerical values may be small
            List<double> posteriorSum = ComputerPosterior(ListOfTokens, this.BagOfWords);


            List<double> equationTerms = new List<double>();
            for (int i = 0; i < posteriorSum.Count; i++)
            {
                double equationTerm = Math.Log(this.prior[i]) + (posteriorSum[i]);
                // Lecture example
                //double equationTerm = (this.prior[i]) * (posteriorSum[i]);

                equationTerms.Add(equationTerm);
            }

            // find the index of the max value in equationTerms
            int maxIndex = equationTerms.IndexOf(equationTerms.Max());

            return maxIndex;
        }

        private List<double> ComputePrior(TextClassificationDataSet dataSet)
        {

            // P(c_j) = (number of documents with class c_j) / (total number of documents)     (4.22 Compendium)
            List<double> prior = new List<double>();
            prior.Add((double)dataSet.numberOfDocumentsOfClass0 / (dataSet.numberOfDocumentsOfClass0 + dataSet.numberOfDocumentsOfClass1));
            prior.Add((double)dataSet.numberOfDocumentsOfClass1 / (dataSet.numberOfDocumentsOfClass0 + dataSet.numberOfDocumentsOfClass1));

            return prior;
        }


        private List<double> ComputerPosterior(List<Token> listOfTokensInReview, List<TokenData> BagOfWords)
        {
            List<double> posterior = new List<double>();

            double posteriorClass0 = 0;
            double posteriorClass1 = 0;

            // lecture example (not LOG)
            //posteriorClass0 = 1;
            //posteriorClass1 = 1;

            // do the loop
            foreach (Token token in listOfTokensInReview){
                
                List<double> wordLikelyhood = computeWordLikelyhood(token, BagOfWords);

                posteriorClass0 += Math.Log(wordLikelyhood[0]);
                posteriorClass1 += Math.Log(wordLikelyhood[1]);
                // to check proper functionality with lecture example
                //posteriorClass0 *= (wordLikelyhood[0]);
                //posteriorClass1 *= (wordLikelyhood[1]);

            }

            posterior.Add(posteriorClass0);
            posterior.Add(posteriorClass1);



            return posterior;
        }

        private List<double> computeWordLikelyhood(Token token, List<TokenData> BagOfWords)
        {
            List<double> wordLikelyhood = new List<double>();

            // first find the token.Spelling in the BagOfWords
            TokenData tokenData = BagOfWords.Find(t => t.Token.Spelling == token.Spelling);

            // if token is not in BagOfWords -> as described in the compendium we neglect these cases
            if (tokenData == null)
            {
                wordLikelyhood.Add(1);
                wordLikelyhood.Add(1);
                //wordLikelyhood.Add(0);
                //wordLikelyhood.Add(0);
                return wordLikelyhood;
            }

            if (numberOfWordsInClass0 == -1 || numberOfWordsInClass1 == -1){
                // compute the number of words in the 2 classes
                numberOfWordsInClass0 = BagOfWords.Sum(t => t.Class0Count);
                numberOfWordsInClass1 = BagOfWords.Sum(t => t.Class1Count);
            }

            double likelyhoodForClass0 = (double)(tokenData.Class0Count + 1) / (numberOfWordsInClass0 + BagOfWords.Count);
            double likelyhoodForClass1 = (double)(tokenData.Class1Count + 1) / (numberOfWordsInClass1 + BagOfWords.Count);


            wordLikelyhood.Add(likelyhoodForClass0);
            wordLikelyhood.Add(likelyhoodForClass1);

            // For report print posteriors of "friendly", "perfectly", "horrible", "poor", print the posteriors of the words in the test set
            if(wordsOfInterestForReport.Contains(token.Spelling))
            //if (1 == 1)
            
            {
                if(token.Spelling == "horrible")
                {
                    Console.WriteLine("banane");
                }
                Console.WriteLine("Word: " + token.Spelling);
                likelyhoodForClass0 = (double)(tokenData.Class0Count) / (numberOfWordsInClass0);
                likelyhoodForClass1 = (double)(tokenData.Class1Count) / (numberOfWordsInClass1);
                int totalNumberOfWordOccurencesInBagOfWords = numberOfWordsInClass0 + numberOfWordsInClass1;
                double probabilityOfWordInBagOfWords = (double)(tokenData.Class1Count + tokenData.Class0Count) / (totalNumberOfWordOccurencesInBagOfWords  );
                double probabilityForClass0 = (double)((likelyhoodForClass0 * prior[0]) / (probabilityOfWordInBagOfWords));
                double probabilityForClass1 = (double)((likelyhoodForClass1 * prior[1]) / (probabilityOfWordInBagOfWords));
                double normalizationFactor = probabilityForClass0 + probabilityForClass1;
                probabilityForClass0 *= (1.0 / normalizationFactor);
                probabilityForClass1 *= (1.0 / normalizationFactor);
                Console.WriteLine("Likelyhood for class 0: " + probabilityForClass0.ToString("F6"));
                Console.WriteLine("Likelyhood for class 1: " + probabilityForClass1.ToString("F6"));
                //Console.WriteLine("Likelyhood for class 0: " + likelyhoodForClass0.ToString("F6"));
                //Console.WriteLine("Likelyhood for class 1: " + likelyhoodForClass1.ToString("F6"));
                // kick this word out of the list
                wordsOfInterestForReport.Remove(token.Spelling);
            }
            


            return wordLikelyhood;

        }
       
    
    }


}
