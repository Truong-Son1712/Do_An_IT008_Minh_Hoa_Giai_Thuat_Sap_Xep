using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// alias cho class SortHistoryItem lồng trong Form1
using SortHistoryItem = Mo_Phong_Giai_Thuat_Sap_Xep.Form1.SortHistoryItem;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form2 : Form
    {
        // danh sách lịch sử truyền từ Form1 sang
        private readonly List<SortHistoryItem> _history;

        // bản ghi mà user chọn (Form1 sẽ đọc biến này)
        public SortHistoryItem SelectedHistory { get; private set; }

        public Form2(List<SortHistoryItem> history)
        {
            InitializeComponent();

            _history = history ?? new List<SortHistoryItem>();

            // cấu hình DataGridView cơ bản
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;   // chỉ xem, chọn bằng nút "Chọn"

            // event
            button_delete_history.Click += button_delete_history_Click;
            button_delete_all_history.Click += button_delete_all_history_Click;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            // đổ dữ liệu lịch sử lên bảng
            LoadHistoryToGrid();
            FixHeightToRows();
        }

        // ----------------- LOAD DỮ LIỆU LÊN BẢNG -----------------
        private void LoadHistoryToGrid()
        {
            dataGridView1.Rows.Clear();

            int stt = 1;
            foreach (var item in _history)
            {
                int rowIndex = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[rowIndex];

                // gán STT
                row.Cells["STT"].Value = stt++;

                // mảng giá trị (list int) -> chuỗi
                row.Cells["values_of_array"].Value = string.Join(", ", item.Values);

                // loại sắp xếp + chiều
                row.Cells["type_sort"].Value = item.Algorithm + " Sort";      // ví dụ: "Quick", "Merge", ...
                row.Cells["direction_sort"].Value = item.Direction; // "Tăng dần" / "Giảm dần"

                // thời gian
                row.Cells["day_time"].Value = item.Time.ToString("HH:mm:ss dd/MM/yyyy");

                // cột "Chọn": nếu là ButtonColumn thì set Text, không thì cũng được
                if (dataGridView1.Columns["select"] is DataGridViewButtonColumn)
                {
                    row.Cells["select"].Value = "Chọn";
                }

                // lưu SortHistoryItem tương ứng vào Tag để tiện lấy lại
                row.Tag = item;
            }
        }

        // cập nhật lại STT sau khi xoá bớt bản ghi
        private void RenumberSTT()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        // ----------------- XÓA 1 BẢN GHI -----------------
        private void button_delete_history_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Hãy chọn một bản ghi cần xoá.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.CurrentRow;
            var item = row.Tag as SortHistoryItem;

            if (item != null)
            {
                // xoá khỏi list history
                _history.Remove(item);
            }

            // xoá khỏi DataGridView
            dataGridView1.Rows.Remove(row);

            // đánh lại số thứ tự
            RenumberSTT();
        }

        // ----------------- XÓA TẤT CẢ BẢN GHI -----------------
        private void button_delete_all_history_Click(object sender, EventArgs e)
        {
            if (_history.Count == 0)
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

            _history.Clear();
            dataGridView1.Rows.Clear();
        }

        // ----------------- BẤM NÚT "CHỌN" TRONG CỘT select -----------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // click header hoặc ngoài vùng data -> bỏ
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // chỉ xử lý nếu bấm đúng cột "select"
            if (dataGridView1.Columns[e.ColumnIndex].Name != "select")
                return;

            var row = dataGridView1.Rows[e.RowIndex];
            var item = row.Tag as SortHistoryItem;
            if (item == null)
                return;

            // lưu bản ghi được chọn
            SelectedHistory = item;

            // báo cho Form1 biết là OK (nếu mở bằng ShowDialog)
            this.DialogResult = DialogResult.OK;

            // đóng Form2
            this.Close();
        }
        private void FixHeightToRows()
        {
            int height = dataGridView1.ColumnHeadersHeight; // chiều cao header

            foreach (DataGridViewRow row in dataGridView1.Rows)
                height += row.Height;

            // +2 để tránh bị cắt viền
            dataGridView1.Height = height + 10;
        }
    }
}
