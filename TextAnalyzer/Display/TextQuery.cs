using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;

namespace TextAnalyzer
{
    /// <summary>    
    /// Matthew Synborski
    /// CSE-681 Fall 2013 for Dr. Jim Fawcett
    /// 
    /// Description: TextQuery Class - Performs Text Query on a list of fully qualified filenames
    /// This Query iterates through each filename, parsing the file completely, looking for any 
    /// or all of the search strings.
    /// 
    /// </summary>
    class TextQuery
    {
        /// <summary>
        /// Demonstration entry point for TextQuery
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Demonstrating TextQuery Class");
            Console.WriteLine("=============================");

            Console.WriteLine("Demonstrates find all vs. find one TextQuery, using the same file table.");
            Console.WriteLine("Expected Result: The \"find one\" query should return more results.");
            Console.WriteLine();

            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "/../..");
            String currDir = Directory.GetCurrentDirectory();

            String[] extArray = { "*.cs" };
            String[] searchStringArray = { "String", "Synborski", "int" };

            Console.WriteLine("Working in " + currDir);
            Console.Write("Looking for {");
            foreach (String s in searchStringArray)
            {
                Console.Write("\"" + s + "\" ");

            }
            Console.WriteLine("}");
            Console.WriteLine();

            IEnumerable<String> ft = FileTable.GetFiles(currDir, extArray, true);

            Console.WriteLine("Printing contents of file set (using FileTable)");
            foreach (String s in ft)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

            List<String> res = TextQuery.run(ft, searchStringArray, true);
            Console.WriteLine("Printing results of \"find all\" TextQuery");
            foreach (String s in res)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

            List<String> res2 = TextQuery.run(ft, searchStringArray, false);
            Console.WriteLine("Printing results of \"find one\" TextQuery");
            foreach (String s in res2)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

        }

        /// <summary>
        /// run is the static method in the TextQuery class.  
        /// This takes an IEnumerable of strings containing the fully qualified filenames 
        /// of the files to be Queried.  This query returns the list of filenames that contain
        /// any or all of the strings within the searchStringList array.
        /// 
        /// If the fileTable entry being searched ends with ".doc", Microsoft.Office.Interop.Word 
        /// will be used to open the file, extract the text content and perform the text Query thereon.
        /// </summary>
        /// <param name="fileTable"></param>
        /// <param name="searchStringList"></param>
        /// <param name="mustContainAll"></param>
        /// <returns></returns>
        public static List<String> run(IEnumerable<string> fileTable, String[] searchStringList, bool mustContainAll)
        {
            List<String> filesContainingSearchStrings = new List<String>();
            IEnumerable<string> strings = searchStringList;

            foreach (String filename in fileTable)
            {
                String stringToCheck = "";

                if (filename.EndsWith(".doc") && (!filename.Contains("~")))
                {
                    Application ap = new Application();
                    Document document = ap.Documents.Open(filename); // Open the .doc using MSWord
                    stringToCheck = document.Content.Text;
                    ((_Application)ap).Quit(); // Close MSWord
                }
                else
                {
                    stringToCheck = File.ReadAllText(filename);
                }

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
