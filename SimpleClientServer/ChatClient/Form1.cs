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

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private bool connected = false;
        private Connection connection = null;
        public Form1(Connection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }
        public void AppendText(string str)
        {
            richTextBox1.Text += str + "\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            if(!connected)
            {
                connected = connection.Connect(this, "127.0.0.1", 4444);
                if(connected)
                {
                    connectButton.Text = "Disconnect";
                }
            }
            else
            {
                connection.Disconnect();
                connected = false;
                connectButton.Text = "Connect";
            }
        }
        private void sendTextButton_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            connection.SendText(textBox1.Text);
            textBox1.Text = "";
        }


    }
}
