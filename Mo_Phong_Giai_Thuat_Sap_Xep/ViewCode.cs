using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form_ViewCode : Form
    {
        Form1.Loai_Sap_Xep Loai;
        bool is_viewing_code = true;
        bool IS_TANGDAN;
        public Form_ViewCode(Form1.Loai_Sap_Xep loai, bool is_tangdan)
        {
            InitializeComponent();
            ViewCodeBox.SelectionIndent = 20;     
            ViewCodeBox.SelectionHangingIndent = 20; 
            ViewCodeBox.Text = ViewCodeRepository.GetCode(loai, is_tangdan);
            head_label.Text = $"MÃ NGUỒN MINH HỌA {loai.ToString().ToUpper()} SORT";
            Loai = loai;
            if (is_tangdan)
            {
                Increase_Radio.Checked = true;
                IS_TANGDAN = true;
            }
            else
            {
                Decrease_Radio.Checked = true;
                IS_TANGDAN = false;
            }
        }
        static class ViewCodeRepository
        {
            private static readonly Dictionary<Form1.Loai_Sap_Xep, Func<bool, string>> Codes
                = new Dictionary<Form1.Loai_Sap_Xep, Func<bool, string>>()
            {
                // bubble sort
                {
                    Form1.Loai_Sap_Xep.Bubble,
                    (bool tangDan) =>
                    {
                        string op = tangDan ? ">" : "<";
                        return $@"
void bubbleSort(vector<int>& arr) {{
    int n = arr.size();

    for (int i = 0; i < n - 1; i++) {{
            for (int j = 0; j < n - i - 1; j++) {{
                    if (arr[j] {op} arr[j + 1]) {{
                            swap(arr[j], arr[j + 1]);
                    }}
            }}
    }}
}}";
                    }
                },

                // selection sort
                {
                    Form1.Loai_Sap_Xep.Selection,
                    (bool tangDan) =>
                    {
                        string op = tangDan ? "<" : ">";
                        return $@"
void selectionSort(vector<int>& arr) {{
        int n = arr.size();
        int i, j, min_idx;

        for (i = 0; i < n - 1; i++) {{
                min_idx = i;
                for (j = i + 1; j < n; j++) {{
                        if (arr[j] {op} arr[min_idx]) {{
                                min_idx = j;
                        }}
                }}
                swap(arr[min_idx], arr[i]);
        }}
}}";
                    }
                },

                // exchange sort
                {
                    Form1.Loai_Sap_Xep.Exchange,
                    (bool tangDan) =>
                    {
                        string op = tangDan ? "<" : ">";
                        return $@"
void exchangeSort(vector<int>& a) {{
        int n = a.size();

        for (int i = 0; i < n - 1; i++) {{
                for (int j = i + 1; j < n; j++) {{
                        if (a[j] {op} a[i]) {{
                                swap(a[i], a[j]);
                        }}
                }}
        }}
}}";
                    }
                },

                // insertion sort
                {
                    Form1.Loai_Sap_Xep.Insertion,
                    (bool tangDan) =>
                    {
                        string op = tangDan ? "<" : ">";
                        return $@"
void insertionSort(vector<int>& arr) {{
        int n = arr.size();

        for (int i = 1; i < n; i++) {{
                int j = i;
                while (j > 0 && arr[j] {op} arr[j - 1]) {{
                        swap(arr[j], arr[j - 1]);
                        j--;
                }}
        }}
}}";
                    }
                },
                // heap sort
                {
                    Form1.Loai_Sap_Xep.Heap,
                    (bool tangDan) =>
                    {
                        // tăng dần => max-heap: >
                        // giảm dần => min-heap: <
                        string op = tangDan ? ">" : "<";

                        return $@"
void heapify(vector<int>& arr, int n, int i) {{
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && arr[left] {op} arr[largest])
                largest = left;

        if (right < n && arr[right] {op} arr[largest])
                largest = right;

        if (largest != i) {{
                swap(arr[i], arr[largest]);
                heapify(arr, n, largest);
        }}
}}

void heapSort(vector<int>& arr) {{
        int n = arr.size();

        for (int i = n / 2 - 1; i >= 0; i--) {{
                heapify(arr, n, i);
        }}

        for (int i = n - 1; i > 0; i--) {{
                swap(arr[0], arr[i]);
                heapify(arr, i, 0);
        }}
}}";
                    }
                },

                // quick sort
                {
                    Form1.Loai_Sap_Xep.Quick,
                    (bool tangDan) =>
                    {
                        string opLeft = tangDan ? "<" : ">";
                        string opRight = tangDan ? ">" : "<";

                        return $@"
vector<int> quicksort(vector<int> a) {{
        if (a.size() <= 1) return a;

        int pivot_index = a.size() / 2;
        int pivot_value = a[pivot_index];

        vector<int> left, right, equal;

        for (int x : a) {{
                if (x {opLeft} pivot_value) 
                        left.push_back(x);
                else if (x {opRight} pivot_value) 
                        right.push_back(x);
                else 
                        equal.push_back(x); 
        }}

        vector<int> sorted_left = quicksort(left);
        vector<int> sorted_right = quicksort(right);

        vector<int> result = sorted_left;
        result.insert(result.end(), equal.begin(), equal.end());
        result.insert(result.end(), sorted_right.begin(), sorted_right.end());

        return result;
}}";
                    }
                },

                // merge sort
                {
                    Form1.Loai_Sap_Xep.Merge,
                    (bool tangDan) =>
                    {
                        string so_sanh = tangDan ? "<=" : ">=";

                        return $@"
void merge(vector<int>& a, int left, int mid, int right) {{
        vector<int> temp;
        int i = left;
        int j = mid + 1;

        while (i <= mid && j <= right) {{
                if (a[i] {so_sanh} a[j]) {{
                        temp.push_back(a[i++]);
                }} else {{
                        temp.push_back(a[j++]);
                }}
        }}

        while (i <= mid) temp.push_back(a[i++]);
        while (j <= right) temp.push_back(a[j++]);

        for (int k = 0; k < temp.size(); k++) {{
                a[left + k] = temp[k];
        }}
}}

void mergeSort(vector<int>& a, int left, int right) {{
        if (left >= right) return;

        int mid = (left + right) / 2;
        mergeSort(a, left, mid);
        mergeSort(a, mid + 1, right);
        merge(a, left, mid, right);
}}";
                    }
                }
            };

            public static string GetCode(Form1.Loai_Sap_Xep loai, bool tangDan)
            {
                return Codes.ContainsKey(loai)
                    ? Codes[loai](tangDan)
                    : "Chưa có mã nguồn minh họa cho thuật toán này.";
            }
        }


        static class ViewPseudoTextRepository
        {
            private static readonly Dictionary<Form1.Loai_Sap_Xep, Func<bool, string>> PseudoTexts
                = new Dictionary<Form1.Loai_Sap_Xep, Func<bool, string>>()
            {
        // bubble sort
        {
            Form1.Loai_Sap_Xep.Bubble,
            (bool tangDan) =>
            {
                string So_Sanh_Text;
                string Ket_Thuc_Text;

                if (tangDan)
                {
                    So_Sanh_Text = "nếu arr[j] > arr[j+1] thì hoán đổi arr[j] và arr[j+1]";
                    Ket_Thuc_Text = "phần tử lớn nhất của đoạn chưa cố định sẽ bị đẩy về cuối";
                }
                else
                {
                    So_Sanh_Text = "nếu arr[j] < arr[j+1] thì hoán đổi arr[j] và arr[j+1]";
                    Ket_Thuc_Text = "phần tử nhỏ nhất của đoạn chưa cố định sẽ bị đẩy về cuối";
                }

                return $@"
BUBBLE SORT (vector<int>)
- Đầu vào: vector<int>& arr.
- Lấy n = arr.size().
- Lặp i từ 0 đến n - 2:
        - Lặp j từ 0 đến n - i - 2:
        - So sánh 2 phần tử kề nhau: arr[j] và arr[j+1]
                - {So_Sanh_Text}.
- Sau mỗi lượt i, {Ket_Thuc_Text}.";
            }
        },

        // selection sort
        {
            Form1.Loai_Sap_Xep.Selection,
            (bool tangDan) =>
            {
                string pickText;
                string so_sanh_Text;

                if (tangDan)
                {
                    pickText = "tìm phần tử nhỏ nhất trong đoạn i..n-1";
                    so_sanh_Text  = "nếu arr[j] < arr[min_idx] thì cập nhật min_idx = j";
                }
                else
                {
                    pickText = "tìm phần tử lớn nhất trong đoạn i..n-1";
                    so_sanh_Text  = "nếu arr[j] > arr[min_idx] thì cập nhật min_idx = j";
                }

                return $@"
SELECTION SORT (vector<int>)
- Đầu vào: vector<int>& arr.
- Lấy n = arr.size().
- Lặp i từ 0 đến n - 2:
        - Đặt min_idx = i.
        - Duyệt j từ i + 1 đến n - 1 để {pickText}:
                - {so_sanh_Text}.
        - Đổi chỗ arr[min_idx] với arr[i] để đưa phần tử phù hợp về vị trí i.";
            }

        },

        // exchange sort
        {
            Form1.Loai_Sap_Xep.Exchange,
            (bool tangDan) =>
            {
                string so_sanh_Text;
                string Ket_Thuc_Text;

                if (tangDan)
                {
                    so_sanh_Text = "nếu a[j] < a[i] thì hoán đổi a[i] và a[j]";
                    Ket_Thuc_Text = "nếu gặp phần tử nhỏ hơn thì đổi chỗ ngay với vị trí i";
                }
                else
                {
                    so_sanh_Text = "nếu a[j] > a[i] thì hoán đổi a[i] và a[j]";
                    Ket_Thuc_Text = "nếu gặp phần tử lớn hơn thì đổi chỗ ngay với vị trí i";
                }

                return $@"
EXCHANGE SORT (vector<int>)
- Đầu vào: vector<int>& a.
- Lấy n = a.size().
- Lặp i từ 0 đến n - 2:
    - Lặp j từ i + 1 đến n - 1:
            - So sánh a[j] với a[i]:
                    - {so_sanh_Text}.
- Ý nghĩa: với mỗi i, {Ket_Thuc_Text}.";
            }
        },

        // insertion sort
        {
            Form1.Loai_Sap_Xep.Insertion,
            (bool tangDan) =>
            {
                string so_sanh_Text;
                string ket_thuc_Text;

                if (tangDan)
                {
                    so_sanh_Text = "khi arr[j] < arr[j-1] thì hoán đổi để đẩy arr[j] về trước";
                    ket_thuc_Text = "chèn phần tử đang xét dần về đúng vị trí trong đoạn [0..i]";
                }
                else
                {
                    so_sanh_Text = "khi arr[j] > arr[j-1] thì hoán đổi để đẩy arr[j] về trước";
                    ket_thuc_Text = "chèn phần tử đang xét dần về đúng vị trí theo thứ tự giảm trong đoạn [0..i]";
                }

                return $@"
INSERTION SORT (vector<int>)
- Đầu vào: vector<int>& arr.
- Lấy n = arr.size().
- Lặp i từ 1 đến n - 1:
      - Đặt j = i.
      - Trong khi j > 0 và còn sai thứ tự:
            - {so_sanh_Text}.
            - Giảm j đi 1.
- Ý nghĩa: mỗi lần xét i, {ket_thuc_Text}.";
            }
        },

        // heap sort
        {
            Form1.Loai_Sap_Xep.Heap,
            (bool tangDan) =>
            {
                string loaiHeap = tangDan ? "max-heap" : "min-heap";
                string bestName = tangDan ? "largest" : "smallest";

                // mô tả quy tắc so sánh cho dễ hiểu
                string ruleText = tangDan
                    ? "chọn nút con có giá trị lớn hơn để làm largest"
                    : "chọn nút con có giá trị nhỏ hơn để làm smallest";

                return $@"
HEAP SORT (vector<int>)
Phần HEAPIFY (arr, n, i):
- Đầu vào: vector<int>& arr.
- Xem i là nút gốc hiện tại.
- Tính chỉ số:
        - left = 2*i + 1
        - right = 2*i + 2
- Ban đầu đặt {bestName} = i.
- Nếu left < n và arr[left] tốt hơn arr[{bestName}] theo kiểu heap ({loaiHeap})
  thì cập nhật {bestName} = left.
- Nếu right < n và arr[right] tốt hơn arr[{bestName}] theo kiểu heap ({loaiHeap})
  thì cập nhật {bestName} = right.
- Nếu {bestName} khác i:
        - Hoán đổi arr[i] và arr[{bestName}].
        - Gọi đệ quy HEAPIFY(arr, n, {bestName}) để sửa heap tiếp ở nhánh bị ảnh hưởng.
(Ghi chú: quy tắc so sánh cụ thể là: {ruleText}.)

Phần HEAP_SORT (arr):
- Đầu vào: vector<int>& arr.
- Lấy n = arr.size().
- Xây dựng heap ban đầu:
        - Duyệt i từ n/2 - 1 về 0, gọi HEAPIFY(arr, n, i).
- Tách phần tử ra khỏi heap:
        - Duyệt i từ n - 1 về 1:
                - Hoán đổi arr[0] (gốc heap) với arr[i].
                - Gọi HEAPIFY(arr, i, 0) để phục hồi heap với kích thước giảm còn i.";
                }
        },

        // quick sort (left/right/equal)
        {
            Form1.Loai_Sap_Xep.Quick,
            (bool tangDan) =>
            {
                string leftRule;
                string rightRule;
                string DK_Dung;

                if (tangDan)
                {
                    leftRule = "đưa vào vector left nếu x < pivot_value";
                    rightRule = "đưa vào vector right nếu x > pivot_value";
                    DK_Dung = "nếu a.size() <= 1 thì trả về luôn";
                }
                else
                {
                    leftRule = "đưa vào vector left nếu x > pivot_value";
                    rightRule = "đưa vào vector right nếu x < pivot_value";
                    DK_Dung = "nếu a.size() <= 1 thì trả về luôn";
                }

                return $@"
QUICK SORT (vector<int>)
- Đầu vào: vector<int> a.
- Điều kiện dừng: {DK_Dung}.
- Chọn pivot:
      - pivot_index = a.size() / 2
      - pivot_value = a[pivot_index]
- Tạo 3 vector rỗng: left, right, equal.
- Duyệt từng phần tử x trong a:
      - {leftRule}.
      - Ngược lại nếu {rightRule}.
      - Còn lại (x == pivot_value) thì đưa vào equal.
- Gọi đệ quy:
      - sorted_left = quicksort(left)
      - sorted_right = quicksort(right)
- Ghép kết quả theo đúng code:
      - result = sorted_left + equal + sorted_right
- Trả về result.";
            }
        },

        // merge sort
        {
            Form1.Loai_Sap_Xep.Merge,
            (bool tangDan) =>
            {
                string chon_Rule;
                string so_sanh_Rule;

                if (tangDan)
                {
                    chon_Rule = "chọn phần tử nhỏ hơn (hoặc bằng) để đưa vào temp";
                    so_sanh_Rule = "nếu a[i] <= a[j] thì lấy a[i], ngược lại lấy a[j]";
                }
                else
                {
                    chon_Rule = "chọn phần tử lớn hơn (hoặc bằng) để đưa vào temp";
                    so_sanh_Rule = "nếu a[i] >= a[j] thì lấy a[i], ngược lại lấy a[j]";
                }

                return $@"
MERGE SORT (vector<int>)
Phần MERGE(a, left, mid, right):
- Đầu vào: vector<int>& a, int left, int mid, int right.
- Tạo vector<int> temp rỗng.
- Đặt i = left (đầu nửa trái), j = mid + 1 (đầu nửa phải).
- Trong khi i <= mid và j <= right:
      - So sánh a[i] và a[j]:
            - {chon_Rule}.
            - Quy tắc cụ thể: {so_sanh_Rule}.
      - Nếu lấy từ trái thì i++, nếu lấy từ phải thì j++.
- Nếu nửa trái còn dư (i <= mid) thì lần lượt push_back các phần tử còn lại vào temp.
- Nếu nửa phải còn dư (j <= right) thì lần lượt push_back các phần tử còn lại vào temp.
- Chép temp trở lại vector a từ vị trí left:
      - a[left + k] = temp[k] với k chạy từ 0 đến temp.size() - 1.

Phần MERGE_SORT(a, left, right):
- Đầu vào: vector<int>& a, int left, int right.
- Nếu left >= right thì dừng (đoạn có 0 hoặc 1 phần tử).
- Tính mid = (left + right) / 2.
- Gọi đệ quy sắp nửa trái: mergeSort(a, left, mid).
- Gọi đệ quy sắp nửa phải: mergeSort(a, mid + 1, right).
- Gọi merge(a, left, mid, right) để trộn 2 nửa đã sắp.";
            }
        }
            };
            public static string GetPseudoText(Form1.Loai_Sap_Xep loai, bool tangDan)
            {
                return PseudoTexts.ContainsKey(loai)
                    ? PseudoTexts[loai](tangDan)
                    : "Chưa có mã giả (văn viết) cho thuật toán này.";
            }
        }
        private void Exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            Owner?.Show();
        }
        private void ViewCode_Button_Click(object sender, EventArgs e)
        {
            is_viewing_code = true;
            head_label.Text = $"MÃ NGUỒN MINH HỌA {Loai.ToString().ToUpper()} SORT";
            ViewCodeBox.Text = ViewCodeRepository.GetCode(Loai, IS_TANGDAN);
        }
        private void Increase_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Increase_Radio.Checked)
            {
                IS_TANGDAN = true;

                if (is_viewing_code)
                    ViewCodeBox.Text = ViewCodeRepository.GetCode(Loai, true);
                else
                    ViewCodeBox.Text = ViewPseudoTextRepository.GetPseudoText(Loai, true);
            }
        }
        private void Decrease_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Decrease_Radio.Checked)
            {
                IS_TANGDAN = false;

                if (is_viewing_code)
                    ViewCodeBox.Text = ViewCodeRepository.GetCode(Loai, false);
                else
                    ViewCodeBox.Text = ViewPseudoTextRepository.GetPseudoText(Loai, false);
            }
        }
        private void ViewPseudoCode_Button_Click(object sender, EventArgs e)
        {
            is_viewing_code = false;
            head_label.Text = $"MÃ GIẢ MINH HỌA {Loai.ToString().ToUpper()} SORT";
            ViewCodeBox.Text = ViewPseudoTextRepository.GetPseudoText(Loai, IS_TANGDAN);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.Oemcomma)   
            {
                ViewCode_Button.PerformClick();
                return true;
            }
            if (keyData == Keys.OemPeriod)
            {
                ViewPseudoCode_Button.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
