using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.Tokenization
{
    public class Tokenizer
    {
        public List<Token> Tokenize(string text)
        {
            // Implement your tokenizer here (to handle abbreviations, numbers, special characters, and so on). 
            // You may wish to add more methods to keep the code well-structured
            List<Token> tokens = new List<Token>();

            // split the text into tokens, as delimiter use (' ', '.', ',' ,'!', '?',)

            // first delimiter is space
            string[] words = text.Split(' ');

            // second delimiter is comma
            List<string> wordsWithComma = new List<string>();




            // Remove the line below - needed only for the compiler.
            return null;
        }

        private bool IsAbbreviation(string word)
        {
            // Implement your abbreviation detection here
            return false;
        }
    }
}
