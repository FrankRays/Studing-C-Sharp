using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadFileTest
{
    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        bool FileChoosed = false;
        string FileName = @"C:\com.cyberbane.abstract.appdf";
        
        private void InputFileName(string FileName)
        {
            IntPtr DialogHandle = FindWindow("#32770", "Выбор файла для выгрузки");//имя окна файлового диалога 
            if (DialogHandle == IntPtr.Zero)
            {
                MessageBox.Show("Dialog is not running.");
                return;
            }
            SetForegroundWindow(DialogHandle);
            SendKeys.Send(FileName);
            SendKeys.SendWait("{ENTER}");
        }
        
        private HtmlElement GetByName(string name)
        {
            foreach (HtmlElement he in webBrowser1.Document.GetElementsByTagName("input"))
            {
                if (he.Name == name)
                    return he;
            }
            return null;
        }

        private HtmlElement GetByValue(string value)
        {
            foreach (HtmlElement he in webBrowser1.Document.GetElementsByTagName("input"))
            {
                if (he.GetAttribute("value") == value)
                    return he;
            }
            return null;
        }

        private HtmlElement GetByClassName(string className)
        {
            foreach (HtmlElement he in webBrowser1.Document.All)
            {
                if (he.GetAttribute("className") == className)
                    return he;
            }
            return null;
        }

        private HtmlElement GetByInnerHtml(string html)
        {
            foreach (HtmlElement he in webBrowser1.Document.All)
            {
                if (he.InnerHtml == html)
                    return he;
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://apps.opera.com/administrator/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetByName("login").SetAttribute("value", "princeofdarkness@i.ua");
            GetByName("pwd").SetAttribute("value", "t9xH0gkse");
            GetByValue("Login").InvokeMember("click");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://apps.opera.com/administrator/mng_products2/?controller=products&action=appdf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetByClassName("upload-button-toggle").InvokeMember("click");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetByValue("Upload").InvokeMember("click");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            GetByName("Filedata").InvokeMember("click");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!FileChoosed)
            {
                Debug.WriteLine("InputFileName");
                InputFileName(FileName);
                FileChoosed = true;
            }
            else
            {
                timer1.Enabled = false;
                FileChoosed = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HtmlElement he = GetByInnerHtml("upload");
            Debug.WriteLine(he.OuterHtml);
            he.InvokeMember("click");
        }

    }
}
