using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Art2
{
    public partial class MainPage : UserControl
    {
        HyperlinkButton pressed;
        string viewSource;

        public MainPage()
        {
            InitializeComponent();
            Load();
        }

        void Load()
        {
            hider.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Hider.To = Color.FromArgb(255, 255, 255, 255);

        //    #region Timer
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = new TimeSpan(0, 0, 1); //Tick each one second
        //    timer.Tick += new EventHandler(Update);
        //    timer.Start();
        //    #endregion
        //}
        //void Update(object sender, EventArgs e) { 
        }

        void ChangePage(object sender, EventArgs e) 
        {
            ContentFrame.Content = null;
            ContentFrame.Navigate(new Uri(viewSource, UriKind.Relative));
            hider.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            panel.IsHitTestVisible = true;
        }

        private void LinkClick(object sender, EventArgs e)
        {
            if (pressed != null)
                pressed.IsEnabled = true;
            pressed = (HyperlinkButton)sender;
            pressed.Width = pressed.ActualWidth;

            viewSource = "/Views/" + pressed.TargetName.ToString() + ".xaml"; 
            pressed.IsEnabled = false;
            panel.IsHitTestVisible = false;

            string[] argb = new string[4];

            argb = pressed.Tag.ToString().Split(';');

            Hider.To = Color.FromArgb(255, byte.Parse(argb[0]),
                                           byte.Parse(argb[1]),
                                           byte.Parse(argb[2]));
            HideContent.Begin();
        }
    }
}