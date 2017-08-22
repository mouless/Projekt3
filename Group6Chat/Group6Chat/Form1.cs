using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group6Chat
{
    public partial class Form1 : Form
    {
        TcpClient HostServer;

        public Form1()
        {
            InitializeComponent();
        }

        public void WriteToTextbox(string message)
        {
            textBoxConvo.Text = message;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void textBoxConvo_TextChanged(object sender, EventArgs e)
        {

        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostServer = Networking_client.StartTheClient(this);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = this.textBoxInput.Text;
            
            try
            {
                if (!message.Equals("quit"))
                {
                    NetworkStream n = HostServer.GetStream();
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write(message);
                    w.Flush();
                }
                else
                    HostServer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
