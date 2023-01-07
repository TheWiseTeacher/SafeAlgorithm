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
        public static List<string> KeyWords;

        static F()
        {
            LogEntries = new BindingList<LogEntry>();
            KeyWords = new List<string>();

            KeyWords.AddRange(new string[] { "ALGORITHME", "DEBUT", "FIN", "VAR", "CONST", "ECRIRE", "LIRE" });

            KeyWords.AddRange(Object.iNames);
            KeyWords.AddRange(Object.dNames);
            KeyWords.AddRange(Object.cNames);
            KeyWords.AddRange(Object.sNames);
            KeyWords.AddRange(Object.bNames);

            KeyWords.AddRange(Object.tValues);
            KeyWords.AddRange(Object.fValues);
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
                if ((i == 0 && char.IsNumber(s[0])) || !(char.IsLetter(s[i]) || char.IsNumber(s[i])) && s[i] != '_')
                    return false;

            if (KeyWords.Contains(s, StringComparer.OrdinalIgnoreCase))
                return false;

            return true;
        }

        public static bool IsFirstWord(this string s, string word)
        {
            return s.Split(' ')[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase);
        }
    }
}
