using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    abstract class Strategy
    {
        public abstract bool MakeMove(Player owner, Player enemy, Checker checker, Cell cell);
    }

    class SimpleMove : Strategy
    {
        public override bool MakeMove(Player owner, Player enemy, Checker checker, Cell cell)
        {
            if (cell.Selected)
            {
                checker.MoveTo(cell);
                if (checker is RealChecker && ((checker.Type == Type.Black ? 8 : 1) == cell.Y))
                        owner.Transform(checker);
                
                return true;
            }
            return false;
        }
    }

    class KillOnly : Strategy
    {
        public override bool MakeMove(Player owner, Player enemy, Checker checker, Cell cell)
        {
            if (cell.Selected)
            {
                int dx = checker.X - cell.X;
                int dy = checker.Y - cell.Y;

                checker.MoveTo(cell);

                if (checker is RealChecker && ((checker.Type == Type.Black ? 8 : 1) == cell.Y))
                    owner.Transform(checker);

                int j = 0;
                int i = 0;
                Debug.WriteLine(dx+"-"+dy);
                while (i != dx && j != dy)
                {
                    foreach (Checker c in enemy.Checkers)
                        if (c.IsHere(cell.X + i ,cell.Y + j))
                        {
                            enemy.KillChacker(c);
                            break;
                        }
                    i += dx > 0 ? 1 : -1;
                    j += dy > 0 ? 1 : -1;
                }
                owner.SetStrategy(false);
                return true;
            }
            return false;
        }
    }
}
