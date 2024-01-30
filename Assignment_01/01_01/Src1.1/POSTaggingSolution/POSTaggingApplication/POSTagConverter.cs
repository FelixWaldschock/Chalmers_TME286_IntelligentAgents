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

    // Constructor
    public POSTagConverter(List<List<string>> BrownAndUniList)
    {
        this.BrownAndUniList = BrownAndUniList;
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
