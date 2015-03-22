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

namespace SilverlightTest
{
    public partial class MainPage : UserControl
    {
        ChatWindow cw;
        public MainPage()
        {
            InitializeComponent();
            LayoutRoot.Children.Add(new ChatWindow());
        }

        private void click(object sender, RoutedEventArgs e)
        {
            btn.Content += "-";
        }   
    }
}
