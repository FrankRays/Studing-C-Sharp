using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Cell
    {
        int x, y;
        bool isEmpty;

        public bool Selected;

        public Cell(int X, int Y)
        {
            this.x = X;
            this.y = Y;
            isEmpty = true;
            Selected = false;
        }
        
        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }
    
        public bool IsHere(int x, int y)
        {
            return this.x == x && this.y == y;
        }

        public bool IsEmpty
        { 
            get {return isEmpty;}
            set { isEmpty = value; }
        }

        public override string ToString()
        {
            return "("+x+";"+y+")";
        }
    }

    //Lazy initialization
    class Field
    {
        List<Cell> Cells;

        public void ClearSelections()
        {
            foreach (Cell c in Cells)
                c.Selected = false;
        }
        public Field()
        {
            Cells = new List<Cell>();
        }

        public Cell GetCell(int x, int y)
        {
            foreach (Cell c in Cells)
                if (c.IsHere(x, y))
                    return c;

            //lazy initialization
            Cell cell = null;
            if (x >= 1 && x <= 8 && y >= 1 && y <= 8)
            {
                cell = new Cell(x, y);
                Cells.Add(cell);
            }
            return cell;
        }

        public List<Cell> GetSelectedCells()
        {
            List<Cell> list = new List<Cell>();
            foreach (Cell c in Cells)
                if (c.Selected)
                    list.Add(c);
            return list;
        }
    }
}
