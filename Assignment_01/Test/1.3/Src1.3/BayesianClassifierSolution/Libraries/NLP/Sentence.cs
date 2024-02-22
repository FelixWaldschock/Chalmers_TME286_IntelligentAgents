using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class Sentence
    // changed the class to take Token objects
    {
        private List<Token> tokenList;
        public int ClassLabel;

        public Sentence()
        {
            tokenList = new List<Token>();  
        }

        public List<Token> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }
    }
}
