using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Nếu login thành công (LoginForm trả về OK)
                Application.Run(new Form1());
            }
            // Nếu login không thành công, ứng dụng sẽ kết thúc
           
        }
    }
}
