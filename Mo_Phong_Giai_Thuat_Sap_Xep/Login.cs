using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Login : Form
    {
        static class DatabaseHelper
        {
           
        }
        public Login()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string Ten_Dang_Nhap = txtUsername.Text.Trim();
            string Mat_Khau = txtPassword.Text;

            string userFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "users.txt"
            );

            if (!File.Exists(userFile))
            {
                MessageBox.Show("Chưa có tài khoản nào!");
                return;
            }

            var lines = File.ReadAllLines(userFile);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length >= 2 &&
                    parts[0] == Ten_Dang_Nhap &&
                    parts[1] == Mat_Khau)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }
            if (Ten_Dang_Nhap.Length<5)
                MessageBox.Show("Tên đăng nhập phải chứa ít nhất 5 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Mat_Khau.Length<3) 
                MessageBox.Show("Mật khẩu phải chứa ít nhất 3 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void SignUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp signUpForm = new SignUp();
            signUpForm.ShowDialog();
            this.Show();
        }
        private void ShowpassCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowpassCheck.Checked)
            {
                pictureBoxEye.Image = Properties.Resources.open_eye;
                txtPassword.UseSystemPasswordChar = false; // hiện pass thật
            }
            else {
                pictureBoxEye.Image = Properties.Resources.close_eye;
                txtPassword.UseSystemPasswordChar = true;  // hiển thị ***
            }
        }  
        private void Login_Load(object sender, EventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(LoginBtn.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(LoginBtn.Width - 20, LoginBtn.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, LoginBtn.Height - 20, 20, 20, 90, 90);
            path.CloseAllFigures();

            LoginBtn.Region = new Region(path);
        }
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                LoginBtn.PerformClick();
            }
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show(
               "Bạn có chắc chắn muốn thoát chương trình không?",
               "Xác nhận thoát",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
            );

            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
