using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class TextClassificationDataItem
    { 
        public string Text { get; set; }    
        public int ClassLabel { get; set; }
        private List<Token> tokenList;
        private List<int> indexedTokenList = new List<int>();
    
        public List<Token> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public void indexingTokens(List<string> vocabulary)
        {
            foreach(Token token in tokenList)
            {
                if (vocabulary.Contains(token.Spelling)){
                    indexedTokenList.Add(vocabulary.IndexOf(token.Spelling));
                }
                else
                {
                    indexedTokenList.Add(-1);
                }
            }
        }
    }
}
