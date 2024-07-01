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
    public class HinhThucDangTuyen_DAO
    {
        public static List<HinhThucDangTuyen_BUS> LayDS()
        {
            List<HinhThucDangTuyen_BUS> dsHinhThuc = new List<HinhThucDangTuyen_BUS>();
            string query = """
				SELECT * FROM HinhThucDangTuyen; 
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HinhThucDangTuyen_BUS hinhThuc = new HinhThucDangTuyen_BUS();
                    hinhThuc.MaHinhThuc = reader.GetGuid(reader.GetOrdinal("MaHinhThuc"));
                    hinhThuc.TenHinhThuc = reader.GetString(reader.GetOrdinal("TenHinhThuc"));
                    hinhThuc.GiaTien = reader.GetDecimal(reader.GetOrdinal("GiaTien"));

                    dsHinhThuc.Add(hinhThuc);
                }
            }

            reader.Close();

            return dsHinhThuc;
        }

        public static HinhThucDangTuyen_BUS Xem(Guid maHinhThuc)
        {
            HinhThucDangTuyen_BUS hinhThuc = new HinhThucDangTuyen_BUS();
            string query = """
				SELECT * FROM HinhThucDangTuyen WHERE MaHinhThuc=@MaHinhThuc; 
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.Add("@MaHinhThuc", SqlDbType.UniqueIdentifier).Value = maHinhThuc;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    hinhThuc.MaHinhThuc = reader.GetGuid(reader.GetOrdinal("MaHinhThuc"));
                    hinhThuc.TenHinhThuc = reader.GetString(reader.GetOrdinal("TenHinhThuc"));
                    hinhThuc.GiaTien = reader.GetDecimal(reader.GetOrdinal("GiaTien"));

                    break;
                }
            }

            reader.Close();

            return hinhThuc;
        }
    }
}
