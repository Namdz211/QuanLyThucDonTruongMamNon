using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Phần_Mềm_Dinh_Dưỡng.Form_Quản_Lý_Chung
{
    public partial class FormKiemTraDinhDuong : Form
    {
        // Biến thành viên
        private DataTable analysisData;
        private DataTable comparisonData;
        private string nhomTre;

        // Constructor chính nhận 3 tham số
        public FormKiemTraDinhDuong(DataTable analysisData, DataTable comparisonData, string nhomTre)
        {
            InitializeComponent();
            LoadData(analysisData, comparisonData, nhomTre);
        }

        // Phương thức public để tải dữ liệu
        public void LoadData(DataTable analysisData, DataTable comparisonData, string nhomTre)
        {
            this.analysisData = analysisData;
            this.comparisonData = comparisonData;
            this.nhomTre = nhomTre;

            this.Text = $"Phân Tích Dinh Dưỡng - {nhomTre}";

            // Tab chi tiết
            dgvChiTiet.DataSource = analysisData;
            FormatDataGridView(dgvChiTiet);

            // Tab tiêu chuẩn
            dgvTieuChuan.DataSource = comparisonData;
            HighlightNutritionGaps();

            // Tab biểu đồ
            CreateNutritionChart();
        }

        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv.Columns.Contains("Ngay"))
                dgv.Columns["Ngay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains("Thu"))
                dgv.Columns["Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.ValueType == typeof(double) || col.ValueType == typeof(float))
                {
                    col.DefaultCellStyle.Format = "N1";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void HighlightNutritionGaps()
        {
            if (dgvTieuChuan == null || dgvTieuChuan.Columns == null || !dgvTieuChuan.Columns.Contains("DanhGia"))
                return;

            foreach (DataGridViewRow row in dgvTieuChuan.Rows)
            {
                if (row.IsNewRow) continue;

                // Xử lý các cột phần trăm
                for (int i = 2; i < dgvTieuChuan.Columns.Count - 1; i++)
                {
                    if (row.Cells[i]?.Value == null) continue;

                    try
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        if (string.IsNullOrEmpty(cellValue)) continue;

                        // Kiểm tra định dạng "(XX%)"
                        if (cellValue.Contains("(") && cellValue.Contains("%"))
                        {
                            int percent = int.Parse(cellValue.Split('(')[1].Split('%')[0]);
                            row.Cells[i].Style.BackColor = percent < 90 ? Color.LightSalmon :
                                                         percent > 110 ? Color.LightYellow : Color.LightGreen;
                        }
                    }
                    catch { continue; }
                }

                // Xử lý cột đánh giá
                if (row.Cells["DanhGia"]?.Value != null)
                {
                    string danhGia = row.Cells["DanhGia"].Value.ToString();
                    row.Cells["DanhGia"].Style.BackColor = danhGia == "Đạt" ? Color.LightGreen :
                                                         danhGia == "Thiếu" ? Color.Orange : Color.Pink;
                }
            }
        }
        private void CreateNutritionChart()
        {
            if (analysisData == null) return;

            chartNutrition.Series.Clear();
            string[] nutrients = { "Calories", "Protein", "Carbs", "Fat", "Fiber" };

            foreach (string nutrient in nutrients)
            {
                if (!analysisData.Columns.Contains(nutrient)) continue;

                var series = chartNutrition.Series.Add(nutrient);
                series.ChartType = SeriesChartType.Column;

                foreach (DataRow row in analysisData.Rows)
                {
                    string label = $"{row["Thu"]}\n{row["Ngay"]}";
                    series.Points.AddXY(label, Convert.ToDouble(row[nutrient]));
                }
            }

            chartNutrition.ChartAreas[0].AxisX.Title = "Ngày";
            chartNutrition.ChartAreas[0].AxisY.Title = "Giá trị dinh dưỡng";
            chartNutrition.Legends[0].Docking = Docking.Bottom;
        }
        private void FormKiemTraDinhDuong_Load(object sender, EventArgs e)
        {

        }
    }
}
