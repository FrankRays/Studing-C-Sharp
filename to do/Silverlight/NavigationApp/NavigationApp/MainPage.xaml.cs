﻿using System;
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

namespace NavigationApp
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LinkClick(object sender, RoutedEventArgs e)
        {
            HyperlinkButton button = (HyperlinkButton)sender;
            string viewSource = button.Tag.ToString();
            ContentFrame.Navigate(new Uri(viewSource, UriKind.Relative));
        }   


    }
}
