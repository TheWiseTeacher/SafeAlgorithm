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

        public List<Variable> Variables { get; set; } = new List<Variable>();


        public MainBloc(string[] sourceCode, int algorithmeStartIndex)
        {
            string line, word;

            mainBlocLine = algorithmeStartIndex;
            F.Log($"Found ALGORITHME keyword", mainBlocLine);

            for (int i = algorithmeStartIndex; i < sourceCode.Length; i++)
            {
                // Trim White spaces
                line = sourceCode[i].Trim();

                // Skip empty lines
                if (line == string.Empty)
                    continue;

                // Get objects since we didn't find "DEBUT"
                if(startLine == -1)
                {
                    // If we found "DEBUT"
                    if (line.IsFirstWord("DEBUT"))
                    {
                        startLine = i;
                        F.Log($"Found DEBUT keyword", mainBlocLine);

                        // Continue to next line
                        continue;
                    }
                    else
                    {
                        if (startLine == -1)
                        {
                            if (!ParseObjects(i, line))
                                break;
                        }
                    }
                }



                if (mainBlockFound)
                {


                }
                else if (line.Split(' ')[0].Trim().ToUpper() == "ALGORITHME")
                {
                    mb = new MainBloc();
                    mainBlockFound = true;
                    mainBlocLine = i;

                    F.Log($"Found ALGORITHME keyword", mainBlocLine);
                    continue;
                }
            }

            if (mainBlocLine == -1)
            {
                F.Log($"Error : Can't find the ALGORITHME keyword");
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
                F.Log($"Syntax error when declaring variables [Too mant types]", lineNumber);
                return false;
            }
            else if(lc.Length == 2)
            {
                string vType = lc[1].Trim().ToUpper();

                if (!Variable.ALLOWED_TYPE.Contains(vType))
                {
                    F.Log($"Error unknown variable type '{vType}'", lineNumber);
                    return false;
                }

                string[] vNames = lc[0].Split(',');
                string vName;

                for (int i = 0; i < vNames.Length; i++)
                {
                    vName = vNames[i].Trim();

                    if (vName.ToUpper() == "VAR")
                        continue;

                    if(vName == String.Empty || !F.IsValidName(vName))
                    {
                        F.Log($"Error : bad variable name found '{vName}'", lineNumber);
                        return false;
                    }

                    // Declare the variable
                    Variables.Add(new Variable() { Name = vName, Type = vType });
                    F.Log($"Declared a new variable '{vName}' of type {vType}", lineNumber);
                }
            }




            return false;
        }

    }
}
