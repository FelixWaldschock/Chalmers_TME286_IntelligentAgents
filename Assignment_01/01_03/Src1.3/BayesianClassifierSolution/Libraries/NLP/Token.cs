using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    public class Token
    {
        public string Spelling { get; set; }

        public int ClassLabel { get; set; } // Changed from POSTag to ClassLabel for this exercise
    }
}
