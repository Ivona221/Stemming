using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stemming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Stemmer.StemWords();
           
            //var prefix1 = @"^до";
            //var prefix2 = @"^по";
            //MatchCollection pref1 = Regex.Matches("погоре", prefix2);
            // MatchCollection pref2 = Regex.Matches("догоре", prefix1);

        }
    }
}
