using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolicyServer;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketPolicyServer sps = new SocketPolicyServer("SocketClientAccessPolicy.xml");
            Console.WriteLine("started");
            Console.ReadKey();
            sps.Close();
        }
    }
}
