using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class TokenData
    {
        private int tokenVocabularyIndex;
        public Token Token { get; set; }    
        public int Count { get; set; }  

        public int Class0Count { get; set; }    // Not relevant in Assignment 1.1, but used in Assignment 1.2 and 1.3
        public int Class1Count { get; set; }    // Not relevant in Assignment 1.1, but used in Assignment 1.2 and 1.3

        // I believe this sets the objects Token to token and the Count to 1. See above.
        public TokenData(Token token)
        {
            Token = token;
            Count = 1;
        }

        public void SetTheIndex(int index)
        {
            tokenVocabularyIndex = index;
        }

        public int GetTheIndex() 
        {
            return tokenVocabularyIndex;
        }
    }
}
