using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTaggingApplication
{
    internal class Statistics
    {
      
        public string Name { get; set; }
        public int Count { get; set; }
        public double Fraction { get; set; }

        public Statistics(string name, int count, double fraction)
        {
            this.Name = name;
            this.Count = count;
            this.Fraction = fraction;
        }
    }

}

