using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group6Chat
{
    public partial class Form1 : Form
    {
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
            Networking_client.StartTheClient();
        }
    }
}
