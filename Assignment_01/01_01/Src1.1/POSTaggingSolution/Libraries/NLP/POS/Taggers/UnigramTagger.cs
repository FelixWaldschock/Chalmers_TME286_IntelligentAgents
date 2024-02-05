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
            // for each token in the sentence, find the spelling in UnigramTaggerWordDataList and return the tag

            List<string> tags = new List<string>();

            foreach(TokenData token in sentence.TokenDataList)
            {
                string spelling = token.Token.Spelling;
                string tag;
                
                // try to find the spelling in the unigramTaggerWordDataList
                if(unigramTaggerWordDataList.Find(x => x.Token.Spelling == spelling) == null)
                {
                    tag = UNKNOWNSPELLING;
                }
                else
                {
                    tag = unigramTaggerWordDataList.Find(x => x.Token.Spelling == spelling).Token.POSTag;
                }
                
            
                tags.Add(tag);
            }
            return tags;
        }
    }
}