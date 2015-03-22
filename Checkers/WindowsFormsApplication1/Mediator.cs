using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Mediator
    {
        static Mediator singlton;
        static List<string> history;

        Player blackplayer;
        Player whiteplayer;
        Field field;

        string currentPlayer;

        private Mediator()
        {
            history = new List<string>();
        }
        public static Mediator GetMediator()
        {
            if (singlton == null)
            {
                singlton = new Mediator();
            }
            return singlton;
        }
        public void RegBlackPlayer(Player player)
        {
            blackplayer = player;
        }
        public void RegWhitePlayer(Player player)
        {
            whiteplayer = player;
        }
        public void RegField(Field field)
        {
            this.field = field;
        }

        public void ClearCheckers()
        {
            if (blackplayer.Checkers != null)
                blackplayer.Checkers.Clear();
            if (whiteplayer.Checkers != null)
                whiteplayer.Checkers.Clear();
        }
        public void GiveChecker(Type player, int x, int y)
        {
            if (player == Type.Black)
                blackplayer.GiveChacker(field.GetCell(x, y));
            else
                whiteplayer.GiveChacker(field.GetCell(x, y));
        }
        public void ChangeCurrentPlayer(Player CurrentPlayer)
        {
            field.ClearSelections();
            bool HaveToKill = false;
            foreach (Checker c in CurrentPlayer.Checkers)
            {
                HaveToKill = HaveToKill ? true : MakePrediction(c);
                field.ClearSelections();
            }
            CurrentPlayer.SetStrategy(HaveToKill);
            currentPlayer = CurrentPlayer.Name;
        }
        public Cell GetCell(int x, int y)
        {
            return field.GetCell(x, y);
        }
        public List<Checker> AllCheckers
        {
            get
            {
                List<Checker> list = new List<Checker>();
                list.AddRange(blackplayer.Checkers);
                list.AddRange(whiteplayer.Checkers);
                return list;
            }
        }
        public List<Cell> SelectedCells
        {
            get
            {
                return field.GetSelectedCells();
            }
        }
        public bool MakePrediction(Checker sender)
        {
            Player owner = sender.Type == Type.Black ? blackplayer : whiteplayer;
            Player enemy = sender.Type == Type.Black ? whiteplayer : blackplayer;
            field.ClearSelections();

            if (sender is RealChecker)
            {
                bool rez = MakeRealPrediction(sender, owner, enemy);
                return rez;
            }
            if (sender is KingChecker)
            {
                return MakeKingPrediction(sender, owner, enemy);
            }
            return false;
        }
        private bool MakeRealPrediction(Checker sender, Player owner, Player enemy)
        {
            bool HaveToKill = owner.HaveToKill;
            foreach (Checker c in enemy.Checkers)
            {
                if (c.X + 1 == sender.X || c.X - 1 == sender.X)
                    if (c.Y + 1 == sender.Y || c.Y - 1 == sender.Y)
                    {
                        int dx = (c.X - sender.X) * 2;
                        int dy = (c.Y - sender.Y) * 2;
                        Cell scell = field.GetCell(sender.X + dx, sender.Y + dy);
                        if (scell != null && scell.IsEmpty)
                        {
                            HaveToKill = true;
                            scell.Selected = true;
                        }
                    }
            }

            if (!HaveToKill)
            {
                int y = (sender.Type == Type.White) ? sender.Y - 1 : sender.Y + 1;
                Cell tmp = field.GetCell(sender.X - 1, y);
                if (tmp != null && tmp.IsEmpty)
                    tmp.Selected = true;
                tmp = field.GetCell(sender.X + 1, y);
                if (tmp != null && tmp.IsEmpty)
                    tmp.Selected = true;
            }

            return HaveToKill;
        }
        private bool MakeKingPrediction(Checker sender, Player owner, Player enemy)
        {
            bool HaveToKill = owner.HaveToKill;
            for (int i = -1; i <= 1; i += 2)
                for (int j = -1; j <= 1; j += 2)
                {
                    int mx = sender.X + i;
                    int my = sender.Y + j;
                    bool was_enemy = false;
                    bool already_changed = false;
                    while (mx <= 8 && my <= 8 && mx >= 1 && my >= 1)
                    {
                        Cell c = field.GetCell(mx, my);
                        if (c.IsEmpty)
                        {
                            if (was_enemy)
                            {
                                HaveToKill = true;
                                c.Selected = true;
                            }
                            else
                            {
                                c.Selected = true;
                                mx += i;
                                my += j;
                                continue;
                            }
                            mx += i;
                            my += j;
                        }
                        else
                        {
                            if (enemy.IsMyCell(c) && !already_changed)
                            {
                                was_enemy = true;
                                already_changed = true;
                                mx += i;
                                my += j;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                }

            if (HaveToKill)
                for (int i = -1; i <= 1; i += 2)
                    for (int j = -1; j <= 1; j += 2)
                    {
                        int mx = sender.X + i;//8
                        int my = sender.Y + j;//3
                        bool was_enemy = false;
                        bool already_changed = false;
                        while (mx <= 8 && my <= 8 && mx >= 1 && my >= 1)
                        {
                            Cell c = field.GetCell(mx, my);
                            if (c.IsEmpty)
                            {
                                if (was_enemy)
                                {
                                    HaveToKill = true;
                                    c.Selected = true;
                                }
                                else
                                {
                                    c.Selected = false;
                                    mx += i;
                                    my += j;
                                    continue;
                                }
                                mx += i;
                                my += j;
                            }
                            else
                            {
                                if (enemy.IsMyCell(c) && !already_changed)
                                {
                                    was_enemy = true;
                                    already_changed = true;
                                    mx += i;
                                    my += j;
                                }
                                else
                                    break;
                            }
                        }
                    }
            return HaveToKill;
        }

        public string CurrentPlayerName
        {
            get
            {
                if (currentPlayer == null)
                    currentPlayer = whiteplayer.Name;
                return currentPlayer;
            }
        }
        public static void WriteTurnInfo(string info)
        {
            history.Add(info);
        }
        public static List<string> History
        {
            get
            {
                return history;
            }
        }
    }
}
