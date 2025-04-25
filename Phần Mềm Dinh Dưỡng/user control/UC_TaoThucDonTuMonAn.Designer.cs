namespace Phần_Mềm_Dinh_Dưỡng.user_control
{
    partial class UC_TaoThucDonTuMonAn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTuanHienTai = new System.Windows.Forms.Label();
            this.cboNhomTre = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dgvThucDon = new System.Windows.Forms.DataGridView();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLyMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnChonTuan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPhanTich = new Guna.UI2.WinForms.Guna2Button();
            this.btnXemTuan = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThucDon)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTuanHienTai
            // 
            this.lblTuanHienTai.AutoSize = true;
            this.lblTuanHienTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuanHienTai.ForeColor = System.Drawing.Color.Red;
            this.lblTuanHienTai.Location = new System.Drawing.Point(243, 45);
            this.lblTuanHienTai.Name = "lblTuanHienTai";
            this.lblTuanHienTai.Size = new System.Drawing.Size(132, 20);
            this.lblTuanHienTai.TabIndex = 1;
            this.lblTuanHienTai.Text = "Chọn nhóm trẻ";
            // 
            // cboNhomTre
            // 
            this.cboNhomTre.BackColor = System.Drawing.Color.Transparent;
            this.cboNhomTre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhomTre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomTre.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhomTre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhomTre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhomTre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNhomTre.ItemHeight = 30;
            this.cboNhomTre.Location = new System.Drawing.Point(391, 29);
            this.cboNhomTre.Name = "cboNhomTre";
            this.cboNhomTre.Size = new System.Drawing.Size(226, 36);
            this.cboNhomTre.TabIndex = 6;
            this.cboNhomTre.SelectedIndexChanged += new System.EventHandler(this.cboNhomTre_SelectedIndexChanged_1);
            // 
            // dgvThucDon
            // 
            this.dgvThucDon.AllowUserToAddRows = false;
            this.dgvThucDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThucDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvThucDon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvThucDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThucDon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvThucDon.Location = new System.Drawing.Point(0, 106);
            this.dgvThucDon.Name = "dgvThucDon";
            this.dgvThucDon.RowHeadersWidth = 51;
            this.dgvThucDon.RowTemplate.Height = 24;
            this.dgvThucDon.Size = new System.Drawing.Size(1882, 746);
            this.dgvThucDon.TabIndex = 7;
            this.dgvThucDon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThucDonMonAn_CellContentClick);
            // 
            // btnLuu
            // 
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.FillColor = System.Drawing.Color.Red;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(1144, 20);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(180, 45);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu thực đơn";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click_1);
            // 
            // btnQuanLyMon
            // 
            this.btnQuanLyMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyMon.FillColor = System.Drawing.Color.LimeGreen;
            this.btnQuanLyMon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyMon.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyMon.Location = new System.Drawing.Point(1353, 20);
            this.btnQuanLyMon.Name = "btnQuanLyMon";
            this.btnQuanLyMon.Size = new System.Drawing.Size(220, 45);
            this.btnQuanLyMon.TabIndex = 9;
            this.btnQuanLyMon.Text = "Xem danh sách món ăn";
            this.btnQuanLyMon.Click += new System.EventHandler(this.btnMonAn_Click);
            // 
            // btnChonTuan
            // 
            this.btnChonTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChonTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChonTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChonTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChonTuan.FillColor = System.Drawing.Color.LimeGreen;
            this.btnChonTuan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonTuan.ForeColor = System.Drawing.Color.White;
            this.btnChonTuan.Location = new System.Drawing.Point(672, 20);
            this.btnChonTuan.Name = "btnChonTuan";
            this.btnChonTuan.Size = new System.Drawing.Size(240, 45);
            this.btnChonTuan.TabIndex = 11;
            this.btnChonTuan.Text = "Chọn tuần xây dựng thực đơn";
            this.btnChonTuan.Click += new System.EventHandler(this.btnChonTuan_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnPhanTich);
            this.pnlTop.Controls.Add(this.btnXemTuan);
            this.pnlTop.Controls.Add(this.cboNhomTre);
            this.pnlTop.Controls.Add(this.btnQuanLyMon);
            this.pnlTop.Controls.Add(this.lblTuanHienTai);
            this.pnlTop.Controls.Add(this.btnLuu);
            this.pnlTop.Controls.Add(this.btnChonTuan);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1882, 100);
            this.pnlTop.TabIndex = 13;
            // 
            // btnPhanTich
            // 
            this.btnPhanTich.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanTich.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanTich.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPhanTich.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPhanTich.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnPhanTich.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPhanTich.ForeColor = System.Drawing.Color.White;
            this.btnPhanTich.Location = new System.Drawing.Point(941, 20);
            this.btnPhanTich.Name = "btnPhanTich";
            this.btnPhanTich.Size = new System.Drawing.Size(180, 45);
            this.btnPhanTich.TabIndex = 13;
            this.btnPhanTich.Text = "Phân tích dinh dưỡng";
            // 
            // btnXemTuan
            // 
            this.btnXemTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemTuan.FillColor = System.Drawing.Color.Red;
            this.btnXemTuan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTuan.ForeColor = System.Drawing.Color.White;
            this.btnXemTuan.Location = new System.Drawing.Point(1627, 20);
            this.btnXemTuan.Name = "btnXemTuan";
            this.btnXemTuan.Size = new System.Drawing.Size(180, 45);
            this.btnXemTuan.TabIndex = 12;
            this.btnXemTuan.Text = "Xem thực đơn tuần";
            this.btnXemTuan.Click += new System.EventHandler(this.btnXemTuan_Click);
            // 
            // UC_TaoThucDonTuMonAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.dgvThucDon);
            this.Name = "UC_TaoThucDonTuMonAn";
            this.Size = new System.Drawing.Size(1882, 852);
            this.Load += new System.EventHandler(this.UC_TaoThucDonTuMonAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThucDon)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTuanHienTai;
        private Guna.UI2.WinForms.Guna2ComboBox cboNhomTre;
        private System.Windows.Forms.DataGridView dgvThucDon;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnQuanLyMon;
        private Guna.UI2.WinForms.Guna2Button btnChonTuan;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2Button btnXemTuan;
        private Guna.UI2.WinForms.Guna2Button btnPhanTich;
    }
}
