using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
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

        public static bool KiemTraEmail(string Email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(Email, pattern);
        }

        public static bool KiemTraChiChuaChuSo(string soDienThoai)
        {
            string pattern = @"^\d+$";
            return Regex.IsMatch(soDienThoai, pattern);
        }

        public static bool KiemTraDoDaiTu10Den12(string soDienThoai)
        {
            string pattern = @"^\d{10,12}$";
            return Regex.IsMatch(soDienThoai, pattern);
        }

        public static bool KiemTraNgaySinhHopLe(DateTime ngaySinh)
        {
            return ngaySinh <= DateTime.Today;
        }

        public static string KiemTraDauVao(UngVien_BUS uv)
        {
            if (string.IsNullOrEmpty(uv.HoTen))
            {
                return "Chưa nhập tên ứng viên.";
            }
            else if (uv.NgaySinh == DateTime.MinValue)
            {
                return "Chưa nhập ngày sinh.";
            }
            else if (!KiemTraNgaySinhHopLe(uv.NgaySinh))
            {
                return "Ngày sinh không hợp lệ (phải nhỏ hơn hoặc bằng ngày hôm nay).";
            }
            else if (string.IsNullOrEmpty(uv.SoDienThoai))
            {
                return "Chưa nhập số điện thoại.";
            }
            else if (!KiemTraChiChuaChuSo(uv.SoDienThoai))
            {
                return "Số điện thoại không hợp lệ (phải là chữ số).";
            }
            else if (!KiemTraDoDaiTu10Den12(uv.SoDienThoai))
            {
                return "Số điện thoại không hợp lệ (phải có 10 - 12 chữ số).";
            }
            else if (string.IsNullOrEmpty(uv.Email))
            {
                return "Chưa nhập email.";
            }

            if (!KiemTraEmail(uv.Email))
            {
                return "Email không hợp lệ.";
            }

            return null;
        }

        public static int ThemPhieuDangKy(UngVien_BUS uv)
        {
            return UngVien_DAO.Them(uv);
        }

        public static List<UngVien_BUS> LayDanhSachUngVien(string name = null)
        {
            return UngVien_DAO.LayDanhSach(name);
        }
    }
}
