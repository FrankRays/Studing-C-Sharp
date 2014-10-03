using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri2
{
    [Serializable()]
    class Arrow
    {
        List<Line> path;
        Line Path;
        Line left, right;
        Color color;

        public PetriObj Of, In;

        public Arrow()
        {
            left = new Line();
            right = new Line();
        }

        public void SetFromPoint(Point p, Color c)
        {
            color = c;
            
            Path = new Line(p, p);
            ChangeDirection();
        }

        public void SetToPoint(Point p)
        {
            Path.End = p;
            ChangeDirection();
        }

        void ChangeDirection()
        {
            double angle = 0;
            int l = global.hcell;
            double d = Math.PI;
            Line main = Path;

            #region left
            left.End = main.End;
            

            angle = main.End.X - main.Start.X;

            if (angle > 0) d = 0;
            if ((main.End.Y - main.Start.Y) != 0) angle /= main.End.Y - main.Start.Y;

            angle = Math.Atan(1 / angle) + d;
            angle += 15 * Math.PI / 180;

            left.Start.X = (int)(left.End.X - l * Math.Cos(angle));
            left.Start.Y = (int)(left.End.Y - l * Math.Sin(angle));
            #endregion

            #region right
            right.End = main.End;

            angle -= 30 * Math.PI / 180;
            right.Start.X = (int)(right.End.X - l * Math.Cos(angle));
            right.Start.Y = (int)(right.End.Y - l * Math.Sin(angle));
            #endregion
        }

        public void Draw(Graphics field)
        {
            Path.Draw(field, color);
            left.Draw(field, color);
            right.Draw(field, color);
        }

    }

    [Serializable()]
    class Line
    {
        public Point Start, End;

        public Line() { }
        public Line(int x1, int y1, int x2, int y2)
        {
            Start = new Point(x1, y1);
            End = new Point(x2, y2);
        }
        
        public Line(Point s, Point e)
        {
            Start = s;
            End = e;
        }

        public void Draw(Graphics field, Color c)
        {
            field.DrawLine(new Pen(c, 2), Start, End);
        }

        public void ReDraw(Graphics field, Color c)
        {
 
        }
    }
}
