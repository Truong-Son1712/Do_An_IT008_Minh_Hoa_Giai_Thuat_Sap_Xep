using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    public partial class Form1 : Form
    {
        readonly List<Label> Mang_labels = new List<Label>();
        readonly List<int> Mang_Gia_Tri = new List<int>();
        List<int> Mang_Pre_Sort = new List<int>();
        List<Lich_su_Sap_Xep> Danh_Sach_Ban_Ghi = new List<Lich_su_Sap_Xep>(15);
        readonly Random random_so = new Random();

        int Le_trai_X = 60; // lề trái cho dãy
        int Toa_Do_Y_Defaut;  //tạo độ Y mặc định khi khởi tạo                
        int Nhac_Y => Toa_Do_Y_Defaut - 80;  // Vị trí tọa độ Y của label khi nhấc nó lên
        int khoang_cach_label = 70; 
        const int so_phan_tu_max = 10;

        int duyet_delay = 200;  // donvi la ms 
        bool is_tang_dan = true;
        bool is_dung_lai = false;
        bool is_running = false;

        double scale_duyet_move = 1.6;
        int van_toc_di_chuyen = 4;   // dv tinh px/tick
        int do_tre = 10; // độ trễ giữa các “tick” di chuyển, donvi la ms 

        CancellationTokenSource CTS;

        readonly Color Mac_Dinh = Color.LightBlue;    // mặc định
        readonly Color Dang_Duyet = Color.Orange;       // đang duyệt
        readonly Color Ung_Vien = Color.Tomato;       // ứng viên hiện tại
        readonly Color Da_co_dinh = Color.LightGreen;   // đã cố định
        
  
        enum Loai_Sap_Xep { None, Exchange, Selection, Insertion, Bubble, Heap, Quick, Merge }
        Loai_Sap_Xep Sap_xep = Loai_Sap_Xep.None;


        float Goc_quay = 0f; // góc quay (radian)
        bool is_xoay = false;

        public class Lich_su_Sap_Xep
        {
            public List<int> DS_DuLieu { get; set; }      
            public DateTime Thoi_Gian_ghi { get; set; }           
            public string Chieu_Sap_Xep { get; set; }        
            public string Giai_Thuat { get; set; }      

            public Lich_su_Sap_Xep(List<int> values, string direction, string algorithm)
            {
                DS_DuLieu = new List<int>(values); 
                Thoi_Gian_ghi = DateTime.Now;
                Chieu_Sap_Xep = direction;
                Giai_Thuat = algorithm;
            }
        }
        void Save_Du_Lieu()
        {
            if (Mang_Gia_Tri.Count == 0) return;
            if (Sap_xep == Loai_Sap_Xep.None) return;

            string Chieu_SX = is_tang_dan ? "Tăng Dần" : "Giảm Dần";
            string Loai_Thuat_Toan = Sap_xep.ToString();

            if (Danh_Sach_Ban_Ghi.Count >= 15)
                Danh_Sach_Ban_Ghi.RemoveAt(Danh_Sach_Ban_Ghi.Count - 1);

            Danh_Sach_Ban_Ghi.Insert(0, new Lich_su_Sap_Xep(Mang_Gia_Tri, Chieu_SX, Loai_Thuat_Toan));
        }
        public Form1()
        {
            InitializeComponent();
            // xử lý mượt cho ngôi sao
            this.DoubleBuffered = true;
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                groupBox1,
                new object[] { true });

            _ = Thay_Doi_Goc_xoay();
            Invalidate();

            XuLy_ViTri_Y();
           
            Khoa_Lua_Chon_InPut_Data(null, EventArgs.Empty);
            label_speed.Text = (550 - duyet_delay).ToString();
            Dieu_chinh_toc_do();
        }
        void Dieu_chinh_toc_do()
        {
            // s: 0..1 (delay nhỏ => s lớn => nhanh hơn)
            double s = (550 - duyet_delay) / 540.0;

            // Bước nhảy nhỏ và tăng dần theo speed: 2..10 px/tick
            van_toc_di_chuyen = 2 + (int)Math.Round(8 * s);
            if (van_toc_di_chuyen < 2) van_toc_di_chuyen = 2;

            // Thời gian nghỉ mỗi tick giảm theo speed: 14..4 ms/tick
            int Thoi_gian_nghi = 14 - (int)Math.Round(10 * s);
            if (Thoi_gian_nghi < 2) Thoi_gian_nghi = 2;

            // Áp hệ số người dùng (nhỏ hơn -> nhanh hơn)
            do_tre = (int)Math.Round(Thoi_gian_nghi * scale_duyet_move);
            if (do_tre < 2) do_tre = 2;      // sàn an toàn
        }
        void Luu_Data_Gan_Nhat()
        {
            Mang_Pre_Sort = new List<int>(Mang_Gia_Tri); 
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            XuLy_ViTri_Y();
            Cap_nhat_vi_tri_Y();
        }
        void XuLy_ViTri_Y()
        {
            Toa_Do_Y_Defaut = Math.Max(80, this.ClientSize.Height / 3);
        }
        void Cap_nhat_vi_tri_Y()
        {
            for (int i = 0; i < Mang_labels.Count; i++)
                Mang_labels[i].Top = Toa_Do_Y_Defaut;
        }
        void Khoa_Lua_Chon_InPut_Data(object sender, EventArgs e)
        {
            bool is_Nhap_Tu_Dong = radioButton_Random.Checked;
            textBox_Input_Number_Element_RanDom.Enabled = is_Nhap_Tu_Dong;
            textBox_Input_Element_By_Hand.Enabled = !is_Nhap_Tu_Dong;
        }
        void Change_Van_toc_duyet(int delta)
        {
            duyet_delay = Math.Max(10, Math.Min(500, duyet_delay + delta));
            label_speed.Text = (550 - duyet_delay).ToString();
            Dieu_chinh_toc_do();
        }
        void Xoa_Tat_ca_Label()
        {
            var Xoa_label = new List<Control>();

            foreach (Control c in Controls)
            {
                if (c is Label lb && Equals(lb.Tag, "Phan_tu_mang"))
                {
                    Xoa_label.Add(lb);
                }
            }
            foreach (var ctl in Xoa_label)
            {
                Controls.Remove(ctl);
                ctl.Dispose();   // cho sạch luôn
            }
            Mang_labels.Clear();
            Mang_Gia_Tri.Clear();
        }
        Label Khoi_tao_Label(int v, int idx) => new Label
        {
            AutoSize = false,
            Size = new Size(50, 30),
            Text = v.ToString(),
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Mac_Dinh,
            Location = new Point(Le_trai_X + idx * khoang_cach_label, Toa_Do_Y_Defaut),
            Tag = "Phan_tu_mang"  
        };
        async Task Tam_dung(CancellationToken tk)
        {
            while (is_dung_lai) await Task.Delay(30, tk); //chờ 30ms mỗi vòng lặp
        }
        async Task Di_Chuyen_pos_moi(Label lb, int tx, int ty, CancellationToken tk)
        {
            while (Math.Abs(lb.Left - tx) > van_toc_di_chuyen || Math.Abs(lb.Top - ty) > van_toc_di_chuyen)
            {
                await Tam_dung(tk);
                lb.Left += Math.Sign(tx - lb.Left) * van_toc_di_chuyen;
                lb.Top += Math.Sign(ty - lb.Top) * van_toc_di_chuyen;
                await Task.Delay(do_tre, tk);
            }
            lb.Left = tx;
            lb.Top = ty;
        }
        async Task Nhac_Label_len(Label lb,CancellationToken tk)
        {
            lb.BackColor = Color.Gold;
            await Di_Chuyen_pos_moi(lb, lb.Left, Nhac_Y, tk);
        }
        async Task Ha_Label_Xuong(Label lb ,CancellationToken tk)
        {
            await Di_Chuyen_pos_moi(lb, lb.Left,Toa_Do_Y_Defaut, tk);
            lb.BackColor = Mac_Dinh;
        }
        private async Task Di_Chuyen_label(Label lbl, int targetX, int targetY, CancellationToken tk)
        {
            await Di_Chuyen_pos_moi(lbl, lbl.Left, targetY, tk);
            await Di_Chuyen_pos_moi(lbl, targetX, lbl.Top, tk);
        }
        async Task Hoan_vi_Label(int i, int j, CancellationToken tk)
        {
            if (i == j) return;
            var A = Mang_labels[i]; var B = Mang_labels[j];

            A.BackColor = Ung_Vien; B.BackColor = Ung_Vien;
            await Task.Delay(duyet_delay, tk);

            await Task.WhenAll(Nhac_Label_len(A ,tk), Nhac_Label_len(B,tk));

            int ax = Le_trai_X + i * khoang_cach_label;
            int bx = Le_trai_X + j * khoang_cach_label;
            await Task.WhenAll(
                Di_Chuyen_pos_moi(A, bx, Nhac_Y, tk),
                Di_Chuyen_pos_moi(B, ax, Nhac_Y, tk)
            );

            await Task.WhenAll(Ha_Label_Xuong(A,tk), Ha_Label_Xuong(B, tk));

            (Mang_labels[i], Mang_labels[j]) = (Mang_labels[j], Mang_labels[i]);
            (Mang_Gia_Tri[i], Mang_Gia_Tri[j]) = (Mang_Gia_Tri[j], Mang_Gia_Tri[i]);

            A.BackColor = Mac_Dinh; B.BackColor = Mac_Dinh;
        }
        async Task Run_Giai_Thuat_Sap_Xep(CancellationToken tk)
        {
            switch (Sap_xep)
            {
                case Loai_Sap_Xep.Exchange: await Exchange_sort(tk); break;
                case Loai_Sap_Xep.Selection: await Selection_Sort(tk); break;
                case Loai_Sap_Xep.Insertion: await Insertion_Sort(tk); break;
                case Loai_Sap_Xep.Bubble: await Bubble_Sort(tk); break;
                case Loai_Sap_Xep.Heap: await Heap_Sort(tk); break;
                case Loai_Sap_Xep.Quick: await Quick_Sort(0, Mang_Gia_Tri.Count - 1, tk); break;
                case Loai_Sap_Xep.Merge: await Merge_Sort(0, Mang_Gia_Tri.Count - 1, tk); break;
            }
            for (int i = 0; i < Mang_labels.Count; i++) Mang_labels[i].BackColor = Da_co_dinh;
        }
        int So_sanh(int a, int b) => is_tang_dan ? a.CompareTo(b) : b.CompareTo(a);
        async Task Exchange_sort(CancellationToken tk)
        {
            for (int i = 0; i < Mang_Gia_Tri.Count - 1; i++)
            {
                if (i > 0) Mang_labels[i - 1].BackColor = Da_co_dinh;

                for (int j = i + 1; j < Mang_Gia_Tri.Count; j++)
                {
                    Mang_labels[i].BackColor = Ung_Vien;
                    Mang_labels[j].BackColor = Dang_Duyet;
                    await Task.Delay(duyet_delay, tk);
                    await Tam_dung(tk);

                    if (So_sanh(Mang_Gia_Tri[j], Mang_Gia_Tri[i]) < 0)
                        await Hoan_vi_Label(i, j, tk);

                    Mang_labels[j].BackColor = Mac_Dinh;
                    Mang_labels[i].BackColor = Mac_Dinh;
                }
            }
            if (Mang_labels.Count > 0) Mang_labels[Mang_labels.Count - 1].BackColor = Da_co_dinh;
        }
        async Task Selection_Sort(CancellationToken tk)
        {
            for (int i = 0; i < Mang_Gia_Tri.Count; i++)
            {
                int index_ung_cu_vien = i;
                Mang_labels[i].BackColor = Ung_Vien; // ứng viên min

                for (int j = i + 1; j < Mang_Gia_Tri.Count; j++)
                {
                    Mang_labels[j].BackColor = Dang_Duyet;
                    await Task.Delay(duyet_delay, tk);
                    await Tam_dung(tk);

                    if (So_sanh(Mang_Gia_Tri[j], Mang_Gia_Tri[index_ung_cu_vien]) < 0)
                    {
                        if (index_ung_cu_vien == i) Mang_labels[index_ung_cu_vien].BackColor = Ung_Vien;
                        else Mang_labels[index_ung_cu_vien].BackColor = Mac_Dinh;
                        index_ung_cu_vien = j;
                        Mang_labels[index_ung_cu_vien].BackColor = Ung_Vien;
                    }
                    if (j != index_ung_cu_vien) Mang_labels[j].BackColor = Mac_Dinh;
                }

                if (index_ung_cu_vien != i)
                    await Hoan_vi_Label(i, index_ung_cu_vien, tk);

                Mang_labels[i].BackColor = Da_co_dinh;
            }
        }
        int Toa_do_label(int idx) => Le_trai_X + idx * khoang_cach_label;

        async Task Insertion_Sort(CancellationToken tk)
        {
            if (Mang_labels.Count == 0) return;
            Mang_labels[0].BackColor = Da_co_dinh;

            for (int i = 1; i < Mang_Gia_Tri.Count; i++)
            {
                int Gia_tri_dang_xet = Mang_Gia_Tri[i];
                Label Label_dang_xet = Mang_labels[i];

                Label_dang_xet.BackColor = Ung_Vien;         
                await Task.Delay(duyet_delay, tk);
                await Nhac_Label_len(Label_dang_xet, tk);        

                int j = i - 1;

                while (j >= 0 && So_sanh(Gia_tri_dang_xet, Mang_Gia_Tri[j]) < 0)
                {
                    Mang_labels[j].BackColor = Dang_Duyet;     
                    await Task.Delay(duyet_delay / 2, tk);

                    // Dịch phần tử j sang phải (j -> j+1) 
                    await Di_Chuyen_pos_moi(Mang_labels[j], Toa_do_label(j + 1), Mang_labels[j].Top, tk);

                    Mang_Gia_Tri[j + 1] = Mang_Gia_Tri[j];
                    Mang_labels[j + 1] = Mang_labels[j];

                    Mang_labels[j + 1].BackColor = Mac_Dinh;
                    j--;
                }
                // Chèn key vào vị trí (j+1)
                int Vi_tri_chen = j + 1;
                await Di_Chuyen_pos_moi(Label_dang_xet, Toa_do_label(Vi_tri_chen), Nhac_Y, tk);
                await Ha_Label_Xuong(Label_dang_xet, tk);         
                Mang_Gia_Tri[Vi_tri_chen] = Gia_tri_dang_xet;
                Mang_labels[Vi_tri_chen] = Label_dang_xet;

                for (int k = 0; k <= i; k++) //tô màu những label đã xét về bình thường
                    Mang_labels[k].BackColor = Da_co_dinh;

                for (int k = i + 1; k < Mang_labels.Count; k++)
                    Mang_labels[k].BackColor = Mac_Dinh;

                await Task.Delay(duyet_delay, tk);
            }
            for (int k = 0; k < Mang_labels.Count; k++)
                Mang_labels[k].BackColor = Da_co_dinh;
        }
        async Task Bubble_Sort(CancellationToken tk)
        {
            for (int i = 0; i < Mang_Gia_Tri.Count - 1; i++)
            {
                for (int j = 0; j < Mang_Gia_Tri.Count - 1 - i; j++)
                {
                    Mang_labels[j].BackColor = Dang_Duyet;
                    Mang_labels[j + 1].BackColor = Dang_Duyet;
                    await Task.Delay(duyet_delay, tk);
                    await Tam_dung(tk);

                    if (So_sanh(Mang_Gia_Tri[j + 1], Mang_Gia_Tri[j]) < 0)
                        await Hoan_vi_Label(j, j + 1, tk);

                    Mang_labels[j].BackColor = Mac_Dinh;
                    Mang_labels[j + 1].BackColor = Mac_Dinh;
                }
                Mang_labels[Mang_labels.Count - 1 - i].BackColor = Da_co_dinh; // phần cuối đã cố định
            }
            if (Mang_labels.Count > 0) Mang_labels[0].BackColor = Da_co_dinh;
        }
        async Task Heap_Sort(CancellationToken tk)
        {
            int n = Mang_Gia_Tri.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                await Heapify(n, i, tk);

            for (int i = n - 1; i > 0; i--)
            {
                Mang_labels[0].BackColor = Ung_Vien;
                Mang_labels[i].BackColor = Ung_Vien;
                await Task.Delay(duyet_delay, tk);

                await Hoan_vi_Label(0, i, tk);
                Mang_labels[i].BackColor = Da_co_dinh;

                await Heapify(i, 0, tk);
            }
            if (Mang_labels.Count > 0) Mang_labels[0].BackColor = Da_co_dinh;
        }
        async Task Heapify(int heapSize, int i, CancellationToken tk)
        {
            while (true)
            {
                await Tam_dung(tk);
                int goc = i;
                int l = 2 * i + 1, r = 2 * i + 2;

                Mang_labels[i].BackColor = Ung_Vien;
                if (l < heapSize) Mang_labels[l].BackColor = Dang_Duyet;
                if (r < heapSize) Mang_labels[r].BackColor = Dang_Duyet;
                await Task.Delay(duyet_delay, tk);

                if (l < heapSize && (is_tang_dan ? Mang_Gia_Tri[l] > Mang_Gia_Tri[goc] : Mang_Gia_Tri[l] < Mang_Gia_Tri[goc])) goc = l;
                if (r < heapSize && (is_tang_dan ? Mang_Gia_Tri[r] > Mang_Gia_Tri[goc] : Mang_Gia_Tri[r] < Mang_Gia_Tri[goc])) goc = r;

                if (goc == i)
                {
                    Mang_labels[i].BackColor = Mac_Dinh;
                    if (l < heapSize) Mang_labels[l].BackColor = Mac_Dinh;
                    if (r < heapSize) Mang_labels[r].BackColor = Mac_Dinh;
                    break;
                }

                await Hoan_vi_Label(i, goc, tk);

                Mang_labels[i].BackColor = Mac_Dinh;
                if (l < heapSize) Mang_labels[l].BackColor = Mac_Dinh;
                if (r < heapSize) Mang_labels[r].BackColor = Mac_Dinh;

                i = goc;
            }
        }
        void Highlight_Phan_Tu_Trong_Doan(int left, int right, Color color)
        {
            for (int i = left; i <= right && i < Mang_labels.Count; i++)
                Mang_labels[i].BackColor = color;
        }
        async Task Quick_Sort(int left, int right, CancellationToken tk)
        {
            if (left >= right) return;

            Highlight_Phan_Tu_Trong_Doan(left, right, Color.LightYellow); //tô màu đoạn đang phân hoạch
            await Task.Delay(duyet_delay, tk); 

            int len = right - left + 1;

            int pos_y_truoc_phan_hoach = Math.Max(30, Toa_Do_Y_Defaut - 140); //kéo dãy đang phân hoạch lên 
            int pos_y_phan_hoach = pos_y_truoc_phan_hoach + 40;     //khi phân hoạch thì hạ xuống 1 đoạn

            // raise đoạn phân hoạch lên 
            for (int i = left; i <= right; i++)
                await Di_Chuyen_pos_moi(Mang_labels[i], Mang_labels[i].Left, pos_y_truoc_phan_hoach, tk);

            int Root_Phan_Hoach = (left + right) / 2;
            int Gia_Tri_Root = Mang_Gia_Tri[Root_Phan_Hoach];
            Label Label_Root = Mang_labels[Root_Phan_Hoach];
            Label_Root.BackColor = Ung_Vien;
            await Di_Chuyen_pos_moi(Label_Root, Label_Root.Left, pos_y_phan_hoach, tk);

            var be_hon = new List<int>();
            var bang = new List<int>();
            var lon_hon = new List<int>();

            for (int i = left; i <= right; i++)
            {
                if (i == Root_Phan_Hoach)
                {
                    bang.Add(i); 
                    continue;
                }
                int cmp = So_sanh(Mang_Gia_Tri[i], Gia_Tri_Root);
                if (cmp < 0) be_hon.Add(i);
                else if (cmp > 0) lon_hon.Add(i);
                else bang.Add(i);
            }

            int so_phan_tu_be_hon = be_hon.Count;
            int so_phan_tu_bang = bang.Count;

            int Vi_Tri_Root_sau_PH = left + so_phan_tu_be_hon;      // vị trí root sau khi phân hoạch
            int Toa_Do_X_Root_Sau_PH = Toa_do_label(Vi_Tri_Root_sau_PH); //tọa độ root sau phân hoạch

            await Di_Chuyen_label(Label_Root, Toa_Do_X_Root_Sau_PH, pos_y_phan_hoach, tk);
           
            int ViTri_ke_tiep_ben_trai = left;                       // vị trí phần tử bé hơn root
            int ViTri_Ke_Tiep_Ptu_bang = Vi_Tri_Root_sau_PH + 1;          // vị trí phần tử bằng root
            int ViTri_Ke_Tiep_Ben_Phai = Vi_Tri_Root_sau_PH + so_phan_tu_bang; // vị trí phần tử lớn hơn root

            for (int i = left; i <= right; i++)
            {
                if (i == Root_Phan_Hoach)
                    continue; 

                Label lbl = Mang_labels[i];
                int cmp = So_sanh(Mang_Gia_Tri[i], Gia_Tri_Root);

                int index_phan_tu_dang_xet;
                if (cmp < 0)
                {
                    index_phan_tu_dang_xet = ViTri_ke_tiep_ben_trai++;
                }
                else if (cmp > 0)
                {
                    index_phan_tu_dang_xet = ViTri_Ke_Tiep_Ben_Phai++;
                }
                else
                {
                    index_phan_tu_dang_xet = ViTri_Ke_Tiep_Ptu_bang++;
                }
                int Toa_Do_Phan_Tu_Dang_Xet = Toa_do_label(index_phan_tu_dang_xet);
                await Di_Chuyen_label(lbl, Toa_Do_Phan_Tu_Dang_Xet, pos_y_phan_hoach, tk);
                lbl.BackColor = Color.Aquamarine;
            }

            //cập nhật giá trị  mới của label 
            int[] Cap_nhat_gia_tri = new int[len];
            Label[] Cap_nhat_label = new Label[len];
            int pos = 0;

            foreach (int idx in be_hon)
            {
                Cap_nhat_gia_tri[pos] = Mang_Gia_Tri[idx];
                Cap_nhat_label[pos] = Mang_labels[idx];
                pos++;
            }
            foreach (int idx in bang)
            {
                Cap_nhat_gia_tri[pos] = Mang_Gia_Tri[idx];
                Cap_nhat_label[pos] = Mang_labels[idx];
                pos++;
            }
            foreach (int idx in lon_hon)
            {
                Cap_nhat_gia_tri[pos] = Mang_Gia_Tri[idx];
                Cap_nhat_label[pos] = Mang_labels[idx];
                pos++;
            }

            for (int i = 0; i < len; i++)
            {
                Mang_Gia_Tri[left + i] = Cap_nhat_gia_tri[i];
                Mang_labels[left + i] = Cap_nhat_label[i];
            }

            Label_Root = Mang_labels[Vi_Tri_Root_sau_PH];

            //hạ toàn bộ đoạn phân hoạch xuống lại mảng cũ
            for (int i = left; i <= right; i++)
            {
                await Di_Chuyen_label(Mang_labels[i], Toa_do_label(i), Toa_Do_Y_Defaut, tk);
                Mang_labels[i].BackColor = Mac_Dinh;
            }
            Label_Root.BackColor = Mac_Dinh;

            if (left < Vi_Tri_Root_sau_PH - 1)    //phân hoạch mảng bên trái node root
                await Quick_Sort(left, Vi_Tri_Root_sau_PH - 1, tk);
            if (Vi_Tri_Root_sau_PH + 1 < right)
                await Quick_Sort(Vi_Tri_Root_sau_PH + 1, right, tk);  //phân hoạch mảng bên phải root
        }
        async Task Merge_2_array(int l, int m, int r, CancellationToken tk)
        {
            int Toa_do_Y_truoc_merge = Math.Max(30, Toa_Do_Y_Defaut - 140);   // tọa độ khi nhấc 2 mảng lên cao để merge
            int Toa_do_Y_sau_merge = Math.Max(30, Toa_Do_Y_Defaut - 80); // tọa độ khi hạ xuống để merge

            var Gia_Tri_Mang_Trai = new List<int>();
            var Gia_Tri_Mang_Phai = new List<int>();
            var Mang_Label_Trai = new List<Label>();
            var Mang_Label_Phai = new List<Label>();

            for (int i = l; i <= m; i++)
            {
                Gia_Tri_Mang_Trai.Add(Mang_Gia_Tri[i]);
                Mang_Label_Trai.Add(Mang_labels[i]);
            }
            for (int i = m + 1; i <= r; i++)
            {
                Gia_Tri_Mang_Phai.Add(Mang_Gia_Tri[i]);
                Mang_Label_Phai.Add(Mang_labels[i]);
            }
            // Tô màu 2 nửa để phân biệt
            foreach (var lb in Mang_Label_Trai) lb.BackColor = Color.LightSkyBlue;
            foreach (var lb in Mang_Label_Phai) lb.BackColor = Color.MediumPurple;

            //nhấc mảng đang merge lên
            for (int i = 0; i < Mang_Label_Trai.Count; i++)
                await Di_Chuyen_pos_moi(Mang_Label_Trai[i], Mang_Label_Trai[i].Left, Toa_do_Y_truoc_merge, tk);

            for (int j = 0; j < Mang_Label_Phai.Count; j++)
                await Di_Chuyen_pos_moi(Mang_Label_Phai[j], Mang_Label_Phai[j].Left, Toa_do_Y_truoc_merge, tk);

            await Task.Delay(duyet_delay, tk);

            int li = 0, ri = 0, k = l;
            var Mang_gia_tri_ptu_da_merge = new List<int>();
            var Mang_Label_ptu_da_merge = new List<Label>();

            while (li < Gia_Tri_Mang_Trai.Count && ri < Gia_Tri_Mang_Phai.Count)
            {
                // Đánh dấu 2 phần tử đang so sánh
                Mang_Label_Trai[li].BackColor = Color.Orange;
                Mang_Label_Phai[ri].BackColor = Color.Orange;
                await Task.Delay(duyet_delay, tk);

                bool Chon_Phan_Tu_Mang_Trai = (So_sanh(Gia_Tri_Mang_Trai[li], Gia_Tri_Mang_Phai[ri]) <= 0);
                if (Chon_Phan_Tu_Mang_Trai)
                {
                    await Di_Chuyen_label(Mang_Label_Trai[li], Toa_do_label(k), Toa_do_Y_sau_merge, tk);
                    Mang_gia_tri_ptu_da_merge.Add(Gia_Tri_Mang_Trai[li]);
                    Mang_Label_ptu_da_merge.Add(Mang_Label_Trai[li]);
                   
                    li++;
                    if (ri < Mang_Label_Phai.Count)
                        Mang_Label_Phai[ri].BackColor = Color.MediumPurple;
                }
                else
                {
                    await Di_Chuyen_label(Mang_Label_Phai[ri], Toa_do_label(k), Toa_do_Y_sau_merge, tk);
                    Mang_gia_tri_ptu_da_merge.Add(Gia_Tri_Mang_Phai[ri]);
                    Mang_Label_ptu_da_merge.Add(Mang_Label_Phai[ri]);
                    ri++;

                    if (li < Mang_Label_Trai.Count)
                        Mang_Label_Trai[li].BackColor = Color.LightSkyBlue;
                }
                Mang_Label_ptu_da_merge[Mang_Label_ptu_da_merge.Count-1].BackColor = Color.Aquamarine;
                k++;
                await Task.Delay(duyet_delay / 2, tk);
            }
            while (li < Gia_Tri_Mang_Trai.Count)
            {
                await Di_Chuyen_label(Mang_Label_Trai[li], Toa_do_label(k), Toa_do_Y_sau_merge, tk);
                Mang_gia_tri_ptu_da_merge.Add(Gia_Tri_Mang_Trai[li]);
                Mang_Label_ptu_da_merge.Add(Mang_Label_Trai[li]);
                Mang_Label_ptu_da_merge[Mang_Label_ptu_da_merge.Count - 1].BackColor = Color.Aquamarine;
                li++;
                k++;
            }
            while (ri < Gia_Tri_Mang_Phai.Count)
            {
                await Di_Chuyen_label(Mang_Label_Phai[ri], Toa_do_label(k), Toa_do_Y_sau_merge, tk);
                Mang_gia_tri_ptu_da_merge.Add(Gia_Tri_Mang_Phai[ri]);
                Mang_Label_ptu_da_merge.Add(Mang_Label_Phai[ri]);
                Mang_Label_ptu_da_merge[Mang_Label_ptu_da_merge.Count - 1].BackColor = Color.Aquamarine;
                ri++;
                k++;
            }
            await Task.Delay(duyet_delay, tk);

            for (int i = 0; i < Mang_Label_ptu_da_merge.Count; i++)
            {
                int idx = l + i;
                await Di_Chuyen_pos_moi(Mang_Label_ptu_da_merge[i], Toa_do_label(idx), Toa_Do_Y_Defaut, tk);

                Mang_Gia_Tri[idx] = Mang_gia_tri_ptu_da_merge[i];
                Mang_labels[idx] = Mang_Label_ptu_da_merge[i];
                Mang_labels[idx].BackColor = Da_co_dinh; 
            }
            await Task.Delay(duyet_delay, tk);
        }
        async Task Merge_Sort(int l, int r, CancellationToken tk)
        {
            if (l >= r) return;
            int m = (l + r) / 2;
            await Merge_Sort(l, m, tk);
            await Merge_Sort(m + 1, r, tk);
            await Merge_2_array(l, m, r, tk);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                button_stop.PerformClick();   
            }
            if (e.KeyCode == Keys.Delete)
            {
                button_reset.PerformClick(); 
            }
            if(e.KeyCode == Keys.F5)
            {
                button_start.PerformClick();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                if (button_stop.Enabled && button_stop.Visible)
                    button_stop.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private async Task Thay_Doi_Goc_xoay()
        {
            if (is_xoay) return; 
            is_xoay = true;

            while (is_xoay)
            {
                Goc_quay += 0.1f;   
                if (Goc_quay > Math.PI * 2)
                    Goc_quay -= (float)(Math.PI * 2);
                groupBox1.Invalidate();  
                await Task.Delay(30);   // tốc độ quay (ms)
            }
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float x = 750, y = 32;

            //vẽ hình tròn đỏ làm nền
            float Ban_kinh_hinh_tron = 15;
            g.FillEllipse(Brushes.IndianRed, x - Ban_kinh_hinh_tron, y - Ban_kinh_hinh_tron, Ban_kinh_hinh_tron * 2, Ban_kinh_hinh_tron * 2);
            g.DrawEllipse(Pens.Black, x - Ban_kinh_hinh_tron, y - Ban_kinh_hinh_tron, Ban_kinh_hinh_tron * 2, Ban_kinh_hinh_tron * 2);

            // vẽ ngôi sao vàng
            float R = 10;
            PointF[] DS_Diem = new PointF[10];

            DS_Diem[0] = new PointF(x, y - R);
            float t1 = R * (float)Math.Cos(2 * Math.PI / 5);
            float t2 = R * (float)Math.Sin(2 * Math.PI / 5);
            DS_Diem[2] = new PointF(x - t2, y - t1);
            DS_Diem[8] = new PointF(x + t2, y - t1);
            float t3 = R * (float)Math.Cos(Math.PI / 5);
            float t4 = R * (float)Math.Sin(Math.PI / 5);
            DS_Diem[4] = new PointF(x - t4, y + t3);
            DS_Diem[6] = new PointF(x + t4, y + t3);
            float t5 = t1 * (float)Math.Tan(Math.PI / 5);
            DS_Diem[1] = new PointF(x - t5, y - t1);
            DS_Diem[9] = new PointF(x + t5, y - t1);
            float r = t1 / (float)Math.Cos(Math.PI / 5);
            float t6 = r * (float)Math.Cos(2 * Math.PI / 5);
            float t7 = r * (float)Math.Sin(2 * Math.PI / 5);
            DS_Diem[3] = new PointF(x - t7, y + t6);
            DS_Diem[7] = new PointF(x + t7, y + t6);
            DS_Diem[5] = new PointF(x, y + r);

            // xoay ngôi sao
            PointF[] Xoay = new PointF[DS_Diem.Length];
            float cosA = (float)Math.Cos(Goc_quay);
            float sinA = (float)Math.Sin(Goc_quay);

            for (int i = 0; i < DS_Diem.Length; i++)
            {
                float px = DS_Diem[i].X;
                float py = DS_Diem[i].Y;

                // chuyển về hệ tọa độ tâm (0,0)
                float dx = px - x;
                float dy = py - y;

                // xoay 
                float rx = dx * cosA - dy * sinA;
                float ry = dx * sinA + dy * cosA;

                // chuyển lại về (x,y)
                Xoay[i] = new PointF(x + rx, y + ry);
            }
            g.FillPolygon(Brushes.Gold, Xoay);
            g.DrawPolygon(Pens.Black, Xoay);
        }
        private void button_history_Click(object sender, EventArgs e)
        {
            var f = new Form2(Danh_Sach_Ban_Ghi);
            f.StartPosition = FormStartPosition.CenterParent;

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                var item = f.Ban_Ghi_Duoc_Chon;
                if (item != null)
                {
                    Apply_Ban_Ghi_Tu_Lich_Su(item);
                }
            }
        }
        private void Apply_Ban_Ghi_Tu_Lich_Su(Lich_su_Sap_Xep Du_Lieu)
        {
            is_dung_lai = false;
            if (is_running)
            {
                CTS?.Cancel();
                is_running = false;
            }
            Xoa_Tat_ca_Label();

            Mang_Gia_Tri.AddRange(Du_Lieu.DS_DuLieu);
            for (int i = 0; i < Mang_Gia_Tri.Count; i++)
            {
                var lb = Khoi_tao_Label(Mang_Gia_Tri[i], i);
                Mang_labels.Add(lb);
                Controls.Add(lb);
            }
            //khôi phục chiều sx
            is_tang_dan = Du_Lieu.Chieu_Sap_Xep == "Tăng Dần";
            radioButton_Increase.Checked = is_tang_dan;
            radioButton_Decrease.Checked = !is_tang_dan;
            // khôi phục loại thuật toán 
            switch (Du_Lieu.Giai_Thuat)
            {
                case "Exchange":
                    Sap_xep = Loai_Sap_Xep.Exchange;
                    radioButton_Exchange_Sort.Checked = true;
                    break;

                case "Selection":
                    Sap_xep = Loai_Sap_Xep.Selection;
                    radioButton_Selection_Sort.Checked = true;
                    break;

                case "Insertion":
                    Sap_xep = Loai_Sap_Xep.Insertion;
                    radioButton_Insertion_Sort.Checked = true;
                    break;

                case "Bubble":
                    Sap_xep = Loai_Sap_Xep.Bubble;
                    radioButton_Bubble_Sort.Checked = true;
                    break;

                case "Heap":
                    Sap_xep = Loai_Sap_Xep.Heap;
                    radioButton_Heap_Sort.Checked = true;
                    break;

                case "Quick":
                    Sap_xep = Loai_Sap_Xep.Quick;
                    radioButton_Quick_Sort.Checked = true;
                    break;

                case "Merge":
                    Sap_xep = Loai_Sap_Xep.Merge;
                    radioButton_Merge_Sort.Checked = true;
                    break;
            }
            Luu_Data_Gan_Nhat();
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            if (radioButton_Random.Checked)
            {
                int so_phantu;
                string tmp = textBox_Input_Number_Element_RanDom.Text;
                bool is_songuyen = int.TryParse(tmp, out so_phantu);
                if (!is_songuyen)
                {
                    MessageBox.Show("Vui lòng nhập số phần tử của mảng trong đoạn [1, 10].",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int So_Phan_Tu_Hien_Co = Mang_Gia_Tri.Count;

                if (So_Phan_Tu_Hien_Co >= so_phan_tu_max)
                {
                    MessageBox.Show("Mảng đã có đủ 10 phần tử, không thể thêm nữa.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (So_Phan_Tu_Hien_Co + so_phantu > so_phan_tu_max)
                {
                    MessageBox.Show(
                        $"Nếu thêm {so_phantu} phần tử nữa thì sẽ vượt quá {so_phan_tu_max}. " +
                        "Vui lòng giảm số lượng cần thêm.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < so_phantu; i++)
                {
                    int val = random_so.Next(1, 100);
                    int idx = So_Phan_Tu_Hien_Co + i;
                    Mang_Gia_Tri.Add(val);
                    var lb = Khoi_tao_Label(val, idx);
                    Mang_labels.Add(lb);
                    Controls.Add(lb);
                }
            }
            else //nhap mang thu cong
            {
                int gia_tri_phantu;
                string tmp = textBox_Input_Element_By_Hand.Text;
                bool is_songuyen = int.TryParse(tmp, out gia_tri_phantu);
                if (!is_songuyen)
                {
                    MessageBox.Show("Vui lòng nhập giá trị phần tử của mảng trong đoạn [-99, 999].",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Mang_Gia_Tri.Count >= so_phan_tu_max)
                {
                    MessageBox.Show("Số phần tử tối đa là 10.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int idx = Mang_Gia_Tri.Count;
                Mang_Gia_Tri.Add(gia_tri_phantu);
                var lb = Khoi_tao_Label(gia_tri_phantu, idx);
                Mang_labels.Add(lb);
                Controls.Add(lb);

                textBox_Input_Element_By_Hand.Clear();
                textBox_Input_Element_By_Hand.Focus();
            }
            Luu_Data_Gan_Nhat(); // lưu lại dữ liệu trước khi sort
        }
        private void button_stop_Click(object sender, EventArgs e)
        {
            if (!is_running) return;

            is_dung_lai = !is_dung_lai; // đảo trạng thái

            if (is_dung_lai)
                button_stop.Text = "Tiếp Tục";
            else
                button_stop.Text = "Tạm Dừng";
        }
        async void button_start_Click(object sender, EventArgs e)
        {
            if (Mang_Gia_Tri.Count == 0) return;
            if (Sap_xep == Loai_Sap_Xep.None)
            {
                MessageBox.Show("Vui Lòng chọn thuật toán sắp xếp!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!is_running)
            {
                is_running = true;
                is_dung_lai = false;
                CTS = new CancellationTokenSource();

                button_add.Enabled = false; // khi chạy thì cấm thêm phần tử
                Save_Du_Lieu();
                try
                {
                    await Run_Giai_Thuat_Sap_Xep(CTS.Token);
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    is_running = false;
                    button_add.Enabled = true;
                }
            }
            else
            {
                is_dung_lai = false;  //bấm bắt đầu nữa thì coi như tiếp tục nè
            }
            button_stop.Text = "Tạm Dừng";
        }
        private void button_before_sort_Click(object sender, EventArgs e)
        {
            is_dung_lai = false;
            if (is_running)
            {
                CTS?.Cancel();
                is_running = false;
            }
            if (Mang_Pre_Sort == null || Mang_Pre_Sort.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu ban đầu để khôi phục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Xoa_Tat_ca_Label();
            for (int i = 0; i < Mang_Pre_Sort.Count; i++)
            {
                int val = Mang_Pre_Sort[i];
                Mang_Gia_Tri.Add(val);
                var lb = Khoi_tao_Label(val, i);
                Mang_labels.Add(lb);
                Controls.Add(lb);
            }
            button_add.Enabled = true;
            button_stop.Text = "Tạm Dừng";
        }
        private void button_reset_Click(object sender, EventArgs e)
        {
            is_dung_lai = false;
            is_running = false;
            CTS?.Cancel();
            Xoa_Tat_ca_Label();
            button_add.Enabled = true;
        }
        private void button_increase_speed_Click(object sender, EventArgs e)
        {
            Change_Van_toc_duyet(-10);
        }
        private void button_decrease_speed_Click(object sender, EventArgs e)
        {
            Change_Van_toc_duyet(10);
        }
        private void radioButton_Random_CheckedChanged(object sender, EventArgs e)
        {
            Khoa_Lua_Chon_InPut_Data(sender, e);
        }
        private void radioButton_By_Hand_CheckedChanged(object sender, EventArgs e)
        {
            Khoa_Lua_Chon_InPut_Data(sender, e);
        }
        private void radioButton_Increase_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Increase.Checked) is_tang_dan = true;
        }
        private void radioButton_Decrease_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Decrease.Checked) is_tang_dan = false;
        }
        private void radioButton_Exchange_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Exchange_Sort.Checked) Sap_xep = Loai_Sap_Xep.Exchange;
        }
        private void radioButton_Selection_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Selection_Sort.Checked) Sap_xep = Loai_Sap_Xep.Selection;
        }
        private void radioButton_Insertion_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Insertion_Sort.Checked) Sap_xep = Loai_Sap_Xep.Insertion;
        }
        private void radioButton_Bubble_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Bubble_Sort.Checked) Sap_xep = Loai_Sap_Xep.Bubble;
        }
        private void radioButton_Heap_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Heap_Sort.Checked) Sap_xep = Loai_Sap_Xep.Heap;
        }
        private void radioButton_Quick_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Quick_Sort.Checked) Sap_xep = Loai_Sap_Xep.Quick;
        }
        private void radioButton_Merge_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Merge_Sort.Checked) Sap_xep = Loai_Sap_Xep.Merge;
        }
        private void textBox_Input_Number_Element_RanDom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;
            TextBox tb = sender as TextBox;
            bool is_Nhap_Bang_Tay = (tb == textBox_Input_Element_By_Hand);

            if (!is_Nhap_Bang_Tay)
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar == (char)Keys.Back) return;
                string text_moi = tb.Text.Insert(tb.SelectionStart, e.KeyChar.ToString());
                if (int.TryParse(text_moi, out int so))
                {
                    if (so < 1 || so > 10)
                        e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }
                return;
            }
        }
        private void textBox_Input_Element_By_Hand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;
            TextBox tb = sender as TextBox;
            if (e.KeyChar == '-')
            {
                string text_IP_bang_tay = tb.Text;
                int Vi_tri_boi_den = tb.SelectionStart;
                int Do_dai_boi_den = tb.SelectionLength;
                // - gõ ở vị trí đầu (selStart == 0)
                // - và hiện tại chưa có '-' (trừ trường hợp đang bôi đen cả chuỗi để gõ đè)
                bool Thay_the_toan_bo = (Do_dai_boi_den == text_IP_bang_tay.Length && text_IP_bang_tay.Length > 0);
                string Text_moi;
                if (Thay_the_toan_bo)
                {
                    Text_moi = "-"; // bôi đen hết rồi gõ '-' => kết quả chỉ còn "-"
                }
                else
                {
                    // có '-' rồi thì k thêm  '-' đc nữa :))))
                    if (Vi_tri_boi_den != 0 || text_IP_bang_tay.Contains("-"))
                    {
                        e.Handled = true;
                        return;
                    }
                    Text_moi = text_IP_bang_tay.Remove(Vi_tri_boi_den, Do_dai_boi_den)
                                  .Insert(Vi_tri_boi_den, "-");
                }

                if (Text_moi.Length > 3)
                {
                    e.Handled = true;
                    return;
                }
                // ok
                e.Handled = false;
                return;
            }
            if (!char.IsDigit(e.KeyChar))  // giới hạn chỉ được nhập kí tự là số
            {
                e.Handled = true;
                return;
            }
            // trường hợp k có dấu -
            {
                string text_input_bang_tay = tb.Text;
                int pos_boiden = tb.SelectionStart;
                int do_dai_boi_den = tb.SelectionLength;

                string text_moi;
                if (do_dai_boi_den > 0)
                {
                    text_moi = text_input_bang_tay.Remove(pos_boiden, do_dai_boi_den)
                                  .Insert(pos_boiden, e.KeyChar.ToString());
                }
                else
                {
                    text_moi = text_input_bang_tay.Insert(pos_boiden, e.KeyChar.ToString());
                }

                if (text_moi.Length > 3)
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
