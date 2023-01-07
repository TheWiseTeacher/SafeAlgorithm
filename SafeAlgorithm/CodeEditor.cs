using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SafeAlgorithm
{
    class CodeEditor : Control
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 30, Height));
        }



    }
}
