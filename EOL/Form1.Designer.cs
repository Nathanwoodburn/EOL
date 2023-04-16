namespace EOL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxout = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            textBoxpath = new TextBox();
            button3 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // textBoxout
            // 
            textBoxout.Location = new Point(420, 12);
            textBoxout.Multiline = true;
            textBoxout.Name = "textBoxout";
            textBoxout.ScrollBars = ScrollBars.Vertical;
            textBoxout.Size = new Size(338, 251);
            textBoxout.TabIndex = 0;
            textBoxout.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(329, 68);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Encrypt";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(329, 97);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Decrypt";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 17);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 3;
            label1.Text = "File:";
            // 
            // textBoxpath
            // 
            textBoxpath.Location = new Point(39, 14);
            textBoxpath.Name = "textBoxpath";
            textBoxpath.Size = new Size(332, 23);
            textBoxpath.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(377, 13);
            button3.Name = "button3";
            button3.Size = new Size(27, 23);
            button3.TabIndex = 5;
            button3.Text = "...";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 158);
            label2.Name = "label2";
            label2.Size = new Size(295, 75);
            label2.TabIndex = 6;
            label2.Text = "Instructions:\r\n1. Get the regular USB (USB A) Yubikey from my wallet.\r\n2. Plug it into the computer\r\n3. Select the EOL-encrypted.eol file using the ... button\r\n4. Press Decrypt\r\n";
            label2.Click += label2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 265);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(textBoxpath);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBoxout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Open EOL File";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBoxout;
        private Button button1;
        private Button button2;
        private Label label1;
        private TextBox textBoxpath;
        private Button button3;
        private Label label2;
    }
}