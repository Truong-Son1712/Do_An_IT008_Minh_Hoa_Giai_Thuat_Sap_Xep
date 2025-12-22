namespace Mo_Phong_Giai_Thuat_Sap_Xep
{
    partial class Form_ViewCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ViewCode));
            this.Exit_Button = new System.Windows.Forms.Button();
            this.ViewCode_Button = new System.Windows.Forms.Button();
            this.ViewPseudoCode_Button = new System.Windows.Forms.Button();
            this.Increase_Radio = new System.Windows.Forms.RadioButton();
            this.Decrease_Radio = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ViewCodeBox = new System.Windows.Forms.RichTextBox();
            this.head_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Exit_Button
            // 
            this.Exit_Button.BackColor = System.Drawing.Color.YellowGreen;
            this.Exit_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_Button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Exit_Button.Location = new System.Drawing.Point(31, 582);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(103, 51);
            this.Exit_Button.TabIndex = 0;
            this.Exit_Button.Text = "Thoát";
            this.Exit_Button.UseVisualStyleBackColor = false;
            this.Exit_Button.Click += new System.EventHandler(this.Exit_Button_Click);
            // 
            // ViewCode_Button
            // 
            this.ViewCode_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewCode_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewCode_Button.Location = new System.Drawing.Point(184, 582);
            this.ViewCode_Button.Name = "ViewCode_Button";
            this.ViewCode_Button.Size = new System.Drawing.Size(103, 51);
            this.ViewCode_Button.TabIndex = 1;
            this.ViewCode_Button.Text = "Mã Nguồn";
            this.ViewCode_Button.UseVisualStyleBackColor = false;
            this.ViewCode_Button.Click += new System.EventHandler(this.ViewCode_Button_Click);
            // 
            // ViewPseudoCode_Button
            // 
            this.ViewPseudoCode_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewPseudoCode_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewPseudoCode_Button.Location = new System.Drawing.Point(338, 583);
            this.ViewPseudoCode_Button.Name = "ViewPseudoCode_Button";
            this.ViewPseudoCode_Button.Size = new System.Drawing.Size(101, 51);
            this.ViewPseudoCode_Button.TabIndex = 2;
            this.ViewPseudoCode_Button.Text = "Mã Giả";
            this.ViewPseudoCode_Button.UseVisualStyleBackColor = false;
            this.ViewPseudoCode_Button.Click += new System.EventHandler(this.ViewPseudoCode_Button_Click);
            // 
            // Increase_Radio
            // 
            this.Increase_Radio.AutoSize = true;
            this.Increase_Radio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Increase_Radio.ForeColor = System.Drawing.Color.Maroon;
            this.Increase_Radio.Location = new System.Drawing.Point(477, 583);
            this.Increase_Radio.Name = "Increase_Radio";
            this.Increase_Radio.Size = new System.Drawing.Size(88, 20);
            this.Increase_Radio.TabIndex = 3;
            this.Increase_Radio.TabStop = true;
            this.Increase_Radio.Text = "Tăng Dần";
            this.Increase_Radio.UseVisualStyleBackColor = true;
            this.Increase_Radio.CheckedChanged += new System.EventHandler(this.Increase_Radio_CheckedChanged);
            // 
            // Decrease_Radio
            // 
            this.Decrease_Radio.AutoSize = true;
            this.Decrease_Radio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decrease_Radio.ForeColor = System.Drawing.Color.Maroon;
            this.Decrease_Radio.Location = new System.Drawing.Point(477, 614);
            this.Decrease_Radio.Name = "Decrease_Radio";
            this.Decrease_Radio.Size = new System.Drawing.Size(88, 20);
            this.Decrease_Radio.TabIndex = 4;
            this.Decrease_Radio.TabStop = true;
            this.Decrease_Radio.Text = "Giảm Dần";
            this.Decrease_Radio.UseVisualStyleBackColor = true;
            this.Decrease_Radio.CheckedChanged += new System.EventHandler(this.Decrease_Radio_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.Image = global::Mo_Phong_Giai_Thuat_Sap_Xep.Properties.Resources.channels4_profile_Photoroom;
            this.pictureBox1.Location = new System.Drawing.Point(498, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Mo_Phong_Giai_Thuat_Sap_Xep.Properties.Resources.dhuit_Photoroom;
            this.pictureBox2.Location = new System.Drawing.Point(515, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(71, 62);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // ViewCodeBox
            // 
            this.ViewCodeBox.Location = new System.Drawing.Point(31, 67);
            this.ViewCodeBox.Name = "ViewCodeBox";
            this.ViewCodeBox.ReadOnly = true;
            this.ViewCodeBox.Size = new System.Drawing.Size(534, 498);
            this.ViewCodeBox.TabIndex = 8;
            this.ViewCodeBox.Text = "";
            // 
            // head_label
            // 
            this.head_label.AutoSize = true;
            this.head_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.head_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.head_label.Location = new System.Drawing.Point(26, 19);
            this.head_label.Name = "head_label";
            this.head_label.Size = new System.Drawing.Size(117, 25);
            this.head_label.TabIndex = 9;
            this.head_label.Text = "View Code";
            // 
            // Form_ViewCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 649);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.head_label);
            this.Controls.Add(this.ViewCodeBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Decrease_Radio);
            this.Controls.Add(this.Increase_Radio);
            this.Controls.Add(this.ViewPseudoCode_Button);
            this.Controls.Add(this.ViewCode_Button);
            this.Controls.Add(this.Exit_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form_ViewCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewCode";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.Button ViewCode_Button;
        private System.Windows.Forms.Button ViewPseudoCode_Button;
        private System.Windows.Forms.RadioButton Increase_Radio;
        private System.Windows.Forms.RadioButton Decrease_Radio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox ViewCodeBox;
        private System.Windows.Forms.Label head_label;
    }
}