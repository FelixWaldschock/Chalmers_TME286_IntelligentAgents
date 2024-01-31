using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.POS.Taggers
{
    public class UnigramTagger : POSTagger
    {
        public List<string> tokensList = new List<string>();
        public List<string> tagsList = new List<string>();

        public UnigramTagger(List<string> tokens, List<string> tags)
        {
            tokensList = tokens;
            tagsList = tags;
        }


        public override List<string> Tag(Token token)
        {
            // find the token in the tokensList
            // return the tag at the same index in the tagsList
            // if the token is not found, return null
            int index = tokensList.IndexOf(token.Spelling);
            if (index == -1)
            {
                return null;
            }
            else
            {
                return tagsList[index];
            }


        }
    }
}