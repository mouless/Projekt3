namespace Group6Chat
{
    partial class PrivateChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateChatForm));
            this.richTextBoxPrivateConvo = new System.Windows.Forms.RichTextBox();
            this.btnPrivateSend = new System.Windows.Forms.Button();
            this.textBoxPrivateInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxPrivateConvo
            // 
            this.richTextBoxPrivateConvo.Location = new System.Drawing.Point(12, 33);
            this.richTextBoxPrivateConvo.Name = "richTextBoxPrivateConvo";
            this.richTextBoxPrivateConvo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxPrivateConvo.Size = new System.Drawing.Size(412, 456);
            this.richTextBoxPrivateConvo.TabIndex = 0;
            this.richTextBoxPrivateConvo.Text = "";
            // 
            // btnPrivateSend
            // 
            this.btnPrivateSend.Location = new System.Drawing.Point(430, 495);
            this.btnPrivateSend.Name = "btnPrivateSend";
            this.btnPrivateSend.Size = new System.Drawing.Size(102, 42);
            this.btnPrivateSend.TabIndex = 1;
            this.btnPrivateSend.Text = "SEND";
            this.btnPrivateSend.UseVisualStyleBackColor = true;
            this.btnPrivateSend.Click += new System.EventHandler(this.btnPrivateSend_Click);
            // 
            // textBoxPrivateInput
            // 
            this.textBoxPrivateInput.Location = new System.Drawing.Point(12, 495);
            this.textBoxPrivateInput.Multiline = true;
            this.textBoxPrivateInput.Name = "textBoxPrivateInput";
            this.textBoxPrivateInput.Size = new System.Drawing.Size(412, 42);
            this.textBoxPrivateInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Private chat:";
            // 
            // PrivateChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Group6Chat.Properties.Resources.page_background_default;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(543, 549);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPrivateInput);
            this.Controls.Add(this.btnPrivateSend);
            this.Controls.Add(this.richTextBoxPrivateConvo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PrivateChatForm";
            this.Text = "PrivateChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxPrivateConvo;
        private System.Windows.Forms.Button btnPrivateSend;
        private System.Windows.Forms.TextBox textBoxPrivateInput;
        private System.Windows.Forms.Label label1;
    }
}