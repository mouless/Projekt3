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
        public string TimeStamp { get; set; } = "";
        public int SelectedForPrivateConvo { get; set; }
        public string NameOfSelectedPrivateConvo { get; set; }
        List<PrivateChatForm> privateChatWindow = new List<PrivateChatForm>();

        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = btnSend;
        }

        public void WriteToTextbox(User u)
        {
            if (u.TypeOfMessage == MessageType.UserList)
            {
                if (listBoxParticipants.SelectedIndex > 0)
                {
                    SelectedForPrivateConvo = listBoxParticipants.SelectedIndex;
                }
                string[] listOfUsers = u.Message.Split(';');
                listBoxParticipants.Items.Clear();
                foreach (var item in listOfUsers)
                {
                    if (!(item == ""))
                    {
                        listBoxParticipants.Items.Add(item);
                    }
                }
            }
            else if (u.TypeOfMessage == MessageType.UserName)
            {
                UniqueUserName = u.UserName;
                MessageBox.Show($"You're connected... {UniqueUserName} brah!");
            }
            else if (u.TypeOfMessage == MessageType.PrivateMessage)
            {
                //bool isNotList = true;
                //foreach (var xXx in privateChatWindow)
                //{
                //    if (xXx.ReceiverOfPrivateMessage == u.Receiver)
                //    {
                //        xXx.WriteToPrivateTextbox(u);
                //        isNotList = false;
                //    }
                //    else if (isNotList == false)
                //    {

                //    }
                //}

                var temp = privateChatWindow.Find(x => x.SenderOfPrivateMessage == u.Receiver);
                if (temp != null)
                {
                    temp.WriteToPrivateTextbox(u);
                }
                else
                {
                    //TODO: FINNS DET REDAN ETT PRIVATECHATFÖNSTER ÖPPET, ÖPPNA FÖR GUDS SKULL INTE ÄNNU ETT FÖNSTER!!!
                    NameOfSelectedPrivateConvo = u.UserName;
                    PrivateChatForm privateChat = new PrivateChatForm(NameOfSelectedPrivateConvo, UniqueUserName, HostServer);
                    privateChatWindow.Add(privateChat);

                    privateChat.Text = $"{NameOfSelectedPrivateConvo} - Private Chat";
                    privateChat.WriteToPrivateTextbox(u);
                    privateChat.Show();
                    // Lägg in det störiga ljudet
                }


            }
            else if (u.TypeOfMessage == MessageType.Message)
            {
                if (!(TimeStamp == DateTime.Now.ToShortTimeString()))
                {
                    TimeStamp = DateTime.Now.ToShortTimeString();
                    richTextBoxConvo.AppendText($"{TimeStamp}\r\n");
                }
                richTextBoxConvo.SelectionFont = new Font(richTextBoxConvo.Font, FontStyle.Bold);
                richTextBoxConvo.AppendText(u.UserName + ": ");
                richTextBoxConvo.SelectionFont = new Font(richTextBoxConvo.Font, FontStyle.Regular);
                richTextBoxConvo.AppendText(u.Message + "\r");
                richTextBoxConvo.ScrollToCaret();
            }
            else if (u.TypeOfMessage == MessageType.ErrorMessage)
            {
                MessageBox.Show("Username is already taken. Please try something else!");
            }
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseClientInANiceWay();
        }

        private void CloseClientInANiceWay()
        {
            if (UniqueUserName != "" && UniqueUserName != null)
            {
                User toDisconnect = new User();
                toDisconnect.TypeOfMessage = MessageType.Quit;
                toDisconnect.UserName = UniqueUserName;
                toDisconnect.Message = "I'm outta here!!!";

                NetworkStream n = HostServer.GetStream();
                BinaryWriter w = new BinaryWriter(n);
                w.Write(User.ToJson(toDisconnect));
                w.Flush();

                Application.Exit();
            }
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
            label.SetBounds(9, 20, 272, 13);
            textBox.SetBounds(12, 36, 272, 20);
            buttonOk.SetBounds(128, 72, 75, 23);
            buttonCancel.SetBounds(209, 72, 75, 23);

            insertUserName.ClientSize = new Size(296, 107);
            insertUserName.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            insertUserName.ClientSize = new Size(Math.Max(300, label.Right + 10), insertUserName.ClientSize.Height);
            insertUserName.FormBorderStyle = FormBorderStyle.FixedDialog;
            //insertUserName.BackgroundImage = Properties.Resources.page_background_default;

            insertUserName.AcceptButton = buttonOk;
            insertUserName.CancelButton = buttonCancel;

            insertUserName.ShowDialog();

            if (textBox.Text.Length > 1 && textBox.Text.Length < 14)
            {
                string trimmedUserName = textBox.Text.Trim();
                if (trimmedUserName.Length > 1 && trimmedUserName.Length < 14)
                {
                    string message = User.ToJson(trimmedUserName, trimmedUserName, MessageType.UserName);
                    HostServer = Networking_client.StartTheClient(this);
                    NetworkStream n = HostServer.GetStream();
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write(message);
                    w.Flush();
                }
                else
                {
                    MessageBox.Show("Invalid username! Try again.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username! Try again.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //string userName = UniqueUserName; //Get the userName from the accepted unique UserName
            string input = this.textBoxInput.Text;
            if (!(input.Length == 0))
            {
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
            else
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBoxParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxParticipants.SelectedIndex >= 0)
            {
                SelectedForPrivateConvo = listBoxParticipants.SelectedIndex;
                NameOfSelectedPrivateConvo = listBoxParticipants.Items[SelectedForPrivateConvo].ToString();

                if (SelectedForPrivateConvo >= 0 && NameOfSelectedPrivateConvo != UniqueUserName)
                {
                    btnPrivateChat.Enabled = true;
                }
                else
                {
                    btnPrivateChat.Enabled = false;
                }
            }
        }

        //private void listBoxParticipants_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    int index = this.listBox1.IndexFromPoint(e.Location);
        //    if (index != System.Windows.Forms.ListBox.NoMatches)
        //    {
        //        MessageBox.Show(index.ToString());
        //    }
        //}


        private void btnPrivateChat_Click(object sender, EventArgs e)
        {
            PrivateChatForm privateChat = new PrivateChatForm(NameOfSelectedPrivateConvo, UniqueUserName, HostServer);
            privateChatWindow.Add(privateChat);

            privateChat.Text = $"{NameOfSelectedPrivateConvo} - Private Chat";
            privateChat.Show();
        }

        private void exitProgramToolStripMenuItem_Click(object sender, FormClosingEventArgs e)
        {
            CloseClientInANiceWay();
        }
    }
}
