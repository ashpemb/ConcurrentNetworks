using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        class NotConnectedException : Exception
        {
            public NotConnectedException()
                : base("TcpClient not connected.")
            { }

            public NotConnectedException(string message)
                : base(message)
            { }
        }

            private TcpClient _tcpClient;
            private NetworkStream _stream;
            private StreamWriter _writer;
            private StreamReader _reader;
            private String message;

            public Form1()
            {
                InitializeComponent();
                _tcpClient = new TcpClient();
                
            }

            public bool Connect(string hostname, int port)
            {
                try
                {
                    _tcpClient.Connect(hostname, port);
                    _stream = _tcpClient.GetStream();
                    _writer = new StreamWriter(_stream, Encoding.UTF8);
                    _reader = new StreamReader(_stream, Encoding.UTF8);
                }
                catch (Exception e)
                {
                    recievedMessages.Text = ("Exception: " + e.Message + '\n');
                    return false;
                }

                return true;
            }

            public void Run()
            {
                if (!_tcpClient.Connected)
                    throw new NotConnectedException();

                try
                {

                    Thread thread = new Thread(new ThreadStart(ProcessServerResponse));
                    thread.Start();
                    recievedMessages.Text = ("Enter the data to be sent: " + '\n');

                    while (message != null)
                    {
                        _writer.WriteLine(message);
                        _writer.Flush();

                        recievedMessages.Text = ("Enter the data to be sent: " + '\n');
                    }
                }
                catch (Exception e)
                {
                    recievedMessages.Text = ("Unexpected Error: " + e.Message + '\n');
                }
                finally
                {
                    _tcpClient.Close();
                }
            }

            private void ProcessServerResponse()
            {
                while (true)
                {
                    recievedMessages.Text = ("Server sys: " + _reader.ReadLine() + '\n');
                    recievedMessages.Text = ("" + '\n');
                }
            }

            private void Form1_Load(object sender, EventArgs e)
            {

            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {

            }

            private void SendButton_Click(object sender, EventArgs e)
            {
                message = MessageText.Text;
                MessageText.Text = String.Empty;
            }
        }
    
}
