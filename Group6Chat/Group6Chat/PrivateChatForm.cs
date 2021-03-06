﻿using System;
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
    public partial class PrivateChatForm : Form
    {
        public string TimeStamp { get; set; } = "";
        public string ReceiverOfPrivateMessage { get; set; }
        public string SenderOfPrivateMessage { get; set; }
        public TcpClient HostServer { get; set; }

        public PrivateChatForm(string receiver, string sender, TcpClient hostServer)
        {
            ReceiverOfPrivateMessage = receiver;
            SenderOfPrivateMessage = sender;
            HostServer = hostServer;
            InitializeComponent();
        }

        private void btnPrivateSend_Click(object sender, EventArgs e)
        {
            //string userName = UniqueUserName; //Get the userName from the accepted unique UserName
            string input = this.textBoxPrivateInput.Text;
            if (!(input.Length == 0))
            {
                string message = User.ToJson(SenderOfPrivateMessage, input, MessageType.PrivateMessage, ReceiverOfPrivateMessage);
                //string message2 = User.ToJson(ReceiverOfPrivateMessage, input, MessageType.PrivateMessage, SenderOfPrivateMessage);
                textBoxPrivateInput.Text = "";
                try
                {
                    if (!message.Equals("quit"))
                    {
                        NetworkStream n = HostServer.GetStream();
                        BinaryWriter w = new BinaryWriter(n);
                        w.Write(message);
                        //w.Write(message2);
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
            else
            {

            }
        }

        public void WriteToPrivateTextbox(User c)
        {
            if (!(TimeStamp == DateTime.Now.ToShortTimeString()))
            {
                TimeStamp = DateTime.Now.ToShortTimeString();
                richTextBoxPrivateConvo.AppendText($"{TimeStamp}\r\n");
            }
            richTextBoxPrivateConvo.SelectionFont = new Font(richTextBoxPrivateConvo.Font, FontStyle.Bold);
            richTextBoxPrivateConvo.AppendText(c.UserName + ": ");
            richTextBoxPrivateConvo.SelectionFont = new Font(richTextBoxPrivateConvo.Font, FontStyle.Regular);
            richTextBoxPrivateConvo.AppendText(c.Message + "\r");
            richTextBoxPrivateConvo.ScrollToCaret();
        }

        private void PrivateChatForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxPrivateInput;
            this.AcceptButton = btnPrivateSend;
        }
    }
}
