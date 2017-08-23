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
        public static string UniqueUserName { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        public void WriteToTextbox(User u)
        {
            if (!listBoxParticipants.Items.Contains(u.UserName))
            {
                listBoxParticipants.Items.Add(u.UserName);
            }
            if (u.TypeOfMessage == MessageType.UserName)
            {
                UniqueUserName = u.UserName;
                MessageBox.Show("You're connected... brah!");
            }
            else if (u.TypeOfMessage == MessageType.Message)
            {
                textBoxConvo.AppendText($"{u.UserName}: {u.Message}\r\n");
            }
            else if (u.TypeOfMessage == MessageType.PrivateMessage)
            {

            }
            else if (u.TypeOfMessage == MessageType.ErrorMessage)
            {
                MessageBox.Show("Username is already taken. Please try something else!");
            }
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
            Form insertUserName = new Form();
            insertUserName.ClientSize = new Size(296, 107);
            insertUserName.MinimizeBox = false;
            insertUserName.MaximizeBox = false;
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            insertUserName.Text = "Insert username";
            label.Text = "Insert your username:";
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            insertUserName.ClientSize = new Size(296, 107);
            insertUserName.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            insertUserName.ClientSize = new Size(Math.Max(300, label.Right + 10), insertUserName.ClientSize.Height);
            insertUserName.FormBorderStyle = FormBorderStyle.FixedDialog;

            insertUserName.AcceptButton = buttonOk;
            insertUserName.CancelButton = buttonCancel;

            insertUserName.ShowDialog();

            if (textBox.Text.Length > 1)
            {
                string message = User.ToJson(textBox.Text, textBox.Text, MessageType.UserName);
                HostServer = Networking_client.StartTheClient(this);
                NetworkStream n = HostServer.GetStream();
                BinaryWriter w = new BinaryWriter(n);
                w.Write(message);
                w.Flush();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //string userName = UniqueUserName; //Get the userName from the accepted unique UserName
            string input = this.textBoxInput.Text;
            string message = User.ToJson(UniqueUserName, input, MessageType.Message);
            textBoxInput.Text = "";
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            //textBoxInput.AcceptsReturn = false;
            //this.AcceptButton = btnSend;
        }

        private void listBoxParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
