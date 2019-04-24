namespace tipy
{
    partial class ConnectionWindow
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
            this.Button_Odbierz = new System.Windows.Forms.Button();
            this.Button_Odrzuc = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button_Odbierz
            // 
            this.Button_Odbierz.BackColor = System.Drawing.Color.LightGreen;
            this.Button_Odbierz.Location = new System.Drawing.Point(46, 30);
            this.Button_Odbierz.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Odbierz.Name = "Button_Odbierz";
            this.Button_Odbierz.Size = new System.Drawing.Size(67, 26);
            this.Button_Odbierz.TabIndex = 2;
            this.Button_Odbierz.Text = "Odbierz";
            this.Button_Odbierz.UseVisualStyleBackColor = false;
            this.Button_Odbierz.Click += new System.EventHandler(this.button3_Click);
            // 
            // Button_Odrzuc
            // 
            this.Button_Odrzuc.BackColor = System.Drawing.Color.LightCoral;
            this.Button_Odrzuc.Location = new System.Drawing.Point(176, 30);
            this.Button_Odrzuc.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Odrzuc.Name = "Button_Odrzuc";
            this.Button_Odrzuc.Size = new System.Drawing.Size(67, 26);
            this.Button_Odrzuc.TabIndex = 3;
            this.Button_Odrzuc.Text = "Odrzuć";
            this.Button_Odrzuc.UseVisualStyleBackColor = false;
            this.Button_Odrzuc.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Teal;
            this.button7.Location = new System.Drawing.Point(106, 261);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 34);
            this.button7.TabIndex = 12;
            this.button7.Text = "Rozłącz";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.textBoxLog.Location = new System.Drawing.Point(66, 134);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(160, 24);
            this.textBoxLog.TabIndex = 14;
            // 
            // ConnectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(297, 315);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.Button_Odrzuc);
            this.Controls.Add(this.Button_Odbierz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ConnectionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectionWindow_FormClosed);
            this.Load += new System.EventHandler(this.logged_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_Odbierz;
        private System.Windows.Forms.Button Button_Odrzuc;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}