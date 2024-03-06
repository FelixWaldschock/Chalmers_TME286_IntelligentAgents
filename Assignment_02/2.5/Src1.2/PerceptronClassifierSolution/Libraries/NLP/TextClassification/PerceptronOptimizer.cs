using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronOptimizer
    {   
        public Vocabulary Vocabulary { get; set; }
        public TextClassificationDataSet DataSet { get; set; }

        public List<double> Weights = new List<double>();
        public PerceptronClassifier perceptronClassifier = new PerceptronClassifier();
        public PerceptronOptimizer(TextClassificationDataSet dataSet, Vocabulary vocabulary) 
        {
            Vocabulary = vocabulary; 
            DataSet = dataSet;
            Weights = null;
        }

        public void Optimize()
        {
            List<TextClassificationDataItem> shuffledList = DataSet.ItemList.OrderBy(x => Guid.NewGuid()).ToList();
            List<TextClassificationDataItem> selectedReviews = shuffledList.Take(100).ToList();

            double learningRate = 0.05;

            foreach (TextClassificationDataItem review in selectedReviews)
            {
                perceptronClassifier.WeightList = Weights;
                int output = perceptronClassifier.Classify(review.ReviewAsVocabularyIndexes);
                int target = review.ClassLabel;
                
                List<int> vector = review.ReviewAsVocabularyIndexes;

                // Update Weights
                Dictionary<int, int> vectorDictionary = vector.GroupBy(idx => idx).ToDictionary(idxGroup => idxGroup.Key, idxGroup => idxGroup.Count());
                List<double> newWeights = Weights.ToList();

                foreach (KeyValuePair<int, int> entry in vectorDictionary)
                {
                    int index = entry.Key;
                    int instancesOfToken = entry.Value;
                    newWeights[index] += learningRate * (target - output) * instancesOfToken;
                }

                Weights = newWeights;
            }   
           
        }

    }

}
