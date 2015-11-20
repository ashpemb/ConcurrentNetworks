using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;

namespace SimpleServerCS
{
    class Client
    {
        public Socket Socket;
        public String ID;
        private Thread thread;
        public NetworkStream stream;
        public StreamReader reader;
        public StreamWriter writer;


        public Client(Socket socket)
        {
            ID = Guid.NewGuid().ToString();
            Socket = socket;
            stream = new NetworkStream(socket, true);
            reader = new StreamReader(stream, Encoding.UTF8);
            writer = new StreamWriter(stream, Encoding.UTF8);
        }

        

        public void Start()
        {
            thread = new Thread(new ThreadStart(SocketMethod));
            thread.Start();
        }

        public void SendText(string receivedMessage)
        {
            writer.WriteLine(receivedMessage);
            writer.Flush();
        }

        public void Stop()
        {
            Socket.Close();

            if (thread.IsAlive == true)
            {
                thread.Abort();
            }
        }

        private void SocketMethod()
        {
            SimpleServer.SocketMethod(this);
        }
    }
}
