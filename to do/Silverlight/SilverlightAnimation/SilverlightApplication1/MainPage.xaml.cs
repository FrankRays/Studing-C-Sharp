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

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            btnStart.Click += new RoutedEventHandler(btnStart_Click);
            btnStop.Click += new RoutedEventHandler(btnStop_Click);
            rectangle.MouseLeftButtonUp += new MouseButtonEventHandler(rectangle_MouseLeftButtonUp);
        }

        bool Paused = false;

        void rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Paused)
            {
                this.MoveRect.Resume();
                Paused = false;
            }
            else
            {
                this.MoveRect.Pause();
                Paused = true;
            }
        }

        void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.MoveRect.Stop();
        }

        void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.MoveRect.Begin();
        } 





    }
}
