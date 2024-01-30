using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLP;

namespace NLP.NGrams
{
    public class NGram
    {
        public string Identifier { get; set; } // e.g. "how are you", with spaces, for a 3-gram

        public List<string> TokenList { get; set; } // e.g. {"how","are","you"}, for the 3-gram exemplified above

        public double FrequencyPerMillionInstances { get; set; }

        // Write this constructor: Should set the identifier, and
        // also generate the WordList after splitting the identifier
        // For n=1, the wordList contains only the identifier itself.
        // 
        // Note: 
        // (1) Here we could use the Token class, but since we do not care about
        //     the POS tags in Assignment 1.4, it is easier just to use a list of strings.
        //     I call it TokenList instead of WordList, since we will also include punctuation
        //     and other special characters.
        //
        // (2) Make sure to include punctuation symbols. The probability of, say, starting
        //     a sentence with word w is determine by the bigram "<SEP> w"
        //
        // (3) Make sure that the identifiers (and words) do not contain
        //     any leading or trailing spaces. See the Trim() method for generic lists
        //
        // You also need to compute the frequency (per million words), which has
        // to be done externally, once you have loaded the entire text data set
        // and counted the number of instances (for a given n-gram as well as
        // the total number, to obtain relative frequencies).
        //
        // Note that the frequencies (per million instances) of (for example) 2-grams is equal to
        // 10^6 x (the number of instances of the bigram)/(total # of bigrams), and so on.
        // 

        public NGram(string identifier)
        {
            TokenList = new List<string>();
        }
    }
}
