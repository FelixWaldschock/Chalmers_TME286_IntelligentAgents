using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLP.NGrams;

namespace NLP.Autocompletion
{
    public class Autocompletion
    {
        List<string> EMPTYLISTRESPONSE = new List<string>() { "" };
        Dictionary<string, NGram> sourceDataForSuggestion;
        Dictionary<string, NGram> backupDataForSuggestion;


        public Autocompletion(Dictionary<string, NGram> sourceDataForSuggestion, Dictionary<string, NGram> backupDataForSuggestion)
        {
            this.sourceDataForSuggestion = new Dictionary<string, NGram>();
            foreach (KeyValuePair<string, NGram> entry in sourceDataForSuggestion)
            {
                this.sourceDataForSuggestion.Add(entry.Key, entry.Value);
            }
            this.backupDataForSuggestion = new Dictionary<string, NGram>();
            foreach (KeyValuePair<string, NGram> entry in backupDataForSuggestion)
            {
                this.backupDataForSuggestion.Add(entry.Key, entry.Value);
            }

        }

        public List<(string, string)> GetSuggestions(List<string> prefix)
        {
            Dictionary<(string, string), double> suggestions = new Dictionary<(string, string), double>();

            string prefixString;
            string currentWord;
            if (prefix.Count < 2)
            {
                if (prefix.Count == 0)
                {
                    return new List<(string, string)>() { ("", "") };
                }
                prefixString = ". " + prefix[0];
                currentWord = prefix[0];
            }
            else
            {
                // join the prefix list into a string with a space between each word
                prefixString = string.Join(" ", prefix);
                currentWord = prefix[prefix.Count - 1];
            }

            //Console.WriteLine(prefixString);

            foreach (KeyValuePair<string, NGram> entry in sourceDataForSuggestion)
            {
                if (entry.Key.StartsWith(prefixString))
                {
                    //Console.WriteLine(entry.Value.Identifier);
                    string suggestedWord = entry.Value.TokenList[entry.Value.TokenList.Count - 1];
                    string remainder = entry.Value.TokenList[entry.Value.TokenList.Count - 2];
                    // cope of the currentWord of the remainder
                    int lenghtOfCurrentWord = currentWord.Length;
                    int lengthOfRemainder = remainder.Length;
                    //Console.WriteLine(remainder);
                    if (lengthOfRemainder >= lenghtOfCurrentWord)
                    {
                        // cut off the first n characters of the remainder
                        int lengthOfSubstring = lengthOfRemainder - lenghtOfCurrentWord;
                        int stopIndex = lengthOfRemainder;
                        remainder = remainder.Substring(lenghtOfCurrentWord, lengthOfSubstring);
                    }
                    else
                    {
                        remainder = "";
                    }


                    //Console.WriteLine(remainder);

                    // check that item is not already in the suggestions
                    if (!suggestions.ContainsKey((suggestedWord, remainder)))
                    {
                        suggestions.Add((suggestedWord, remainder), entry.Value.FrequencyPerMillionInstances);
                    }
                }
            }

            // if no suggestions were found try the backup data (Bigrams)
            if (suggestions.Count == 0)
            {
                foreach (KeyValuePair<string, NGram> entry in backupDataForSuggestion)
                {
                    if (entry.Key.StartsWith(prefixString))
                    {
                        string suggestedWord = entry.Value.TokenList[entry.Value.TokenList.Count - 1];
                        string remainder = "";
                        if (suggestedWord.StartsWith(prefixString))
                        {
                            remainder = suggestedWord.Substring(prefixString.Length);
                        }

                        // check that item is not already in the suggestions
                        if (!suggestions.ContainsKey((suggestedWord, remainder)))
                        {
                            suggestions.Add((suggestedWord, remainder), entry.Value.FrequencyPerMillionInstances);
                        }
                    }
                }
            }

            // sort the suggestions by frequency
            suggestions = suggestions.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // create a list of tuples with the suggested word and the remainder
            List<(string, string)> result = new List<(string, string)>();

            // return the top 5 suggestions
            foreach (KeyValuePair<(string, string), double> entry in suggestions)
            {
                result.Add(entry.Key);
            }

            // return the top 5 suggestions
            return result;
        }
    }



    /*
    public List<string> GetSuggestions(List<string> prefix)
        {
            Dictionary<string, double> suggestions = new Dictionary<string, double>();

            string prefixString;
            if (prefix.Count < 2){
                if (prefix.Count == 0)
                {
                    return EMPTYLISTRESPONSE;
                }
                prefixString = ". " + prefix[0];
            }
            else
            {
                // join the prefix list into a string with a space between each word
                prefixString = string.Join(" ", prefix);
            }

            foreach (KeyValuePair<string, NGram> entry in sourceDataForSuggestion)
            {
                if (entry.Key.StartsWith(prefixString))
                {
                    // get last element of the TokenList
                    //suggestions.Add(entry.Value.TokenList[entry.Value.TokenList.Count - 1]);

                    // get last element of the TokenList and add it to the suggestions, also add the FrequencyPerMillionInstances of the NGram
                    // check that item is not already in the suggestions
                    if (!suggestions.ContainsKey(entry.Value.TokenList[entry.Value.TokenList.Count - 1]))
                    {
                        suggestions.Add(entry.Value.TokenList[entry.Value.TokenList.Count - 1], entry.Value.FrequencyPerMillionInstances);
                    }
                
                }
            }

            // if no suggestions were found try the backup data (Bigrams)
            if (suggestions.Count == 0)
            {
                foreach (KeyValuePair<string, NGram> entry in backupDataForSuggestion)
                {
                    if (entry.Key.StartsWith(prefixString))
                    {
                        // get last element of the TokenList and add it to the suggestions, also add the FrequencyPerMillionInstances of the NGram
                        // check that item is not already in the suggestions
                        if (!suggestions.ContainsKey(entry.Value.TokenList[entry.Value.TokenList.Count - 1]))
                        {
                            suggestions.Add(entry.Value.TokenList[entry.Value.TokenList.Count - 1], entry.Value.FrequencyPerMillionInstances);
                        }
                    }
                }
            }
         
            // sort the suggestions by frequency
            suggestions = suggestions.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // return the top 5 suggestions
            return suggestions.Keys.Take(5).ToList();
            
        }
        */
}
