using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeAlgorithm
{
    public partial class CodeEditorForm : Form
    {
        public CodeEditorForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = new Point(1923, 10);
            Size = new Size(1274, 791);

            lb_Output.DataSource = F.LogEntries;
            lb_Output.DisplayMember = "Info";

            try{codeEditor.Lines = File.ReadAllLines("LastCode.txt"); codeEditor.SelectionStart = codeEditor.Text.Length; }
            catch (Exception){F.Log("Error : Unable to retreive last code !");}
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            CompileAndRun();
        }

        private void Save()
        {
            try { File.WriteAllLines("LastCode.txt", codeEditor.Lines); }
            catch (Exception) { F.Log("Error : Unable to save code !"); }
        }

        //List<string> codeLines = new List<string>();



        public void CompileAndRun()
        {
            F.ClearLog();

            DateTime parseStartTime = DateTime.Now;
            F.Log($"Found {codeEditor.Lines.Length} Lines of code !");
            F.Log($"# Started Parsing code...");

            string line;
            MainBloc mb = null;

            for (int i = 0; i < codeEditor.Lines.Length; i++)
            {
                line = codeEditor.Lines[i].Trim();

                if (line.IsFirstWord("ALGORITHME"))
                {
                    if(mb == null)
                    {
                        mb = new MainBloc(codeEditor.Lines, i);

                        if (!mb.CodeReady)
                            break;
                    }
                    else
                    {
                        F.Log($"Error : Multiple start points detected");
                        break;
                    }

                    continue;
                }
            }

            if(mb == null)
            {
                F.Log($"Error : Can't find the ALGORITHME keyword");
            }

            F.Log($"# Parsed in : {(DateTime.Now - parseStartTime).TotalMilliseconds} ms");
        
        
        }







        /*
         * 
        private void CodeEditor_TextChanged(object sender, EventArgs e)
        {

            DrawingControl.SuspendDrawing(CodeEditor);

            int selectStart = CodeEditor.SelectionStart;

            CodeEditor.SelectAll();
            CodeEditor.SelectionColor = Color.Black;
            CodeEditor.Select(selectStart, 0);


            CheckKeyword("algorithme", Color.Blue, 0);

            DrawingControl.ResumeDrawing(CodeEditor);
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (CodeEditor.Text.Contains(word))
            {
                int index = -1;
                int selectStart = CodeEditor.SelectionStart;

                while ((index = CodeEditor.Text.IndexOf(word, (index + 1))) != -1)
                {
                    CodeEditor.Select((index + startIndex), word.Length);
                    CodeEditor.SelectionColor = color;


                    CodeEditor.Select(selectStart, 0);
                    CodeEditor.SelectionColor = Color.Black;
                }
            }
        }

        */

    }
}
