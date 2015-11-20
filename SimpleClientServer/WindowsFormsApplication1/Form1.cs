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
                    Console.WriteLine("Exception: " + e.Message);
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
                    string userInput;

                    Thread thread = new Thread(new ThreadStart(ProcessServerResponse));
                    thread.Start();
                    Console.Write("Enter the data to be sent: ");

                    while ((userInput = Console.ReadLine()) != null)
                    {
                        _writer.WriteLine(userInput);
                        _writer.Flush();

                        if (userInput.Equals("9"))
                            break;

                        Console.Write("Enter the data to be sent: ");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected Error: " + e.Message);
                }
                finally
                {
                    _tcpClient.Close();
                }
                Console.Read();
            }

            private void ProcessServerResponse()
            {
                while (true)
                {
                    Console.WriteLine("Server sys: " + _reader.ReadLine());
                    Console.WriteLine();
                }
            }

            private void Form1_Load(object sender, EventArgs e)
            {

            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {

            }

            private void button1_Click(object sender, EventArgs e)
            {

            }
        }
    
}
