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
        private BinaryWriter bWriter;
        private BinaryReader bReader;
        private BinaryFormatter formatter;
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
                bWriter = new BinaryWriter(_stream, Encoding.UTF8);
                bReader = new BinaryReader(_stream, Encoding.UTF8);
                formatter = new BinaryFormatter();

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
                bReader.Close();
                bWriter.Close();
                
            }
            catch {}

            OutputText("Disconnected");
        }

        private void ProcessServerResponse()
        {
            try
            {
                while (_tcpClient.Connected)
                {
                    int noOfIncomingBytes;
                    while ((noOfIncomingBytes = bReader.ReadInt32()) != 0)
                    {
                        byte[] bytes = bReader.ReadBytes(noOfIncomingBytes);
                        MemoryStream memoryStream = new MemoryStream(bytes);
                        Packet packet = formatter.Deserialize(memoryStream) as Packet;
                        switch (packet.type)
                        {
                                case PacketType.CHATMESSAGE: 
                                string message = ((ChatMessagePacket)packet).chatMessage;

                                OutputText(message);
                                   
                                
                                break;
                        }
                    }
                }
            }
            catch { }
        }

        private delegate void AppendTextDelegate(string str);
        private void OutputText(string text)
        {
            _form.Invoke(new AppendTextDelegate(_form.AppendText), new object[] { text });
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

        private void SetNickname(string nickname)
        {
            NicknamePacket chatMessagePacket = new NicknamePacket(nickname);
            Send(chatMessagePacket);
        }

    }
}
