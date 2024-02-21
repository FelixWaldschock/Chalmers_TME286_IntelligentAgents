using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NLP.Tokenization
{
    public class Tokenizer
    {
        private List<string> abbreviations;

        public Tokenizer(List<string> abbreviations){
            this.abbreviations = abbreviations;
        }

        public List<Token> Tokenize(string sentence)
        {
            List<string> tempTokens = new List<string>();
            List<string> finalTokens = new List<string>();

            // init regex pattern to split by ',', ' ', '!','?'
            
            string regexPattern = @"(?<=[,!?%\p{Sc} ])|(?=[,!?%\p{Sc} ])";


            // Split the sentence by the specified characters
            string[] tokens = Regex.Split(sentence, regexPattern);


            foreach (string token in tokens)
            {
                if (!string.IsNullOrWhiteSpace(token)) // This check removes empty entries from the result
                {
                    // make all lowercase
                    tempTokens.Add(token.ToLower());
                }
            }

            // now handle the '.' character
            foreach (string token in tempTokens) // Use ToList() to avoid modifying the collection during iteration
            {
                
                // check if token contains a '.' character
                if (token.Contains("."))
                {
                    // check if token is an abbreviation
                    if (this.abbreviations.Contains(token))
                    {
                        finalTokens.Add(token);
                    }
                    // check if token is a decimal number
                    else if (Regex.IsMatch(token, @"^\d+\.\d+$"))
                    {
                        finalTokens.Add(token);
                    }
                    else
                    {
                        // split the token by the '.' character and add all the resulting tokens to the finalTokens list
                        string[] subTokens = Regex.Split(token, @"(?=\.)");

                        finalTokens.Remove(token);
                        finalTokens.AddRange(subTokens);
                    }
                }
                else
                {
                    // Convert currency signs to their spellings
                    string tokenWithSpellings = token
                        .Replace("€", "euro")
                        .Replace("$", "dollar")
                        .Replace("£", "pound");

                    finalTokens.Add(tokenWithSpellings);
                }
            }

            // create list of tokens
            List<Token> tokensList = new List<Token>();
            foreach (string token in finalTokens)
            {
                Token newToken = new Token();
                newToken.Spelling = token;
                tokensList.Add(newToken);
            }

        return tokensList;
    
        }
    }
}
