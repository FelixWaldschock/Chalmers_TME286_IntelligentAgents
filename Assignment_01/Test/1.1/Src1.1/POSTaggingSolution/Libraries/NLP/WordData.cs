using System;
using System.Collections.Generic;
using System.Linq;

namespace NLP
{
    public class WordData
    {
        public string Spelling { get; set; }
        public Dictionary<string, int> TagsCount { get; } = new Dictionary<string, int>();

        public WordData(string spelling, string tag)
        {
            Spelling = spelling;
            AddTag(tag);
        }

        public void AddTag(string tag)
        {
            // Check if the tag is already in the dictionary
            if (!TagsCount.ContainsKey(tag))
            {
                // Tag not found, add it to the dictionary with count 1
                TagsCount[tag] = 1;
            }
            else
            {
                // Tag found, increment the count of the tag
                TagsCount[tag]++;
            }

            // if the word has more than 5 tags linked print the word and its tags

            // if (TagsCount.Count >= 5)
            // {
            //     Console.WriteLine("Word: " + Spelling + " has + " + TagsCount.Count + " tags linked to it");
            //     foreach (var pair in TagsCount)
            //     {
            //         Console.WriteLine(pair.Key + " " + pair.Value);
            //     }

            // }

        }

        public string GetMostFrequentTag()
        {
            if (TagsCount.Count > 0)
            {
                // Get the tag with the maximum count
                var mostFrequentTag = TagsCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                return mostFrequentTag;
            }
            else
            {
                // Handle the case when TagsCount is empty (no tags added)
                return "No tags added";
            }
        }
    }
}
