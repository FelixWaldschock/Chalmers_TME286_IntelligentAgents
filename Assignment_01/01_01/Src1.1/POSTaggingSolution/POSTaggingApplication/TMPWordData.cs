// using System;
// using System.Collections.Generic;
// using System.Linq;
// using  System.Text;
// using System.Threading.Tasks;
// using NLP;
// using NLP.POS;
// using NLP.POS.Taggers;

// namespace POSTaggingApplication
// {
//     internal class WordData
//     {
//         public string Spelling { get; set; }
//         public List<string> TagsList;
//         public List<int> TagCounterList;

//         public WordData(string Spelling, string Tag)
//         {
//             this.Spelling = Spelling;
//             this.TagsList = new List<string>();

//         }

//         public WordData(string spelling, List<string> POSTags)
//         {
//             Spelling = spelling;
//             TagsList = POSTags;

//             // Initialize the TagCounter with 0 for each tag and the length of the Tags list
//             TagCounterList = new List<int>(TagsList.Count);
//             for (int i = 0; i < TagsList.Count; i++)
//             {
//                 TagCounterList.Add(0);
//             }
//         }

//         public void updateTagCounter(string tag)
//         {
//             // get the index where tag == TagsList[i]
//             int index = TagsList.IndexOf(tag);
//             TagCounterList[index]++;
//         }


//         public string getMostLikelyTag()
//         {
//             // get the index of the maximum value in the TagCounterList
//             int index = TagCounterList.IndexOf(TagCounterList.Max());
//             return TagsList[index];
//         }

//     }
// }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
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