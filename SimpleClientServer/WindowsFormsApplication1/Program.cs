using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private const string hostname = "127.0.0.1";
        private const int port = 4444;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            SimpleClient _client = new SimpleClient();

            if (_client.Connect(hostname, port))
            {
                Console.WriteLine("Connected...");

                try
                {
                    _client.Run();
                }
                catch (NotConnectedException e)
                {
                    Console.WriteLine("Client not Connected");
                }
            }
            else
            {
                Console.WriteLine("Failed to connect to: " + hostname + ":" + port);
            }
            Console.Read();
        }
        

 
    }
}
