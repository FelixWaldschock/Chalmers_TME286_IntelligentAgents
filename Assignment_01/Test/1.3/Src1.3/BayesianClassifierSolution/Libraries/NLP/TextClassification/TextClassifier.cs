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
        public abstract void Initialize(TextClassificationDataSet trainingDataSet, List<TokenData> BagOfWords);
        public abstract int Classify(List<Token> ListOfTokens);
        public abstract int Classify(List<int> tokenIndexList);

        // NOTE: For the Bayesian text classifier (Assignment 1.3), you may wish to use
        // a different input than just a token index list.
        // You are allowed to add another Classify() method, taking
        // different input, e.g., Classify(Document document), where
        // you'll need to define the Document class (or some other
        // class, if you prefer.
        //
        // Note that it is possible to have several (overloaded) methods
        // with the same name, as long as they take different input.
        //
        // However, please include both an abstract method (as above)
        // here, and then override that method in your own Bayesian
        // text classifier class (called, e.g., NaiveBayesianClassifier)
    }
}
