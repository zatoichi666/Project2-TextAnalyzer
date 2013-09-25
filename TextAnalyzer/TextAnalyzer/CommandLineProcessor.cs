using System;
using System.Collections.Generic;

namespace TextAnalyzer
{

    public class CommandLineProcessor
    {
        private List<Switch_s> switches = new List<Switch_s>();
        private Job _job = new Job();

        public List<Switch_s> getSwitches()
        {
            return switches;
        }

        public Job getJob()
        {
            return _job;
        }
        /// <summary>
        /// Check for the form of the arguments from the user.  
        /// Ensure:
        /// 1. There are arguments, otherwise show the usage statement.
        /// 2. There are entries, consisting of a single character "switch character" 
        ///     and a payload.  Note, the payload can be empty, so it can have any form.
        /// </summary>
        /// <param name="args"></param>
        public CommandLineProcessor(String[] args)
        {
            if (args.Length == 0)
            {
                DisplayUsage.toConsole();
            }
            else
            {
                // Populate the switches object
                for (int i = 0; i < args.Length; i++)
                {
                    try
                    {
                        Switch_s entry = new Switch_s();
                        if (args[i].IndexOf("/") == 0)
                        {
                            entry.payload = args[i].Substring(args[i].IndexOf("/") + 2, args[i].Length - args[i].IndexOf("/") - 2);
                            entry.switchChar = args[i].Substring(args[i].IndexOf("/") + 1, 1);
                        }
                        else
                        {
                            entry.payload = args[i];
                            entry.switchChar = "";
                        }
                        switches.Add(entry);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                }
            }

            // Use the switches object to build the Job
            _job.setJobFromSwitches(switches);

        }

    }


}
