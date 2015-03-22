using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Art2
{

    public class Class1
    {
        enum state
        {
            center,
            left,
            longleft,
            right,
            longright,
            invisible
        }

        Image img;
        TextBlock text;
        


        public Class1(ImageSource Img, string name)
        {
            img.Source = Img;
            text.Text = name;

            img.Opacity = 0;
            text.Opacity = 0;
        }







    }
}
