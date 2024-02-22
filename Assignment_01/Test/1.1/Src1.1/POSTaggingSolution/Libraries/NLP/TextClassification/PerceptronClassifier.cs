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
            // Write this method, setting up the vector of (initially random) weights.
            // Here you can use the Random class, with a suitable (integer) random 
            // number seed.
        }

        public override int Classify(List<Token> tokenList)
        {
            // ToDo: Write this method



            // Remove the line below - needed for compilation, since the method must return an integer.
            // The returned integer should be the class ID (in this case, either 0 or 1).
            return 0; 
 
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
