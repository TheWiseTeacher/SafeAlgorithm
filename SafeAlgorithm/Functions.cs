using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeAlgorithm
{
    public static class F
    {
        public static BindingList<LogEntry> LogEntries;

        static F()
        {
            LogEntries = new BindingList<LogEntry>();
        }


        public static void ClearLog()
        {
            LogEntries.Clear();
        }

        public static void Log(string message, int line = -1)
        {
            LogEntries.Add(new LogEntry(message, line));
        }


        public static bool IsValidName(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if ((i == 0 && Char.IsDigit(s[0])) || (i > 0 && !Char.IsLetterOrDigit(s[i]) && s[i] != '_' ))
                    return false;
            }

            return true;
        }

        public static bool IsFirstWord(this string s, string word)
        {
            return s.Split(' ')[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase);
        }
    }
}
