using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lich_su_Sap_Xep = Mo_Phong_Giai_Thuat_Sap_Xep.Form1.Lich_su_Sap_Xep;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form2 : Form
    {
        private readonly List<Lich_su_Sap_Xep> Lich_Su;
        public Lich_su_Sap_Xep Ban_Ghi_Duoc_Chon { get; private set; }

        public Form2(List<Lich_su_Sap_Xep> history)
        {   
            InitializeComponent();
            Lich_Su = history ?? new List<Lich_su_Sap_Xep>();
            Load_Du_lieu_Lich_Su_Len_Grid();
            Cap_Nhat_Chieu_Cao_Grid();
        }
        private void Load_Du_lieu_Lich_Su_Len_Grid()
        {
            dataGridView1.Rows.Clear();
            int stt = 1;
            foreach (var Ban_ghi in Lich_Su)
            {
                int Index_Hang = dataGridView1.Rows.Add();
                var Hang = dataGridView1.Rows[Index_Hang];

                Hang.Cells["STT"].Value = stt++;
                Hang.Cells["values_of_array"].Value = string.Join(", ", Ban_ghi.DS_DuLieu);
                Hang.Cells["type_sort"].Value = Ban_ghi.Giai_Thuat + " Sort";     
                Hang.Cells["direction_sort"].Value = Ban_ghi.Chieu_Sap_Xep; 
                Hang.Cells["day_time"].Value = Ban_ghi.Thoi_Gian_ghi.ToString("HH:mm:ss dd/MM/yyyy");
                Hang.Cells["select"].Value = "Apply";
                Hang.Tag = Ban_ghi;
            }
        }
        private void Cap_Nhat_STT()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
            }
        }
        private void Cap_Nhat_Chieu_Cao_Grid()
        {
            int Chieu_Cao = dataGridView1.ColumnHeadersHeight; 
            foreach (DataGridViewRow row in dataGridView1.Rows)
                Chieu_Cao += row.Height-2;

            dataGridView1.Height = Chieu_Cao + 10;
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button_delete_history.PerformClick(); 
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }
        private void button_delete_history_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Hãy chọn một bản ghi cần xoá.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var Hang_Can_Xoa = dataGridView1.CurrentRow;
            var Ban_Ghi_Can_Xoa = Hang_Can_Xoa.Tag as Lich_su_Sap_Xep;

            if (Ban_Ghi_Can_Xoa != null)
            {
                Lich_Su.Remove(Ban_Ghi_Can_Xoa); //xóa trong data
            }
            dataGridView1.Rows.Remove(Hang_Can_Xoa); //xóa trong giao diện

            Cap_Nhat_STT();
            Cap_Nhat_Chieu_Cao_Grid();
        }
        private void button_delete_all_history_Click(object sender, EventArgs e)
        {
            if (Lich_Su.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào để xoá.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                "Xoá mọi bản ghi lịch sử?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            Lich_Su.Clear();
            dataGridView1.Rows.Clear();
            Cap_Nhat_Chieu_Cao_Grid();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dataGridView1.Columns[e.ColumnIndex].Name != "select")
                return;

            var Hang_Duoc_Chon = dataGridView1.Rows[e.RowIndex];
            var DuLieu_Duoc_Chon = Hang_Duoc_Chon.Tag as Lich_su_Sap_Xep;
            if (DuLieu_Duoc_Chon == null)
                return;

            Ban_Ghi_Duoc_Chon = DuLieu_Duoc_Chon;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
