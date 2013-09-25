using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    public static class FileTable
    {
        private static string[] correctExtensionArray(string[] ext)
        {
            List<String> extCorr = new List<String>();

            foreach (string s in ext)
            {
                if (!s.Contains("*"))
                {
                    extCorr.Add(String.Concat("*.", s));
                }
                else if (!s.Contains("."))
                {
                    extCorr.Add(String.Concat(".", s));
                }
                else
                {
                    extCorr.Add(s);
                }

            }
            return extCorr.ToArray();
        }


        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, bool isRecursive)
        {
            searchPatterns = correctExtensionArray(searchPatterns);
            SearchOption searchOption;
            if (isRecursive)
            {

                searchOption = SearchOption.AllDirectories;
            }
            else
            {
                searchOption = SearchOption.TopDirectoryOnly;
            }
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
    }
}
