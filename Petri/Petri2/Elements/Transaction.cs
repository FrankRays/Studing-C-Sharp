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
    class Transaction : PetriObj
    {
        Color color;
        Font font;

        public int baseT, currT;

        public Transaction()
        {
        }

        public Point Create(int X, int Y)
        {
            if (name == null)
            {
                if (data.TList.Count > 0)
                    name = "T" +
                        (int.Parse(data.TList[data.TList.Count - 1].name.Remove(0, 1)) + 1).ToString();
                else
                    name = "T1";
            }
            
            color = Color.Black;
            font = new Font("Times New Roman", 12.0f);

            Point returnPoint = SetLocation(X, Y);

            return returnPoint;
        }

        public void Draw(Graphics field)
        {
            Draw(field, color);
            field.DrawRectangle(new Pen(color, 2),
                location.X + global.hcell, location.Y + global.hcell, global.cell, global.cell);

            field.DrawString(name, font, new SolidBrush(color),
                new PointF(location.X + global.hcell, location.Y + 3 * global.hcell));
        }
        public void Clear(Graphics field)
        {

        }

        #region Moveing
        bool IsMoveing;

        void mouseDown(object sender, MouseEventArgs e)
        {
            global.outtxt.Text = "moveing";
            IsMoveing = true;
        }

        void mouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveing)
            {
                global.outtxt.Text = "moveing";
            }
        }

        void mouseUp(object sender, MouseEventArgs e)
        {
            IsMoveing = false;
        }
        #endregion

        void onClick(object sender, MouseEventArgs e)
        { 

        }

    }
}
