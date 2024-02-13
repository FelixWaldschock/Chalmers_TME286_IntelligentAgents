using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.POS.Taggers
{
    public class UnigramTagger : POSTagger
    {

        private string UNKNOWNSPELLING = "UNKNOWNSPELLING";
        private List<TokenData> unigramTaggerWordDataList;

        // give the entire vocabulary (of the training set), the different possible tags to the Tagger

        public UnigramTagger(List<TokenData> UnigramTaggerWordDataList)
        {
            this.unigramTaggerWordDataList = UnigramTaggerWordDataList;
        }

        public override List<string> Tag(Sentence sentence) 
        {
            List<string> tags = new List<string>();
            
            // Create a dictionary for faster lookups
            Dictionary<string, string> unigramTagDict = unigramTaggerWordDataList
                .ToDictionary(x => x.Token.Spelling, x => x.Token.POSTag);

            foreach (TokenData token in sentence.TokenDataList)
            {
                string spelling = token.Token.Spelling;
                string tag;

                // Check if the spelling is in the dictionary
                if (unigramTagDict.TryGetValue(spelling, out tag))
                {
                    tags.Add(tag);
                }
                else
                {
                    tags.Add(UNKNOWNSPELLING);
                }
            }

            return tags;
        }

    }
}