using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class TextClassificationDataItem
    {
        private List<int> reviewAsVocabularyIndexes = new List<int>();
        public string Text { get; set; }    
        public int ClassLabel { get; set; }

        public List<int> ReviewAsVocabularyIndexes
        {  
            get { return reviewAsVocabularyIndexes;} 
            set { reviewAsVocabularyIndexes = value;}
        }
        
    }
}
