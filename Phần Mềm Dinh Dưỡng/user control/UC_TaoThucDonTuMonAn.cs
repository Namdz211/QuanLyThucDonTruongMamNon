using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Phần_Mềm_Dinh_Dưỡng.Form_Quản_Lý_Chung;

namespace Phần_Mềm_Dinh_Dưỡng.user_control
{

    public partial class UC_TaoThucDonTuMonAn : UserControl
    {
        // 🔹 Định nghĩa chuỗi kết nối trước
        private string connectionString = "Data Source=.;Initial Catalog=ThucDonMamNon;Integrated Security=True";
        private DateTime mondayDate;
        private string currentUser = "Admin";
        public UC_TaoThucDonTuMonAn()
        {
            InitializeComponent();
            mondayDate = GetThisMonday();
        }
        private DateTime GetThisMonday()
        {
            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            return today.AddDays(delta);
        }
        private void dgvThucDonMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void UC_TaoThucDonTuMonAn_Load(object sender, EventArgs e)
        {
            cboNhomTre.Items.AddRange(new[] { "Nhà trẻ", "Mẫu giáo" });
            cboNhomTre.SelectedIndex = 0;
            cboNhomTre.SelectedIndexChanged += CboNhomTre_SelectedIndexChanged;

            btnChonTuan.Click += btnChonTuan_Click;
            btnPhanTich.Click += btnPhanTich_Click;
            btnLuu.Click += btnXemTuan_Click;
            btnQuanLyMon.Click += btnMonAn_Click;

            LoadThucDonTuan();
        }

        private void TaoCotDataGridView()
        {
            dgvThucDon.Columns.Clear();

            // Cột chung
            dgvThucDon.Columns.Add("Ngay", "Ngày");
            dgvThucDon.Columns.Add("Thu", "Thứ");

            if (cboNhomTre.SelectedItem.ToString() == "Nhà trẻ")
            {
                // Thêm cột ComboBox cho nhà trẻ
                ThemCotComboBox("NT_Sang_Chao", "Sáng: Cháo");
                // Thêm các cột khác tương tự
            }
            else
            {
                // Thêm cột ComboBox cho mẫu giáo
                ThemCotComboBox("MG_Sang_MonChinh", "Sáng: Món chính");
                // Thêm các cột khác tương tự
            }
        }

        private void ThemCotComboBox(string tenCot, string tieuDe)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.Name = tenCot;
            col.HeaderText = tieuDe;
            // Cấu hình DataSource từ CSDL
            dgvThucDon.Columns.Add(col);
        }

        private void CboNhomTre_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThucDonTuan();
        }
        private void LoadThucDonTuan()
        {
            dgvThucDon.Rows.Clear();
            dgvThucDon.Columns.Clear();

            // Thêm cột Ngày và Thứ
            dgvThucDon.Columns.Add("Ngay", "Ngày");
            dgvThucDon.Columns.Add("Thu", "Thứ");

            string nhomTre = cboNhomTre.SelectedItem?.ToString() ?? "Nhà trẻ";

            if (nhomTre == "Nhà trẻ")
            {
                ThemCotComboBox("NT_Sang_Chao", "Sáng: Cháo", "Cháo");
                ThemCotComboBox("NT_Trua_Chao", "Trưa: Cháo", "Cháo");
                ThemCotComboBox("NT_Trua_TrangMieng", "Trưa: Tráng miệng", "Tráng miệng");
                ThemCotComboBox("NT_Chieu_Chao", "Chiều: Cháo", "Cháo");
                ThemCotComboBox("NT_Chieu_SuaChua", "Chiều: Sữa chua", "Sữa chua");
            }
            else
            {
                ThemCotComboBox("MG_Sang_MonChinh", "Sáng: Món chính", "Món chính");
                ThemCotComboBox("MG_Sang_TrangMieng", "Sáng: Tráng miệng", "Tráng miệng");
                ThemCotComboBox("MG_Trua_MonMan", "Trưa: Món mặn", "Món mặn");
                ThemCotComboBox("MG_Trua_MonCanh", "Trưa: Món canh", "Món canh");
                ThemCotComboBox("MG_Trua_MonRau", "Trưa: Món rau", "Món rau củ");
                ThemCotComboBox("MG_Trua_TrangMieng", "Trưa: Tráng miệng", "Tráng miệng");
                ThemCotComboBox("MG_Chieu_MonChinh", "Chiều: Món chính", "Món chính");
                ThemCotComboBox("MG_Chieu_TrangMieng", "Chiều: Tráng miệng", "Tráng miệng");
            }

            // Thêm dữ liệu cho 5 ngày (Thứ 2 - Thứ 6)
            for (int i = 0; i < 5; i++)
            {
                DateTime ngay = mondayDate.AddDays(i);
                string thu = "Thứ " + (i + 2); // Thứ 2 đến Thứ 6

                int rowIndex = dgvThucDon.Rows.Add(ngay.ToString("dd/MM/yyyy"), thu);

                // Load thực đơn đã lưu nếu có
                LoadThucDonNgay(ngay, dgvThucDon.Rows[rowIndex]);
            }
        }
        private void ThemCotComboBox(string tenCot, string tieuDe, string loaiMon)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn
            {
                Name = tenCot,
                HeaderText = tieuDe,
                DataSource = GetMonAnTheoLoai(loaiMon),
                DisplayMember = "TenMon",
                ValueMember = "MaMon",
                FlatStyle = FlatStyle.Flat
            };
            dgvThucDon.Columns.Add(col);
        }
        private DataTable GetMonAnTheoLoai(string loaiMon)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaMon, TenMon FROM MonAn WHERE LoaiMon = @LoaiMon ORDER BY TenMon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoaiMon", loaiMon);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        private void LoadThucDonNgay(DateTime ngay, DataGridViewRow row)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ThucDon WHERE Ngay = @Ngay AND NhomTre = @NhomTre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ngay", ngay);
                    cmd.Parameters.AddWithValue("@NhomTre", cboNhomTre.SelectedItem?.ToString() ?? "Nhà trẻ");

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell is DataGridViewComboBoxCell comboBoxCell)
                            {
                                string columnName = comboBoxCell.OwningColumn.Name;
                                if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                {
                                    comboBoxCell.Value = reader[columnName].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thực đơn: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridViewComboBoxColumn CreateComboBoxColumn(string name, string header, string loaiMon)
        {
            var column = new DataGridViewComboBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataSource = GetMonAnByLoai(loaiMon),
                DisplayMember = "TenMon",
                ValueMember = "MaMon"
            };
            return column;
        }
        private DataTable GetMonAnByLoai(string loaiMon)
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaMon, TenMon FROM MonAn WHERE LoaiMon = @LoaiMon";

            // 🔹 Định nghĩa chuỗi kết nối trước


            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LoaiMon", loaiMon);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraThucDon())
                    return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvThucDon.Rows)
                    {
                        if (row.IsNewRow) continue;

                        DateTime ngay = DateTime.Parse(row.Cells["Ngay"].Value.ToString());
                        string thu = row.Cells["Thu"].Value.ToString();
                        string nhomTre = cboNhomTre.SelectedItem.ToString();

                        using (SqlCommand cmd = new SqlCommand("sp_ThemCapNhatThucDon", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TenThucDon", $"Thực đơn {nhomTre} - {thu}");
                            cmd.Parameters.AddWithValue("@NhomTre", nhomTre);
                            cmd.Parameters.AddWithValue("@Ngay", ngay);
                            cmd.Parameters.AddWithValue("@Thu", thu);
                            cmd.Parameters.AddWithValue("@NguoiTao", currentUser);

                            // Thêm các tham số món ăn
                            if (nhomTre == "Nhà trẻ")
                            {
                                ThemThamSoMonAn(cmd, row, "NT_Sang_Chao");
                                ThemThamSoMonAn(cmd, row, "NT_Trua_Chao");
                                // Thêm các tham số khác tương tự
                            }
                            else
                            {
                                ThemThamSoMonAn(cmd, row, "MG_Sang_MonChinh");
                                ThemThamSoMonAn(cmd, row, "MG_Sang_TrangMieng");
                                // Thêm các tham số khác tương tự
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Lưu thực đơn thành công!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thực đơn: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThemThamSoMonAn(SqlCommand cmd, DataGridViewRow row, string tenCot)
        {// Kiểm tra nếu ô có giá trị
            if (row.Cells[tenCot].Value != null)
            {
                // Thêm parameter với giá trị string
                cmd.Parameters.AddWithValue($"@{tenCot}", row.Cells[tenCot].Value.ToString());
            }
            else
            {
                // Thêm parameter với DBNull.Value nếu ô trống
                cmd.Parameters.AddWithValue($"@{tenCot}", DBNull.Value);
            }
        }

        private bool KiemTraThucDon()
        {
            foreach (DataGridViewRow row in dgvThucDon.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell is DataGridViewComboBoxCell comboBoxCell && comboBoxCell.Value == null)
                    {
                        MessageBox.Show($"Vui lòng chọn đầy đủ món ăn cho {row.Cells["Thu"].Value}", "Lỗi",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (KiemTraTrungMonAn(row))
                {
                    return false;
                }
            }
            return true;
        }

        private bool KiemTraTrungMonAn(DataGridViewRow row)
        {
            var monAnDaChon = new HashSet<string>();

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell is DataGridViewComboBoxCell comboBoxCell && comboBoxCell.Value != null)
                {
                    string maMon = comboBoxCell.Value.ToString();
                    if (monAnDaChon.Contains(maMon))
                    {
                        MessageBox.Show($"Món ăn {maMon} được chọn nhiều lần trong {row.Cells["Thu"].Value}",
                                      "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    monAnDaChon.Add(maMon);
                }
            }
            return false;
        }

        private void btnMonAn_Click(object sender, EventArgs e)
        {
            FormDsMonAn dsMonAn = new FormDsMonAn();
            dsMonAn.Show();
        }
        //private NutritionInfo GetStandardNutrition(string nhomTre)
        //{
        //    return new NutritionInfo
        //    {
        //        Calories = nhomTre == "Nhóm mẫu giáo" ? 1200 : 900,
        //        Protein = nhomTre == "Nhóm mẫu giáo" ? 30 : 25,
        //        Carbs = nhomTre == "Nhóm mẫu giáo" ? 150 : 120,
        //        Fat = nhomTre == "Nhóm mẫu giáo" ? 40 : 30,
        //        Fiber = nhomTre == "Nhóm mẫu giáo" ? 15 : 10
        //    };
        //}


        //private void btnPhanTichDinhDuong_Click(object sender, EventArgs e)
        //{
        //    if (dgvThucDon.Rows.Count == 0)
        //    {
        //        MessageBox.Show("Vui lòng tạo thực đơn trước khi phân tích", "Thông báo",
        //                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        string nhomTre = cboNhomTre.SelectedItem.ToString();
        //        DataTable analysisData = TaoBangPhanTich(nhomTre);
        //        DataTable comparisonData = TaoBangSoSanh(analysisData, nhomTre);

        //        FormKtraDinhDuong form = new FormKtraDinhDuong(analysisData, comparisonData, nhomTre);
        //        form.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi phân tích dinh dưỡng: {ex.Message}", "Lỗi",
        //                      MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private DataTable TaoBangPhanTich(string nhomTre)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngay", typeof(string));
            dt.Columns.Add("Thu", typeof(string));
            dt.Columns.Add("Calories", typeof(double));
            dt.Columns.Add("Protein", typeof(double));
            dt.Columns.Add("Carbs", typeof(double));
            dt.Columns.Add("Fat", typeof(double));
            dt.Columns.Add("Fiber", typeof(double));

            foreach (DataGridViewRow row in dgvThucDon.Rows)
            {
                if (row.IsNewRow) continue;

                double calories = 0, protein = 0, carbs = 0, fat = 0, fiber = 0;

                // Duyệt từ cột thứ 2 trở đi (bỏ Ngày và Thứ)
                for (int i = 2; i < row.Cells.Count; i++)
                {
                    var cell = row.Cells[i];
                    if (cell is DataGridViewComboBoxCell comboBoxCell && comboBoxCell.Value != null)
                    {
                        string maMon = comboBoxCell.Value.ToString();
                        if (!string.IsNullOrEmpty(maMon))
                        {
                            var nutrition = GetDinhDuongMonAn(maMon);
                            calories += nutrition.Calories;
                            protein += nutrition.Protein;
                            carbs += nutrition.Carbs;
                            fat += nutrition.Fat;
                            fiber += nutrition.Fiber;
                        }
                    }
                }

                string ngay = row.Cells[0].Value?.ToString() ?? "";
                string thu = row.Cells[1].Value?.ToString() ?? "";

                dt.Rows.Add(ngay, thu, calories, protein, carbs, fat, fiber);
            }

            return dt;
        }   

        private DataTable TaoBangSoSanh(DataTable analysisData, string nhomTre)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngay", typeof(string));
            dt.Columns.Add("Thu", typeof(string));
            dt.Columns.Add("Calories", typeof(string));
            dt.Columns.Add("Protein", typeof(string));
            dt.Columns.Add("Carbs", typeof(string));
            dt.Columns.Add("Fat", typeof(string));
            dt.Columns.Add("Fiber", typeof(string));
            dt.Columns.Add("DanhGia", typeof(string));

            var tieuChuan = GetTieuChuanDinhDuong(nhomTre);

            foreach (DataRow row in analysisData.Rows)
            {
                double calories = Convert.ToDouble(row["Calories"]);
                double protein = Convert.ToDouble(row["Protein"]);
                double carbs = Convert.ToDouble(row["Carbs"]);
                double fat = Convert.ToDouble(row["Fat"]);
                double fiber = Convert.ToDouble(row["Fiber"]);

                string danhGia = "Đạt";
                if (calories < tieuChuan.Calories * 0.9 || protein < tieuChuan.Protein * 0.9)
                    danhGia = "Thiếu";
                else if (calories > tieuChuan.Calories * 1.1 || fat > tieuChuan.Fat * 1.1)
                    danhGia = "Dư";

                dt.Rows.Add(
                    row["Ngay"],
                    row["Thu"],
                    $"{calories:F0} kcal ({(calories * 100 / tieuChuan.Calories):F0}%)",
                    $"{protein:F0}g ({(protein * 100 / tieuChuan.Protein):F0}%)",
                    $"{carbs:F0}g ({(carbs * 100 / tieuChuan.Carbs):F0}%)",
                    $"{fat:F0}g ({(fat * 100 / tieuChuan.Fat):F0}%)",
                    $"{fiber:F0}g ({(fiber * 100 / tieuChuan.Fiber):F0}%)",
                    danhGia
                );
            }

            return dt;
        }

        private TieuChuanDinhDuong GetTieuChuanDinhDuong(string nhomTre)
        {
            TieuChuanDinhDuong tc = new TieuChuanDinhDuong();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TieuChuanDinhDuong WHERE NhomTre = @NhomTre";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NhomTre", nhomTre);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tc.Calories = Convert.ToDouble(reader["Calories"]);
                    tc.Protein = Convert.ToDouble(reader["Protein"]);
                    tc.Carbs = Convert.ToDouble(reader["Carbs"]);
                    tc.Fat = Convert.ToDouble(reader["Fat"]);
                    tc.Fiber = Convert.ToDouble(reader["Fiber"]);
                }
            }

            return tc;
        }



        private DinhDuong GetDinhDuongMonAn(string maMon)
        {
            DinhDuong dn = new DinhDuong();

            if (string.IsNullOrEmpty(maMon))
            {
                return dn; // Trả về đối tượng rỗng nếu mã món không hợp lệ
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Calories, Protein, Carbs, Fat, Fiber FROM MonAn WHERE MaMon = @MaMon";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMon", maMon);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        dn.Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? 0 : Convert.ToDouble(reader["Calories"]);
                        dn.Protein = reader.IsDBNull(reader.GetOrdinal("Protein")) ? 0 : Convert.ToDouble(reader["Protein"]);
                        dn.Carbs = reader.IsDBNull(reader.GetOrdinal("Carbs")) ? 0 : Convert.ToDouble(reader["Carbs"]);
                        dn.Fat = reader.IsDBNull(reader.GetOrdinal("Fat")) ? 0 : Convert.ToDouble(reader["Fat"]);
                        dn.Fiber = reader.IsDBNull(reader.GetOrdinal("Fiber")) ? 0 : Convert.ToDouble(reader["Fiber"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin dinh dưỡng cho món ăn {maMon}: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dn;
        }
        private DataTable CreateComparisonTable(DataTable analysisData, NutritionInfo standard)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Calories", typeof(string));
            dt.Columns.Add("Protein", typeof(string));
            dt.Columns.Add("Carbs", typeof(string));
            dt.Columns.Add("Fat", typeof(string));
            dt.Columns.Add("Fiber", typeof(string));
            dt.Columns.Add("Đánh giá", typeof(string));

            foreach (DataRow row in analysisData.Rows)
            {
                double dayCalories = Convert.ToDouble(row["Calories"]);
                double dayProtein = Convert.ToDouble(row["Protein (g)"]);
                double dayCarbs = Convert.ToDouble(row["Carbs (g)"]);
                double dayFat = Convert.ToDouble(row["Fat (g)"]);
                double dayFiber = Convert.ToDouble(row["Fiber (g)"]);

                // Tính phần trăm so với tiêu chuẩn
                string caloriesText = $"{dayCalories:F0} kcal ({(dayCalories / standard.Calories * 100):F0}%)";
                string proteinText = $"{dayProtein:F0}g ({(dayProtein / standard.Protein * 100):F0}%)";
                string carbsText = $"{dayCarbs:F0}g ({(dayCarbs / standard.Carbs * 100):F0}%)";
                string fatText = $"{dayFat:F0}g ({(dayFat / standard.Fat * 100):F0}%)";
                string fiberText = $"{dayFiber:F0}g ({(dayFiber / standard.Fiber * 100):F0}%)";

                // Đánh giá tổng thể
                string danhGia = "Đạt";
                if (dayCalories < standard.Calories * 0.9 ||
                    dayProtein < standard.Protein * 0.9 ||
                    dayFiber < standard.Fiber * 0.9)
                {
                    danhGia = "Thiếu";
                }

                dt.Rows.Add(
                    row["Ngày"],
                    caloriesText,
                    proteinText,
                    carbsText,
                    fatText,
                    fiberText,
                    danhGia
                );
            }

            return dt;
        }

        private void btnChonTuan_Click(object sender, EventArgs e)
        {
            using (FormChonTuan form = new FormChonTuan())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    mondayDate = form.MondayDate;
                    lblTuanHienTai.Text = $"Tuần từ {mondayDate.ToString("dd/MM/yyyy")} đến {mondayDate.AddDays(4).ToString("dd/MM/yyyy")}";
                    LoadThucDonTuan();
                }
            }
        }

        private void btnXemTuan_Click(object sender, EventArgs e)
        {
            string nhomTre = cboNhomTre.SelectedItem.ToString();
            FormXemThucDonTuan form = new FormXemThucDonTuan(mondayDate, nhomTre);
            form.Show();
        }
        public class DinhDuong
        {
            public double Calories { get; set; }
            public double Protein { get; set; }
            public double Carbs { get; set; }
            public double Fat { get; set; }
            public double Fiber { get; set; }
        }

        public class TieuChuanDinhDuong
        {
            public double Calories { get; set; }
            public double Protein { get; set; }
            public double Carbs { get; set; }
            public double Fat { get; set; }
            public double Fiber { get; set; }
        }
        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            if (dgvThucDon.Rows.Count <= 1)
            {
                MessageBox.Show("Thực đơn chưa có dữ liệu hoặc không hợp lệ", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboNhomTre.SelectedItem == null || cboNhomTre.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhóm trẻ trước khi phân tích", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nhomTre = cboNhomTre.SelectedItem.ToString();

                // Chỉ gọi với 1 tham số là nhóm trẻ
                DataTable analysisData = TaoBangPhanTich(nhomTre);

                DataTable comparisonData = TaoBangSoSanh(analysisData, nhomTre);

                FormKiemTraDinhDuong form = new FormKiemTraDinhDuong(analysisData, comparisonData, nhomTre);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi phân tích: {ex.Message}", "Lỗi hệ thống",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboNhomTre_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
