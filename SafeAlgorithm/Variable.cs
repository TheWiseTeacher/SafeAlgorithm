using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeAlgorithm
{
    public class Variable
    {
        public static readonly string[] ALLOWED_TYPE = new string[]
        {
            "ENTIER",
            "REEL",

            "BOOLEAN",
            "BOOL",

            "CARACTERE",

            "CHAINE DE CARACTERES",
            "CC",
        };

        public string Name { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

    }
}
