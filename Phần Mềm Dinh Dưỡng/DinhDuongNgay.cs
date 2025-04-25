using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phần_Mềm_Dinh_Dưỡng
{
    public class DinhDuongNgay
    {
        public DateTime Ngay { get; set; }
        public string Thu { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public double Fiber { get; set; }

        // So sánh với tiêu chuẩn
        public string DanhGiaCalories(double standard)
        {
            double percent = (Calories / standard) * 100;
            return $"{Calories:F0} kcal ({percent:F0}%)";
        }
        public string DanhGiaProtein(double standard)
        {
            double percent = (Protein / standard) * 100;
            return $"{Protein:F0} g ({percent:F0}%)";
        }
        public string DanhGiaCarbs(double standard)
        {
            double percent = (Carbs / standard) * 100;
            return $"{Carbs:F0} g ({percent:F0}%)";
        }
        public string DanhGiaFat(double standard)
        {
            double percent = (Fat / standard) * 100;
            return $"{Fat:F0} g ({percent:F0}%)";
        }
        public string DanhGiaFiber(double standard)
        {
            double percent = (Fiber / standard) * 100;
            return $"{Fiber:F0} g ({percent:F0}%)";
        }
        public string DanhGiaTongHop(double standard)
        {
            double percent = (Calories / standard) * 100;
            return $"{Calories:F0} kcal ({percent:F0}%)";
        }
    }
}
