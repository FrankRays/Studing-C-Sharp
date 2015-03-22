using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace butterfly
{
    public partial class MainPage : UserControl
    {
        double mousePositionX;
        double mousePositionY;
        Ellipse point;

        public MainPage()
        {
            InitializeComponent();
            SetCookie("key", "hello");
            text.Content = GetCookie("key");
        }

        private void move(object sender, MouseEventArgs e)
        {
            mousePositionX = e.GetPosition(null).X;
            mousePositionY = e.GetPosition(null).Y;
            point = new Ellipse();
            point.Width = 10;
            point.Height = 10;
            point.Fill = ellipse1.Fill;

            point.SetValue(Canvas.LeftProperty, mousePositionX - 5);
            point.SetValue(Canvas.TopProperty, mousePositionY - 5);
            canvas.Children.Add(point);
        }



        private void SetCookie(string key, string value)
        {
            // Expire in 7 days
            DateTime expireDate = DateTime.Now + TimeSpan.FromDays(7);
            string newCookie = key + "=" + value 
                               + ";expires=" + expireDate.ToString("R")
                               + ";domain=" + ".host";
            HtmlPage.Document.SetProperty("cookie", newCookie);
        }



        private string GetCookie(string key)
        {
            string[] cookies = HtmlPage.Document.Cookies.Split(';');
            foreach (string cookie in cookies)
            {
                string[] keyValue = cookie.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].ToString() == key)
                        return keyValue[1];
                }
            }
            return null;
        }

        public void DelCookie(string Name)
        {
            HtmlPage.Document.Cookies = Name + "=; expires=Fri, 31 Dec 1999 23:59:59 GMT;";
        }


        
    }
}
