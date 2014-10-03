using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notebook
{
    public partial class Form1 : Form
    {
        bool saved = true;
        string filename;
        CLPBRD clpbrd;

        public Form1()
        {
            Clipboard.SetDataObject("\0");
            InitializeComponent();
            richTextBox1.Focus();
            clpbrd = new CLPBRD();
            clpbrd.listBox1.DoubleClick += вставкаToolStripMenuItem_Click;

            statusStrip1.Items[0].Text = "строка "
                + (richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1).ToString()
                + " столбец "
                + (richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine()).ToString()
                + "                ";

            statusStrip1.Items[1].Text = DateTime.Now.ToString("HH:mm:ss");
        }

        DialogResult msg()
        {
            return MessageBox.Show("Документ не сохранен. Сохранить?",
                    "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, 
                    MessageBoxDefaultButton.Button1);
                
        }

        void save()
        {
            if (filename == null)
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    filename = saveFileDialog1.FileName;
                else
                    return;
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(richTextBox1.Text);
            sw.Close();
            saved = true;
        }

        void open()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                if ((filename = openFileDialog1.FileName) != null)
                {
                    richTextBox1.Text = new StreamReader(filename).ReadToEnd();
                    new StreamReader(filename).Close();
                }
                else ;
            else
                return;
            
            saved = true; 
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                richTextBox1.Clear();
                saved = true;
                filename = null;
            }
            else
            {
                switch (msg())
                {
                    case DialogResult.No: richTextBox1.Clear(); saved = true; break;
                    case DialogResult.Cancel: break;
                    case DialogResult.Yes: save(); break;
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved) open();
            else
            {
                switch (msg())
                {
                    case DialogResult.No: open(); break;
                    case DialogResult.Cancel: break;
                    case DialogResult.Yes: save(); break;
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = null;
            save();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
                switch (msg())
                {
                    case DialogResult.No: break;
                    case DialogResult.Cancel: e.Cancel = true; break;
                    case DialogResult.Yes: save(); break;
                }
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void отменадействияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void выделитьвсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void переносСловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = !richTextBox1.WordWrap;
        }

        private void буферОбменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clpbrd.Show();
        }

        private void tick(object sender, EventArgs e)
        {
            statusStrip1.Items[0].Text = "сторка "
                + (richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1).ToString()
                + " столбец "
                + (richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine()).ToString()
                + "";

            statusStrip1.Items[1].Text = DateTime.Now.ToString("HH:mm:ss");

            string current = (string)Clipboard.GetDataObject().GetData(DataFormats.Text);
            if (current == null) return;
            if (!clpbrd.listBox1.Items.Contains(current))
                clpbrd.listBox1.Items.Add(current);
        }

        private void найтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FindWindow(richTextBox1)).Show();
        }

        private void UnSave(object sender, EventArgs e)
        {
            saved = false;
        }
    }
}
