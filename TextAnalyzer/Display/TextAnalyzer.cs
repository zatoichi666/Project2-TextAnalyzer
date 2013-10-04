using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    class TextAnalyzer
    {
        static void Main(string[] args)
        {
            CommandLineProcessor cmd = new CommandLineProcessor(args);
            TextAnalyzerJob _job = new TextAnalyzerJob();

            int errcode = _job.setJobFromSwitches(cmd.getSwitches());

            if (errcode == -1)
            {
                Console.WriteLine("There were errors:");
                foreach (String errString in _job.resultStringList)
                {
                    Console.WriteLine(errString);
                }
                Console.WriteLine("");
                _job.DisplayUsage();
            }
            else if (errcode == 0)
            {

                IEnumerable<string> fileTable = FileTable.GetFiles(_job.taskPath, _job.extensionList.ToArray(), _job.recursive);

                if (_job.queryType == "text")
                {
                    /*
                     * /T"String" /T"int" /A /F"C:\School\CSE-681\Project2-TextAnalyzer" /X"txt" /X"cs" /R
                     */
                    Console.Write("Searching for ");
                    foreach (String s in _job.textQueryList)
                    {
                        Console.Write(s);
                        Console.Write(", ");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Printing the results of the text query");
                    List<String> tq = TextQuery.run(fileTable, _job.textQueryList.ToArray(), _job.mustFindAll);
                    foreach (String s in tq)
                    {
                        Console.WriteLine(s);
                    }
                }
                if (_job.queryType == "metadata")
                {
                    /*
                     * /M"time" /M"keyword" /F"C:\School\CSE-681\Project2-TextAnalyzer" /X"metadata" /R
                     */
                    Console.Write("Searching for ");
                    foreach (String s in _job.metadataQueryList)
                    {
                        Console.Write(s);
                        Console.Write(", ");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Printing the results of the metadata query");
                    List<XmlSearchResult_c> mq = MetadataQuery.run(fileTable, _job.metadataQueryList.ToArray());
                    foreach (XmlSearchResult_c s in mq)
                    {
                        Console.WriteLine("Results for: " + s.filename);

                        foreach (String res in s.tagAndValue)
                        {
                            Console.WriteLine("   " + res);
                        }


                    }
                }
            }
            else
            {
                _job.DisplayUsage();
            }

        }
    }
}
