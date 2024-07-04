using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace ApplicationManagementSystem.DataAccess
{
    public class PhieuDangKyThanhVien_DAO
    {
        public static List<PhieuDangKyThanhVien_BUS> LayDanhSach()
        {
            List<PhieuDangKyThanhVien_BUS> list = new List<PhieuDangKyThanhVien_BUS>();

            string sql = "SELECT * FROM PhieuDangKyThanhVien";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuDangKyThanhVien_BUS phieuDangKy = new();
                phieuDangKy.MaPhieu = (Guid) reader["MaPhieu"];
                phieuDangKy.TenCongTy = (string) reader["TenCongTy"];
                phieuDangKy.MaSoThue = (string) reader["MaSoThue"];
                phieuDangKy.NguoiDaiDien = (string) reader["NguoiDaiDien"];
                phieuDangKy.DiaChi = (string) reader["DiaChi"];
                phieuDangKy.Email = (string) reader["Email"];
                phieuDangKy.NgayTao = (DateTime) reader["NgayTao"];
                phieuDangKy.TrangThai = (string) reader["TrangThai"];
                list.Add(phieuDangKy);
            }
            reader.Close();

            return list;
        }

        public static int Them(PhieuDangKyThanhVien_BUS pdk)
        {
            string query = """
				INSERT INTO PhieuDangKyThanhVien(TenCongTy, MaSoThue, NguoiDaiDien, DiaChi, Email, NgayTao, TrangThai)
				VALUES(@TenCongTy, @MaSoThue, @NguoiDaiDien, @DiaChi, @Email, @NgayTao, @TrangThai);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@TenCongTy", SqlDbType.NVarChar).Value = pdk.TenCongTy;
            command.Parameters.Add("@MaSoThue", SqlDbType.NVarChar).Value = pdk.MaSoThue;
            command.Parameters.Add("@NguoiDaiDien", SqlDbType.NVarChar).Value = pdk.NguoiDaiDien;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = pdk.DiaChi;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = pdk.Email;
            command.Parameters.Add("@NgayTao", SqlDbType.DateTime).Value = pdk.NgayTao;
            command.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = pdk.TrangThai;

            return command.ExecuteNonQuery();
        }

        public static List<PhieuDangKyThanhVien_BUS> DocDanhSachHetHan()
        {
            List<PhieuDangKyThanhVien_BUS> list = new List<PhieuDangKyThanhVien_BUS>();
            DateTime now = DateTime.Now;
            DateTime date;
            if (now.Month == 1)
            {
                date = new DateTime(now.Year - 1, 12, 31);
            }
            else if (now.Month == 3)
            {
                if ((now.Year % 4) == 0)
                {
                    date = new DateTime(now.Year, now.Month - 1, 29);
                }
                else
                {
                    date = new DateTime(now.Year, now.Month - 1, 28);
                }
            }
            else if (now.Month == 5 || now.Month == 7 || now.Month == 8 || now.Month == 10 || now.Month == 12)
            {
                date = new DateTime(now.Year, now.Month - 1, 30);
            }
            else 
            {
                date = new DateTime(now.Year, now.Month - 1, 31);
            }
            string formatDate = date.ToString().Split(" ")[0];
            string sql = "SELECT DISTINCT PDK.*" +
                        " FROM PhieuDangKyThanhVien PDK JOIN PhieuTTDangTuyen PTT ON PDK.MaPhieu = PTT.MaPhieuDKTV" +
                        " WHERE datediff(d, @day, dateadd(day, PTT.KhoangThoiGian, PTT.NgayBatDau)) < 3 and datediff(d, @day, dateadd(day, PTT.KhoangThoiGian, PTT.NgayBatDau)) > 0;";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@day", formatDate);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuDangKyThanhVien_BUS phieuDangKy = new();
                phieuDangKy.MaPhieu = (Guid)reader["MaPhieu"];
                phieuDangKy.TenCongTy = (string)reader["TenCongTy"];
                phieuDangKy.MaSoThue = (string)reader["MaSoThue"];
                phieuDangKy.NguoiDaiDien = (string)reader["NguoiDaiDien"];
                phieuDangKy.DiaChi = (string)reader["DiaChi"];
                phieuDangKy.Email = (string)reader["Email"];
                phieuDangKy.NgayTao = (DateTime)reader["NgayTao"];
                phieuDangKy.TrangThai = (string)reader["TrangThai"];
                list.Add(phieuDangKy);
            }
            reader.Close();

            return list;
        }

        public static PhieuDangKyThanhVien_BUS DocPhieuDangKyThanhVien(Guid MaPhieu)
        {
            string sql = "SELECT * FROM PhieuDangKyThanhVien WHERE MaPhieu = @MaPhieu";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaPhieu", MaPhieu);
            var reader = command.ExecuteReader();
            PhieuDangKyThanhVien_BUS phieuDangKy = new();
            while (reader.Read())
            {
                phieuDangKy.MaPhieu = (Guid)reader["MaPhieu"];
                phieuDangKy.TenCongTy = (string)reader["TenCongTy"];
                phieuDangKy.MaSoThue = (string)reader["MaSoThue"];
                phieuDangKy.NguoiDaiDien = (string)reader["NguoiDaiDien"];
                phieuDangKy.DiaChi = (string)reader["DiaChi"];
                phieuDangKy.Email = (string)reader["Email"];
                phieuDangKy.NgayTao = (DateTime)reader["NgayTao"];
                phieuDangKy.TrangThai = (string)reader["TrangThai"];
            }
            reader.Close();
            return phieuDangKy;
        }
    }
}
