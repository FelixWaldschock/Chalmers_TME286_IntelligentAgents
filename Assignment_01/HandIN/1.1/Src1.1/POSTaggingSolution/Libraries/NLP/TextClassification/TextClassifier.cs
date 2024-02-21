using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public abstract class TextClassifier
    {
        public abstract void Initialize(int numberOfFeatures);
        public abstract int Classify(List<Token> tokenList);
    }
}
