using System.Net.Sockets;
using System.IO;
using System;

namespace MessengerServer
{
    public class MessengerConnection
    {
        private Socket Client;
        private string ID;
        private MessengerServerClass MServer;

        public MessengerConnection(Socket Client, string ID,
        MessengerServerClass server)
        {
            this.Client = Client;
            this.ID = ID;
            this.MServer = server;
        }
        private byte[] Message = new byte[1024];
        public void Start()
        {
            try
            {
                // Listen for messages
                Client.BeginReceive(Message, 0, Message.Length, SocketFlags.None,
                new AsyncCallback(OnMsgReceived), null);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }
        public void OnMsgReceived(IAsyncResult ar)
        {
            try
            {
                int bytesRead = Client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    //Send message to all connected clients
                    MServer.DeliverMessage(Message, bytesRead);
                    // Listen for next message
                    Client.BeginReceive(Message, 0, Message.Length,
                    SocketFlags.None, new AsyncCallback(OnMsgReceived), null);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        public void Close()
        {
        }
        public void ReceiveMessage(byte[] data, int bytesRead)
        {
            Client.Send(data, 0, bytesRead, SocketFlags.None);
        }
    }
}