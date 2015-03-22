using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;

namespace MessengerServer
{
    public class MessengerServerClass
    {
        private Socket Listener;
        private int ClientNo;
        private List<MessengerConnection> Clients = new
        List<MessengerConnection>();
        private bool isRunning;
        public void Start()
        {
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listener.SetSocketOption(SocketOptionLevel.Tcp,(SocketOptionName)SocketOptionName.NoDelay, 0);
            // The allowed port range in Silverlight is 4502 to 4534.
            Listener.Bind(new IPEndPoint(IPAddress.Any, 4530));
            // Waiting on connection request
            Listener.Listen(10);
            Listener.BeginAccept(new AsyncCallback(OnConnection), null);
            isRunning = true;
        }
        private void OnConnection(IAsyncResult ar)
        {
            if (isRunning == false) return;
            ClientNo++;
            // Look for other connections
            Listener.BeginAccept(new AsyncCallback(OnConnection), null);
            Console.WriteLine("Messenger client No: " +
            ClientNo.ToString() + " is connected.");
            Socket Client = Listener.EndAccept(ar);
            // Handle the current connection 
            MessengerConnection NewClient = new
            MessengerConnection(Client, "Client " +
            ClientNo.ToString(), this);
            NewClient.Start();
            lock (Clients)
            {
                Clients.Add(NewClient);
            }
        }
        public void Close()
        {
        }
        public void DeliverMessage(byte[] message, int bytesRead)
        {
            Console.WriteLine("Delivering the message...");
            // Duplication of connection to prevent cross-threading issues
            MessengerConnection[] ClientsConnected;
            lock (Clients)
            {
                ClientsConnected = Clients.ToArray();
            }
            foreach (MessengerConnection cnt in ClientsConnected)
            {
                try
                {
                    cnt.ReceiveMessage(message, bytesRead);
                }
                catch
                {
                    // Remove disconnected clients
                    lock (Clients)
                    {
                        Clients.Remove(cnt);
                    }
                    cnt.Close();
                }
            }
        }
    }
}