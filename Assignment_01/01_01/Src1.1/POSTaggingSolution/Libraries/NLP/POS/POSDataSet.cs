using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.POS
{
    public class POSDataSet
    {
        private List<Sentence> sentenceList;

        public POSDataSet()
        {
            sentenceList = new List<Sentence>();
        }

        public List<Sentence> SentenceList
        {
            get { return sentenceList; }
            set { sentenceList = value; }
        }

        public static (POSDataSet, POSDataSet) Split(POSDataSet completeDataSet, double splitFraction){
            POSDataSet trainingDataSet = new POSDataSet();
            POSDataSet testDataSet = new POSDataSet();

            // Shuffle the completeDataSet
            Random random = new Random();
            completeDataSet.SentenceList = completeDataSet.SentenceList.OrderBy(x => random.Next()).ToList();

            // Split the completeDataSet into training and test data
            int splitIndex = (int)(completeDataSet.SentenceList.Count * splitFraction);

            trainingDataSet.SentenceList = completeDataSet.SentenceList.GetRange(0, splitIndex);
            testDataSet.SentenceList = completeDataSet.SentenceList.GetRange(splitIndex, completeDataSet.SentenceList.Count - splitIndex);


            return (trainingDataSet, testDataSet);
        }

    }
}
