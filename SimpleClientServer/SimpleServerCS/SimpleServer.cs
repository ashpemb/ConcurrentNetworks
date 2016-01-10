using Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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

                NetworkStream stream = client.Stream;
                StreamReader reader = client.Reader;
                BinaryReader bReader = client.bReader;
                BinaryFormatter formatter = client.formatter;

                client.SendTextPacket("Successfully Connected");

                int noOfIncomingBytes;
                    while ((noOfIncomingBytes = bReader.ReadInt32()) != 0)
                    {
                        byte[] bytes = bReader.ReadBytes(noOfIncomingBytes);
                        MemoryStream memoryStream = new MemoryStream(bytes);
                        Packet packet = formatter.Deserialize(memoryStream) as Packet;
                        switch (packet.type)
                        {
                                case PacketType.CHATMESSAGE: 
                                string message = ((ChatMessagePacket) packet).chatMessage;
                                Console.WriteLine(message);
                                foreach (Client c in clients)
                                {
                                    if (c != null)
                                    {
                                        c.SendTextPacket(message);
                                    }
                                }
                                break;
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
