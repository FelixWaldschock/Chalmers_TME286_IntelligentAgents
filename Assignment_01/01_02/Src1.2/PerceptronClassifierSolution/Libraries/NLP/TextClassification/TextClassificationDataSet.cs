using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class TextClassificationDataSet
    {
        private List<TextClassificationDataItem> itemList;
        private List<Token> completeSetTokenList;
        //private List<int> indexedTokenList = new List<int>();
        
        public TextClassificationDataSet()
        {
            itemList = new List<TextClassificationDataItem>();
        }

        public List<TextClassificationDataItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public List<Token> getTokenList()
        {
            completeSetTokenList = new List<Token>();
            foreach(TextClassificationDataItem item in itemList)
            {
                completeSetTokenList.AddRange(item.TokenList);
            }
            return completeSetTokenList;
        }

        public List<Token> CompleteSetTokenList
        {
            get { return getTokenList();}
            set { completeSetTokenList = value; }
        }


        // public List<Token> TokenList
        // {
        //     get { return tokenList; }
        //     set { tokenList = value; }
        // }

        // public void indexingTokens(List<string> vocabulary)
        // {
        //     foreach(Token token in tokenList)
        //     {
        //         if (vocabulary.Contains(token.Spelling)){
        //             indexedTokenList.Add(vocabulary.IndexOf(token.Spelling));
        //         }
        //         else
        //         {
        //             indexedTokenList.Add(-1);
        //         }
        //     }
        // }
    }
}
