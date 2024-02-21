using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTaggingApplication
{
    internal class POSTagData
    {
        public string name;
        public int count;
        public double fraction;

        public POSTagData(string name, int count)
        {
            this.name = name;
            this.count = count;
        }

        public void computeFraction(int totalNumberOfTokens){
            this.fraction = ((double)count / totalNumberOfTokens);

        }
    }
}
