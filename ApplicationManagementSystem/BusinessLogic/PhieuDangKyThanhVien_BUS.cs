#nullable disable

using ApplicationManagementSystem.DataAccess;
using System.Text.RegularExpressions;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class PhieuDangKyThanhVien_BUS
    {
        public Guid MaPhieu { get; set; }
        public string TenCongTy { get; set; }
        public string MaSoThue { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }

        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra email hợp lệ
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool KiemTraTonTai(string maSothue)
        {
            return true;
        }

        public static string? KiemTraDauVao(PhieuDangKyThanhVien_BUS pdk)
        {
            if (string.IsNullOrEmpty(pdk.TenCongTy))
            {
                return "Chưa nhập tên công ty.";
            }
            else if (string.IsNullOrEmpty(pdk.MaSoThue))
            {
                return "Chưa nhập mã số thuế.";
            }
            else if (string.IsNullOrEmpty(pdk.NguoiDaiDien))
            {
                return "Chưa nhập người đại diện.";
            }
            else if(string.IsNullOrEmpty(pdk.DiaChi))
            {
                return "Chưa nhập địa chỉ.";
            }
            else if(string.IsNullOrEmpty(pdk.Email))
            {
                return "Chưa nhập email.";
            }

            if (!IsValidEmail(pdk.Email))
                return "Email không hợp lệ";

            return null;
        }

        public static int ThemPhieuDangKy(PhieuDangKyThanhVien_BUS pdk)
        {
            return PhieuDangKyThanhVien_DAO.Them(pdk);
        }

        public static List<PhieuDangKyThanhVien_BUS> LayDanhSachPhieuDangKy()
        {
            return PhieuDangKyThanhVien_DAO.LayDanhSach();
        }
    }
}
