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
        private string text;
        private string name = "unknown";
        
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

        public void UpdateClients(string[] clients)
        {
            if (!connected)
            {
                return;
            }
            clientListBox.Items.Clear();
            for (int i = 0; i<clients.Length; i++)
            {
                clientListBox.Items.Add(clients[i]);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(connectButton.Text == "Connect")
            {
                connected = connection.Connect(this, "127.0.0.1", 4444);
                if(connected)
                {
                    if (name != null)
                    {
                        connection.SetNickname(name);
                    }
                    connectButton.Text = "Disconnect";
                }
            }
            else if (connectButton.Text == "Disconnect")
            {
                connection.Disconnect();
                connected = false;
                connectButton.Text = "Reconnect";
                this.Close();
            }
            else if (connectButton.Text == "Reconnect")
            {
                connected = true;
                connection.Reconnect("127.0.0.1", 4444);
                connectButton.Text = "Disconnect";
            }
        }
        private void sendTextButton_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;
            else
                text = textBox1.Text;
                connection.SendTextPacket(text, name);
                textBox1.Text = "";
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            name = nameBox.Text;
            if (!connected)
                return;
            connection.SetNickname(name);
        }



    }
}
