using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        GameEngine gameEngine;
        Graphics GraphicsField;

        public Form1()
        {
            InitializeComponent();
            GraphicsField = pictureBox1.CreateGraphics();
        }

        private void Click(object sender, MouseEventArgs e)
        {
            if (gameEngine != null)
                gameEngine.FieldClick(e);
            pictureBox1.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameEngine = new AdapterFormAndGame(
                new Player("player 1", Type.White), 
                new Player("player 2", Type.Black), GraphicsField, listBox1, label2);
            gameEngine.Begin();
            pictureBox1.Invalidate();
        }

        private void paint(object sender, PaintEventArgs e)
        {
            if (gameEngine != null)
                gameEngine.DrawField(e);            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gameEngine != null)
                gameEngine.ConsoleCmd("save");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
                gameEngine = new AdapterFormAndGame(
                new Player("player 1", Type.White),
                new Player("player 2", Type.Black), GraphicsField, listBox1, label2);
            gameEngine.ConsoleCmd("load");
            pictureBox1.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
