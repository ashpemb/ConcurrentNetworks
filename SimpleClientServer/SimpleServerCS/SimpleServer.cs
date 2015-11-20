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
        static List<Client> _clients = new List<Client>();

        public SimpleServer(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            _tcpListener = new TcpListener(ip, port);
        }

        public void Start()
        {
            _tcpListener.Start();
            
            Console.WriteLine("Listening...");
            while(true)
            { 
                Socket socket = _tcpListener.AcceptSocket();
                Console.WriteLine("Connection Made");
                Client client = new Client(socket);
                _clients.Add(client);
                client.Start();
            }
        }

        public void Stop()
        {
            foreach(Client client in _clients)
            {
                _tcpListener.Stop();
            }
        }

        public static void SocketMethod(Client client)
        {
            try
            {
                Socket socket = client.Socket;

                string receivedMessage;
               

                client.writer.WriteLine("Send 0 for available options");
                client.writer.Flush();

                while ((receivedMessage = client.reader.ReadLine()) != null)
                {
                    foreach (Client c in _clients)
                    {
                        c.SendText(receivedMessage);
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
