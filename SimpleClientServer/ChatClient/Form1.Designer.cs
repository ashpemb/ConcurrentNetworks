namespace ChatClient
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
            this.sendTextButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.setButton = new System.Windows.Forms.Button();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // sendTextButton
            // 
            this.sendTextButton.Location = new System.Drawing.Point(366, 293);
            this.sendTextButton.Name = "sendTextButton";
            this.sendTextButton.Size = new System.Drawing.Size(75, 23);
            this.sendTextButton.TabIndex = 0;
            this.sendTextButton.Text = "Send";
            this.sendTextButton.UseVisualStyleBackColor = true;
            this.sendTextButton.Click += new System.EventHandler(this.sendTextButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(435, 250);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 293);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(355, 20);
            this.textBox1.TabIndex = 2;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(5, 5);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(132, 5);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(157, 20);
            this.nameBox.TabIndex = 4;
            // 
            // setButton
            // 
            this.setButton.Location = new System.Drawing.Point(321, 5);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(70, 19);
            this.setButton.TabIndex = 5;
            this.setButton.Text = "Set Name";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // clientListBox
            // 
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.Location = new System.Drawing.Point(447, 34);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(206, 251);
            this.clientListBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 318);
            this.Controls.Add(this.clientListBox);
            this.Controls.Add(this.setButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.sendTextButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendTextButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.ListBox clientListBox;
    }
}

