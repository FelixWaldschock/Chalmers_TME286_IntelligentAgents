using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class TokenData
    {
        public Token Token { get; set; }    
        public int Count { get; set; }  
        public int Class0Count { get; set; }    // Not relevant in Assignment 1.1, but used in Assignment 1.2 and 1.3
        public int Class1Count { get; set; }    // Not relevant in Assignment 1.1, but used in Assignment 1.2 and 1.3

        public TokenData(Token token)
        {
            Token = token;
            Count = 1;
        }
    }
}
