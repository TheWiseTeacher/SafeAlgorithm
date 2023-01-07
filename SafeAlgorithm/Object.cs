using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeAlgorithm
{
    public class Object
    {
        public static readonly string[] iNames = { "ENTIER" };
        public static readonly string[] dNames = { "REEL" };
        public static readonly string[] cNames = { "CARACTERE" };
        public static readonly string[] sNames = { "CHAINE DE CARACTERE", "CC" };
        public static readonly string[] bNames = { "BOOLEEN", "BOOLEAN" };

        public static readonly string[] tValues = { "Vrai", "True" };
        public static readonly string[] fValues = { "Faux", "False" };

        public string Name { get; private set; }

        public object Value { get; set; }

        public bool ReadOnly { get; set; }

        public Object(string name, bool isReadOnly = false)
        {
            Name = name;
            Value = null;
            ReadOnly = isReadOnly;
        }

        public bool SetValueByType(string type)
        {
            if(iNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = string.Empty;
            }
            else if(iNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = 0;
            }
            else if (dNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = 0.0m;
            }
            else if (cNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = ' ';
            }
            else if (sNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = "";
            }
            else if (bNames.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                Value = false;
            }

            // If Variable type found correctly Value is not null
            return Value != null;
        }

        public void SetTypeByValue(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                // Remove double quotes
                Value = value.Substring(1, value.Length - 2);

            }
            else if (value.StartsWith("'") && value.EndsWith("'"))
            {
                // Remove quotes
                value = value.Substring(1, value.Length - 2);

                // Check if it's a string with wrong quotes
                if (value.Length > 1)
                    throw new Exception("LongChar");

                // Only grab the character
                Value = value[0];
            }
            else if (int.TryParse(value, out int iValue))
            {
                // Save the parsed value
                Value = iValue;
            }
            else if (decimal.TryParse(value, out decimal dValue))
            {
                // Save the parsed value
                Value = dValue;
            }
            else
            {
                if (tValues.Contains(value))
                {
                    Value = true;
                }
                else if (fValues.Contains(value))
                {
                    Value = false;
                }
            }

            if(Value == null)
                // We can't parse this value UnknownValue;
                throw new Exception("UnknownValue");
        }
    }
}
