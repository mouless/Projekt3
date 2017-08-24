namespace Group6Chat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSend = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.lblYourNameHere = new System.Windows.Forms.Label();
            this.listBoxParticipants = new System.Windows.Forms.ListBox();
            this.labelOnlineUsers = new System.Windows.Forms.Label();
            this.btnPrivateChat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxConvo = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(430, 495);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(102, 42);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(12, 495);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(412, 42);
            this.textBoxInput.TabIndex = 1;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // lblYourNameHere
            // 
            this.lblYourNameHere.AutoSize = true;
            this.lblYourNameHere.Location = new System.Drawing.Point(12, 45);
            this.lblYourNameHere.Name = "lblYourNameHere";
            this.lblYourNameHere.Size = new System.Drawing.Size(120, 17);
            this.lblYourNameHere.TabIndex = 3;
            this.lblYourNameHere.Text = "Name of the chat:";
            // 
            // listBoxParticipants
            // 
            this.listBoxParticipants.FormattingEnabled = true;
            this.listBoxParticipants.ItemHeight = 16;
            this.listBoxParticipants.Location = new System.Drawing.Point(431, 65);
            this.listBoxParticipants.Name = "listBoxParticipants";
            this.listBoxParticipants.Size = new System.Drawing.Size(272, 148);
            this.listBoxParticipants.TabIndex = 4;
            this.listBoxParticipants.SelectedIndexChanged += new System.EventHandler(this.listBoxParticipants_SelectedIndexChanged);
            // 
            // labelOnlineUsers
            // 
            this.labelOnlineUsers.AutoSize = true;
            this.labelOnlineUsers.Location = new System.Drawing.Point(431, 42);
            this.labelOnlineUsers.Name = "labelOnlineUsers";
            this.labelOnlineUsers.Size = new System.Drawing.Size(91, 17);
            this.labelOnlineUsers.TabIndex = 5;
            this.labelOnlineUsers.Text = "Users online:";
            // 
            // btnPrivateChat
            // 
            this.btnPrivateChat.Enabled = false;
            this.btnPrivateChat.Location = new System.Drawing.Point(572, 219);
            this.btnPrivateChat.Name = "btnPrivateChat";
            this.btnPrivateChat.Size = new System.Drawing.Size(131, 30);
            this.btnPrivateChat.TabIndex = 6;
            this.btnPrivateChat.Text = "Start private chat";
            this.btnPrivateChat.UseVisualStyleBackColor = true;
            this.btnPrivateChat.Click += new System.EventHandler(this.btnPrivateChat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToServerToolStripMenuItem,
            this.exitProgramToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToServerToolStripMenuItem
            // 
            this.connectToServerToolStripMenuItem.Name = "connectToServerToolStripMenuItem";
            this.connectToServerToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.connectToServerToolStripMenuItem.Text = "Connect to server";
            this.connectToServerToolStripMenuItem.Click += new System.EventHandler(this.connectToServerToolStripMenuItem_Click);
            // 
            // exitProgramToolStripMenuItem
            // 
            this.exitProgramToolStripMenuItem.Name = "exitProgramToolStripMenuItem";
            this.exitProgramToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.exitProgramToolStripMenuItem.Text = "Exit program";
            this.exitProgramToolStripMenuItem.Click += new System.EventHandler(this.exitProgramToolStripMenuItem_Click);
            // 
            // richTextBoxConvo
            // 
            this.richTextBoxConvo.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxConvo.Location = new System.Drawing.Point(12, 65);
            this.richTextBoxConvo.Name = "richTextBoxConvo";
            this.richTextBoxConvo.ReadOnly = true;
            this.richTextBoxConvo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxConvo.Size = new System.Drawing.Size(412, 424);
            this.richTextBoxConvo.TabIndex = 8;
            this.richTextBoxConvo.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 549);
            this.Controls.Add(this.richTextBoxConvo);
            this.Controls.Add(this.btnPrivateChat);
            this.Controls.Add(this.labelOnlineUsers);
            this.Controls.Add(this.listBoxParticipants);
            this.Controls.Add(this.lblYourNameHere);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Group6Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.exitProgramToolStripMenuItem_Click);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label lblYourNameHere;
        private System.Windows.Forms.ListBox listBoxParticipants;
        private System.Windows.Forms.Label labelOnlineUsers;
        private System.Windows.Forms.Button btnPrivateChat;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToServerToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxConvo;
    }
}

