using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.DataAccess
{
    public class ChiTietHoSo_DAO
    {
        public static List<ChiTietHoSo_BUS> XemCTHoSo(string ma)
        {
            List<ChiTietHoSo_BUS> list = new List<ChiTietHoSo_BUS>();

            string sql = "SELECT * FROM ChiTietHoSo WHERE MaHoSo = @MaHS ";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaHS", ma);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ChiTietHoSo_BUS chiTietHoSo = new();
                chiTietHoSo.MaHoSo = (Guid)reader["MaHoSo"];
                chiTietHoSo.STT = (int)reader["STT"];
                chiTietHoSo.TenHoSo = (string)reader["TenHoSo"];
                chiTietHoSo.MoTa = (string)reader["MoTa"];
                list.Add(chiTietHoSo);
            }
            reader.Close();

            return list;
        }
    }
}
