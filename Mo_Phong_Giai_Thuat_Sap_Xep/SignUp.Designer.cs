namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    partial class SignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUp));
            this.label1 = new System.Windows.Forms.Label();
            this.SendOTPBtn = new System.Windows.Forms.Button();
            this.OTPBox = new System.Windows.Forms.TextBox();
            this.OTPLbl = new System.Windows.Forms.Label();
            this.SignUpBtn = new System.Windows.Forms.Button();
            this.PsswrdLbl = new System.Windows.Forms.Label();
            this.UsernameLbl = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.EmailLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rewritetextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(93, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "Đăng Ký Tài Khoản";
            // 
            // SendOTPBtn
            // 
            this.SendOTPBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.SendOTPBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SendOTPBtn.Location = new System.Drawing.Point(133, 236);
            this.SendOTPBtn.Name = "SendOTPBtn";
            this.SendOTPBtn.Size = new System.Drawing.Size(129, 27);
            this.SendOTPBtn.TabIndex = 4;
            this.SendOTPBtn.Text = "Send";
            this.SendOTPBtn.UseVisualStyleBackColor = false;
            this.SendOTPBtn.Click += new System.EventHandler(this.SendOTPBtn_Click);
            // 
            // OTPBox
            // 
            this.OTPBox.Location = new System.Drawing.Point(269, 241);
            this.OTPBox.Name = "OTPBox";
            this.OTPBox.Size = new System.Drawing.Size(147, 22);
            this.OTPBox.TabIndex = 5;
            // 
            // OTPLbl
            // 
            this.OTPLbl.AutoSize = true;
            this.OTPLbl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OTPLbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.OTPLbl.Location = new System.Drawing.Point(44, 234);
            this.OTPLbl.Name = "OTPLbl";
            this.OTPLbl.Size = new System.Drawing.Size(58, 25);
            this.OTPLbl.TabIndex = 9;
            this.OTPLbl.Text = "OTP :";
            // 
            // SignUpBtn
            // 
            this.SignUpBtn.BackColor = System.Drawing.Color.SteelBlue;
            this.SignUpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignUpBtn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignUpBtn.ForeColor = System.Drawing.Color.White;
            this.SignUpBtn.Location = new System.Drawing.Point(178, 285);
            this.SignUpBtn.Name = "SignUpBtn";
            this.SignUpBtn.Size = new System.Drawing.Size(115, 41);
            this.SignUpBtn.TabIndex = 6;
            this.SignUpBtn.Text = "SignUp";
            this.SignUpBtn.UseVisualStyleBackColor = false;
            this.SignUpBtn.Click += new System.EventHandler(this.SignUpBtn_Click);
            // 
            // PsswrdLbl
            // 
            this.PsswrdLbl.AutoSize = true;
            this.PsswrdLbl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PsswrdLbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.PsswrdLbl.Location = new System.Drawing.Point(44, 112);
            this.PsswrdLbl.Name = "PsswrdLbl";
            this.PsswrdLbl.Size = new System.Drawing.Size(106, 25);
            this.PsswrdLbl.TabIndex = 4;
            this.PsswrdLbl.Text = "Mật khẩu :";
            // 
            // UsernameLbl
            // 
            this.UsernameLbl.AutoSize = true;
            this.UsernameLbl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.UsernameLbl.Location = new System.Drawing.Point(44, 70);
            this.UsernameLbl.Name = "UsernameLbl";
            this.UsernameLbl.Size = new System.Drawing.Size(159, 25);
            this.UsernameLbl.TabIndex = 3;
            this.UsernameLbl.Text = "Tên Đăng Nhập :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(268, 116);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(147, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(268, 74);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(147, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(268, 196);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(147, 22);
            this.EmailBox.TabIndex = 3;
            // 
            // EmailLbl
            // 
            this.EmailLbl.AutoSize = true;
            this.EmailLbl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailLbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.EmailLbl.Location = new System.Drawing.Point(43, 192);
            this.EmailLbl.Name = "EmailLbl";
            this.EmailLbl.Size = new System.Drawing.Size(69, 25);
            this.EmailLbl.TabIndex = 8;
            this.EmailLbl.Text = "Email :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(44, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nhập Lại Mật Khẩu :";
            // 
            // rewritetextbox
            // 
            this.rewritetextbox.Location = new System.Drawing.Point(269, 156);
            this.rewritetextbox.Name = "rewritetextbox";
            this.rewritetextbox.Size = new System.Drawing.Size(146, 22);
            this.rewritetextbox.TabIndex = 2;
            this.rewritetextbox.UseSystemPasswordChar = true;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 356);
            this.Controls.Add(this.rewritetextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SignUpBtn);
            this.Controls.Add(this.SendOTPBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OTPBox);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.OTPLbl);
            this.Controls.Add(this.EmailLbl);
            this.Controls.Add(this.UsernameLbl);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.PsswrdLbl);
            this.Controls.Add(this.txtPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "SignUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký Tài Khoản";
            this.Load += new System.EventHandler(this.SignUp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SignUp_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SignUpBtn;
        private System.Windows.Forms.Label PsswrdLbl;
        private System.Windows.Forms.Label UsernameLbl;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button SendOTPBtn;
        private System.Windows.Forms.TextBox OTPBox;
        private System.Windows.Forms.Label OTPLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Label EmailLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rewritetextbox;
    }
}