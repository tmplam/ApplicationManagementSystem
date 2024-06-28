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
        public static int Them(PhieuThongTinDangTuyen_BUS pdk)
        {
            string query = """
				INSERT INTO PhieuTTDangTuyen(TenViTri, SoLuong, YeuCau, KhoangThoiGian, TinhTrang, TinhTrangTT, KieuTT, MaPhieuDKTV, MaNhanVien)
				VALUES(@TenCongTy, @MaSoThue, @NguoiDaiDien, @DiaChi, @Email, @NgayTao, @TrangThai);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@TenViTri", SqlDbType.NVarChar).Value = pdk.TenViTri;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = pdk.SoLuong;
            command.Parameters.Add("@YeuCau", SqlDbType.Text).Value = pdk.YeuCau;
            command.Parameters.Add("@KhoangThoiGian", SqlDbType.Text).Value = pdk.KhoangThoiGian;
            command.Parameters.Add("@TinhTrang", SqlDbType.NVarChar).Value = pdk.TinhTrang;
            command.Parameters.Add("@TinhTrangTT", SqlDbType.NVarChar).Value = pdk.TinhTrangThanhToan;
            command.Parameters.Add("@KieuTT", SqlDbType.NVarChar).Value = pdk.KieuThanhToan;
            command.Parameters.Add("@MaPhieuDKTV", SqlDbType.UniqueIdentifier).Value = pdk.MaPhieuDKTV;
            command.Parameters.Add("@MaNhanVien", SqlDbType.NVarChar).Value = pdk.MaNhanVien;

            return command.ExecuteNonQuery();
        }
    }
}
