using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.PowerPacks;

namespace Petri
{
    class JumpClass : TPClass
    {
        //RectangleShape icon;

        public JumpClass(Point location, int n)
        {
            GiveName(location, n, Type.jmp);
        }
    }
}
