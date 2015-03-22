using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace SilverlightTest
{
    public partial class ChatWindow : FloatableWindow
    {
        
        public ChatWindow()
        {
            InitializeComponent();

            Title = new Random().Next(100, 999).ToString();
            Timer t = new Timer(update_chat, new AutoResetEvent(true), 1000, 10000);
            msg.Focus();
        }

        void update_chat(Object stateInfo)
        {
             this.Dispatcher.BeginInvoke(() =>
                {
                    ChatEntry chatEntry = new ChatEntry(2, "тролололо");

                    listBox1.Items.Add(chatEntry);
                    listBox1.ScrollIntoView(listBox1.Items.Last());
                });
            
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            send_msg();
        }

        private void Check_if_send(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) send_msg();
        }

        private void send_msg()
        {
            //добавляем новое сообщение в чат лист, в будущем будет 
            //передаваться сообщение написанное юзером, его ава, и "Я" в качестве имени
            //по идее так)
            if (msg.Text == "") return;
            ChatEntry chatEntry = new ChatEntry(1, msg.Text);
            
            listBox1.Items.Add(chatEntry);
            listBox1.ScrollIntoView(listBox1.Items.Last());
            msg.Text = "";
        }

        Point last; bool hiden = false;
        private void turn(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!hiden)
            {
                last.Y = Height;
                Height = 20;
                last.X = Width;
                Width = 100;
                hiden = !hiden;
            }
            else
            {
                Height = last.Y;
                Width = last.X;
                hiden = !hiden;
            }
        }



    }
}
