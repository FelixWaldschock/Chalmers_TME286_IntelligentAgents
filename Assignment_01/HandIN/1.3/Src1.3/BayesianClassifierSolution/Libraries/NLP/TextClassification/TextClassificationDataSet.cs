using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.TextClassification
{
    public class TextClassificationDataSet
    {
        public int numberOfDocumentsOfClass0;
        public int numberOfDocumentsOfClass1;

        private List<TextClassificationDataItem> itemList;
        
        public TextClassificationDataSet()
        {
            itemList = new List<TextClassificationDataItem>();
        }

        public List<TextClassificationDataItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
