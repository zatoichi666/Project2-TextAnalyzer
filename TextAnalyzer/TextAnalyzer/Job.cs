using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalyzer
{
    interface IJob
    {
        bool recursive { get; set; }               // /R
        String queryType { get; set; }             // /T or /M
        String taskPath { get; set; }                  // /F
        List<String> extensionList { get; set; }       // /X
        List<String> categoryList { get; set; }        // /C
        List<String> keywordList { get; set; }         // /K
        List<String> textQueryList { get; set; }       // /T
        List<String> metadataQueryList { get; set; }   // /M
        List<String> resultStringList { get; set; }
        bool mustFindAll { get; set; }             // /A or /O

    }
    public class Job : IJob
    {
        // Fields:
        private bool _recursive = false;
        private bool _mustFindAll = false;
        private String _queryType;
        private String _taskPath;
        private List<String> _extensionList = new List<String>();
        private List<String> _categoryList = new List<String>();
        private List<String> _keywordList = new List<String>();
        private List<String> _textQueryList = new List<String>();
        private List<String> _metadataQueryList = new List<String>();
        private List<String> _resultStringList = new List<String>();


        bool _switchO { get; set; }
        bool _switchA { get; set; }
        bool _switchT { get; set; }
        bool _switchM { get; set; }
        bool _switchX { get; set; }
        bool _switchF { get; set; }



        /*public Job(bool recursive, String queryType, String[] extensionList,
            String[] categoryList, String[] keywordList, String[] textQueryList,
            String[] metadataQueryList, bool mustFindAll)
        {
            _recursive = recursive;
            _queryType = queryType;
            _extensionList = extensionList;
            _categoryList = categoryList;
            _keywordList = keywordList;
            _textQueryList = textQueryList;
            _metadataQueryList = metadataQueryList;
            _mustFindAll = mustFindAll;

        }*/

        // Property implementation: 
        public String taskPath
        {
            get
            {
                return _taskPath;
            }

            set
            {
                _taskPath = value;
            }
        }

        // Property implementation: 
        public bool recursive
        {
            get
            {
                return _recursive;
            }

            set
            {
                _recursive = value;
            }
        }

        // Property implementation: 
        public String queryType
        {
            get
            {
                return _queryType;
            }

            set
            {
                _queryType = value;
            }
        }

        // Property implementation: 
        public List<String> extensionList
        {
            get
            {
                return _extensionList;
            }

            set
            {
                _extensionList = value;
            }
        }

        // Property implementation: 
        public List<String> categoryList
        {
            get
            {
                return _categoryList;
            }

            set
            {
                _categoryList = value;
            }
        }

        // Property implementation: 
        public List<String> keywordList
        {
            get
            {
                return _keywordList;
            }

            set
            {
                _keywordList = value;
            }
        }

        // Property implementation: 
        public List<String> textQueryList
        {
            get
            {
                return _textQueryList;
            }

            set
            {
                _textQueryList = value;
            }
        }

        // Property implementation: 
        public List<String> metadataQueryList
        {
            get
            {
                return _metadataQueryList;
            }

            set
            {
                _metadataQueryList = value;
            }
        }

        // Property implementation: 
        public bool mustFindAll
        {
            get
            {
                return _mustFindAll;
            }

            set
            {
                _mustFindAll = value;
            }
        }

        // Property implementation: 
        public List<String> resultStringList
        {
            get
            {
                return _resultStringList;
            }

            set
            {
                _resultStringList = value;
            }
        }

        private int handleSwitchO(Switch_s element)
        {
            int returncode = 0;
            if (_switchA == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting find all/one option, /O and /A");
            }

            if (_switchM == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting text find option /O or /A with metadata query switch /M");
            }
            _mustFindAll = false;
            _switchO = true;
            return returncode;
        }

        private int handleSwitchA(Switch_s element)
        {
            int returncode = 0;
            if (_switchO == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting find all/one option, /O and /A");
            }

            if (_switchM == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting text find option /O or /A with metadata query switch /M");
            }

            _mustFindAll = true;
            _switchA = true;
            return returncode;
        }


        private int handleSwitchF(Switch_s element)
        {
            int returncode = 0;
            if (_switchF == false)
            {
                _switchF = true;
            }
            _taskPath = element.payload;
            return returncode;
        }

        private int handleSwitchX(Switch_s element)
        {
            int returncode = 0;
            if (_switchX == false)
            {
                _extensionList.Clear();
            }
            _switchX = true;
            _extensionList.Add(element.payload);
            return returncode;
        }

        private int handleSwitchM(Switch_s element)
        {
            int returncode = 0;

            if (_switchM == false)
            {
                _extensionList.Clear();
                _extensionList.Add("metadata");
            }

            if (_switchT == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting query types, /T and /M");
            }

            if (_switchO == true || _switchA == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting text find option /O or /A with metadata query switch /M");
            }

            _queryType = "metadata";
            if (_metadataQueryList == null)
            {
                _metadataQueryList = new List<String>();
            }
            _metadataQueryList.Add(element.payload);
            _switchM = true;

            return returncode;
        }

        private int handleSwitchT(Switch_s element)
        {
            int returncode = 0;

            if (_switchT == false)
            {
                _extensionList.Clear();
                _extensionList.Add("cs");
                _extensionList.Add("txt");
            }

            if (_switchM == true)
            {
                returncode = -1;
                _resultStringList.Add("Specified conflicting query types, /T and /M");
            }
            else
            {
                _queryType = "text";
                if (_textQueryList == null)
                {
                    _textQueryList = new List<String>();
                }
                _textQueryList.Add(element.payload);
            }
            _switchT = true;
            return returncode;
        }


        public int setJobFromSwitches(List<Switch_s> _switches)
        {
            int returncode = 0;
            Console.WriteLine("Processing the command line switches");
            List<Switch_s> switches = new List<Switch_s>();
            switches = _switches;

            foreach (Switch_s element in _switches)
            {
                // Handle query type switches
                if (element.switchChar == "T")
                {
                    returncode = handleSwitchT(element);
                }
                else if (element.switchChar == "M")
                {
                    returncode = handleSwitchM(element);
                }
                // Handle recursion switch
                else if (element.switchChar == "R")
                {
                    _recursive = true;
                }
                // Handle all/one options
                else if (element.switchChar == "O")
                {
                    returncode = handleSwitchO(element);
                }
                else if (element.switchChar == "A")
                {
                    returncode = handleSwitchA(element);
                }

                else if (element.switchChar == "X")
                {
                    returncode = handleSwitchX(element);
                }
                else if (element.switchChar == "F")
                {
                    returncode = handleSwitchF(element);

                }
                else
                {
                    returncode = -1;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Bad option: ");
                    sb.Append(element.switchChar);
                    sb.Append(" ");
                    sb.Append(element.payload);
                    _resultStringList.Add(sb.ToString());

                }
            }
            return returncode;
        }
    }
}
