using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronClassifier: TextClassifier
    {
        private double bias;
        private List<double> weightList = new List<double>();

        public override void Initialize(int numberOfFeatures)
        {
            // Write this method, setting up the vector of (initially random) weights.
            // Here you can use the Random class, with a suitable (integer) random 
            // number seed.
            
            int seed = 10; // You can use any integer value here

            
            Random random = new Random(seed);
            for (int i = 0; i < numberOfFeatures; i++)
            {
                double weight = random.NextDouble();
                weightList.Add(weight);
            }
            
            
            
        }

        public override int Classify(List<int> tokenIndexList)
        {
            //int lastIndex = weightList.Count - 1;
            //tokenIndexList.Add(lastIndex); // Added Bias to each sentence
            double sum = 0;
            foreach (int index in tokenIndexList)
            {
                if (index != -1)
                {  
                    sum += weightList[index]; 
                }
                    
            }

            // Step function
            int output = 0;
            if (sum > 0)
            {
                output = 1;
            }
            else
            {
                output = 0;
            }

            return output; 
 
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
    }
}
