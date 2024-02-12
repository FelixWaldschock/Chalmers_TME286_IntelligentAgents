﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronOptimizer

    {
        double learningRate = 0.001; 
        public int trainingEpochs = 0;


        // take the weights and bias from the Perceptron Classifier and update them according to EQ 4.10 
        public void trainClassifier(PerceptronClassifier perceptronClassifier, TextClassificationDataSet trainingDataSet)
        {
            // shuffle the dataset
            Random random = new Random();
            trainingDataSet.ItemList = trainingDataSet.ItemList.OrderBy(x => random.Next()).ToList();

            // loop over each item in the dataset
            foreach (TextClassificationDataItem item in trainingDataSet.ItemList)
            {
                // calculate the predicted class
                int predictedClass = perceptronClassifier.Classify(item.TokenIndexList);

                // calculate the error
                int error = item.ClassLabel - predictedClass;

                // create a dictionary with the sentence to find number of occurences
                Dictionary<int, int> tokenCounts = new Dictionary<int, int>();
                
                foreach(int tokenIndex in item.TokenIndexList)
                {
                    if (tokenCounts.ContainsKey(tokenIndex))
                    {
                        tokenCounts[tokenIndex] ++;
                    }
                    else
                    {
                        tokenCounts[tokenIndex] = 1;
                    }
                }

                if (error != 0)
                {

                    // ! Implementation with the dictionary
                    // Iterate over the dictionary
                    foreach (KeyValuePair<int, int> entry in tokenCounts)
                    {
                        // get the needed values for the training
                        int tokenIndex = entry.Key;
                        int featureValue = entry.Value;

                        // update the weights
                        perceptronClassifier.WeightList[tokenIndex] = perceptronClassifier.WeightList[tokenIndex] + (double)learningRate * error * featureValue;
    
                        // Do something with tokenIndex and count, for example, print them
                        //Console.WriteLine($"Token Index: {tokenIndex}, Count: {count}");
                    }



                    // // update the weigths
                    // for (int i = 0; i < item.TokenIndexList.Count; i++)
                    // {
                    //     // the token number represent simultanuously the index in the weigth vector
                    //     int token = item.TokenIndexList[i];
                    //     int featureValue = tokenCounts[token];
                        
                    //     // update the weight -> w_j = w_j + learningRate * error * x_ij
                    //     perceptronClassifier.WeightList[i] = perceptronClassifier.WeightList[i] + (double)learningRate * error * featureValue;
                    // }
                }

            }
            trainingEpochs++;
        }
    }
}
