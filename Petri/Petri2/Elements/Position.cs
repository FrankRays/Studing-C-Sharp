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
    class Position : PetriObj
    {
        public int points;
        Color color;
        Font font;

        public Position()
        {
            points = 0;
        }

        public Point Create(int X, int Y)
        {
            if (name == null)
            {
                if (data.PList.Count > 0)
                    name = "P" +
                        (int.Parse(data.PList[data.PList.Count - 1].name.Remove(0, 1)) + 1).ToString();
                else
                    name = "P1";
            }

            color = Color.Black;
            font = new Font("Times New Roman", 12.0f);

            Point returnPoint = SetLocation(X, Y);

            return returnPoint;
        }

        public void Draw(Graphics field)
        {
            field.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Draw(field, color);
            field.DrawEllipse(new Pen(color, 2),
                location.X + global.hcell, location.Y + global.hcell, global.cell, global.cell);

            field.DrawString(name, font, new SolidBrush(color),
                new PointF(location.X + global.hcell, location.Y + 3 * global.hcell));
            field.DrawString(points.ToString(), font, new SolidBrush(color),
                location.X + global.hcell, location.Y + global.hcell);
        }
        
        public void AddPoints(Graphics field)
        {
            field.FillEllipse(new SolidBrush(Color.White),
                location.X + global.hcell, location.Y + global.hcell, global.cell, global.cell);
            field.DrawEllipse(new Pen(color, 2),
                location.X + global.hcell, location.Y + global.hcell, global.cell, global.cell);

            field.DrawString(points.ToString(), font, new SolidBrush(color),
                location.X + global.hcell, location.Y + global.hcell);
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
