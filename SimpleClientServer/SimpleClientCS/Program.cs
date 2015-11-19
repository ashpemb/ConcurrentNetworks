using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleClient
{
    class Program
    {
        private const string hostname = "127.0.0.1";
        private const int port = 4444;

        public static void Main()
        {
            SimpleClient _client = new SimpleClient();
           
            if (_client.Connect(hostname, port))
            {
                Console.WriteLine("Connected...");

                try
                {
                    _client.Run();
                }
                catch (NotConnectedException e)
                {
                    Console.WriteLine("Client not Connected");
                }
            }
            else
            {
                Console.WriteLine("Failed to connect to: " + hostname + ":" + port);
            }
            Console.Read();
        }
    }
}
