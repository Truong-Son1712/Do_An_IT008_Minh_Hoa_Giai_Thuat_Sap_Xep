using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form1 : Form
    {
        // --------------------------- DỮ LIỆU & TRẠNG THÁI ---------------------------
        readonly List<Label> _labels = new List<Label>();
        readonly List<int> _values = new List<int>();
        readonly Random _rnd = new Random();

        // Vị trí hiển thị (đặt ở giữa vùng phía trên)
        int startX = 60;            // lề trái cho dãy
        int baseY;                  // sẽ tính động theo chiều cao form (~ 1/3)
        int raisedY => baseY - 80;  // khi nhấc lên
        int spacing = 70;           // khoảng cách giữa các ô
        const int MAX_COUNT = 10;   // tối đa 10 phần tử

        // Tốc độ & điều khiển
        int delay = 200;             // ms giữa các bước "duyệt"
        bool ascending = true;
        bool paused = false;
        bool running = false;

        double moveSpeedScale = 1.6;
        int moveStep = 4;   // số px mỗi “tick” di chuyển
        int moveSleep = 10; // độ trễ (ms) giữa các “tick” di chuyển

        CancellationTokenSource cts;

        // Màu trạng thái (đúng "hướng" bạn yêu cầu)
        readonly Color normal = Color.LightBlue;    // mặc định
        readonly Color scan = Color.Orange;       // đang duyệt
        readonly Color current = Color.Tomato;       // ứng viên hiện tại (min/pivot/điểm chèn...)
        readonly Color fixedC = Color.LightGreen;   // đã cố định
        // màu vàng chỉ khi swap (đặt trong Raise/Lower)
        List<int> _beforeSort = new List<int>();
        enum SortType { None, Exchange, Selection, Insertion, Bubble, Heap, Quick, Merge }
        SortType _sort = SortType.None;

        // --------------------------- KHỞI TẠO ---------------------------

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form1_KeyDown;
         
            // vị trí mặc định của dãy (giữa vùng phía trên)
            RecalcBaseY();

            // Bật/tắt khu nhập
            radioButton_Random.CheckedChanged += ModeChanged;
            radioButton_By_Hand.CheckedChanged += ModeChanged;
            radioButton_Random.Checked = true;
            ModeChanged(null, EventArgs.Empty);

            // Ràng buộc chỉ nhập số
            textBox_Input_Number_Element_RanDom.KeyPress += OnlyDigit_KeyPress;
            textBox_Input_Element_By_Hand.KeyPress += OnlyDigit_KeyPress;

            // Nút
            button_add.Click += button_add_Click;
            button_start.Click += button_start_Click;
            button_stop.Click += (sender, e) =>
            {
                if (!running) return;

                paused = !paused; // đảo trạng thái

                if (paused)
                    button_stop.Text = "Tiếp tục";
                else
                    button_stop.Text = "Tạm dừng";
            };
            button_before_sort.Click += button_before_sort_Click;
            button_reset.Click += (_, __) => { paused = false; running = false; cts?.Cancel(); ClearAll(); button_add.Enabled = true; };
           
            // Tăng/giảm tốc
            button_increase_speed.Click += (_, __) => ChangeSpeed(-10);
            button_decrease_speed.Click += (_, __) => ChangeSpeed(+10);
            label_speed.Text = (550 - delay).ToString();
            UpdateMotionParams();

            // Chiều sắp xếp
            radioButton_Increase.CheckedChanged += (_, __) => { if (radioButton_Increase.Checked) ascending = true; };
            radioButton_Decrease.CheckedChanged += (_, __) => { if (radioButton_Decrease.Checked) ascending = false; };

            // Chọn thuật toán
            radioButton_Exchange_Sort.CheckedChanged += (_, __) => { if (radioButton_Exchange_Sort.Checked) _sort = SortType.Exchange; };
            radioButton_Selection_Sort.CheckedChanged += (_, __) => { if (radioButton_Selection_Sort.Checked) _sort = SortType.Selection; };
            radioButton_Insertion_Sort.CheckedChanged += (_, __) => { if (radioButton_Insertion_Sort.Checked) _sort = SortType.Insertion; };
            radioButton_Bubble_Sort.CheckedChanged += (_, __) => { if (radioButton_Bubble_Sort.Checked) _sort = SortType.Bubble; };
            radioButton_Heap_Sort.CheckedChanged += (_, __) => { if (radioButton_Heap_Sort.Checked) _sort = SortType.Heap; };
            radioButton_Quick_Sort.CheckedChanged += (_, __) => { if (radioButton_Quick_Sort.Checked) _sort = SortType.Quick; };
            radioButton_Merge_Sort.CheckedChanged += (_, __) => { if (radioButton_Merge_Sort.Checked) _sort = SortType.Merge; };
        }
        void UpdateMotionParams()
        {
            // s: 0..1 (delay nhỏ => s lớn => nhanh hơn)
            double s = (550 - delay) / 540.0;

            // Bước nhảy nhỏ nhưng tăng dần theo speed: 2..10 px/tick
            moveStep = 2 + (int)Math.Round(8 * s);
            if (moveStep < 2) moveStep = 2;

            // Thời gian nghỉ mỗi tick giảm theo speed: 14..4 ms/tick
            int baseSleep = 14 - (int)Math.Round(10 * s);
            if (baseSleep < 2) baseSleep = 2;

            // Áp hệ số người dùng (nhỏ hơn -> nhanh hơn). Không kẹp trần 28 nữa.
            moveSleep = (int)Math.Round(baseSleep * moveSpeedScale);
            if (moveSleep < 2) moveSleep = 2;      // sàn an toàn
        }

        void button_before_sort_Click(object sender, EventArgs e)
        {
            // Nếu đang chạy, dừng ngay lập tức
            paused = false;
            if (running)
            {
                cts?.Cancel();
                running = false;
            }

            if (_beforeSort == null || _beforeSort.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu ban đầu để khôi phục!");
                return;
            }

            // Xóa hiện trạng và dựng lại đúng mảng trước sort
            ClearAll();
            for (int i = 0; i < _beforeSort.Count; i++)
            {
                int val = _beforeSort[i];
                _values.Add(val);
                var lb = MakeLabel(val, i);   // MakeLabel của bạn đã đặt đúng vị trí & màu normal
                _labels.Add(lb);
                Controls.Add(lb);
            }
            button_add.Enabled = true;
            // Reset trạng thái nút tạm dừng (không thay đổi logic khác của bạn)
            button_stop.Text = "Tạm dừng";
            //button_stop.Enabled = false; // vì hiện tại không chạy

            // (Không đụng gì đến thuật toán, màu sắc sẽ là normal do MakeLabel)
        }
        void SaveBeforeSortSnapshot()
        {
            _beforeSort = new List<int>(_values); // sao chép mảng hiện tại
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcBaseY();
            ReflowLabelsToBaseY();
        }

        // --------------------------- VỊ TRÍ HIỂN THỊ ---------------------------
        void RecalcBaseY()
        {
            // đặt dãy ở khoảng 1/3 chiều cao form (giữa phía trên)
            baseY = Math.Max(80, this.ClientSize.Height / 3);
        }

        void ReflowLabelsToBaseY()
        {
            for (int i = 0; i < _labels.Count; i++)
                _labels[i].Top = baseY;
        }

        // --------------------------- NHẬP & UI ---------------------------
        void ModeChanged(object sender, EventArgs e)
        {
            bool isRandom = radioButton_Random.Checked;
            textBox_Input_Number_Element_RanDom.Enabled = isRandom;
            textBox_Input_Element_By_Hand.Enabled = !isRandom;
        }

        void OnlyDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        void ChangeSpeed(int delta)
        {
            delay = Math.Max(10, Math.Min(500, delay + delta));
            label_speed.Text = (550 - delay).ToString();
            UpdateMotionParams();
        }

        void ClearAll()
        {
            foreach (var lb in _labels) Controls.Remove(lb);
            _labels.Clear();
            _values.Clear();
        }

        Label MakeLabel(int v, int idx) => new Label
        {
            AutoSize = false,
            Size = new Size(50, 30),
            Text = v.ToString(),
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = normal,
            Location = new Point(startX + idx * spacing, baseY)
        };

        // --------------------------- NÚT THÊM ---------------------------
        void button_add_Click(object sender, EventArgs e)
        {
            if (radioButton_Random.Checked)
            {
                // Đọc số lượng cần thêm
                if (!int.TryParse(textBox_Input_Number_Element_RanDom.Text, out int n) || n < 1)
                {
                    MessageBox.Show("Vui lòng nhập số lượng phần tử ≥ 1",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int current = _values.Count;

                // ĐÃ ĐỦ 10 PHẦN TỬ RỒI
                if (current >= MAX_COUNT)
                {
                    MessageBox.Show("Mảng đã có đủ 10 phần tử, không thể thêm nữa.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // THÊM N NỮA SẼ VƯỢT 10 → KHÔNG CHO THÊM
                if (current + n > MAX_COUNT)
                {
                    MessageBox.Show(
                        $"Nếu thêm {n} phần tử nữa thì sẽ vượt quá {MAX_COUNT}. " +
                        "Vui lòng giảm số lượng cần thêm.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // KHÔNG CLEAR MẢNG CŨ, CHỈ APPEND VÀO CUỐI
                for (int i = 0; i < n; i++)
                {
                    int val = _rnd.Next(1, 100);
                    int idx = current + i;              // index mới
                    _values.Add(val);

                    var lb = MakeLabel(val, idx);       // label nằm kế bên mảng cũ
                    _labels.Add(lb);
                    Controls.Add(lb);
                }
            }
            else // nhập thủ công: thêm 1 phần tử
            {
                if (_values.Count >= MAX_COUNT)
                {
                    MessageBox.Show("Số phần tử tối đa là 10.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox_Input_Element_By_Hand.Text, out int v))
                {
                    MessageBox.Show("Vui lòng nhập số nguyên hợp lệ!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox_Input_Element_By_Hand.Focus();
                    return;
                }

                int idx = _values.Count;
                _values.Add(v);
                var lb = MakeLabel(v, idx);
                _labels.Add(lb);
                Controls.Add(lb);

                textBox_Input_Element_By_Hand.Clear();
                textBox_Input_Element_By_Hand.Focus();
            }

            // Cập nhật snapshot "trước khi sắp xếp"
            SaveBeforeSortSnapshot();
        }

        // --------------------------- NÚT BẮT ĐẦU ---------------------------
        async void button_start_Click(object sender, EventArgs e)
        {
            if (_values.Count == 0) return;
            if (_sort == SortType.None)
            {
                MessageBox.Show("Vui Lòng chọn thuật toán sắp xếp!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!running)
            {
                running = true;
                paused = false;
                cts = new CancellationTokenSource();

                // VÔ HIỆU HÓA NÚT THÊM KHI ĐANG CHẠY
                button_add.Enabled = false;

                try
                {
                    await RunSortAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    running = false;
                    // KHI KẾT THÚC (XONG / BỊ HỦY) THÌ BẬT LẠI NÚT THÊM
                    button_add.Enabled = true;
                }
            }
            else
            {
                // đang chạy mà bấm Start nữa thì coi như "tiếp tục"
                paused = false;
            }

            button_stop.Text = "Tạm dừng";
        }

        // --------------------------- ANIMATION CƠ BẢN ---------------------------
        async Task WaitPause(CancellationToken tk)
        {
            while (paused) await Task.Delay(30, tk);
        }

        async Task MoveAsync(Label lb, int tx, int ty, CancellationToken tk)
        {
            while (Math.Abs(lb.Left - tx) > moveStep || Math.Abs(lb.Top - ty) > moveStep)
            {
                await WaitPause(tk);
                lb.Left += Math.Sign(tx - lb.Left) * moveStep;
                lb.Top += Math.Sign(ty - lb.Top) * moveStep;
                await Task.Delay(moveSleep, tk);
            }
            lb.Left = tx;
            lb.Top = ty;
        }

        async Task RaiseAsync(Label lb, CancellationToken tk)
        {
            lb.BackColor = Color.Gold;
            await MoveAsync(lb, lb.Left, raisedY, tk);
        }
        async Task LowerAsync(Label lb, CancellationToken tk)
        {
            await MoveAsync(lb, lb.Left, baseY, tk);
            lb.BackColor = normal;
        }

        async Task SwapAnimatedAsync(int i, int j, CancellationToken tk)
        {
            if (i == j) return;
            var A = _labels[i]; var B = _labels[j];

            // đúng "hướng": đổi màu trước, rồi swap
            A.BackColor = current; B.BackColor = current;
            await Task.Delay(delay, tk);

            await Task.WhenAll(RaiseAsync(A, tk), RaiseAsync(B, tk));

            int ax = startX + i * spacing;
            int bx = startX + j * spacing;
            await Task.WhenAll(
                MoveAsync(A, bx, raisedY, tk),
                MoveAsync(B, ax, raisedY, tk)
            );

            await Task.WhenAll(LowerAsync(A, tk), LowerAsync(B, tk));

            (_labels[i], _labels[j]) = (_labels[j], _labels[i]);
            (_values[i], _values[j]) = (_values[j], _values[i]);

            A.BackColor = normal; B.BackColor = normal;
        }

        int Cmp(int a, int b) => ascending ? a.CompareTo(b) : b.CompareTo(a);

        // --------------------------- CHẠY THUẬT TOÁN ---------------------------
        async Task RunSortAsync(CancellationToken tk)
        {
            switch (_sort)
            {
                case SortType.Exchange: await ExchangeSortAsync(tk); break;
                case SortType.Selection: await SelectionSortAsync(tk); break;
                case SortType.Insertion: await InsertionSortAsync(tk); break;
                case SortType.Bubble: await BubbleSortAsync(tk); break;
                case SortType.Heap: await HeapSortAsync(tk); break;
                case SortType.Quick: await QuickSortAsync(0, _values.Count - 1, tk); break;
                case SortType.Merge: await MergeSortAsync(0, _values.Count - 1, tk); break;
            }

            // tô xanh lá toàn bộ khi xong
            for (int i = 0; i < _labels.Count; i++) _labels[i].BackColor = fixedC;
        }

        // --------------------------- O(N^2) ---------------------------
        async Task ExchangeSortAsync(CancellationToken tk)
        {
            for (int i = 0; i < _values.Count - 1; i++)
            {
                if (i > 0) _labels[i - 1].BackColor = fixedC;

                for (int j = i + 1; j < _values.Count; j++)
                {
                    _labels[i].BackColor = current;
                    _labels[j].BackColor = scan;
                    await Task.Delay(delay, tk);
                    await WaitPause(tk);

                    if (Cmp(_values[j], _values[i]) < 0)
                        await SwapAnimatedAsync(i, j, tk);

                    _labels[j].BackColor = normal;
                    _labels[i].BackColor = normal;
                }
            }
            if (_labels.Count > 0) _labels[_labels.Count - 1].BackColor = fixedC;
        }

        async Task SelectionSortAsync(CancellationToken tk)
        {
            for (int i = 0; i < _values.Count; i++)
            {
                int minIdx = i;
                _labels[i].BackColor = current; // ứng viên min

                for (int j = i + 1; j < _values.Count; j++)
                {
                    _labels[j].BackColor = scan;
                    await Task.Delay(delay, tk);
                    await WaitPause(tk);

                    if (Cmp(_values[j], _values[minIdx]) < 0)
                    {
                        if (minIdx == i) _labels[minIdx].BackColor = current;
                        else _labels[minIdx].BackColor = normal;
                        minIdx = j;
                        _labels[minIdx].BackColor = current;
                    }
                    if (j != minIdx) _labels[j].BackColor = normal;
                }

                if (minIdx != i)
                    await SwapAnimatedAsync(i, minIdx, tk);

                _labels[i].BackColor = fixedC;
            }
        }
        int XOfIndex(int idx) => startX + idx * spacing;

        async Task InsertionSortAsync(CancellationToken tk)
        {
            if (_labels.Count == 0) return;

            // phần tử đầu coi như đã “cố định”
            _labels[0].BackColor = fixedC;

            for (int i = 1; i < _values.Count; i++)
            {
                // Lấy key ra ngoài
                int keyVal = _values[i];
                Label keyLb = _labels[i];

                keyLb.BackColor = current;           // key tô đỏ (Tomato)
                await Task.Delay(delay, tk);
                await RaiseAsync(keyLb, tk);         // nhấc lên (màu vàng bên trong Raise)

                int j = i - 1;

                // Dò và dịch phải các phần tử > key (tùy chiều sort)
                while (j >= 0 && Cmp(keyVal, _values[j]) < 0)
                {
                    _labels[j].BackColor = scan;     // đang duyệt (cam)
                    await Task.Delay(delay / 2, tk);

                    // Dịch phần tử j sang phải (j -> j+1) bằng cách di chuyển label
                    await MoveAsync(_labels[j], XOfIndex(j + 1), _labels[j].Top, tk);

                    // Cập nhật mảng dữ liệu & label
                    _values[j + 1] = _values[j];
                    _labels[j + 1] = _labels[j];

                    // Trả màu ô đã dịch xong về bình thường
                    _labels[j + 1].BackColor = normal;

                    j--;
                }

                // Chèn key vào vị trí (j+1)
                int insertIdx = j + 1;
                await MoveAsync(keyLb, XOfIndex(insertIdx), raisedY, tk);
                await LowerAsync(keyLb, tk);         // hạ xuống vị trí baseY & trả màu normal trong LowerAsync

                _values[insertIdx] = keyVal;
                _labels[insertIdx] = keyLb;

                // Tô “đã cố định” từ 0..i để thấy nửa trái đã có thứ tự cục bộ (style giống bạn)
                for (int k = 0; k <= i; k++)
                    _labels[k].BackColor = fixedC;

                // Phần bên phải (i+1.. cuối) trả về màu thường
                for (int k = i + 1; k < _labels.Count; k++)
                    _labels[k].BackColor = normal;

                await Task.Delay(delay, tk);
            }

            // Kết thúc: tất cả xanh lá
            for (int k = 0; k < _labels.Count; k++)
                _labels[k].BackColor = fixedC;
        }

        async Task BubbleSortAsync(CancellationToken tk)
        {
            for (int i = 0; i < _values.Count - 1; i++)
            {
                for (int j = 0; j < _values.Count - 1 - i; j++)
                {
                    _labels[j].BackColor = scan;
                    _labels[j + 1].BackColor = scan;
                    await Task.Delay(delay, tk);
                    await WaitPause(tk);

                    if (Cmp(_values[j + 1], _values[j]) < 0)
                        await SwapAnimatedAsync(j, j + 1, tk);

                    _labels[j].BackColor = normal;
                    _labels[j + 1].BackColor = normal;
                }
                _labels[_labels.Count - 1 - i].BackColor = fixedC; // phần cuối đã cố định
            }
            if (_labels.Count > 0) _labels[0].BackColor = fixedC;
        }

        // --------------------------- O(N log N) ---------------------------
        async Task HeapSortAsync(CancellationToken tk)
        {
            int n = _values.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                await HeapifyAsync(n, i, tk);

            for (int i = n - 1; i > 0; i--)
            {
                _labels[0].BackColor = current;
                _labels[i].BackColor = current;
                await Task.Delay(delay, tk);

                await SwapAnimatedAsync(0, i, tk);
                _labels[i].BackColor = fixedC;

                await HeapifyAsync(i, 0, tk);
            }
            if (_labels.Count > 0) _labels[0].BackColor = fixedC;
        }

        async Task HeapifyAsync(int heapSize, int i, CancellationToken tk)
        {
            while (true)
            {
                await WaitPause(tk);
                int best = i;
                int l = 2 * i + 1, r = 2 * i + 2;

                _labels[i].BackColor = current;
                if (l < heapSize) _labels[l].BackColor = scan;
                if (r < heapSize) _labels[r].BackColor = scan;
                await Task.Delay(delay, tk);

                if (l < heapSize && (ascending ? _values[l] > _values[best] : _values[l] < _values[best])) best = l;
                if (r < heapSize && (ascending ? _values[r] > _values[best] : _values[r] < _values[best])) best = r;

                if (best == i)
                {
                    _labels[i].BackColor = normal;
                    if (l < heapSize) _labels[l].BackColor = normal;
                    if (r < heapSize) _labels[r].BackColor = normal;
                    break;
                }

                await SwapAnimatedAsync(i, best, tk);

                _labels[i].BackColor = normal;
                if (l < heapSize) _labels[l].BackColor = normal;
                if (r < heapSize) _labels[r].BackColor = normal;

                i = best;
            }
        }
       
        void HighlightPartition(int left, int right, Color color)
        {
            for (int i = left; i <= right && i < _labels.Count; i++)
                _labels[i].BackColor = color;
        }

        void ResetPartitionColor(int left, int right)
        {
            for (int i = left; i <= right && i < _labels.Count; i++)
                if (_labels[i].BackColor != fixedC)  // đừng ghi đè phần đã cố định
                    _labels[i].BackColor = normal;
        }
        // Giả sử trong Form có:
        // int baseY;           // Y của hàng mảng gốc
        // int raisedY => baseY - 80;  // Y khi "nhấc lên"
        // int delay;           // thời gian chờ chính
        // List<int> _values;
        // List<Label> _labels;
        // Color normal, current, scan;
        // int Cmp(int a, int b) => isAscending ? a.CompareTo(b) : b.CompareTo(a);

        async Task QuickSortAsync(int left, int right, CancellationToken tk)
        {
            if (left >= right) return;

            // --- Tô màu vùng đang phân hoạch ---
            HighlightPartition(left, right, Color.LightYellow);
            await Task.Delay(delay, tk);   // delay chính để ăn theo slider

            int len = right - left + 1;

            // Hàng phân hoạch: kéo lên cao hơn cho đỡ dính mảng trên
            int partY = Math.Max(30, baseY - 140); // dãy đang phân hoạch
            int pivotRowY = partY + 40;            // pivot thấp hơn dãy 1 đoạn

            // 1) Nhấc cả đoạn [left..right] lên hàng partY (đi dọc)
            for (int i = left; i <= right; i++)
                await MoveLabelVerticalAsync(_labels[i], partY, tk);

            // 2) Chọn pivot ở giữa
            int pivotIndexLogical = (left + right) / 2;
            int pivotVal = _values[pivotIndexLogical];
            Label pivotLabel = _labels[pivotIndexLogical];
            pivotLabel.BackColor = current;

            // Cho pivot xuống hàng riêng (cùng cột ban đầu)
            await MoveLabelVerticalAsync(pivotLabel, pivotRowY, tk);

            // 3) Chia chỉ số thành 3 nhóm theo GIÁ TRỊ: < pivot, = pivot, > pivot
            var less = new List<int>();
            var equal = new List<int>();
            var greater = new List<int>();

            for (int i = left; i <= right; i++)
            {
                if (i == pivotIndexLogical)
                {
                    equal.Add(i);  // pivot
                    continue;
                }

                int cmp = Cmp(_values[i], pivotVal);
                if (cmp < 0) less.Add(i);
                else if (cmp > 0) greater.Add(i);
                else equal.Add(i);
            }

            int lessCount = less.Count;
            int equalCount = equal.Count;
            int greaterCount = greater.Count;

            // --- TÍNH VỊ TRÍ CUỐI CÙNG CỦA PIVOT TRƯỚC ---
            int newPivotIndex = left + lessCount;      // pivot đứng ngay sau dãy < pivot
            int pivotTargetX = XOfIndex(newPivotIndex);

            // 4) Đưa pivot về đúng CỘT cuối cùng (đi dọc rồi ngang)
            await MoveLabelToAsync(pivotLabel, pivotTargetX, pivotRowY, tk);

            // 5) Xếp các phần tử NHỎ HƠN sang TRÁI pivot, cùng hàng pivotRowY
            for (int i = 0; i < lessCount; i++)
            {
                int oldIndex = less[i];
                Label lbl = _labels[oldIndex];

                int targetIndex = left + i;
                int targetX = XOfIndex(targetIndex);

                await MoveLabelToAsync(lbl, targetX, pivotRowY, tk);
            }

            // 6) Xếp các phần tử BẰNG pivot (trừ chính pivot) bên PHẢI pivot
            // equal[0] là pivot ban đầu, pivotLabel đã đứng ở newPivotIndex rồi
            for (int i = 1; i < equalCount; i++)
            {
                int oldIndex = equal[i];
                Label lbl = _labels[oldIndex];

                int targetIndex = newPivotIndex + i;   // ngay sau pivot
                int targetX = XOfIndex(targetIndex);

                await MoveLabelToAsync(lbl, targetX, pivotRowY, tk);
            }

            // 7) Xếp các phần tử LỚN HƠN pivot sang PHẢI tiếp nữa
            for (int i = 0; i < greaterCount; i++)
            {
                int oldIndex = greater[i];
                Label lbl = _labels[oldIndex];

                int targetIndex = newPivotIndex + equalCount + i;
                int targetX = XOfIndex(targetIndex);

                await MoveLabelToAsync(lbl, targetX, pivotRowY, tk);
            }

            // (không thêm delay nữa, animation tự tốn thời gian rồi)

            // 8) Cập nhật lại _values & _labels theo thứ tự [less][equal][greater]
            int[] newValues = new int[len];
            Label[] newLabels = new Label[len];
            int pos = 0;

            foreach (int idx in less)
            {
                newValues[pos] = _values[idx];
                newLabels[pos] = _labels[idx];
                pos++;
            }
            foreach (int idx in equal)
            {
                newValues[pos] = _values[idx];
                newLabels[pos] = _labels[idx];
                pos++;
            }
            foreach (int idx in greater)
            {
                newValues[pos] = _values[idx];
                newLabels[pos] = _labels[idx];
                pos++;
            }

            for (int i = 0; i < len; i++)
            {
                _values[left + i] = newValues[i];
                _labels[left + i] = newLabels[i];
            }

            // Cập nhật lại pivotLabel theo vị trí mới trong mảng
            pivotLabel = _labels[newPivotIndex];

            // 9) Hạ CẢ ĐOẠN [left..right] về hàng baseY (đi dọc + ngang)
            for (int i = left; i <= right; i++)
                await MoveLabelToAsync(_labels[i], XOfIndex(i), baseY, tk);

            // Bỏ màu pivot & reset vùng
            pivotLabel.BackColor = normal;
            ResetPartitionColor(left, right);

            // 10) Đệ quy 2 bên
            if (left < newPivotIndex - 1)
                await QuickSortAsync(left, newPivotIndex - 1, tk);
            if (newPivotIndex + 1 < right)
                await QuickSortAsync(newPivotIndex + 1, right, tk);
        }

        /// <summary>
        /// Di chuyển label theo trục dọc (Y) từng bước, không đi chéo
        /// </summary>
        private async Task MoveLabelVerticalAsync(Label lbl, int targetY, CancellationToken tk)
        {
            // X giữ nguyên, chỉ đổi Y → MoveAsync sẽ đi thẳng đứng
            await MoveAsync(lbl, lbl.Left, targetY, tk);
        }

        private async Task MoveLabelHorizontalAsync(Label lbl, int targetX, CancellationToken tk)
        {
            // Y giữ nguyên, chỉ đổi X → MoveAsync sẽ đi ngang
            await MoveAsync(lbl, targetX, lbl.Top, tk);
        }

        private async Task MoveLabelToAsync(Label lbl, int targetX, int targetY, CancellationToken tk)
        {
            // Đi dọc trước, rồi ngang sau (L-shape) nhưng dùng MoveAsync để ăn theo tốc độ chung
            await MoveLabelVerticalAsync(lbl, targetY, tk);
            await MoveLabelHorizontalAsync(lbl, targetX, tk);
        }


        async Task MergeStepAsync(int l, int m, int r, CancellationToken tk)
        {
            // Ba hàng hiển thị ở phía trên (cách baseY một đoạn)
            int rowTopLeft = Math.Max(30, baseY - 160); // hàng nửa TRÁI
            int rowTopRight = Math.Max(30, baseY - 120); // hàng nửa PHẢI
            int rowResult = Math.Max(30, baseY - 80); // hàng KẾT QUẢ (nằm dưới 2 hàng trên)

            // Snapshot 2 nửa (chưa đụng _values/_labels cho đến khi trả về)
            var leftVals = new List<int>();
            var rightVals = new List<int>();
            var leftLabels = new List<Label>();
            var rightLabels = new List<Label>();

            for (int i = l; i <= m; i++) { leftVals.Add(_values[i]); leftLabels.Add(_labels[i]); }
            for (int i = m + 1; i <= r; i++) { rightVals.Add(_values[i]); rightLabels.Add(_labels[i]); }

            // Tô màu 2 nửa để phân biệt
            foreach (var lb in leftLabels) lb.BackColor = Color.LightSkyBlue;
            foreach (var lb in rightLabels) lb.BackColor = Color.MediumPurple;

            // Nhấc lên: đi THẲNG DỌC (không rung)
            for (int i = 0; i < leftLabels.Count; i++)
                await MoveAsync(leftLabels[i], leftLabels[i].Left, rowTopLeft, tk);
            for (int j = 0; j < rightLabels.Count; j++)
                await MoveAsync(rightLabels[j], rightLabels[j].Left, rowTopRight, tk);

            await Task.Delay(delay, tk);

            // MERGE: phần tử được CHỌN rơi xuống hàng KẾT QUẢ theo L-shape (dọc rồi ngang)
            int li = 0, ri = 0, k = l;
            var mergedVals = new List<int>();
            var mergedLabels = new List<Label>();

            int XOf(int idx) => startX + idx * spacing;

            while (li < leftVals.Count && ri < rightVals.Count)
            {
                // Đánh dấu 2 phần tử đang so sánh
                leftLabels[li].BackColor = Color.Orange;
                rightLabels[ri].BackColor = Color.Orange;
                await Task.Delay(delay, tk);

                bool takeLeft = (Cmp(leftVals[li], rightVals[ri]) <= 0);
                if (takeLeft)
                {
                    // L-shape: xuống dọc -> sang ngang vào cột k
                    await MoveAsync(leftLabels[li], leftLabels[li].Left, rowResult, tk); // dọc
                    await MoveAsync(leftLabels[li], XOf(k), rowResult, tk); // ngang

                    mergedVals.Add(leftVals[li]);
                    mergedLabels.Add(leftLabels[li]);
                    li++;

                    // Trả màu phía còn lại về màu nguồn
                    if (ri < rightLabels.Count) rightLabels[ri].BackColor = Color.MediumPurple;
                }
                else
                {
                    await MoveAsync(rightLabels[ri], rightLabels[ri].Left, rowResult, tk); // dọc
                    await MoveAsync(rightLabels[ri], XOf(k), rowResult, tk); // ngang

                    mergedVals.Add(rightVals[ri]);
                    mergedLabels.Add(rightLabels[ri]);
                    ri++;

                    if (li < leftLabels.Count) leftLabels[li].BackColor = Color.LightSkyBlue;
                }
                k++;
                await Task.Delay(delay / 2, tk);
            }

            // Đổ nốt phần còn lại xuống hàng kết quả (cũng theo L-shape)
            while (li < leftVals.Count)
            {
                await MoveAsync(leftLabels[li], leftLabels[li].Left, rowResult, tk);
                await MoveAsync(leftLabels[li], XOf(k), rowResult, tk);
                mergedVals.Add(leftVals[li]);
                mergedLabels.Add(leftLabels[li]);
                li++; k++;
            }
            while (ri < rightVals.Count)
            {
                await MoveAsync(rightLabels[ri], rightLabels[ri].Left, rowResult, tk);
                await MoveAsync(rightLabels[ri], XOf(k), rowResult, tk);
                mergedVals.Add(rightVals[ri]);
                mergedLabels.Add(rightLabels[ri]);
                ri++; k++;
            }

            await Task.Delay(delay, tk);

            // TRẢ hàng kết quả về mảng gốc [l..r]: hạ THẲNG DỌC rồi giữ X đúng cột
            for (int i = 0; i < mergedLabels.Count; i++)
            {
                int idx = l + i;
                // đang ở rowResult, X đã là XOf(idx) → chỉ cần hạ dọc
                await MoveAsync(mergedLabels[i], XOf(idx), baseY, tk);
                _values[idx] = mergedVals[i];
                _labels[idx] = mergedLabels[i];
                _labels[idx].BackColor = fixedC; // đánh dấu đã merge xong đoạn này
            }

            await Task.Delay(delay, tk);
        }

        async Task MergeSortAsync(int l, int r, CancellationToken tk)
        {
            if (l >= r) return;
            int m = (l + r) / 2;

            await MergeSortAsync(l, m, tk);
            await MergeSortAsync(m + 1, r, tk);

            // TẠI MỖI BƯỚC GỘP: nhấc 2 nửa lên, merge ở hàng tạm, rồi trả về
            await MergeStepAsync(l, m, r, tk);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                // chặn Space không chèn khoảng trắng vào TextBox
                e.Handled = true;
                e.SuppressKeyPress = true;

                
                button_stop.PerformClick();   // giống như bấm nút "Tạm dừng"
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                // luôn coi Space là nhấn nút Tạm dừng
                if (button_stop.Enabled && button_stop.Visible)
                    button_stop.PerformClick();

                // trả về true => đã xử lý xong, không cho controls khác nhận nữa
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
