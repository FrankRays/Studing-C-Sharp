using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    class GameEngine
    {
        protected Mediator mediator;
        protected bool started;

        protected Player CurrentPlayer;
        Player CurrentEnemy;
        Checker SelectedChecker;

        public GameEngine(Player player1, Player player2) 
        {
            mediator = Mediator.GetMediator();
            if (player1.Type == Type.Black)
            {
                CurrentPlayer = player2;
                CurrentEnemy = player1;
            }
            else
            {
                CurrentPlayer = player1;
                CurrentEnemy = player2;
            }
            started = false;
        }
        
        public virtual object ConsoleCmd(string cmd)
        {

            return null;
        }

        public virtual void Begin()
        {
            mediator.RegField(new Field());
            mediator.ClearCheckers();
            for (int i = 1; i <= 3; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j <= 8; j += 2)
                    {
                        mediator.GiveChecker(Type.Black, j, i);
                    }
                    for (int j = 2; j <= 8; j += 2)
                    {
                        mediator.GiveChecker(Type.White, j, 8 - i + 1);
                    }
                }
                else
                {
                    for (int j = 2; j <= 8; j += 2)
                    {
                        mediator.GiveChecker(Type.Black, j, i);
                    }
                    for (int j = 1; j <= 8; j += 2)
                    {
                        mediator.GiveChecker(Type.White, j, 8 - i + 1);
                    }
                }
            }
            started = true;
        }

        public virtual void FieldClick(object xy)
        {
            if (started)
            {
                Point p = (Point)xy;
                Cell ClickedCell = mediator.GetCell(p.X, p.Y);
                if (SelectedChecker == null && ClickedCell.IsEmpty)
                    return;
                Debug.WriteLine("ba dum s " + ClickedCell.IsEmpty);
                if (!ClickedCell.IsEmpty)
                {

                    foreach (Checker c in CurrentPlayer.Checkers)
                    {
                        Debug.WriteLine(c.GetType().ToString());
                        if (c.IsHere(ClickedCell))
                        {
                            if (SelectedChecker != null)
                                SelectedChecker.Selected = false;
                            SelectedChecker = c;
                            SelectedChecker.Selected = true;
                            mediator.MakePrediction(SelectedChecker);
                        }
                    }
                }
                else if (SelectedChecker != null)
                {
                    if (CurrentPlayer.MakeMove(CurrentEnemy, SelectedChecker, ClickedCell))
                    {
                        if (CurrentEnemy.Checkers.Count == 0)
                        {
                            started = false;
                            return;
                        }
                        ChangeCurrentPlayer();
                    }
                }
            }
        }
        
        public virtual void DrawField(object info)
        {
        }

        public virtual void ChangeCurrentPlayer()
        {
            Player tmp = CurrentPlayer;
            CurrentPlayer = CurrentEnemy;
            CurrentEnemy = tmp;

            SelectedChecker.Selected = false;
            SelectedChecker = null;
            mediator.ChangeCurrentPlayer(CurrentPlayer);
        }
    }

    class AdapterFormAndGame : GameEngine
    {
        Adaptee adap;

        public AdapterFormAndGame(Player player1, Player player2, Graphics GraphicsField, ListBox lb, Label l)
            : base(player1, player2)
        {
            adap = new Adaptee(GraphicsField, lb,l);
        }

        public override void Begin()
        {
            base.Begin();
            adap.DrawBegin(mediator.AllCheckers);
        }

        public override object ConsoleCmd(string cmd)
        {
            if (cmd == "save")
                adap.SaveHistory();
            else if (cmd == "load")
            {
                List<string> moves = adap.LoadGame();
                if (moves == null)
                    return null;
                base.Begin();
                foreach (string move in moves)
                {
                    string str = move.Substring(move.IndexOf('(')+1, 1);
                    int x = int.Parse(move.Substring(move.IndexOf('(') + 1, 1));
                    int y = int.Parse(move.Substring(move.IndexOf(';') + 1, 1));
                    base.FieldClick(new Point(x, y));
                    x = int.Parse(move.Substring(move.LastIndexOf('(') + 1, 1));
                    y = int.Parse(move.Substring(move.LastIndexOf(';') + 1, 1));
                    base.FieldClick(new Point(x, y));
                }
            }
            
            return null;
        }

        public override void FieldClick(object info)
        {
            if (started)
            {
                Point p = adap.FieldClick(info as MouseEventArgs);
                base.FieldClick(p);
                if (!base.started)
                {
                    adap.FinishGame(base.CurrentPlayer.Name);
                }
                adap.MoveDone();
            }
        }

        public override void DrawField(object info)
        {
            if (started)
            {
                base.DrawField(info);
                adap.ReDraw(info as PaintEventArgs, mediator.AllCheckers, mediator.SelectedCells);
            }
        }
    }

    class Adaptee
    {
        Graphics GF;
        int Width, Height;
        Image board;
        Image cell;
        ListBox history;
        Label CurrentPlayer;

        public Adaptee(Graphics GraphicsField, ListBox lb, Label l)
        {
            GF = GraphicsField;
            Height = Width = 431;
            cell = Resources.cell;
            board = Resources.board;
            history = lb;
            CurrentPlayer = l;
        }

        public void DrawBegin(List<Checker> list)
        {
            Mediator.History.Clear();
            history.Items.Clear();
            GF.DrawImage(board, 0, 0);

            foreach (Checker c in list)
            {
                GF.DrawImage(c.GetImage, RealXY(c.X, c.Y));
            }

            Debug.WriteLine("DrawBegin");
            CurrentPlayer.Text = Mediator.GetMediator().CurrentPlayerName;

        }

        private Point RealXY(int x, int y)
        {
            return new Point((x-1)*41, (y-1)*41);
        }

        public Point FieldClick(MouseEventArgs e)
        {
            Debug.WriteLine((e.X / 41 + 1).ToString() + "*" + (e.Y / 41 + 1).ToString());
            int x = e.X / 41 + 1;
            x = x > 8 ? 8 : x;
            x = x < 1 ? 1 : x;
            int y = e.Y / 41 + 1;
            y = y > 8 ? 8 : y;
            y = y < 1 ? 1 : y;
            return new Point(x, y);
        }

        public void MoveDone()
        {
            history.Items.Clear();
            history.Items.AddRange(Mediator.History.ToArray());

            CurrentPlayer.Text = Mediator.GetMediator().CurrentPlayerName;
        }

        public void ReDraw(PaintEventArgs e, List<Checker> list, List<Cell> field)
        {
            Bitmap g = new Bitmap(Width, Height, e.Graphics);
            Graphics gr = Graphics.FromImage(g);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //draw on gr
            foreach (Checker c in list)
            {
                    gr.DrawImage(c.GetImage, RealXY(c.X, c.Y));
            }

            foreach (Cell c in field)
            {
                gr.DrawImage(cell, RealXY(c.X, c.Y));
            }

            e.Graphics.DrawImageUnscaled(g, 0, 0);
            gr.Dispose();
        }

        public void FinishGame(string name)
        {
            DialogResult dr = MessageBox.Show("Winner: " + name + " !" + Environment.NewLine +"Save game history?", "Congratulations", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                SaveHistory();
            }
        }

        public void SaveHistory()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Game history files (*.checkers)|*.checkers";
            DialogResult dr_sfd = sfd.ShowDialog();
            if (dr_sfd == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.OpenFile());
                foreach (string s in Mediator.History)
                    sw.WriteLine(s);
                sw.Close();
                MessageBox.Show("Saved");
            }
        }

        public List<string> LoadGame()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Game history files (*.checkers)|*.checkers";
            DialogResult dr_sfd = ofd.ShowDialog();
            List<string> moves = null; 
            if (dr_sfd == DialogResult.OK)
            {
                moves = new List<string>();
                history.Items.Clear();
                Mediator.History.Clear();
                history.Enabled = true;
                StreamReader sr = new StreamReader(ofd.OpenFile());
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    history.Items.Add(str);
                    moves.Add(str);
                }
                MessageBox.Show("opened");
            }

            return moves;
        }
    }
}
