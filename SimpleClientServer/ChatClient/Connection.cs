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
        private BinaryWriter _writer;
        private BinaryReader _reader;
        private string name;
        private Thread _thread;

        private delegate void RecieveMessageDelegate(string str);
        private delegate void RecieveClientListDelegate(string[] clients);

        //----------------------------------------------------------------------------------
        public bool Connect(Form1 form, string hostname, int port)
        {
            try
            {
                _form = form;

                _tcpClient = new TcpClient();
                _tcpClient.Connect(hostname, port);

                _stream = _tcpClient.GetStream();
                _writer = new BinaryWriter(_stream, Encoding.UTF8);
                _reader = new BinaryReader(_stream, Encoding.UTF8);

                _thread = new Thread(new ThreadStart(ProcessServerResponse));
                _thread.Start();

                NicknamePacket nickNamePacket = new NicknamePacket("Bill");
                Send(nickNamePacket);
            }
            catch (Exception e)
            {
                _form.Invoke(new RecieveMessageDelegate(_form.AppendText), new object[] { "Exception: " + e.Message });
                return false;
            }

            return true;
        }

        public bool Reconnect(string hostname, int port)
        {
            _tcpClient = new TcpClient();
            
            _tcpClient.Connect(hostname, port);

            _thread = new Thread(new ThreadStart(ProcessServerResponse));
            _thread.Start();

            //NicknamePacket nickNamePacket = new NicknamePacket("Bill");
            //Send(nickNamePacket);
            return true;
        }

        //----------------------------------------------------------------------------------
        public void Disconnect()
        {
            try
            {
                SysPacket SystemPacket = new SysPacket(name, " Has disconnected", SystemType.DISCONNECTED);
                Send(SystemPacket);
                //_reader.Close();
                //_writer.Close();
                this._tcpClient.Close();
                _thread.Abort();
            }
            catch (Exception e)
            {
                _form.Invoke(new RecieveMessageDelegate(_form.AppendText), new object[] { "Error: " + e });
            }

            _form.Invoke(new RecieveMessageDelegate(_form.AppendText), new object[] { "Disconnected" });
        }

        //----------------------------------------------------------------------------------
        private void ProcessServerResponse()
        {
            try
            {
                _reader = new BinaryReader(_stream, Encoding.UTF8);
            }

            catch (ArgumentException e)
            {

            }

            int noOfIncomingBytes;

            try
            {
                //while (_tcpClient.Connected)
                //{
                    while ((noOfIncomingBytes = _reader.ReadInt32()) != 0)
                    {
                        byte[] bytes = _reader.ReadBytes(noOfIncomingBytes);
                        MemoryStream memoryStream = new MemoryStream(bytes);
                        BinaryFormatter formatter = new BinaryFormatter();

                        Packet packet = formatter.Deserialize(memoryStream) as Packet;
                        switch (packet.type)
                        {
                                case (PacketType.CHATMESSAGE): 
                                    string message = ((ChatMessagePacket)packet).chatMessage;

                                    _form.Invoke(new RecieveMessageDelegate(_form.AppendText), new object[] { message });                              
                                    break;

                            case (PacketType.CLIENTLIST):
                                string[] clients = ((ClientList)packet).clients;
                                _form.Invoke(new RecieveClientListDelegate(_form.UpdateClients), new object[] {clients});
                                break; 
                        }
                    //}
                }
            }

            catch (IOException e)
            {
            
            }

            catch (ObjectDisposedException e)
            {

            }
        }


        //----------------------------------------------------------------------------------
        public void Send(Packet data)
        {
            MemoryStream mem = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(mem, data);
            byte[] buffer = mem.GetBuffer();

            try
            {
                _writer.Write(buffer.Length);
                _writer.Write(buffer);
                _writer.Flush();
            }

            catch (ObjectDisposedException e)
            {

            }
        }

        //----------------------------------------------------------------------------------
        public void SendTextPacket(string text, string sender)
        {

            ChatMessagePacket chatMessagePacket = new ChatMessagePacket(text, sender);
            Send(chatMessagePacket);
        }

        //----------------------------------------------------------------------------------
        public void SetNickname(string nickname)
        {
            name = nickname;
            NicknamePacket chatMessagePacket = new NicknamePacket(nickname);
            Send(chatMessagePacket);
        }

    }
}
