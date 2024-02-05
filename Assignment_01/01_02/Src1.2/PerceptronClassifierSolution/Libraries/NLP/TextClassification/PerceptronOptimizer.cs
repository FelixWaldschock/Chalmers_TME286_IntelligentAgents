using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    internal class PerceptronOptimizer

    {
        double learningRate = 0.1;

        // take the weights and bias from the Perceptron Classifier and update them according to EQ 4.10 
        public void updateClassifier(PerceptronClassifier perceptronClassifier, TextClassificationDataSet dataSet)
        {
            // loop over each item in the dataset
            foreach (TextClassificationDataItem item in dataSet.ItemList)
            {
                // calculate the predicted class
                int predictedClass = perceptronClassifier.Classify(item.TokenIndexList);

                // calculate the error
                int error = item.ClassLabel - predictedClass;

                // update the weigths
                for (int i = 0; i < item.TokenIndexList.Count; i++)
                {
                    // w_j = w_j + learningRate * error * x_ij
                    perceptronClassifier.WeightList[i] += learningRate * error * item.TokenIndexList[i];
                }

            }
        }

        public void optimizeClassifier(PerceptronEvaluator PerceptronEvaluator, PerceptronClassifier perceptronClassifier, TextClassificationDataSet dataSet)
        {
            while (PerceptronEvaluator.accuracy < 0.9)
            {
                updateClassifier(perceptronClassifier, dataSet);
                evaluatePerceptron(perceptronClassifier, dataSet);
            }
        }



    }
}
