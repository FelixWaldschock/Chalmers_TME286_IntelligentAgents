using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class TextClassificationDataItem
    { 
        public string Text { get; set; }    
        public int ClassLabel { get; set; }
    }
}
