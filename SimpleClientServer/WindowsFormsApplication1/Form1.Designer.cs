namespace WindowsFormsApplication1
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
            this.MessageText = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.recievedMessages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // MessageText
            // 
            this.MessageText.Location = new System.Drawing.Point(186, 199);
            this.MessageText.Name = "MessageText";
            this.MessageText.Size = new System.Drawing.Size(278, 22);
            this.MessageText.TabIndex = 0;
            this.MessageText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(232, 252);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(187, 63);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Send Message";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // recievedMessages
            // 
            this.recievedMessages.Location = new System.Drawing.Point(176, 29);
            this.recievedMessages.Name = "recievedMessages";
            this.recievedMessages.Size = new System.Drawing.Size(288, 115);
            this.recievedMessages.TabIndex = 2;
            this.recievedMessages.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 402);
            this.Controls.Add(this.recievedMessages);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageText;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.RichTextBox recievedMessages;
    }
}

