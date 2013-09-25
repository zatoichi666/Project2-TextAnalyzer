using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    public static class DisplayUsage
    {

        public static void toConsole()
        {
            Console.WriteLine("TextAnalyzer by Matthew Synborski");
            Console.WriteLine("Usage: TextAnalyzer [OPTIONS]");
            Console.WriteLine("   or: TextAnalyzer path [OPTIONS]");
            Console.WriteLine("");
            Console.WriteLine("/R,           Recursively search");
            Console.WriteLine("/T,           Text query");
            Console.WriteLine("    /O,       -must contain one of text strings: default");
            Console.WriteLine("    /A,       -must contain all text strings");            
            Console.WriteLine("/M,           Metadata query");
            Console.WriteLine("/C,           Category query");
            Console.WriteLine("/X,           File Extension (one /X for each extension)");
                      

        }
    }
}
