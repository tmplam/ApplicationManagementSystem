using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;

namespace ApplicationManagementSystem.DataAccess
{
    internal class HoaDon_DAO
    {
        public static List<HoaDon_BUS> layDSHoaDon(Guid MaPhieuTTDT)
        {
            List<HoaDon_BUS> list = new List<HoaDon_BUS>();

            string sql = "SELECT * FROM HoaDon WHERE MaPhieuTTDT = @MaPhieuTTDT";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaPhieuTTDT", MaPhieuTTDT);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HoaDon_BUS hoaDon = new HoaDon_BUS
                {
                    NgayThanhToan = (DateTime)reader["NgayThanhToan"],
                    SoTienThanhToan = (decimal)reader["SoTienThanhToan"],
                    HinhThucThanhToan = (string)reader["HinhThucThanhToan"],
                    MaPhieuTTDT = (Guid)reader["MaPhieuTTDT"],
                    MaNhanVien = (string)reader["MaNhanVien"]
                };
                list.Add(hoaDon);
            }
            reader.Close();
            return list;
        }

        public static int themHoaDon(HoaDon_BUS hd)
        {
            string query = """
				INSERT INTO HoaDon(NgayThanhToan, SoTienThanhToan, HinhThucThanhToan, MaPhieuTTDT, MaNhanVien)
				VALUES(@NgayThanhToan, @SoTienThanhToan, @HinhThucThanhToan, @MaPhieuTTDT, @MaNhanVien);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@NgayThanhToan", SqlDbType.DateTime).Value = hd.NgayThanhToan;
            command.Parameters.Add("@SoTienThanhToan", SqlDbType.Decimal).Value = hd.SoTienThanhToan;
            command.Parameters.Add("@HinhThucThanhToan", SqlDbType.NVarChar).Value = hd.HinhThucThanhToan;
            command.Parameters.Add("@MaPhieuTTDT", SqlDbType.UniqueIdentifier).Value = hd.MaPhieuTTDT;
            command.Parameters.Add("@MaNhanVien", SqlDbType.VarChar).Value = hd.MaNhanVien;

            return command.ExecuteNonQuery();
        }
    }
}
