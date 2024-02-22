using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.POS.Taggers
{
    public abstract class POSTagger
    {
        public abstract List<string> Tag(Sentence sentence);
    }
}
