using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri
{
    class Master
    {
        public static Panel properties;
        public static Panel freespace;
        public static TPClass selected;
        //public static ToolStripStatusLabel ss;
        public static Point MouseLocation;
    }

    class Info
    {
        public static List<Arrow> AList = new List<Arrow>();
        public static List<PositionClass> PCList = new List<PositionClass>();
        public static List<JumpClass> JCList = new List<JumpClass>();
    }
}
