using System;
using System.Collections.Generic;
using System.Linq;

namespace POSTaggingApplication
{
    internal class POSTagConverter
    {
        // Dictionary to store the mapping between Brown and Universal tags
        private Dictionary<string, string> brownToUniversalMap;

        // Initialize POS tag counters
        public Dictionary<string, int> posTagCounters = new Dictionary<string, int>
        {
            { ".", 0 },
            { "ADJ", 0 },
            { "ADP", 0 },
            { "ADV", 0 },
            { "CONJ", 0 },
            { "DET", 0 },
            { "NOUN", 0 },
            { "NUM", 0 },
            { "PRON", 0 },
            { "PRT", 0 },
            { "VERB", 0 },
            { "X", 0 },
            { "BLANKS", 0 }
        };

        // Constructor
        public POSTagConverter(List<List<string>> brownAndUniList)
        {
            // Populate the brownToUniversalMap dictionary for faster lookups
            brownToUniversalMap = brownAndUniList.ToDictionary(pair => pair[0], pair => pair[1]);
        }

        public void showConverter()
        {
            // show the BrownAndUniList
            foreach (var pair in brownToUniversalMap)
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }
        }

        public int getConverterSize()
        {
            return brownToUniversalMap.Count();
        }

        public void updatePOSCounters(string universalTag)
        {
            // update the POS tag counters
            posTagCounters[universalTag] += 1;
        }

        public string getUniversalTag(string brownTag)
        {
            // Use the dictionary for faster lookups
            if (brownToUniversalMap.TryGetValue(brownTag, out var universalTag))
            {
                return universalTag;
            }

            // throw an exception if the Brown tag does not exist in the Brown Corpus
            throw new Exception("The Brown tag " + brownTag + " does not exist in the Brown Corpus");
        }
    }
}
