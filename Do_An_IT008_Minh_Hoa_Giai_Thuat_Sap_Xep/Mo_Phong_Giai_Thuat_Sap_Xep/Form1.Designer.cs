namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_stop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_Random = new System.Windows.Forms.RadioButton();
            this.radioButton_By_Hand = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Input_Number_Element_RanDom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Input_Element_By_Hand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_speed = new System.Windows.Forms.Label();
            this.button_increase_speed = new System.Windows.Forms.Button();
            this.button_decrease_speed = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_history = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_Exchange_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Selection_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Insertion_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Bubble_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Heap_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Quick_Sort = new System.Windows.Forms.RadioButton();
            this.radioButton_Merge_Sort = new System.Windows.Forms.RadioButton();
            this.button_reset = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton_Increase = new System.Windows.Forms.RadioButton();
            this.radioButton_Decrease = new System.Windows.Forms.RadioButton();
            this.button_start = new System.Windows.Forms.Button();
            this.button_before_sort = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_stop
            // 
            this.button_stop.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button_stop.Location = new System.Drawing.Point(289, 15);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(95, 38);
            this.button_stop.TabIndex = 2;
            this.button_stop.Text = "Tạm Dừng";
            this.button_stop.UseVisualStyleBackColor = false;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Controls.Add(this.button_history);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button_add);
            this.groupBox2.Controls.Add(this.button_decrease_speed);
            this.groupBox2.Controls.Add(this.button_increase_speed);
            this.groupBox2.Controls.Add(this.label_speed);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_Input_Element_By_Hand);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox_Input_Number_Element_RanDom);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.radioButton_By_Hand);
            this.groupBox2.Controls.Add(this.radioButton_Random);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 193);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thiết lập giá trị đầu vào";
            // 
            // radioButton_Random
            // 
            this.radioButton_Random.AutoSize = true;
            this.radioButton_Random.Checked = true;
            this.radioButton_Random.Location = new System.Drawing.Point(6, 30);
            this.radioButton_Random.Name = "radioButton_Random";
            this.radioButton_Random.Size = new System.Drawing.Size(144, 20);
            this.radioButton_Random.TabIndex = 0;
            this.radioButton_Random.TabStop = true;
            this.radioButton_Random.Text = "Nhập ngẫu nhiên";
            this.radioButton_Random.UseVisualStyleBackColor = true;
            this.radioButton_Random.CheckedChanged += new System.EventHandler(this.radioButton_Random_CheckedChanged);
            // 
            // radioButton_By_Hand
            // 
            this.radioButton_By_Hand.AutoSize = true;
            this.radioButton_By_Hand.Location = new System.Drawing.Point(6, 62);
            this.radioButton_By_Hand.Name = "radioButton_By_Hand";
            this.radioButton_By_Hand.Size = new System.Drawing.Size(127, 20);
            this.radioButton_By_Hand.TabIndex = 1;
            this.radioButton_By_Hand.TabStop = true;
            this.radioButton_By_Hand.Text = "Nhập thủ công";
            this.radioButton_By_Hand.UseVisualStyleBackColor = true;
            this.radioButton_By_Hand.CheckedChanged += new System.EventHandler(this.radioButton_By_Hand_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số lượng phần tử khởi tạo";
            // 
            // textBox_Input_Number_Element_RanDom
            // 
            this.textBox_Input_Number_Element_RanDom.Location = new System.Drawing.Point(365, 26);
            this.textBox_Input_Number_Element_RanDom.Name = "textBox_Input_Number_Element_RanDom";
            this.textBox_Input_Number_Element_RanDom.Size = new System.Drawing.Size(48, 22);
            this.textBox_Input_Number_Element_RanDom.TabIndex = 3;
            this.textBox_Input_Number_Element_RanDom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Input_Number_Element_RanDom_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(174, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Giá trị phần tử thêm vào";
            // 
            // textBox_Input_Element_By_Hand
            // 
            this.textBox_Input_Element_By_Hand.Location = new System.Drawing.Point(365, 58);
            this.textBox_Input_Element_By_Hand.Name = "textBox_Input_Element_By_Hand";
            this.textBox_Input_Element_By_Hand.Size = new System.Drawing.Size(48, 22);
            this.textBox_Input_Element_By_Hand.TabIndex = 5;
            this.textBox_Input_Element_By_Hand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Input_Element_By_Hand_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tốc độ ";
            // 
            // label_speed
            // 
            this.label_speed.AutoSize = true;
            this.label_speed.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label_speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_speed.Location = new System.Drawing.Point(285, 148);
            this.label_speed.Name = "label_speed";
            this.label_speed.Size = new System.Drawing.Size(50, 27);
            this.label_speed.TabIndex = 7;
            this.label_speed.Text = "350";
            // 
            // button_increase_speed
            // 
            this.button_increase_speed.BackColor = System.Drawing.Color.LightGreen;
            this.button_increase_speed.Location = new System.Drawing.Point(177, 146);
            this.button_increase_speed.Name = "button_increase_speed";
            this.button_increase_speed.Size = new System.Drawing.Size(73, 34);
            this.button_increase_speed.TabIndex = 8;
            this.button_increase_speed.Text = "Tăng";
            this.button_increase_speed.UseVisualStyleBackColor = false;
            this.button_increase_speed.Click += new System.EventHandler(this.button_increase_speed_Click);
            // 
            // button_decrease_speed
            // 
            this.button_decrease_speed.BackColor = System.Drawing.Color.LightSalmon;
            this.button_decrease_speed.Location = new System.Drawing.Point(371, 146);
            this.button_decrease_speed.Name = "button_decrease_speed";
            this.button_decrease_speed.Size = new System.Drawing.Size(73, 34);
            this.button_decrease_speed.TabIndex = 9;
            this.button_decrease_speed.Text = "Giảm";
            this.button_decrease_speed.UseVisualStyleBackColor = false;
            this.button_decrease_speed.Click += new System.EventHandler(this.button_decrease_speed_Click);
            // 
            // button_add
            // 
            this.button_add.BackColor = System.Drawing.Color.Moccasin;
            this.button_add.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_add.Location = new System.Drawing.Point(439, 26);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(68, 54);
            this.button_add.TabIndex = 11;
            this.button_add.Text = "Thêm";
            this.button_add.UseVisualStyleBackColor = false;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 48);
            this.label4.TabIndex = 12;
            this.label4.Text = "Lưu ý :  - Số lượng phần tử tối đa là 10      \r\n             - Phạm vi phần tử tr" +
    "ong đoạn [-99, 999]\r\n\r\n";
            // 
            // button_history
            // 
            this.button_history.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button_history.Location = new System.Drawing.Point(365, 98);
            this.button_history.Name = "button_history";
            this.button_history.Size = new System.Drawing.Size(142, 33);
            this.button_history.TabIndex = 13;
            this.button_history.Text = "Lịch Sử Duyệt";
            this.button_history.UseVisualStyleBackColor = false;
            this.button_history.Click += new System.EventHandler(this.button_history_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox3.Controls.Add(this.radioButton_Merge_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Quick_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Heap_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Bubble_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Insertion_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Selection_Sort);
            this.groupBox3.Controls.Add(this.radioButton_Exchange_Sort);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(540, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(512, 193);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thiết Lập Thuật Toán";
            // 
            // radioButton_Exchange_Sort
            // 
            this.radioButton_Exchange_Sort.AutoSize = true;
            this.radioButton_Exchange_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Exchange_Sort.Location = new System.Drawing.Point(17, 32);
            this.radioButton_Exchange_Sort.Name = "radioButton_Exchange_Sort";
            this.radioButton_Exchange_Sort.Size = new System.Drawing.Size(115, 20);
            this.radioButton_Exchange_Sort.TabIndex = 0;
            this.radioButton_Exchange_Sort.TabStop = true;
            this.radioButton_Exchange_Sort.Text = "Exchange Sort";
            this.radioButton_Exchange_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Exchange_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Exchange_Sort_CheckedChanged);
            // 
            // radioButton_Selection_Sort
            // 
            this.radioButton_Selection_Sort.AutoSize = true;
            this.radioButton_Selection_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Selection_Sort.Location = new System.Drawing.Point(17, 73);
            this.radioButton_Selection_Sort.Name = "radioButton_Selection_Sort";
            this.radioButton_Selection_Sort.Size = new System.Drawing.Size(111, 20);
            this.radioButton_Selection_Sort.TabIndex = 1;
            this.radioButton_Selection_Sort.TabStop = true;
            this.radioButton_Selection_Sort.Text = "Selection Sort";
            this.radioButton_Selection_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Selection_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Selection_Sort_CheckedChanged);
            // 
            // radioButton_Insertion_Sort
            // 
            this.radioButton_Insertion_Sort.AutoSize = true;
            this.radioButton_Insertion_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Insertion_Sort.Location = new System.Drawing.Point(17, 111);
            this.radioButton_Insertion_Sort.Name = "radioButton_Insertion_Sort";
            this.radioButton_Insertion_Sort.Size = new System.Drawing.Size(105, 20);
            this.radioButton_Insertion_Sort.TabIndex = 2;
            this.radioButton_Insertion_Sort.TabStop = true;
            this.radioButton_Insertion_Sort.Text = "Insertion Sort";
            this.radioButton_Insertion_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Insertion_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Insertion_Sort_CheckedChanged);
            // 
            // radioButton_Bubble_Sort
            // 
            this.radioButton_Bubble_Sort.AutoSize = true;
            this.radioButton_Bubble_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Bubble_Sort.Location = new System.Drawing.Point(17, 153);
            this.radioButton_Bubble_Sort.Name = "radioButton_Bubble_Sort";
            this.radioButton_Bubble_Sort.Size = new System.Drawing.Size(98, 20);
            this.radioButton_Bubble_Sort.TabIndex = 3;
            this.radioButton_Bubble_Sort.TabStop = true;
            this.radioButton_Bubble_Sort.Text = "Bubble Sort";
            this.radioButton_Bubble_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Bubble_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Bubble_Sort_CheckedChanged);
            // 
            // radioButton_Heap_Sort
            // 
            this.radioButton_Heap_Sort.AutoSize = true;
            this.radioButton_Heap_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Heap_Sort.Location = new System.Drawing.Point(304, 32);
            this.radioButton_Heap_Sort.Name = "radioButton_Heap_Sort";
            this.radioButton_Heap_Sort.Size = new System.Drawing.Size(89, 20);
            this.radioButton_Heap_Sort.TabIndex = 4;
            this.radioButton_Heap_Sort.TabStop = true;
            this.radioButton_Heap_Sort.Text = "Heap Sort";
            this.radioButton_Heap_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Heap_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Heap_Sort_CheckedChanged);
            // 
            // radioButton_Quick_Sort
            // 
            this.radioButton_Quick_Sort.AutoSize = true;
            this.radioButton_Quick_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Quick_Sort.Location = new System.Drawing.Point(304, 73);
            this.radioButton_Quick_Sort.Name = "radioButton_Quick_Sort";
            this.radioButton_Quick_Sort.Size = new System.Drawing.Size(89, 20);
            this.radioButton_Quick_Sort.TabIndex = 5;
            this.radioButton_Quick_Sort.TabStop = true;
            this.radioButton_Quick_Sort.Text = "Quick Sort";
            this.radioButton_Quick_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Quick_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Quick_Sort_CheckedChanged);
            // 
            // radioButton_Merge_Sort
            // 
            this.radioButton_Merge_Sort.AutoSize = true;
            this.radioButton_Merge_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_Merge_Sort.Location = new System.Drawing.Point(304, 111);
            this.radioButton_Merge_Sort.Name = "radioButton_Merge_Sort";
            this.radioButton_Merge_Sort.Size = new System.Drawing.Size(94, 20);
            this.radioButton_Merge_Sort.TabIndex = 6;
            this.radioButton_Merge_Sort.TabStop = true;
            this.radioButton_Merge_Sort.Text = "Merge Sort";
            this.radioButton_Merge_Sort.UseVisualStyleBackColor = true;
            this.radioButton_Merge_Sort.CheckedChanged += new System.EventHandler(this.radioButton_Merge_Sort_CheckedChanged);
            // 
            // button_reset
            // 
            this.button_reset.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button_reset.Location = new System.Drawing.Point(6, 15);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(95, 38);
            this.button_reset.TabIndex = 0;
            this.button_reset.Text = "Đặt Lại";
            this.button_reset.UseVisualStyleBackColor = false;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(552, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Thứ Tự Sắp Xếp";
            // 
            // radioButton_Increase
            // 
            this.radioButton_Increase.AutoSize = true;
            this.radioButton_Increase.Checked = true;
            this.radioButton_Increase.Location = new System.Drawing.Point(736, 30);
            this.radioButton_Increase.Name = "radioButton_Increase";
            this.radioButton_Increase.Size = new System.Drawing.Size(88, 20);
            this.radioButton_Increase.TabIndex = 5;
            this.radioButton_Increase.TabStop = true;
            this.radioButton_Increase.Text = "Tăng Dần";
            this.radioButton_Increase.UseVisualStyleBackColor = true;
            this.radioButton_Increase.CheckedChanged += new System.EventHandler(this.radioButton_Increase_CheckedChanged);
            // 
            // radioButton_Decrease
            // 
            this.radioButton_Decrease.AutoSize = true;
            this.radioButton_Decrease.Location = new System.Drawing.Point(844, 30);
            this.radioButton_Decrease.Name = "radioButton_Decrease";
            this.radioButton_Decrease.Size = new System.Drawing.Size(88, 20);
            this.radioButton_Decrease.TabIndex = 6;
            this.radioButton_Decrease.TabStop = true;
            this.radioButton_Decrease.Text = "Giảm Dần";
            this.radioButton_Decrease.UseVisualStyleBackColor = true;
            this.radioButton_Decrease.CheckedChanged += new System.EventHandler(this.radioButton_Decrease_CheckedChanged);
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button_start.Location = new System.Drawing.Point(150, 15);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(95, 38);
            this.button_start.TabIndex = 1;
            this.button_start.Text = "Bắt Đầu";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_before_sort
            // 
            this.button_before_sort.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button_before_sort.Location = new System.Drawing.Point(428, 15);
            this.button_before_sort.Name = "button_before_sort";
            this.button_before_sort.Size = new System.Drawing.Size(95, 38);
            this.button_before_sort.TabIndex = 7;
            this.button_before_sort.Text = "Data Trước";
            this.button_before_sort.UseVisualStyleBackColor = false;
            this.button_before_sort.Click += new System.EventHandler(this.button_before_sort_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.button_before_sort);
            this.groupBox1.Controls.Add(this.button_start);
            this.groupBox1.Controls.Add(this.radioButton_Decrease);
            this.groupBox1.Controls.Add(this.radioButton_Increase);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button_reset);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button_stop);
            this.groupBox1.Location = new System.Drawing.Point(12, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1058, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // Form1
            // 
            this.AcceptButton = this.button_add;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1082, 588);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Mô Phỏng Giải Thuật Sắp Xếp Trực Quan Trong Giảng Dạy Cấu Trúc Dữ Liệu Và Giải Th" +
    "uật";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_history;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_decrease_speed;
        private System.Windows.Forms.Button button_increase_speed;
        private System.Windows.Forms.Label label_speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Input_Element_By_Hand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Input_Number_Element_RanDom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_By_Hand;
        private System.Windows.Forms.RadioButton radioButton_Random;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton_Merge_Sort;
        private System.Windows.Forms.RadioButton radioButton_Quick_Sort;
        private System.Windows.Forms.RadioButton radioButton_Heap_Sort;
        private System.Windows.Forms.RadioButton radioButton_Bubble_Sort;
        private System.Windows.Forms.RadioButton radioButton_Insertion_Sort;
        private System.Windows.Forms.RadioButton radioButton_Selection_Sort;
        private System.Windows.Forms.RadioButton radioButton_Exchange_Sort;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton_Increase;
        private System.Windows.Forms.RadioButton radioButton_Decrease;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_before_sort;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

