using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    enum Type
    {
        Black,
        White
    }

    abstract class Checker
    {
        protected Cell cell;
        public virtual void MoveTo(Cell cell)
        {
            Mediator.WriteTurnInfo(this.Type.ToString() + " moveing from " + this.cell + " to " + cell);
            Debug.WriteLine("moveing from " + this.cell + " to " + cell);
            this.cell.IsEmpty = true;
            this.cell = cell;
            this.cell.IsEmpty = false;
        }
        public virtual bool IsHere(Cell cell)
        {
            return this.cell == cell;
        }
        public virtual bool IsHere(int x, int y)
        {
            return this.cell.X == x && this.cell.Y == y;
        }
        public abstract void Die();

        public abstract Image GetImage{ get; }
        public abstract Type Type { get; }
        public abstract bool Selected { get; set; }
        public abstract int X { get; }
        public abstract int Y { get; }
    }

    class RealChecker : Checker
    {
        private Type checkerType;
        private bool selected;

        public RealChecker(Cell cell, Type checkerType) 
        {
            cell.IsEmpty = false;
            this.cell = cell;
            this.checkerType = checkerType;
        }

        public override void Die() 
        {
            cell.IsEmpty = true;
        }

        public override Image GetImage
        {
            get
            {
                Image im;
                if(this.checkerType == Type.Black)
                    im = this.Selected ? Resources.black_s : Resources.black;
                else
                    im = this.Selected ? Resources.white_s : Resources.white;
                return im;
            }
        }
        public override Type Type
        { get { return this.checkerType; } }
        public override bool Selected
        { get { return selected; } set { selected = value; } }
        public override int X
        { get { return cell.X; } }
        public override int Y
        { get { return cell.Y; } }
    }

    class KingChecker : Checker 
    {
        RealChecker checker;
        public KingChecker(RealChecker checker)
        {
            this.checker = checker;
        }

        public override void MoveTo(Cell cell)
        {
            checker.MoveTo(cell);
        }
        public override bool IsHere(Cell cell)
        {
            return checker.IsHere(cell);
        }
        public override bool IsHere(int x, int y)
        {
            return checker.IsHere(x, y);
        }

        public override void Die()
        {
            checker.Die();
        }

        public override Image GetImage
        {
            get
            {
                Image im;
                if (checker.Type == Type.Black)
                    im = checker.Selected ? Resources.kblack_s : Resources.kblack;
                else
                    im = checker.Selected ? Resources.kwhite_s : Resources.kwhite;
                return im;
            }
        }
        public override Type Type
        { get { return checker.Type; } }
        public override bool Selected
        { get { return checker.Selected; } set { checker.Selected = value; } }
        public override int X
        { get { return checker.X; } }
        public override int Y
        { get { return checker.Y; } }
    }

}
