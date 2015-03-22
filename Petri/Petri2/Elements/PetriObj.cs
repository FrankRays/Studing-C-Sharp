using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri2
{
    [Serializable()]
    class PetriObj
    {
        public string name;
        public Size size = new Size(global.w * global.cell, global.h * global.cell);
        public Point location;

        public void Draw(Graphics field, Color c)
        {
            field.FillRectangle(new SolidBrush(Color.White), new Rectangle(location, size));
            field.DrawRectangle(new Pen(c, 1), new Rectangle(location, size));
        }

        public Point SetLocation(int X, int Y)
        {
            double tmp = Math.IEEERemainder(X, global.cell);
            tmp = (tmp < 0) ? global.cell + tmp : tmp;
            location.X = X - (int)tmp;
            
            tmp = Math.IEEERemainder(Y, global.cell);
            tmp = (tmp < 0) ? global.cell + tmp : tmp;
            location.Y = Y - (int)tmp;
            
            return new Point(location.X / global.cell, location.Y / global.cell);
        }
    }
}
