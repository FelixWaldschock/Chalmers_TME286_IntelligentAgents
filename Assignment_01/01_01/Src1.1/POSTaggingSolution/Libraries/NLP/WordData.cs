using System;
using System.Collections.Generic;

namespace NLP
{
    public class WordData
    {
        public string Spelling { get; set; }
        public List<string> Tags = new List<string>();
        public int TagCount => Tags.Count;

        public WordData(string spelling, string tag)
        {
            Spelling = spelling;
            Tags.Add(tag);
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }
}
