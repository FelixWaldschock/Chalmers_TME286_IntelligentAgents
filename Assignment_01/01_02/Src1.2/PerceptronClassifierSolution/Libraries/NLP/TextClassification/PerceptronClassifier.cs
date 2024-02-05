using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class PerceptronClassifier: TextClassifier
    {
        private double bias;
        private List<double> weightList = null;

        public override void Initialize(int numberOfFeatures)
        {
            Random random = new Random(0);
            weightList = new List<double>();
            for (int i = 0; i < numberOfFeatures; i++)
            {
                weightList.Add(random.NextDouble());
            }

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
                sum += weightList[i] * tokenIndexList[i];
            }

            // classify
            if (sum + bias > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }


            // Remove the line below - needed for compilation, since the method must return an integer.
            // The returned integer should be the class ID (in this case, either 0 or 1).

 
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
