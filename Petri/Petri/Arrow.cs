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
    class Arrow 
    {
        ShapeContainer Parent;
        List<LineShape> path;
        LineShape left, right;

        public bool Ended;
        
        public Arrow(Control parent)
        {
            path = new List<LineShape>();
            left = new LineShape();
            right = new LineShape();

            Parent = new ShapeContainer();
            Parent.BorderStyle = BorderStyle.FixedSingle;
            Parent.Enabled = false;
            Parent.Parent = parent;
            Parent.Parent.KeyPress += ls_KeyPress;
            left.Parent = Parent;
            right.Parent = Parent;

            Ended = false;
        }

        public void SetPoint(int x, int y)
        {
            if (path.Count == 0)
                SetFromPoint(x, y);
            else
                SetPathPoint(x, y);
        }

        void SetFromPoint(int x, int y)
        {
            LineShape ls = new LineShape();
            ls.X2 = ls.X1 = x;
            ls.Y2 = ls.Y1 = y;
            ls.KeyPress += ls_KeyPress;
            ls.Parent = Parent;
            path.Add(ls);

            ChangeDirection();
        }

        public void SetToPoint(int x, int y)
        {
            path[path.Count - 1].X2 = x;
            path[path.Count - 1].Y2 = y;
            ChangeDirection();
            this.Ended = true;
            this.Parent.Enabled = true;
        }

        void SetPathPoint(int x, int y)
        {
            if (path[path.Count - 1] == null)
            {
                LineShape ls = new LineShape();
                ls.KeyPress += ls_KeyPress;
                ls.X1 = x;
                ls.Y1 = y;
                ls.Parent = Parent;
                path.Add(ls);
            }
            else 
            {
                path[path.Count - 1].X2 = x;
                path[path.Count - 1].Y2 = y;
                path.Add(null);
            }
        }

        void ChangeDirection()
        {
            double angle = 0;
            int l = 15;
            double d = Math.PI;
            LineShape main = path[path.Count - 1];

            #region left
            left.X2 = main.X2;
            left.Y2 = main.Y2;

            angle = main.X2 - main.X1;

            if (angle > 0) d = 0; 
            if ((main.Y2 - main.Y1) != 0) angle /= main.Y2 - main.Y1;

            angle = Math.Atan(1/angle) + d;
            angle += 15 * Math.PI / 180;

            left.X1 = (int)(left.X2 - l * Math.Cos(angle));
            left.Y1 = (int)(left.Y2 - l * Math.Sin(angle));
            #endregion

            #region right
            right.X2 = main.X2;
            right.Y2 = main.Y2;

            angle -= 30 * Math.PI / 180;
            right.X1 = (int)(right.X2 - l * Math.Cos(angle));
            right.Y1 = (int)(right.Y2 - l * Math.Sin(angle));
            #endregion
        }

        public void MouseMoveing(object sender, MouseEventArgs e)
        {
            if (!Ended)
            {
                path[path.Count - 1].X2 = Master.MouseLocation.X;
                path[path.Count - 1].Y2 = Master.MouseLocation.Y;
                ChangeDirection();
            }
        }

        void ls_KeyPress(object s, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Parent.Controls.Clear();
                Parent.Parent.Controls.Remove(Parent);
                Info.AList.Remove(this);
            }
        }
    }
}
