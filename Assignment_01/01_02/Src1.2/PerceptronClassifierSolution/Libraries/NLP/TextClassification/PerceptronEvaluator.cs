using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronEvaluator
    {
        public double accuracy;
        public double error;
        private bool toggleTestReviewTracker = false;
        public List<List<int>> TestReviewTrackerCorretlyClassified = new List<List<int>>();
        public List<List<int>> TestReviewTrackerFalselyClassified = new List<List<int>>();




        // take a PerceptronEvaluator object to predict the class of a sentence
        // compare the prediction with the ground truth

        public double evaluatePerceptron(PerceptronClassifier perceptronClassifier, TextClassificationDataSet dataSet)
        {
            int correctPredictions = 0;
            int totalPredictions = 0;

            foreach (TextClassificationDataItem item in dataSet.ItemList)
            {
                int predictedClass = perceptronClassifier.Classify(item.TokenIndexList);
                if (predictedClass == item.ClassLabel)
                {
                    correctPredictions++;
                    if (toggleTestReviewTracker){
                        TestReviewTrackerCorretlyClassified.Add(item.TokenIndexList);
                    }
                }
                else {
                    if (toggleTestReviewTracker){
                        TestReviewTrackerFalselyClassified.Add(item.TokenIndexList);
                    }
                }
                totalPredictions++;
            }
            accuracy = (double)correctPredictions / totalPredictions;
            error = 1 - accuracy;

            return accuracy;

        }

        public void ActivateTestReviewTracker()
        {
            toggleTestReviewTracker = true;
        }
    }
}
