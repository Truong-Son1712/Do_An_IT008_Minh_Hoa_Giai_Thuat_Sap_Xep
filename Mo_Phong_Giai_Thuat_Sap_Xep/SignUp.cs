using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class SignUp : Form
    {
        private string Ma_OTP = "";
        private DateTime Thoi_Han_OTP;
        string userFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"users.txt");
        int countdown = 60;
        public SignUp()
        {
            InitializeComponent();
        }
        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            string Ten_Dang_Nhap = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string email = EmailBox.Text.Trim();
            string OTP_Nhap_Vao = OTPBox.Text.Trim();

            if (Ten_Dang_Nhap == "" || password == "" || email == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Ten_Dang_Nhap.Length < 5) { 
                MessageBox.Show("Tên đăng nhập phải có ít nhất 5 ký tự!","Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(password.Length < 3) {
                MessageBox.Show("Mật khẩu phải có ít nhất 3 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (OTP_Nhap_Vao != Ma_OTP)
            {
                MessageBox.Show("OTP không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DateTime.Now > Thoi_Han_OTP)
            {
                MessageBox.Show("OTP đã hết hạn, vui lòng gửi lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string userFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "users.txt"
            );
            if (!File.Exists(userFile))
                File.Create(userFile).Close();

            // Kiểm tra trùng username
            var Kiem_Tra = File.ReadAllLines(userFile);
            foreach (var line in Kiem_Tra)
            {
                var parts = line.Split('|');
                if (parts[0] == Ten_Dang_Nhap)
                {
                    MessageBox.Show("Tài khoản đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // Ghi user mới
            string user_moi = $"{Ten_Dang_Nhap}|{password}|{email}";
            File.AppendAllText(userFile, user_moi + Environment.NewLine);

            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private async void SendOTP(string toEmail)
        {
            string email_nhap_vao = EmailBox.Text.Trim();
            if (!IsValidEmail(email_nhap_vao))
            {
                MessageBox.Show("Email không đúng định dạng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(email_nhap_vao))
            {
                MessageBox.Show("Vui lòng nhập email", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rewritetextbox.Text != txtPassword.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Random rnd = new Random();
            Ma_OTP = rnd.Next(100000, 999999).ToString();
            Thoi_Han_OTP = DateTime.Now.AddMinutes(5);
            // FAKE gửi mail
            MessageBox.Show(
                $" Mã xác nhận đăng ký\n\n" +
                $"Mã OTP của bạn là: {Ma_OTP}\n",
                $"(Hết hạn sau 1 phút)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            SendOTPBtn.Enabled = false;
            for (int i = 60; i >= 0; i--)
            {
                SendOTPBtn.Text = "resend after "+i + "s";
                await Task.Delay(1000); // không block UI
            }
            SendOTPBtn.Text = "Gửi OTP";
            SendOTPBtn.Enabled = true;
        }
        private void SendOTPBtn_Click(object sender, EventArgs e)
        {
            string email = EmailBox.Text.Trim();
            if (email == "")
            {
                MessageBox.Show("Vui lòng nhập email trước khi gửi OTP!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SendOTP(email);
        }
        private void SignUp_Load(object sender, EventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(SignUpBtn.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(SignUpBtn.Width - 20, SignUpBtn.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, SignUpBtn.Height - 20, 20, 20, 90, 90);
            path.CloseAllFigures();

            SignUpBtn.Region = new Region(path);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void SignUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SignUpBtn.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
