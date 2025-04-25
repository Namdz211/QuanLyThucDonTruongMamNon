using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Phần_Mềm_Dinh_Dưỡng.Form_Quản_Lý_Chung
{
    public partial class FormXemThucDonTuan : Form
{
    private string connectionString = "Data Source=.;Initial Catalog=ThucDonMamNon;Integrated Security=True";
    private DataTable dataSource;
    
    public FormXemThucDonTuan(DateTime mondayDate, string nhomTre)
    {
        InitializeComponent();
        this.Text = $"Thực đơn tuần từ {mondayDate.ToString("dd/MM/yyyy")} - {nhomTre}";
        LoadThucDon(mondayDate, nhomTre);
    }

    private void LoadThucDon(DateTime mondayDate, string nhomTre)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT td.*, 
                                ma_sc.TenMon AS Ten_Sang_Chao, ma_tc.TenMon AS Ten_Trua_Chao,
                                ma_ttm.TenMon AS Ten_Trua_TrangMieng, ma_cc.TenMon AS Ten_Chieu_Chao,
                                ma_csc.TenMon AS Ten_Chieu_SuaChua, ma_smc.TenMon AS Ten_Sang_MonChinh,
                                ma_stm.TenMon AS Ten_Sang_TrangMieng, ma_tmm.TenMon AS Ten_Trua_MonMan,
                                ma_tmc.TenMon AS Ten_Trua_MonCanh, ma_tmr.TenMon AS Ten_Trua_MonRau,
                                ma_ttm_mg.TenMon AS Ten_Trua_TrangMieng_MG, ma_cmc.TenMon AS Ten_Chieu_MonChinh,
                                ma_ctm.TenMon AS Ten_Chieu_TrangMieng
                                FROM ThucDon td
                                LEFT JOIN MonAn ma_sc ON td.NT_Sang_Chao = ma_sc.MaMon
                                LEFT JOIN MonAn ma_tc ON td.NT_Trua_Chao = ma_tc.MaMon
                                LEFT JOIN MonAn ma_ttm ON td.NT_Trua_TrangMieng = ma_ttm.MaMon
                                LEFT JOIN MonAn ma_cc ON td.NT_Chieu_Chao = ma_cc.MaMon
                                LEFT JOIN MonAn ma_csc ON td.NT_Chieu_SuaChua = ma_csc.MaMon
                                LEFT JOIN MonAn ma_smc ON td.MG_Sang_MonChinh = ma_smc.MaMon
                                LEFT JOIN MonAn ma_stm ON td.MG_Sang_TrangMieng = ma_stm.MaMon
                                LEFT JOIN MonAn ma_tmm ON td.MG_Trua_MonMan = ma_tmm.MaMon
                                LEFT JOIN MonAn ma_tmc ON td.MG_Trua_MonCanh = ma_tmc.MaMon
                                LEFT JOIN MonAn ma_tmr ON td.MG_Trua_MonRau = ma_tmr.MaMon
                                LEFT JOIN MonAn ma_ttm_mg ON td.MG_Trua_TrangMieng = ma_ttm_mg.MaMon
                                LEFT JOIN MonAn ma_cmc ON td.MG_Chieu_MonChinh = ma_cmc.MaMon
                                LEFT JOIN MonAn ma_ctm ON td.MG_Chieu_TrangMieng = ma_ctm.MaMon
                                WHERE td.Ngay BETWEEN @Monday AND @Friday
                                AND td.NhomTre = @NhomTre
                                ORDER BY td.Ngay";
                                
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Monday", mondayDate);
                da.SelectCommand.Parameters.AddWithValue("@Friday", mondayDate.AddDays(4));
                da.SelectCommand.Parameters.AddWithValue("@NhomTre", nhomTre);
                
                dataSource = new DataTable();
                da.Fill(dataSource);
                
                dgvThucDon.DataSource = dataSource;
                FormatDataGridView(nhomTre);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tải thực đơn: {ex.Message}", "Lỗi", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FormatDataGridView(string nhomTre)
    {
        dgvThucDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        
        // Ẩn các cột không cần thiết
        string[] hiddenColumns = { "ID", "NhomTre", "NgayTao", "NguoiTao" };
        foreach (string col in hiddenColumns)
        {
            if (dgvThucDon.Columns.Contains(col))
                dgvThucDon.Columns[col].Visible = false;
        }
        
        // Đổi tên cột cho dễ đọc
        Dictionary<string, string> columnNames = new Dictionary<string, string>()
        {
            { "Ngay", "Ngày" },
            { "Thu", "Thứ" },
            { "Ten_Sang_Chao", "Sáng: Cháo" },
            { "Ten_Trua_Chao", "Trưa: Cháo" },
            { "Ten_Trua_TrangMieng", "Trưa: Tráng miệng" },
            // Thêm các cột khác tương tự
        };
        
        foreach (var pair in columnNames)
        {
            if (dgvThucDon.Columns.Contains(pair.Key))
                dgvThucDon.Columns[pair.Key].HeaderText = pair.Value;
        }
        
        // Ẩn các cột không thuộc nhóm được chọn
        if (nhomTre == "Nhà trẻ")
        {
            string[] mgColumns = { "MG_Sang_MonChinh", "MG_Sang_TrangMieng", /* các cột khác của mẫu giáo */ };
            foreach (string col in mgColumns)
            {
                if (dgvThucDon.Columns.Contains(col))
                    dgvThucDon.Columns[col].Visible = false;
            }
        }
        else
        {
            string[] ntColumns = { "NT_Sang_Chao", "NT_Trua_Chao", /* các cột khác của nhà trẻ */ };
            foreach (string col in ntColumns)
            {
                if (dgvThucDon.Columns.Contains(col))
                    dgvThucDon.Columns[col].Visible = false;
            }
        }
    }

    private void btnInThucDon_Click(object sender, EventArgs e)
    {
        printPreviewDialog1.Document = printDocument1;
        printPreviewDialog1.ShowDialog();
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        Font titleFont = new Font("Arial", 16, FontStyle.Bold);
        Font headerFont = new Font("Arial", 12, FontStyle.Bold);
        Font contentFont = new Font("Arial", 10);
        
        float yPos = 50;
        float leftMargin = e.MarginBounds.Left;
        float rightMargin = e.MarginBounds.Right;
        
        // Tiêu đề
        string title = $"THỰC ĐƠN TUẦN - {this.Text}";
        e.Graphics.DrawString(title, titleFont, Brushes.Black, 
                             (e.PageBounds.Width - e.Graphics.MeasureString(title, titleFont).Width) / 2, yPos);
        yPos += 40;
        
        // Vẽ bảng
        float[] columnWidths = { 80, 80, 150, 150, 150 }; // Điều chỉnh theo nhu cầu
        string[] headers = { "Ngày", "Thứ", "Bữa sáng", "Bữa trưa", "Bữa chiều" };
        
        // Vẽ header
        for (int i = 0; i < headers.Length; i++)
        {
            e.Graphics.DrawString(headers[i], headerFont, Brushes.Black, 
                                 leftMargin + (i > 0 ? columnWidths.Take(i).Sum() : 0), yPos);
            e.Graphics.DrawRectangle(Pens.Black, leftMargin + (i > 0 ? columnWidths.Take(i).Sum() : 0), yPos, 
                                   columnWidths[i], 30);
        }
        yPos += 30;
        
        // Vẽ dữ liệu
        foreach (DataRow row in dataSource.Rows)
        {
            string[] rowData = {
                Convert.ToDateTime(row["Ngay"]).ToString("dd/MM"),
                row["Thu"].ToString(),
                GetMealInfo(row, "Sáng"),
                GetMealInfo(row, "Trưa"),
                GetMealInfo(row, "Chiều")
            };
            
            for (int i = 0; i < rowData.Length; i++)
            {
                e.Graphics.DrawString(rowData[i], contentFont, Brushes.Black, 
                                     leftMargin + (i > 0 ? columnWidths.Take(i).Sum() : 0), yPos);
                e.Graphics.DrawRectangle(Pens.Black, leftMargin + (i > 0 ? columnWidths.Take(i).Sum() : 0), yPos, 
                                       columnWidths[i], 40);
            }
            yPos += 40;
        }
    }

    private string GetMealInfo(DataRow row, string mealTime)
    {
        if (row.Table.Columns.Contains("NhomTre") && row["NhomTre"].ToString() == "Nhà trẻ")
        {
            switch (mealTime)
            {
                case "Sáng": return row["Ten_Sang_Chao"].ToString();
                case "Trưa": return $"{row["Ten_Trua_Chao"]}\n{row["Ten_Trua_TrangMieng"]}";
                case "Chiều": return $"{row["Ten_Chieu_Chao"]}\n{row["Ten_Chieu_SuaChua"]}";
                default: return "";
            }
        }
        else
        {
            switch (mealTime)
            {
                case "Sáng": return $"{row["Ten_Sang_MonChinh"]}\n{row["Ten_Sang_TrangMieng"]}";
                case "Trưa": return $"{row["Ten_Trua_MonMan"]}\n{row["Ten_Trua_MonCanh"]}\n{row["Ten_Trua_MonRau"]}\n{row["Ten_Trua_TrangMieng_MG"]}";
                case "Chiều": return $"{row["Ten_Chieu_MonChinh"]}\n{row["Ten_Chieu_TrangMieng"]}";
                default: return "";
            }
        }
    }
}
}
        
