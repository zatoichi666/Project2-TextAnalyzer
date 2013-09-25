using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TextAnalyzer
{
    class TextQuery
    {
        public static List<String> run(IEnumerable<string> fileTable, String[] searchStringList, bool mustContainAll)
        {
            List<String> filesContainingSearchStrings = new List<String>();
            IEnumerable<string> strings = searchStringList;

            foreach (String filename in fileTable)
            {
                String stringToCheck = File.ReadAllText(filename);
                
                if (mustContainAll)
                {
                    if (searchStringList.All(s => stringToCheck.Contains(s)))
                    {
                        filesContainingSearchStrings.Add(filename);
                    }
                }
                else
                {
                    if (searchStringList.Any(stringToCheck.Contains))
                    {
                        filesContainingSearchStrings.Add(filename);
                    }
                }
            }
            return filesContainingSearchStrings;
        }
    }
}
