using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleServerCS
{
    class SimpleServer
    {
        TcpListener _tcpListener;
        static List<Client> clients = new List<Client>();

        public SimpleServer(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            _tcpListener = new TcpListener(ip, port);
        }

        public void Start()
        {
            _tcpListener.Start();
            
            Console.WriteLine("Listening...");

            while (true)
            {
                Socket socket = _tcpListener.AcceptSocket();
                Client client = new Client(socket);
                clients.Add(client);
                client.Start();
            }
            
        }

        public void Stop()
        {
            foreach (Client c in clients)
            {
                c.Stop();
            }

            _tcpListener.Stop();
        }

        public static void SocketMethod(Client client)
        {
            try
            {
                Socket socket = client.Socket;

                string receivedMessage;
                NetworkStream stream = client.Stream;
                StreamReader reader = client.Reader;

                client.SendText(client, "Successfully Connected");

                while ((receivedMessage = reader.ReadLine()) != null)
                {
                    Console.WriteLine("[" + client.ID.GetHashCode().ToString() + "] " + receivedMessage);

                    foreach(Client c in clients)
                    {
                        c.SendText(client, receivedMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: " + e.Message);
            }
            finally
            {
                client.Stop();
            }
        }
    }
}
