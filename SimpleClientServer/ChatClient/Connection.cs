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

namespace ChatClient
{
    public class Connection
    {
        private Form1 _form;
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private StreamReader _reader;
        private Thread _thread;
        public bool Connect(Form1 form, string hostname, int port)
        {
            try
            {
                _form = form;
                _tcpClient = new TcpClient();
                _tcpClient.Connect(hostname, port);
                _stream = _tcpClient.GetStream();
                _writer = new StreamWriter(_stream, Encoding.UTF8);
                _reader = new StreamReader(_stream, Encoding.UTF8);

                _thread = new Thread(new ThreadStart(ProcessServerResponse));
                _thread.Start();
            }
            catch (Exception e)
            {
                OutputText("Exception: " + e.Message);
                return false;
            }

            return true;
        }

        public void Disconnect()
        {
            try
            {
                _reader.Close();
                _writer.Close();
                _tcpClient.Close();
                _thread.Abort();
            }
            catch {}

            OutputText("Disconnected");
        }
        public void SendText(string text)
        {
            if (!_tcpClient.Connected)
                return;

            _writer.WriteLine(text);
            _writer.Flush();
        }

        private void ProcessServerResponse()
        {
            try
            {
                while (_tcpClient.Connected)
                {
                    string text = _reader.ReadLine();
                    if (text.Length > 0)
                        OutputText(text);
                }
            }
            catch { }
        }

        private delegate void AppendTextDelegate(string str);
        private void OutputText(string text)
        {
            _form.Invoke(new AppendTextDelegate(_form.AppendText), new object[] { text });
        }

        public void SendPacket(Packet data)
        {
            MemoryStream mem = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(mem, data);
            byte[] buffer = mem.GetBuffer();

            _writer.Write(buffer.Length);
            _writer.Write(buffer);
            _writer.Flush();
        }

        public void SendText(string text)
        {
            if (!_tcpClient.Connected)
                return;

            ChatMessagePacket chatMessagePacket = new ChatMessagePacket(text);
            SendPacket(chatMessagePacket);
        }

        private void SetNickname(string nickname)
        {
            NicknamePacket chatMessagePacket = new NicknamePacket(nickname);
            SendPacket(chatMessagePacket);
        }

    }
}
