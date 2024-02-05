using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class HelloWorld
{
    static void Main()
    {
        List<string> tempTokens = new List<string>();
        List<string> finalTokens = new List<string>();

        // Example sentence
        string sentence = "Hello, Mr. Smith! For example, e.g., it's 2023. I have 2.453 apples, i.e., a lot.";

        // Define a pattern to split by commas, periods, and spaces, treating each as a separate token
        // Including commas, periods, and spaces in the character class and using lookahead and lookbehind to keep them in the result
        string regexPattern = @"(?<=[,!? ])|(?=[,!? ])|\s+";

        // Split the sentence by the specified characters
        string[] tokens = Regex.Split(sentence, regexPattern);

        foreach (string token in tokens)
        {
            if (!string.IsNullOrWhiteSpace(token)) // This check removes empty entries from the result
            {
                //Console.WriteLine(token);
                // make all lowercase
                tempTokens.Add(token.ToLower());
            }
        }

        // now handle the '.' character

        List<string> abbreviations = new List<string> { "mr.", "mrs.", "e.g.", "i.e." };

        foreach (string token in tempTokens) // Use ToList() to avoid modifying the collection during iteration
        {
            // check if token is an abbreviation
            if (abbreviations.Contains(token))
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
                foreach (string subToken in subTokens)
                {
                    //Console.WriteLine(subToken); // Print each subToken
                }
                finalTokens.Remove(token);
                finalTokens.AddRange(subTokens);
            }
        }

        // // print the final tokens
        foreach (string token in finalTokens)
        {
            Console.WriteLine(token);
        }
    }
}
