using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    /// <summary>
    /// Matthew Synborski
    /// CSE-681 Fall 2013 for Dr. Jim Fawcett
    /// 
    /// Description: XmlSearchResult_c class is used for storing XML search results. 
    /// The class contains the filename, and the associated list of XML tag+values.
    /// A ToString() method is provided, which provides the content of the 
    /// XmlSearchResult_c instance in human-readable form.
    /// 
    /// </summary>
    class XmlSearchResult_c
    {

        static void Main()
        {
            Console.WriteLine("Testing XmlSearchResult_c class");
            XmlSearchResult_c test = new XmlSearchResult_c();
            test.filename = "somefilename.txt";
            test.tagAndValue.Add("time:19910315052456");
            test.tagAndValue.Add("keyword:source");
            test.tagAndValue.Add("keyword:pictures");
            Console.Write(test.ToString());
        }

        public String filename;
        public List<String> tagAndValue = new List<String>();

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Filename: " + this.filename + "\n");
            sb.Append('_',10+this.filename.Length);
            sb.Append("\n");

            foreach (String s in this.tagAndValue)
            {
                sb.Append(s + "\n");
            }
            return sb.ToString();
        }
    }

}
