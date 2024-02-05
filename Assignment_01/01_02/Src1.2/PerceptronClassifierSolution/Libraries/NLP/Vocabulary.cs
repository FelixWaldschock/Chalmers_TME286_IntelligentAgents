using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class Vocabulary
    {
        private List<string> vocabulary;

        public Vocabulary(List<Token> tokensList)
        {
            // sort the tokensList into alphabetical order
            tokensList = tokensList.OrderBy(t => t.Spelling).ThenBy(t => t.POSTag).ToList();

            // remove duplicates
            tokensList = tokensList.Distinct().ToList();

            // set the vocabulary to the tokensList spelling
            vocabulary = tokensList.Select(t => t.Spelling).ToList();            
        }

        public List<string> GetVocabulary()
        {
            return vocabulary;
        }

        public int GetIndex(string word)
        {
            return vocabulary.IndexOf(word);
        }

        public string GetWord(int index)
        {
            return vocabulary[index];
        }
    }
}
