using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.DataAccess
{
    internal class PhieuThongTinDangTuyen_DAO
    {
        public static PhieuThongTinDangTuyen_BUS Them(PhieuThongTinDangTuyen_BUS pdk)
        {
            string query = """
				INSERT INTO PhieuTTDangTuyen(TenViTri, SoLuong, YeuCau, KhoangThoiGian, MaPhieuDKTV, MaNhanVien)
				OUTPUT inserted.*
				VALUES(@TenViTri, @SoLuong, @YeuCau, @KhoangThoiGian, @MaPhieuDKTV, @MaNhanVien);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@TenViTri", SqlDbType.NVarChar).Value = pdk.TenViTri;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = pdk.SoLuong;
            command.Parameters.Add("@YeuCau", SqlDbType.Text).Value = pdk.YeuCau;
            command.Parameters.Add("@KhoangThoiGian", SqlDbType.Text).Value = pdk.KhoangThoiGian;
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
                    phieu.KhoangThoiGian = reader.GetString(reader.GetOrdinal("KhoangThoiGian"));
                    phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                    phieu.TinhTrangThanhToan = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                    phieu.KieuThanhToan = reader.GetString(reader.GetOrdinal("KieuTT"));
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
                    phieu.KhoangThoiGian = reader.GetString(reader.GetOrdinal("KhoangThoiGian"));
                    phieu.TinhTrang = reader.GetString(reader.GetOrdinal("TinhTrang"));
                    phieu.TinhTrangThanhToan = reader.GetString(reader.GetOrdinal("TinhTrangTT"));
                    phieu.KieuThanhToan = reader.GetString(reader.GetOrdinal("KieuTT"));
                    phieu.MaPhieuDKTV = reader.GetGuid(reader.GetOrdinal("MaPhieuDKTV"));
                    phieu.MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien"));

                    break;
                }
            }
            reader.Close(); 

            return phieu;
        }
    }
}
