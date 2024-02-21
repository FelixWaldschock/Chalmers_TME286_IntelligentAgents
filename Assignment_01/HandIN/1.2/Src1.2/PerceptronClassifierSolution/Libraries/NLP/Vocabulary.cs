using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class Vocabulary
    {
        public List<string> vocabulary;

        public Vocabulary(List<Token> tokensList)
        {
            // sort the tokensList into alphabetical order
            tokensList = tokensList.OrderBy(t => t.Spelling).ThenBy(t => t.POSTag).ToList();

            // remove duplicates
            vocabulary = tokensList.Select(t => t.Spelling).Distinct().ToList();

            // set the vocabulary to the tokensList spelling
            //vocabulary = tokensList.Select(t => t.Spelling).ToList();            
        }

        public List<string> GetVocabulary()
        {
            return vocabulary;
        }

        public int GetIndex(string word)
        {
            return vocabulary.IndexOf(word);
        }

        public string GetSpelling(int index)
        {
            if (index == -1)
            {
                return "UNKNOWNTOKEN";
            }
            return vocabulary[index];
        }

        public int SizeOfVocabulary 
        {
            get { return vocabulary.Count; }
            
        }

        public string GetReviewFromTokenIndexList(List<int> TokenIndexList)
        {
            string review = "";
            for (int i = 0; i < TokenIndexList.Count; i++)
            {
                review = review + " " + GetSpelling(TokenIndexList[i]);
            }
            return review;
        }

    }
}
