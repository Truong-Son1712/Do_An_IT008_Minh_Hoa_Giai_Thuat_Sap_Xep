namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.values_of_array = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type_sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direction_sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.day_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button_delete_history = new System.Windows.Forms.Button();
            this.button_delete_all_history = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.values_of_array,
            this.type_sort,
            this.direction_sort,
            this.day_time,
            this.select});
            this.dataGridView1.Location = new System.Drawing.Point(40, 101);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1001, 42);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // STT
            // 
            this.STT.FillWeight = 45.34061F;
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // values_of_array
            // 
            this.values_of_array.FillWeight = 224.599F;
            this.values_of_array.HeaderText = "Giá Trị Phần Tử Trong Mảng";
            this.values_of_array.MinimumWidth = 6;
            this.values_of_array.Name = "values_of_array";
            this.values_of_array.ReadOnly = true;
            // 
            // type_sort
            // 
            this.type_sort.FillWeight = 86.58665F;
            this.type_sort.HeaderText = "Loại Sắp Xếp";
            this.type_sort.MinimumWidth = 6;
            this.type_sort.Name = "type_sort";
            this.type_sort.ReadOnly = true;
            // 
            // direction_sort
            // 
            this.direction_sort.FillWeight = 76.22993F;
            this.direction_sort.HeaderText = "Chiều Sắp Xếp";
            this.direction_sort.MinimumWidth = 6;
            this.direction_sort.Name = "direction_sort";
            this.direction_sort.ReadOnly = true;
            // 
            // day_time
            // 
            this.day_time.FillWeight = 119.2602F;
            this.day_time.HeaderText = "Thời Gian";
            this.day_time.MinimumWidth = 6;
            this.day_time.Name = "day_time";
            this.day_time.ReadOnly = true;
            // 
            // select
            // 
            this.select.FillWeight = 47.98375F;
            this.select.HeaderText = "Chọn";
            this.select.MinimumWidth = 6;
            this.select.Name = "select";
            this.select.ReadOnly = true;
            this.select.Text = "Chọn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(389, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lịch Sử Mảng Đã Sắp Xếp";
            // 
            // button_delete_history
            // 
            this.button_delete_history.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button_delete_history.Location = new System.Drawing.Point(40, 40);
            this.button_delete_history.Name = "button_delete_history";
            this.button_delete_history.Size = new System.Drawing.Size(137, 41);
            this.button_delete_history.TabIndex = 2;
            this.button_delete_history.Text = "Xóa Bản Ghi";
            this.button_delete_history.UseVisualStyleBackColor = false;
            this.button_delete_history.Click += new System.EventHandler(this.button_delete_history_Click);
            // 
            // button_delete_all_history
            // 
            this.button_delete_all_history.BackColor = System.Drawing.Color.LightCoral;
            this.button_delete_all_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_delete_all_history.Location = new System.Drawing.Point(904, 40);
            this.button_delete_all_history.Name = "button_delete_all_history";
            this.button_delete_all_history.Size = new System.Drawing.Size(137, 41);
            this.button_delete_all_history.TabIndex = 3;
            this.button_delete_all_history.Text = "Xóa Mọi Bản Ghi";
            this.button_delete_all_history.UseVisualStyleBackColor = false;
            this.button_delete_all_history.Click += new System.EventHandler(this.button_delete_all_history_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Mo_Phong_Giai_Thuat_Sap_Xep.Properties.Resources.channels4_profile_Photoroom;
            this.pictureBox2.Location = new System.Drawing.Point(774, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(60, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Mo_Phong_Giai_Thuat_Sap_Xep.Properties.Resources.dhuit_Photoroom;
            this.pictureBox1.Location = new System.Drawing.Point(263, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 588);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_delete_all_history);
            this.Controls.Add(this.button_delete_history);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch Sử Sắp Xếp";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_delete_history;
        private System.Windows.Forms.Button button_delete_all_history;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn values_of_array;
        private System.Windows.Forms.DataGridViewTextBoxColumn type_sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn direction_sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn day_time;
        private System.Windows.Forms.DataGridViewButtonColumn select;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}