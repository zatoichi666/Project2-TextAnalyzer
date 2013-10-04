using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TextAnalyzer
{

    /// <summary>    
    /// Matthew Synborski
    /// CSE-681 Fall 2013 for Dr. Jim Fawcett
    /// 
    /// Description: MetadataQuery Class - Performs Metadata Query on a list of fully qualified filenames
    /// This Query iterates through each filename, parsing the file completely, looking for any 
    /// or all of the search tags.
    /// 
    /// </summary>
    static class MetadataQuery
    {
        /// <summary>
        /// Demonstration entry point for MetadataQuery
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Demonstrating MetadataQuery Class");
            Console.WriteLine("=============================");

            Console.WriteLine("Demonstrates MetadataQuery.");
            //Console.WriteLine("Expected Result: The \"find one\" query should return more results.");
            Console.WriteLine();

            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "/../..");
            String currDir = Directory.GetCurrentDirectory();

            String[] extArray = { "*.metadata" };
            String[] searchTagArray = { "time", "keyword" };

            Console.WriteLine("Working in " + currDir);
            Console.Write("Looking for {");
            foreach (String s in searchTagArray)
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

            Console.WriteLine("Printing contents of each .xml file in file set\n");
            foreach (String s in ft)
            {
                Console.WriteLine("Printing contents of " + s);
                Console.WriteLine();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(s);
                Console.Write(xDoc.OuterXml);
                Console.WriteLine();
            }
            

            Console.WriteLine("Printing results of MetadataQuery\n");

            List<XmlSearchResult_c> res = MetadataQuery.run(ft, searchTagArray);
            
            foreach (XmlSearchResult_c s in res)
            {
                s.ToString();
            }
            Console.WriteLine();  

        }

        private static XmlSearchResult_c queryXDocumentMetadata(XDocument xD, String filename, String[] searchTagList)
        {

            XmlSearchResult_c entry = new XmlSearchResult_c();
            entry.filename = filename;

            Console.WriteLine("Found valid metadata file: " + filename);
            foreach (String tag in searchTagList)
            {
                var q = from x in
                            xD.Elements("metadata")
                            .Descendants(tag)
                        select x;

                foreach (var elem in q)
                {
                    Console.Write("\n  {0} {1}", elem.Name.ToString(), elem.Value.ToString());
                    entry.tagAndValue.Add(elem.Name.ToString() + ":" + elem.Value.ToString());
                }
            }
            Console.Write("\n\n");

            return entry;
        }

        /// <summary>
        /// run is the static method in the MetadataQuery class.  
        /// This takes an IEnumerable of strings containing the fully qualified filenames 
        /// of the files to be Queried.  This query returns the list of XmlSearchResult_c objects that contain
        /// any of the specified tags within the searchTagList array.
        /// </summary>
        /// <param name="fileTable"></param>
        /// <param name="searchTagList"></param>
        /// <returns></returns>
        public static List<XmlSearchResult_c> run(IEnumerable<string> fileTable, String[] searchTagList)
        {
            List<XmlSearchResult_c> xmlSearchResults = new List<XmlSearchResult_c>();

            IEnumerable<string> strings = searchTagList;

            foreach (String filename in fileTable)
            {
                // for each file in the file table, read in the XML content
                XmlDocument xd = new XmlDocument();

                // Test if the corresponding file exists
                if (File.Exists(filename))
                {
                    xd.Load(filename);
                    XDocument xD = XDocument.Parse(xd.OuterXml.ToString());

                    // Test if the root node is "metadata"                    
                    if (xD.Root.Name.ToString() == "metadata")
                    {
                        xmlSearchResults.Add(queryXDocumentMetadata(xD, filename, searchTagList));
                    }
                }
                else
                {
                    Console.WriteLine("Skipping " + filename);
                }
            }

            return xmlSearchResults;
        }
    }
}
