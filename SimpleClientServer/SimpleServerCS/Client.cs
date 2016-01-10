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
        public StreamReader Reader { get; private set; }
        public StreamWriter Writer { get; private set; }

        public BinaryReader bReader { get; private set; }

        public BinaryFormatter formatter { get; private set; }
        public BinaryWriter bWriter { get; private set; }

        private Thread thread;

        public Client(Socket socket)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;

            Stream = new NetworkStream(Socket, true);
            Writer = new StreamWriter(Stream, Encoding.UTF8);
            Reader = new StreamReader(Stream, Encoding.UTF8);
            bWriter = new BinaryWriter(Stream, Encoding.UTF8);
            bReader = new BinaryReader(Stream, Encoding.UTF8);
            formatter = new BinaryFormatter();

        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(SocketMethod));
            thread.Start();
        }

        public void Stop(bool abortThread = false)
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
        public void SendTextPacket(string text)
        {
            ChatMessagePacket chatMessagePacket = new ChatMessagePacket(text);
            Send(chatMessagePacket);
        }
    }
}
