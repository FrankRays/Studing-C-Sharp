using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerServer;

namespace MSGServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MessengerServerClass msc = new MessengerServerClass();
            msc.Start();
            Console.WriteLine("started");
            Console.ReadKey();
            msc.Close();
        }
    }
}
