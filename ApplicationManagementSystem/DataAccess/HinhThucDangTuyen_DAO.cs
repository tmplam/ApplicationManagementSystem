using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.DataAccess
{
    internal class HinhThucDangTuyen_DAO
    {
        public static List<HinhThucDangTuyen_BUS> layDSHinhThuc(Guid MaPhieuTTDT)
        {
            List<HinhThucDangTuyen_BUS> list = new List<HinhThucDangTuyen_BUS>();

            string sql = "SELECT h.MaHinhThuc, h.TenHinhThuc, h.GiaTien FROM PhieuDangKyQuangCao p " +
                "JOIN HinhThucDangTuyen h ON h.MaHinhThuc = p.MaHT " +
                "WHERE p.MaPhieuTTDT = @MaPhieuTTDT";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaPhieuTTDT", MaPhieuTTDT);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HinhThucDangTuyen_BUS hoaDon = new HinhThucDangTuyen_BUS
                {
                    MaHinhThuc = (Guid)reader["MaHinhThuc"],
                    TenHinhThuc = (string)reader["TenHinhThuc"],
                    GiaTien = (decimal)reader["GiaTien"]
                };
                list.Add(hoaDon);
            }
            reader.Close();
            return list;
        }
    }
}
