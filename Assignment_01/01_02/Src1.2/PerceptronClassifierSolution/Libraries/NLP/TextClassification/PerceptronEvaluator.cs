using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    internal class PerceptronEvaluator
    {
        public float accuracy;

        // take a PerceptronEvaluator object to predict the class of a sentence
        // compare the prediction with the ground truth

        public void evaluatePerceptron(PerceptronClassifier perceptronClassifier, TextClassificationDataSet dataSet)
        {
            int correctPredictions = 0;
            int totalPredictions = 0;

            foreach (TextClassificationDataItem item in dataSet.ItemList)
            {
                int predictedClass = perceptronClassifier.Classify(item.TokenIndexList);
                if (predictedClass == item.ClassLabel)
                {
                    correctPredictions++;
                }
                totalPredictions++;
            }
            accuracy = (float)correctPredictions / totalPredictions;

        }
    }
}
