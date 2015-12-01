using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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

        private Thread thread;

        public Client(Socket socket)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;

            Stream = new NetworkStream(Socket, true);
            Writer = new StreamWriter(Stream, Encoding.UTF8);
            Reader = new StreamReader(Stream, Encoding.UTF8);
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

        public void SendText(Client fromClient, string text)
        {
            if (!Socket.Connected)
                return;

            Writer.WriteLine(fromClient.ID.GetHashCode().ToString() + ": " + text);
            Writer.Flush();
        }
    }
}
