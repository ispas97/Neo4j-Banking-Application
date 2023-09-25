
namespace Neo4jproject
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
            this.button1 = new System.Windows.Forms.Button();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.ForUserName = new System.Windows.Forms.TextBox();
            this.ForPassword = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "Log In";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(155, 84);
            this.UserName.Multiline = true;
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(144, 47);
            this.UserName.TabIndex = 1;
            this.UserName.TextChanged += new System.EventHandler(this.UserName_TextChanged);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(155, 165);
            this.Password.Multiline = true;
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(144, 48);
            this.Password.TabIndex = 2;
            // 
            // ForUserName
            // 
            this.ForUserName.Location = new System.Drawing.Point(442, 84);
            this.ForUserName.Multiline = true;
            this.ForUserName.Name = "ForUserName";
            this.ForUserName.Size = new System.Drawing.Size(144, 47);
            this.ForUserName.TabIndex = 3;
            // 
            // ForPassword
            // 
            this.ForPassword.Location = new System.Drawing.Point(442, 166);
            this.ForPassword.Multiline = true;
            this.ForPassword.Name = "ForPassword";
            this.ForPassword.Size = new System.Drawing.Size(144, 47);
            this.ForPassword.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(442, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 54);
            this.button2.TabIndex = 5;
            this.button2.Text = "Register";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ForPassword);
            this.Controls.Add(this.ForUserName);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox ForUserName;
        private System.Windows.Forms.TextBox ForPassword;
        private System.Windows.Forms.Button button2;
    }
}

