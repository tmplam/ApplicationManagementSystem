#nullable disable

using ApplicationManagementSystem.DataAccess;
using System.Text.RegularExpressions;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class UngVien_BUS
    {
        public string MaUngVien { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public static bool KiemTraEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra email hợp lệ
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        
        #nullable enable
        public static string? KiemTraDauVao(UngVien_BUS pdk)
        {
            if (string.IsNullOrEmpty(pdk.HoTen))
            {
                return "Chưa nhập tên ứng viên.";
            }
            else if (pdk.NgaySinh == DateTime.MinValue)
            {
                return "Chưa nhập ngày sinh.";
            }
            else if (string.IsNullOrEmpty(pdk.SoDienThoai))
            {
                return "Chưa nhập số điện thoại.";
            }
            else if (string.IsNullOrEmpty(pdk.Email))
            {
                return "Chưa nhập email.";
            }

            if (!KiemTraEmail(pdk.Email))
            {
                return "Email không hợp lệ";
            }

            return null;
        }

        public static int ThemPhieuDangKy(UngVien_BUS pdk)
        {
            return UngVien_DAO.Them(pdk);
        }

        public static List<UngVien_BUS> LayDanhSachUngVien(string name = null)
        {
            return UngVien_DAO.LayDanhSach(name);
        }
    }
}
