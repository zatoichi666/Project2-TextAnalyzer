using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    class Executive
    {
        static void Main(string[] args)
        {
            CommandLineProcessor cmd = new CommandLineProcessor(args);
            Job _job = cmd.getJob();

            if (_job.resultStringList.Count > 0)
            {
                Console.WriteLine("There were errors:");
                foreach (String errString in _job.resultStringList)
                {
                    Console.WriteLine(errString);
                }
                Console.WriteLine("");
                DisplayUsage.toConsole();
            }
            else
            {
                Console.Write("Searching for ");
                foreach (String s in _job.textQueryList)
                {
                    Console.Write(s);
                    Console.Write(", ");
                }
                Console.WriteLine();
                IEnumerable<string> fileTable = FileTable.GetFiles(_job.taskPath, _job.extensionList.ToArray(), _job.recursive);

                if (_job.queryType == "text")
                {
                    Console.WriteLine("Printing the results of the text query");
                    List<String> tq = TextQuery.run(fileTable, _job.textQueryList.ToArray(), _job.mustFindAll);
                    foreach (String s in tq)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        }
    }
}
