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
                Client client = new Client(socket, "User");
                clients.Add(client);
                client.Start();
            }

        }

        public void Stop()
        {
            foreach (Client c in clients)
            {
                //c.Stop();
                _tcpListener.Stop();
            }

            
        }



        public static void SocketMethod(Client client)
        {
            try
            {
                Socket socket = client.Socket;

                NetworkStream stream = client.Stream;
                BinaryReader bReader = client.bReader;
                
                
                client.SendTextPacket("Successfully Connected", null);

                int noOfIncomingBytes;
                    while ((noOfIncomingBytes = bReader.ReadInt32()) != 0)
                    {
                        byte[] bytes = bReader.ReadBytes(noOfIncomingBytes);
                        MemoryStream memoryStream = new MemoryStream(bytes);
                        BinaryFormatter formatter = new BinaryFormatter();
                        Packet packet = formatter.Deserialize(memoryStream) as Packet;
                        switch (packet.type)
                        {

                                case (PacketType.NICKNAME):
                                string name = ((NicknamePacket)packet).nickName;
                                
                                client.SetUsername(name);
                                string[] clientList = clients.ConvertAll(c=>c.userName).ToArray();
                                foreach (Client c in clients)
                                {
                                    if (c != null)
                                    {
                                        c.SendClientList(clientList);
                                    }
                                }

                                break;

                                case (PacketType.CHATMESSAGE):
                                string sender = ((ChatMessagePacket)packet).sender;
                                string message = sender + " : " + ((ChatMessagePacket) packet).chatMessage;
                                Console.WriteLine(message);
                                foreach (Client c in clients)
                                {
                                    if (c != null)
                                    {
                                        c.SendTextPacket(message, sender);
                                    }
                                }
                                break;

                            case (PacketType.SYSTEM):
                                SysPacket systemPacket = ((SysPacket)packet);
                                string sysMessage = systemPacket.message;
                                if (systemPacket.sysType.Equals(SystemType.DISCONNECTED))
                                {
                                    sysMessage = "[System] " + client.userName + " " + sysMessage;
                                    Client temp = null; 
                                    foreach (Client c in clients)
                                    {
                                        if ( c.GetUsername().Equals(systemPacket.sender))
                                        {
                                            temp = c;
                                        }
                                    }

                                    clients.Remove(temp);

                                    String[] clients2 = clients.ConvertAll(con => con.userName).ToArray();
                                    Console.WriteLine(sysMessage);

                                    foreach (Client c in clients)
                                    {
                                        c.SendTextPacket(c.GetUsername(), sysMessage);
                                        c.SendClientList(clients2);
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
