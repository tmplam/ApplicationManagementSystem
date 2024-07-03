using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

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

        public static PhieuDangKyThanhVien_BUS docThongTin(Guid MaPhieuDKTV)
        {
            PhieuDangKyThanhVien_BUS phieuDangKy = null;
            try
            {
                string sql = "SELECT * FROM PhieuDangKyThanhVien WHERE MaPhieu = @MaPhieuDKTV";
                var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
                command.Parameters.AddWithValue("@MaPhieuDKTV", MaPhieuDKTV);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    phieuDangKy = new PhieuDangKyThanhVien_BUS
                    {
                        MaPhieu = (Guid)reader["MaPhieu"],
                        TenCongTy = (string)reader["TenCongTy"],
                        MaSoThue = (string)reader["MaSoThue"],
                        NguoiDaiDien = (string)reader["NguoiDaiDien"],
                        DiaChi = (string)reader["DiaChi"],
                        Email = (string)reader["Email"],
                        NgayTao = (DateTime)reader["NgayTao"],
                        TrangThai = (string)reader["TrangThai"]
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                // Xử lý lỗi nếu cần thiết
            }
            return phieuDangKy;
        }

    }
}
