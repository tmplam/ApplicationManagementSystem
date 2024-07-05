using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.DataAccess
{
    public class HoSoUngTuyen_DAO
    {
        public static List<HoSoUngTuyen_BUS> XemDSPhieuUngTuyen(Guid mahs)
        {
            List<HoSoUngTuyen_BUS> list = new List<HoSoUngTuyen_BUS>();

            string sql = $"SELECT * FROM PhieuDangKyUngTuyen WHERE MaPhieuTTDT = '{mahs}' AND TinhTrang <> N'Chờ xác thực'";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HoSoUngTuyen_BUS phieuUngTuyen = new();
                phieuUngTuyen.MaPhieu = (Guid)reader["MaPhieu"];
                phieuUngTuyen.MaUV = (string)reader["MaUngVien"];
                phieuUngTuyen.MaPhieuTTDT = (Guid)reader["MaPhieuTTDT"];
                phieuUngTuyen.TinhTrangHS = (string)reader["TinhTrang"];
                phieuUngTuyen.DoUuTien = (int)reader["DoUuTien"];
                phieuUngTuyen.NgayLap = (DateTime)reader["NgayLap"];
                phieuUngTuyen.MaNhanVien = (string)reader["MaNhanVien"];
                list.Add(phieuUngTuyen);
            }
            reader.Close();

            return list;
        }

        public static List<HoSoUngTuyen_BUS> LocPhieuUngTuyen(Guid ma, string trangthai)
        {
            List<HoSoUngTuyen_BUS> list = new List<HoSoUngTuyen_BUS>();
            string sql;
            if (trangthai == "Tất cả")
            {
                sql = $"SELECT * FROM PhieuDangKyUngTuyen WHERE MaPhieuTTDT = '{ma}' AND TinhTrang <> N'Chờ xác thực'";
            }
            else
            {
                sql = $"SELECT * FROM PhieuDangKyUngTuyen WHERE TinhTrang = N'{trangthai}'";
            }

            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HoSoUngTuyen_BUS phieuUngTuyen = new();
                phieuUngTuyen.MaPhieu = (Guid)reader["MaPhieu"];
                phieuUngTuyen.MaUV = (string)reader["MaUngVien"];
                phieuUngTuyen.MaPhieuTTDT = (Guid)reader["MaPhieuTTDT"];
                phieuUngTuyen.TinhTrangHS = (string)reader["TinhTrang"];
                phieuUngTuyen.DoUuTien = (int)reader["DoUuTien"];
                phieuUngTuyen.NgayLap = (DateTime)reader["NgayLap"];
                phieuUngTuyen.MaNhanVien = (string)reader["MaNhanVien"];
                list.Add(phieuUngTuyen);
            }
            reader.Close();

            return list;
        }

        public static int CapNhatTTPhieuUT(Guid ma, string trangthai)
        {
            string query = $"UPDATE PhieuDangKyUngTuyen SET TinhTrang = N'{trangthai}' WHERE MaPhieu = '{ma}';";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            return command.ExecuteNonQuery();
        }

        public static int CapNhatUuTienPhieuUT(Guid ma, int uutien)
        {
            string query = $"UPDATE PhieuDangKyUngTuyen SET DoUuTien = {uutien} WHERE MaPhieu = N'{ma}';";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@TrangThai", uutien);
            command.Parameters.AddWithValue("@MaPhieu", ma);
            return command.ExecuteNonQuery();
        }

        public static List<HoSoUngTuyen_BUS> Tim(string keyword)
        {
            List<HoSoUngTuyen_BUS> list = new List<HoSoUngTuyen_BUS>();
            string query = "SELECT * FROM PhieuDangKyUngTuyen WHERE TenViTri = @ViTri";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@ViTri", keyword);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HoSoUngTuyen_BUS phieuUngTuyen = new();
                phieuUngTuyen.MaPhieu = (Guid)reader["MaPhieu"];
                phieuUngTuyen.MaUV = (string)reader["MaUngVien"];
                phieuUngTuyen.MaPhieuTTDT = (Guid)reader["MaPhieuTTDT"];
                phieuUngTuyen.TinhTrangHS = (string)reader["TinhTrang"];
                phieuUngTuyen.DoUuTien = (int)reader["DoUuTien"];
                phieuUngTuyen.NgayLap = (DateTime)reader["NgayLap"];
                phieuUngTuyen.MaNhanVien = (string)reader["MaNhanVien"];
                list.Add(phieuUngTuyen);
            }
            reader.Close();

            return list;
        }
    }
}
