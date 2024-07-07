using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationManagementSystem.DataAccess
{
    internal class PhieuThongTinDangTuyen_DAO
    {
        public static PhieuThongTinDangTuyen_BUS Them(PhieuThongTinDangTuyen_BUS pdk)
        {
            string query = """
				INSERT INTO PhieuTTDangTuyen(TenViTri, SoLuong, YeuCau, KhoangThoiGian, MaPhieuDKTV, MaNhanVien, NgayBatDau)
				OUTPUT inserted.*
				VALUES(@TenViTri, @SoLuong, @YeuCau, @KhoangThoiGian, @MaPhieuDKTV, @MaNhanVien, @NgayBatDau);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = pdk.NgayBatDau.Date;
            command.Parameters.Add("@TenViTri", SqlDbType.NVarChar).Value = pdk.TenViTri;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = pdk.SoLuong;
            command.Parameters.Add("@YeuCau", SqlDbType.NText).Value = pdk.YeuCau;
            command.Parameters.Add("@KhoangThoiGian", SqlDbType.Int).Value = pdk.KhoangThoiGian;
            command.Parameters.Add("@MaPhieuDKTV", SqlDbType.UniqueIdentifier).Value = pdk.MaPhieuDKTV;
            command.Parameters.Add("@MaNhanVien", SqlDbType.NVarChar).Value = pdk.MaNhanVien;

            SqlDataReader reader = command.ExecuteReader();

            PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                    phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                    phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                    phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                    phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                    phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                    phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));

                    break;
                }
            }
            reader.Close();

            return phieu;
        }


        public static PhieuThongTinDangTuyen_BUS Xem(Guid maPhieuTTDT)
        {
            string query = """
				SELECT * FROM PhieuTTDangTuyen WHERE MaPhieu=@MaPhieu
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@maPhieu", SqlDbType.UniqueIdentifier).Value = maPhieuTTDT;

            SqlDataReader reader = command.ExecuteReader();

            PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                    phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                    phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                    phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                    phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                    phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                    phieu.TinhTrangTT = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                    phieu.KieuTT = reader.GetString(reader.GetOrdinal("KieuTT"));
                    phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                    phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));
                    phieu.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));

                    break;
                }
            }
            reader.Close();

            return phieu;
        }
        public static int capNhapTinhTrangTT(Guid MaPhieu)
        {
            int rowsUpdated = 0;

            var command = new SqlCommand("CapNhapTinhTrangTT", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));

            rowsUpdated = command.ExecuteNonQuery();

            return rowsUpdated;
        }

        public static int capNhapKieuTT(Guid MaPhieu, string KTT)
        {
            string query = """
				UPDATE PhieuTTDangTuyen SET KieuTT = @KieuThanhToan WHERE MaPhieu = @MaPhieuTTDT;
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add("@MaPhieuTTDT", SqlDbType.UniqueIdentifier).Value = MaPhieu;
            command.Parameters.Add("@KieuThanhToan", SqlDbType.NVarChar).Value = KTT;
            return command.ExecuteNonQuery();
        }

        public static decimal tinhToanSoTienTT(Guid MaPhieu, string KieuTT)
        {
            decimal soTienCanThanhToanTiepTheo = 0;
            var command = new SqlCommand("TinhSoTienCanThanhToanTiepTheo", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));
            command.Parameters.Add(new SqlParameter("@KieuThanhToan", KieuTT));
            command.Parameters.Add(new SqlParameter("@SoTienCanThanhToanTiepTheo", System.Data.SqlDbType.Decimal));
            command.Parameters["@SoTienCanThanhToanTiepTheo"].Direction = System.Data.ParameterDirection.Output;

            command.ExecuteNonQuery();

            // Lấy giá trị đầu ra từ tham số @SoTienCanThanhToanTiepTheo
            soTienCanThanhToanTiepTheo = Convert.ToDecimal(command.Parameters["@SoTienCanThanhToanTiepTheo"].Value);
            return soTienCanThanhToanTiepTheo;
        }


        public static PhieuThongTinDangTuyen_BUS docThongTin(Guid MaPhieuTTDT)
        {
            PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();
            try
            {
                string sql = "SELECT * FROM PhieuTTDangTuyen WHERE MaPhieu = @MaPhieuTTDT";
                var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
                command.Parameters.AddWithValue("@MaPhieuTTDT", MaPhieuTTDT);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                    phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                    phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                    phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                    phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                    phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                    phieu.TinhTrangTT = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                    phieu.KieuTT = reader.GetString(reader.GetOrdinal("KieuTT"));
                    phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                    phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));
                    phieu.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                }
                reader.Close(); // Đóng reader sau khi sử dụng  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                // Xử lý lỗi nếu cần thiết
            }
            return phieu;
        }

        public static decimal tinhToanSoTienChuaTT(Guid MaPhieu, string KieuTT)
        {
            decimal soTienChuaThanhToan = 0;
            var command = new SqlCommand("TinhSoTienChuaThanhToan", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));
            command.Parameters.Add(new SqlParameter("@KieuThanhToan", KieuTT));
            command.Parameters.Add(new SqlParameter("@SoTienChuaThanhToan", System.Data.SqlDbType.Decimal));
            command.Parameters["@SoTienChuaThanhToan"].Direction = System.Data.ParameterDirection.Output;

            command.ExecuteNonQuery();

            // Lấy giá trị đầu ra từ tham số @SoTienCanThanhToanTiepTheo
            soTienChuaThanhToan = Convert.ToDecimal(command.Parameters["@SoTienChuaThanhToan"].Value);
            return soTienChuaThanhToan;
        }

        public static List<PhieuThongTinDangTuyen_BUS> layDSPhieuTTDT()
        {
            List<PhieuThongTinDangTuyen_BUS> list = new List<PhieuThongTinDangTuyen_BUS>();

            string sql = "SELECT * FROM PhieuTTDangTuyen";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();

                phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                phieu.TinhTrangTT = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                phieu.KieuTT = reader.GetString(reader.GetOrdinal("KieuTT"));
                phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));
                phieu.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                list.Add(phieu);
            }
            reader.Close();
            return list;
        }

        public static List<PhieuThongTinDangTuyen_BUS> XemDSPhieuDangTuyen(string trangthai)
        {
            List<PhieuThongTinDangTuyen_BUS> list = new List<PhieuThongTinDangTuyen_BUS>();
            string sql;
            if (trangthai == "Tất cả")
            {
                sql = "SELECT * FROM PhieuTTDangTuyen";
            }
            else
            {
                sql = $"SELECT * FROM PhieuTTDangTuyen WHERE TinhTrang = N'{trangthai}'";
            }

            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();

                phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                phieu.TinhTrangTT = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                phieu.KieuTT = reader.GetString(reader.GetOrdinal("KieuTT"));
                phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));
                phieu.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                list.Add(phieu);
            }
            reader.Close();
            return list;
        }

        public static int CapNhatTTPhieuDT(Guid ma, string trangthai) //SỬA
        {
            string query = "UPDATE PhieuTTDangTuyen SET TinhTrang = @TrangThai WHERE MaPhieu = @MaPhieu;";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@TrangThai", trangthai);
            command.Parameters.AddWithValue("@MaPhieu", ma);
            return command.ExecuteNonQuery();
        }

        public static List<PhieuThongTinDangTuyen_BUS> Tim(string keyword)
        {
            List<PhieuThongTinDangTuyen_BUS> list = new List<PhieuThongTinDangTuyen_BUS>();
            string query = $"SELECT * FROM PhieuTTDangTuyen WHERE TenViTri like N'%{keyword}%'";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@ViTri", keyword);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuThongTinDangTuyen_BUS phieu = new PhieuThongTinDangTuyen_BUS();

                phieu.MaPhieu = reader.GetGuid(reader.GetOrdinal("MaPhieu"));
                phieu.TenViTri = reader.GetString(reader.GetOrdinal("TenViTri"));
                phieu.SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"));
                phieu.YeuCau = reader.GetString(reader.GetOrdinal("YeuCau"));
                phieu.KhoangThoiGian = reader.GetInt32(reader.GetOrdinal("KhoangThoiGian"));
                phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                phieu.TinhTrangTT = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                phieu.KieuTT = reader.GetString(reader.GetOrdinal("KieuTT"));
                phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));
                phieu.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                list.Add(phieu);
            }
            reader.Close();

            return list;
        }
    }
}
