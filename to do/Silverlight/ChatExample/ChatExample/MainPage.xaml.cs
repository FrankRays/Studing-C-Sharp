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
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace ChatExample
{
    public partial class MainPage : UserControl
    {
        // The MSocket for the connection
        private Socket MSocket;

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((MSocket != null) && (MSocket.Connected == true))
                    MSocket.Close();
            }
            catch (Exception err)
            {
                AddMessage("ERROR: " + err.Message);
            }
            DnsEndPoint endPoint = new DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4530);
            MSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            SocketAsyncEventArgs SocketArgs = new SocketAsyncEventArgs();
            SocketArgs.UserToken = MSocket;
            SocketArgs.RemoteEndPoint = endPoint;
            SocketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketArgs_Completed);
            MSocket.ConnectAsync(SocketArgs);
        }

        private void AddMessage(string message)
        {
            //Separate thread 
            Dispatcher.BeginInvoke(
            delegate()
            {
                Messages.Text += message + "\n";
                Scroller.ScrollToVerticalOffset(Scroller.ScrollableHeight);
            });
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SocketAsyncEventArgs Args = new SocketAsyncEventArgs();
            // Prepare the message.
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, new Message(txtMessage.Text, txtName.Text));
            byte[] messageData = ms.ToArray();
            List<ArraySegment<byte>> bufferList = new List<ArraySegment<byte>>();
            bufferList.Add(new ArraySegment<byte>(messageData));
            Args.BufferList = bufferList;
            // Send the message.
            MSocket.SendAsync(Args);
            //clear the text box
            txtMessage.Text = string.Empty;
        }

        private void SocketArgs_Completed(object sender, EventArgs e)
        {
            txt.Text += "1";
        }
    }
}



