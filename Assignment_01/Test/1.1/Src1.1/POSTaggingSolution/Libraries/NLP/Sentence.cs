using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class Sentence
    {
        private List<TokenData> tokenDataList;

        public Sentence()
        {
            tokenDataList = new List<TokenData>();  
        }

        public List<TokenData> TokenDataList
        {
            get { return tokenDataList; }
            set { tokenDataList = value; }
        }
    }
}
