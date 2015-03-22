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
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Text;

namespace SilverlightTest
{
    public class ChatEntry : Grid
    {
        int id;// по идее от него зависит имя и ава)
        Image img; //ава
        Label name; // имя
        TextBlock msg;//сообщение

        public ChatEntry(int ID, string message)
        {
            id = ID;
            img = new Image();
            name = new Label();
            msg = new TextBlock();
            msg.Text = message;
            name.Content = (ID == 1)?"Я":"вася";


            buil_grid(); // делаем все красиво(или не очень), разметка и прочее
        }


        void buil_grid()
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(60);
            RowDefinition rd = new RowDefinition();
            rd.MaxHeight = 30;
            RowDefinitions.Add(rd);
            RowDefinitions.Add(new RowDefinition());
            ColumnDefinitions.Add(cd);
            ColumnDefinitions.Add(new ColumnDefinition());
            Width = 270;
        
            //////////////
            //img //name//
            //<60>////////
            //img //msg //
            //<---270-->//

            img.Source = new BitmapImage(new Uri("Images/" + id + ".jpg", UriKind.Relative));
            img.Height = 60;
            img.Width = 60;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRowSpan(img, 2);

            name.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(name, 1);

            msg.Width = 200;
            msg.TextWrapping = TextWrapping.Wrap;// текст переходит на новую линию
            Grid.SetColumn(msg, 1);
            Grid.SetRow(msg, 1);

            Children.Add(img);
            Children.Add(name);
            Children.Add(msg);
        }


    }
}
