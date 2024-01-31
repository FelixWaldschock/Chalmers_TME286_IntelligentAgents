using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTaggingApplication
{
    internal class WordData
    {
        public string Spelling { get; set; }
        public List<string> Tags = new List<string>();
        public int TagCount;

        public WordData(string spelling, string tag)
        {
            Spelling = spelling;
            Tags.Add(tag);
        }

        public int computeTagCount(){
            return Tags.Count;
        }

    }
}
