using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace Petri
{
    class PositionClass : TPClass
    {
        //OvalShape icon;
                
        public PositionClass(Point location, int n)
        {
            GiveName(location, n, Type.pos);
        }

        
    }
}
