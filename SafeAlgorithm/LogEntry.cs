using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeAlgorithm
{
    public class LogEntry
    {
        public int LineNumber { get; set; }

        public string Message { get; set; }

        public string Info
        {
            get
            {
                if (LineNumber == -1)
                    return Message;
                else
                    return $"{Message} | Line {LineNumber + 1}";
            }
        }

        public LogEntry(string message, int lineNumber = -1)
        {
            Message = message;
            LineNumber = lineNumber;
        }
    }
}
