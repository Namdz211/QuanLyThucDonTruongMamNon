using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phần_Mềm_Dinh_Dưỡng
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
            // Bật chế độ nhận phím toàn cục
            this.KeyPreview = true;

            // Đăng ký sự kiện
            this.KeyDown += FormDangNhap_KeyDown;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // Thiết lập nút mặc định
            this.AcceptButton = btnLogin; // Enter sẽ kích hoạt btnLogin
        }
        // 1. Đăng nhập bằng phím Enter khi ở ô mật khẩu
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick(); // Kích hoạt sự kiện click của nút đăng nhập
                e.SuppressKeyPress = true; // Ngăn tiếng "bíp" của hệ thống
            }
        }

        // 2. Di chuyển giữa các ô bằng phím mũi tên
        // 3. Thoát bằng phím Esc
        private void FormDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // Di chuyển lên/xuống bằng phím mũi tên
                case Keys.Down:
                    if (txtUsername.Focused) txtPassword.Focus();
                    else if (txtPassword.Focused) btnLogin.Focus();
                    e.Handled = true;
                    break;

                case Keys.Up:
                    if (txtPassword.Focused) txtUsername.Focus();
                    else if (btnLogin.Focused) txtPassword.Focus();
                    e.Handled = true;
                    break;

                // Thoát bằng phím Esc
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }
       

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private string connectionString = "Data Source=.;Initial Catalog=DangNhap;Integrated Security=True;";


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện đăng nhập khi nhấn Enter
                button2_Click(sender, e);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (CheckLogin(username, password))
            {
                MessageBox.Show("Đăng nhập thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Dashboard mainForm = new Dashboard();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT  COUNT(*) FROM DangNhap WHERE userName = @Username AND password = @Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar(); // Lấy số tk khớp
                    return count > 0; // Nếu có ít nhất tài khoảng khớp
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // 🔹 Hàm hỗ trợ mã hóa mật khẩu bằng SHA256



        private void label3_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình không !", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}