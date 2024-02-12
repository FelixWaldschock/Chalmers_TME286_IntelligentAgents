using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronClassifier: TextClassifier
    {
        private double bias = 0.0;
        private List<double> weightList = null;
        private int numberOfEpochs = 0;
        private double bestTestingAccuracy;
        public double bestValidationAccuracy;
        private List<double> bestWeights;
        private int bestEpoch;
        public List<double> trackerTestingAccuracy = new List<double>();
        public List<double> trackerValidationAccuracy = new List<double>();
        public List<double> trackerTrainingAccuracy = new List<double>();



        public override void Initialize(int numberOfFeatures)
        {
            Random random = new Random(0);
            weightList = new List<double>();
            for (int i = 0; i < numberOfFeatures; i++)
            {
                weightList.Add(random.NextDouble());
            }
            Console.WriteLine("Number of weights initialized: " + numberOfFeatures.ToString());

            // Write this method, setting up the vector of (initially random) weights.
            // Here you can use the Random class, with a suitable (integer) random 
            // number seed.
        }

        public override int Classify(List<int> tokenIndexList)
        {
            // ToDo: Write this method

            // The input should be the indices (in the vocabulary) of the
            // words in the text that is being classified.

            double sum = 0;
            
            for (int i = 0; i < tokenIndexList.Count; i++)
            {
                int tokenIndex = tokenIndexList[i];
                if (tokenIndex != -1)
                {
                    sum += weightList[tokenIndex];
                }
                
            }

            // classify
            if (sum + bias >= 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void optimizePerceptron(PerceptronOptimizer perceptronOptimizer, PerceptronEvaluator perceptronEvaluator, TextClassificationDataSet trainingDataSet, TextClassificationDataSet validationDataSet)
        {
            // use the PerceptronOptimizer to update the weights and bias
            perceptronOptimizer.trainClassifier(this, trainingDataSet);
            numberOfEpochs++;
        }

        public void setBestWeightsAsWeights()
        {
            if (bestWeights != null)
            {
                if(bestWeights == weightList)
                {
                Console.WriteLine("Weights are already the best weights");
                }
                Console.WriteLine("Setting best weights as weights");
                
                // create new list for this
                List<double> newWeights = new List<double>();
                for (int i = 0; i < bestWeights.Count;i++)
                {
                    newWeights.Add(bestWeights[i]);
                }
                
            }
        }

        public void setWeightAsBestWeights()
        {
            
            bestWeights = new List<double>();
            for (int i = 0; i < weightList.Count; i++)
            {
                bestWeights.Add(weightList[i]);
            }
        }

        public double Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        public List<double> WeightList
        {
            get { return weightList; }
            set { weightList = value; }
        }

        public int NumberOfEpochs
        { 
            get { return numberOfEpochs; }
        }

        public double BestTestingAccuracy
        {
            get { return bestTestingAccuracy; }
            set { bestTestingAccuracy = value; }
        }

        public double BestValidationAccuracy
        {
            get { return bestValidationAccuracy; }
            set { bestValidationAccuracy = value; }
        }

        public List<double> getBestWeights()
        {
            return bestWeights;
        }

        // public List<double> BestWeights
        // {
        //     get { return bestWeights; }
        //     set { bestWeights = value; }
        // }

        public int BestEpoch
        {
            get { return bestEpoch; }
            set { bestEpoch = value; }
        }


    }
}
