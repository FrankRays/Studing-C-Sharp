using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Player
    {
        Strategy strategy;
        string name;
        Type checkerType;
        public List<Checker> Checkers;
        Mediator mediator;
        Factory CheckerFactory;

        public Player(string name, Type checkerType)
        {
            mediator = Mediator.GetMediator();
            if (checkerType == Type.Black)
            {
                mediator.RegBlackPlayer(this);
                CheckerFactory = new Black_Factory();
            }
            else
            {
                mediator.RegWhitePlayer(this);
                CheckerFactory = new White_Factory();
            }

            this.name = name;
            SetStrategy(false);
            this.checkerType = checkerType;
        }

        public void SetStrategy(bool HaveToKill)
        {
            if (HaveToKill)
            {
                Debug.WriteLine(checkerType.ToString() + " HaveToKill");
                strategy = new KillOnly();
            }
            else
            {
                strategy = new SimpleMove();
            }
        }

        public bool MakeMove(Player enemy, Checker checker, Cell cell)
        {
            bool hadkill = strategy is KillOnly;
            bool rez = strategy.MakeMove(this, enemy, checker, cell);
            bool HaveToKill = mediator.MakePrediction(checker);
            
            if (hadkill)
            {
                if (HaveToKill)
                    strategy = new KillOnly();
                return !HaveToKill;
            }
            else
            {
                return rez;
            }
        }

        public void GiveChacker(Cell cell)
        { 
            if(Checkers == null)
                Checkers = new List<Checker>();

            Checkers.Add(CheckerFactory.CreateChecker(cell));
        }

        public bool KillChacker(Checker checker)
        {
            Checkers.Remove(checker);
            checker.Die();
                    
            if (Checkers.Count == 0)
                return true;
            else
                return false;
        }

        public void Transform(Checker checker)
        {
            Debug.WriteLine("Transform");
            Checkers.Remove(checker);
            Checkers.Add(new KingChecker((RealChecker)checker));
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool IsMyCell(Cell cell)
        {
            foreach (Checker c in Checkers)
                if (c.IsHere(cell))
                    return true;
            return false;
        }

        public bool HaveToKill
        {
            get
            {
                return strategy is KillOnly;
            }
        }

        public Type Type
        {
            get
            {
                return checkerType;
            }
        }
    }

    //Abstract factory
    abstract class Factory
    {
        public abstract Checker CreateChecker(Cell cell);
    }

    class Black_Factory : Factory
    {
        public override Checker CreateChecker(Cell cell)
        {
            return new RealChecker(cell, Type.Black);
        }
    }

    class White_Factory : Factory
    {
        public override Checker CreateChecker(Cell cell)
        {
            return new RealChecker(cell, Type.White);
        }
    }
}
