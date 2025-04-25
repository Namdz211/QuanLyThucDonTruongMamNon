using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phần_Mềm_Dinh_Dưỡng.Form_Quản_Lý_Chung
{
    public partial class FormChonTuan: Form
    {
       
            public DateTime MondayDate { get; private set; }

        public FormChonTuan()
        {
            InitializeComponent();
            dtpNgayBatDau.Value = GetThisMonday();
        }

        private DateTime GetThisMonday()
        {
            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            return today.AddDays(delta);
        }

        //private void btnXacNhan_Click(object sender, EventArgs e)
        //{
        //    MondayDate = dtpNgayBatDau.Value;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        //private void btnHuy_Click(object sender, EventArgs e)
        //{
        //    DialogResult = DialogResult.Cancel;
        //    Close();
        //}

        private void dtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayBatDau.Value.DayOfWeek != DayOfWeek.Monday)
            {
                MessageBox.Show("Vui lòng chọn ngày Thứ Hai", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpNgayBatDau.Value = GetThisMonday();
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnXacNhan_Click_1(object sender, EventArgs e)
        {
            MondayDate = dtpNgayBatDau.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
