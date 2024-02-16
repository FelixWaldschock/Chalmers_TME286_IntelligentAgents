// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace NLP
// {
//     public class WordData
//     {
//         public string Spelling { get; set; }
//         public List<string> Tags = new List<string>();
//         public List<int> TagsCounts = new List<int>();
//         public int TagCount => Tags.Count;

//         public WordData(string spelling, string tag)
//         {
//             Spelling = spelling;
//             Tags.Add(tag);
//         }

//         // public void AddTag(string tag)
//         // {
//         //     // check if the tag is already in the list
//         //     if (!Tags.Contains(tag))
//         //     {
//         //         Tags.Add(tag);
//         //         TagsCounts.Add(1);
//         //     }
//         //     // else increment the count of the tag
//         //     else
//         //     {
//         //         int index = Tags.IndexOf(tag);
//         //         TagsCounts[index] += 1;
//         //     }            
//         // }
//         public void AddTag(string tag)
//         {
//             // check if the tag is already in the list
//             int index = Tags.IndexOf(tag);
//             if (index == -1)
//             {
//                 // tag not found, add it to both Tags and TagsCounts
//                 Tags.Add(tag);
//                 TagsCounts.Add(1);
//             }
//             else
//             {
//                 // tag found, increment the count of the tag
//                 TagsCounts[index] += 1;
//             }
//         }


//         public string GetMostFrequentTag()
//         {
//             if (TagsCounts.Count > 0)
//             {
//                 // get index of max value in the TagsCounts
//                 int maxIndex = TagsCounts.IndexOf(TagsCounts.Max());
//                 return Tags[maxIndex];
//             }
//             else
//             {
//                 // Handle the case when TagsCounts is empty (no tags added)
//                 return "No tags added";
//             }
//         }

//     }
// }

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
