using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeAlgorithm
{
    public class MainBloc
    {
        public string Name { get; set; }

        public bool CodeReady { get; set; } = false;

        int mainBlocLine = -1;
        int startLine = -1;
        int endLine = -1;

        public List<Object> Variables { get; set; } = new List<Object>();


        public MainBloc(string[] sourceCode, int algorithmeStartIndex)
        {
            string line, word;
            string[] words;

            //Remember where it starts
            mainBlocLine = algorithmeStartIndex;

            // Extract algorithm name
            line = sourceCode[mainBlocLine].Trim();
            words = line.Split(' '); word = words[0];
            Name = words.Length > 1 ? line.Substring(word.Length, line.Length - word.Length).Trim() : "Unamed Algorithm";

            F.Log($"Found ALGORITHME keyword", mainBlocLine);
            F.Log($"# Main Algorithm name : {Name}");

            for (int i = algorithmeStartIndex + 1; i < sourceCode.Length; i++)
            {
                // Trim White spaces
                line = sourceCode[i].Trim();

                // Skip empty lines
                if (line == string.Empty)
                    continue;

                if(startLine < 0)
                {
                    if (line.IsFirstWord("DEBUT"))
                    {
                        // If we found "DEBUT"

                        startLine = i;
                        F.Log($"Found DEBUT keyword", i);

                        // Continue to next line
                        continue;
                    }
                    else
                    {
                        // Get objects since

                        if (!ParseObjects(i, line))
                            break;
                    }
                }
                else if(endLine < 0)
                {
                    if (line.IsFirstWord("FIN"))
                    {
                        // If we found "FIN"

                        endLine = i;
                        F.Log($"Found FIN keyword", i);

                        // Continue to next line
                        break;
                    }
                    else
                    {
                        // Get instructions


                    }
                }
            }

            if (endLine < 0)
            {
                F.Log($"Error : The main algorithm bloc is not terminated !");
                return;
            }

            CodeReady = true;
        }



        public bool ParseObjects(int lineNumber, string lineValue)
        {
            lineValue.Trim();

            if (lineValue == String.Empty)
                return true;

            // Check if object is a variable (Supported syntaxe)
            //
            //      name : Type
            //      Var name : Type
            //      Var nameA, nameB, nameC : Type
            //
            string[] lc = lineValue.Split(':');
            if(lc.Length > 2)
            {
                F.Log($"Syntax error when declaring a variable [Too many types]", lineNumber);
                return false;
            }
            else if(lc.Length == 2)
            {
                lc[0] = lc[0].Trim();
                string vType = lc[1].Trim().ToUpper();

                if (lc[0].IsFirstWord("VAR"))
                    lc[0] = lc[0].Remove(0, 4);

                if (lc[0].IsFirstWord("CONST"))
                {
                    F.Log($"Confused object declaration, found CONST and : in one line !", lineNumber);
                    return false;
                }

                string[] vNames = lc[0].Split(',');
                string vName;

                for (int i = 0; i < vNames.Length; i++)
                {
                    vName = vNames[i].Trim();

                    if(vName == String.Empty || !F.IsValidName(vName))
                    {
                        F.Log($"Error : bad variable name found '{vName}'", lineNumber);
                        return false;
                    }

                    if(CheckObjectExistence(vName))
                    {
                        F.Log($"Error : Variable already declared '{vName}'", lineNumber);
                        return false;
                    }

                    // Create a new variable object
                    Object v = new Object(vName);

                    if (!v.SetValueByType(vType))
                    {
                        F.Log($"Error unknown variable type '{vType}'", lineNumber);
                        return false;
                    }

                    // Declare the variable
                    Variables.Add(v);
                    F.Log($"Declared a new variable '{vName}' of type {vType}", lineNumber);
                }
            }

            // Check if object is a constant (Supported syntaxe)
            //
            //      name = Value
            //      Const name = Type
            //
            lc = lineValue.Split('=');
            if (lc.Length > 2)
            {
                F.Log($"Syntax error when declaring a constant [Too many values]", lineNumber);
                return false;
            }
            else if (lc.Length == 2)
            {
                if (lc[0].IsFirstWord("CONST"))
                    lc[0] = lc[0].Remove(0, 4);

                if (lc[0].IsFirstWord("VAR"))
                {
                    F.Log($"Confused object declaration, found VAR and = in one line !", lineNumber);
                    return false;
                }

                string cName = lc[0].Trim();
                string cValue = lc[1].Trim();

                if (cName == String.Empty || !F.IsValidName(cName))
                {
                    F.Log($"Error : Bad constant name found '{cName}'", lineNumber);
                    return false;
                }

                // Declare and instantiate constant object
                Object v = new Object(cName, true);

                try
                {
                    v.SetTypeByValue(cValue);
                }
                catch (Exception ex)
                {
                    F.Log($"Error : Unable to parse constant value '{cValue}' | {ex.Message}", lineNumber);
                    return false;
                }

                Variables.Add(v);
                F.Log($"Declared a new constant '{cName}' with the value of {v.Value} | {v.Value.GetType()}", lineNumber);

            }

            return true;
        }

        private bool CheckObjectExistence(string name)
        {
            for (int i = 0; i < Variables.Count; i++)
            {
                if (Variables[i].Name == name)
                    return true;
            }

            return false;
        }

    }
}
