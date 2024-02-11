using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTaggingApplication
{
    internal class POSTagConverter
    {
        // 2D list that contains the Brown Corpus and the Universal tag
        List<List<string>> BrownAndUniList = null;

        // Initialize POS tag counters
        public Dictionary<string, int> posTagCounters = new Dictionary<string, int>
        {
            {".", 0},
            {"ADJ", 0},
            {"ADP", 0},
            {"ADV", 0},
            {"CONJ", 0},
            {"DET", 0},
            {"NOUN", 0},
            {"NUM", 0},
            {"PRON", 0},
            {"PRT", 0},
            {"VERB", 0},
            {"X", 0},
            {"BLANKS", 0}
        };

    // Constructor
    public POSTagConverter(List<List<string>> BrownAndUniList)
    {
        this.BrownAndUniList = BrownAndUniList;
    }

    public void showConverter()
    {
        // show the BrownAndUniList
        for (int i = 0; i < BrownAndUniList.Count; i++)
        {
            Console.WriteLine(BrownAndUniList[i][0] + " " + BrownAndUniList[i][1]);
        }
    }


    public void updatePOSCounters(string UniversalTag)
    {
        // update the POS tag counters
        posTagCounters[UniversalTag] += 1;
    }


    public string getUniversalTag(string BrownTag)
    {
        // in the BrownAndUniList, search in the first column for the BrownTag and get the corresponding Universal tag
        string UniversalTag = null;

        // search in the first column for the BrownTag
        for (int i = 0; i < BrownAndUniList.Count; i++)
        {
            if (BrownAndUniList[i][0] == BrownTag)
            {
                // get the corresponding Universal tag
                UniversalTag = BrownAndUniList[i][1];
            }
        }



        if (UniversalTag == null)
        {
            // throw an exception 
            throw new Exception("The Brown tag " + BrownTag + " does not exist in the Brown Corpus");
        }

        return UniversalTag;

    }

}
}
