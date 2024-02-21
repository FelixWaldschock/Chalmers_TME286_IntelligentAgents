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
        private List<int> tokenIndexList = new List<int>();
    
        public List<Token> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public List<int> TokenIndexList
        {
            get { return tokenIndexList; }
            set { tokenIndexList = value; }
        }

        public void indexingTokens(List<string> vocabulary)
        {
            foreach(Token token in tokenList)
            {
                if (vocabulary.Contains(token.Spelling)){
                    tokenIndexList.Add(vocabulary.IndexOf(token.Spelling));
                }
                else
                {
                    tokenIndexList.Add(-1);
                }
            }
        }
    }
}
