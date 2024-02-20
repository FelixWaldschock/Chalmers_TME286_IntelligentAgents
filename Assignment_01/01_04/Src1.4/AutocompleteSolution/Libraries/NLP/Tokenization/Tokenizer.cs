using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace NLP.Tokenization
{
    public class Tokenizer
    {

        private List<string> abbreviations = new List<string>
        {
            //"mr.", "mrs.", "dr.", "ms.", "prof.", "sr.", "jr.", "st.", "ave.", "blvd.", "rd.", "sr.", "sra.", "srta."
            "mr."
        };


        public List<string> Tokenize(string dataSet)
        {
            List<string> tempTokens = new List<string>();
            List<string> finalTokens = new List<string>();

            // init a regex pattern, to split the strings
            string regexPattern = @"(?<=[,!?;:%\p{Sc} ])|(?=[,!?;:%\p{Sc} ])";


            // Split the dataSet by the specified characters
            string[] tokens = Regex.Split(dataSet, regexPattern);


            foreach (string token in tokens)
            {
                if (!string.IsNullOrWhiteSpace(token) & token != "," & token != ":" & token != ";" & token != "\"") // This check removes empty entries from the result
                {
                    // make all lowercase
                    tempTokens.Add(token.ToLower());
                }
            }

            return tempTokens;

            int currentTokenIndex = 0;
            int totalNumberTokens = tempTokens.Count();

            // now handle the '.' character
            foreach (string token in tempTokens) // Use ToList() to avoid modifying the collection during iteration
            {
                // Handle the case when the token contains "..."
                if (token.Contains("..."))
                {
                    string[] subTokens = Regex.Split(token, @"(?=...)");
                    finalTokens.Remove(token);
                    finalTokens.AddRange(subTokens);
                }

                // handle '.' characters
                else if (token.Contains("."))
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
                    finalTokens.Add(token);
                }
                currentTokenIndex++;

                if (currentTokenIndex % 100000 == 0)

                {
                    Console.WriteLine(currentTokenIndex.ToString() + "/" + totalNumberTokens);
                }                
            }
            return finalTokens;
        }

    }
}
