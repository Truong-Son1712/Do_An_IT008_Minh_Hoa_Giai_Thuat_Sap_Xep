using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form_ViewCode : Form
    {
        public Form_ViewCode()
        {
            InitializeComponent();
           
        }

        private void Form_ViewCode_Load(object sender, EventArgs e)
        {

        }
        public Form_ViewCode(string code, Form1.Loai_Sap_Xep loai)
        {
            InitializeComponent();
            Text = $"View Code - {loai}";
            rtbSourceCode.ReadOnly = true;
            rtbSourceCode.Font = new Font("Consolas", 11);
            rtbSourceCode.Text = code;
        }
    }
}
