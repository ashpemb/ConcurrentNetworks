using Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleServerCS
{
    public class Client
    {
        public Socket Socket { get; private set; }
        public string ID { get; private set; }
        public NetworkStream Stream { get; private set; }
        public BinaryReader bReader { get; private set; }

        public BinaryFormatter formatter { get; private set; }
        public BinaryWriter bWriter { get; private set; }

        private Thread thread;
        public string userName;
        public Client(Socket socket, string name)
        {
            ID = Guid.NewGuid().ToString();
            userName = name;
            Socket = socket;

            Stream = new NetworkStream(Socket, true);
            bWriter = new BinaryWriter(Stream, Encoding.UTF8);
            bReader = new BinaryReader(Stream, Encoding.UTF8);
            

            Console.WriteLine("Client Connected");           
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(this.SocketMethod));
            thread.Start();
        }

        public void Stop()
        {
            Socket.Close();

            if (thread.IsAlive)
                thread.Abort();
        }
        private void SocketMethod()
        {
            SimpleServer.SocketMethod(this);
        }

        public void Send(Packet data)
        {
            MemoryStream mem = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(mem, data);
            byte[] buffer = mem.GetBuffer();

            bWriter.Write(buffer.Length);
            bWriter.Write(buffer);
            bWriter.Flush();
        }
        public void SendTextPacket(string text, string sender)
        {
            ChatMessagePacket chatMessagePacket = new ChatMessagePacket(text, sender);
            Send(chatMessagePacket);
        }

        public void SetUsername(string name)
        {
            userName = name;
        }

        public string GetUsername()
        {
            return userName;
        }

        public void SendClientList(string[] clients)
        {
            ClientList clientList = new ClientList(clients);
            Send(clientList);
        }
    }
}
