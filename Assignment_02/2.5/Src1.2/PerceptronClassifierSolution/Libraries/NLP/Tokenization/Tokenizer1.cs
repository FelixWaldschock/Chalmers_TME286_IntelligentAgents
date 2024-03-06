using NLP.TextClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NLP.Tokenization
{
    public class Tokenizer1
    {
        private void TokenizeAndUpdateTokens(string word, List<Token> tokens)
        {
            // Tokenize the word recursively for the cases where checks at the end and beginning are made.
            List<Token> wordTokens = Tokenize(word);
            tokens.AddRange(wordTokens);
        }

        public List<Token> Tokenize(string text)
        {
            char[] delimiters = new char[] { ' ' };
            List<string> words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList(); // But befor this is done the text must be checked for abbreviations or numbers. 
            List<string> abbreviations = new List<string>{"i.e.", "e.g.", "etc.", "et al.", "vs.", "a.m.", "p.m.", "e.g.", "i.a.", "cf.", "etc.", "esp.", "i.e.", "e.g.", "ex.", "no.", "op.", "cit.", "vol.", "v.", "p.", "pp.", "dr.", "mrs.", "mr.", "ph.d.", "st.", "ave.", "rd.", "blvd.", "apt.", "fig.", "al.", "b.c.", "a.d.", "p.o.", "p.s.", "inc.", "ltd.", "co.", "est.", "jan.", "feb.", "mar.", "apr.", "jun.", "jul.", "mon.", "tue.", "wed.", "thu.", "fri.", "sat.", "sun.", "aug.", "sep.", "oct.", "nov.", "dec."
            };
            List<Token> tokens = new List<Token>();

            foreach (string word in words)
            {
                // Checks for abbreviations
                if (abbreviations.Contains(word.ToLower()))
                {
                    Token tokenAbbreviation = new Token();
                    tokenAbbreviation.Spelling = word;
                    tokens.Add(tokenAbbreviation);
                }
                // Checks for punctuations at the end of the word
                else if (word.EndsWith(".") || word.EndsWith(",") || word.EndsWith("?") || word.EndsWith("!") || word.EndsWith(")") || word.EndsWith("\"") || word.EndsWith("$") || word.EndsWith("£") || word.EndsWith("€"))
                {
                    string wordOnly = word.Substring(0, word.Length - 1);
                    TokenizeAndUpdateTokens(wordOnly, tokens);

                    string punctuationOnly = word.Substring(word.Length - 1);

                    Token tokenPunctuationOnly = new Token();
                    tokenPunctuationOnly.Spelling = punctuationOnly;
                    tokens.Add(tokenPunctuationOnly);
                }
                // Checks for punctuations at the beginning of the word
                else if (word.StartsWith("(") || word.StartsWith("\"") || word.StartsWith("$") || word.StartsWith("£") || word.StartsWith("€"))
                {
                    string punctuationOnly = word.Substring(0, 1);

                    Token tokenPunctuationOnly = new Token();
                    tokenPunctuationOnly.Spelling = punctuationOnly;
                    tokens.Add(tokenPunctuationOnly);

                    string wordOnly = word.Substring(1, word.Length - 1);
                    TokenizeAndUpdateTokens(wordOnly, tokens);

                }
                // Checks for decimal numbers
                else if (Regex.IsMatch(word, @"\d+\.\d+"))
                {
                    Token tokenDecimal = new Token();
                    tokenDecimal.Spelling = word;
                    tokens.Add(tokenDecimal);
                }
                // Creates a token for a regular word
                else
                {
                    Token token = new Token();
                    token.Spelling = word;
                    tokens.Add(token);
                }

            }

            return tokens;
            // Implement your tokenizer here (to handle abbreviations, numbers, special characters, and so on). 
            // You may wish to add more methods to keep the code well-structured

            // What I want to do: Split the text into individual words.BUT there are abbreviations such as e.g., i.e., mr., mrs., etc
            // which should not be split. Also numbers e.g. 3.14 should not be split and be defined as one token.

            // While tokenizing there should also be generated a vocabulary, in alphabetical order. Created with a list or Dictionary.

            // Remove the line below - needed only for the compiler.
            // return null;

        }


    }
}
