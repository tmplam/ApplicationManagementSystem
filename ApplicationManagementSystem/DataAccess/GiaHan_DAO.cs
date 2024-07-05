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
    class GiaHan_DAO
    {
        public static GiaHan_BUS DocThongTinGiaHan(int Thang, int Nam)
        {
            GiaHan_BUS gh = new GiaHan_BUS();
            string sql = "SELECT * FROM GiaHan WHERE Thang = @Thang AND Nam = @Nam";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@Thang", Thang);
            command.Parameters.AddWithValue("@Nam", Nam);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                gh.MaBLD = (string)reader["MaBLD"];
                gh.Thang = Thang;
                gh.Nam = Nam;
            }
            reader.Close();
            return gh;
        }

        public static int Them(GiaHan_BUS gh)
        {
            string query = """
				INSERT INTO GiaHan(Thang, Nam, MaBLD)
				VALUES(@Thang, @Nam, @MaBLD);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@Thang", SqlDbType.Int).Value = gh.Thang;
            command.Parameters.Add("@Nam", SqlDbType.Int).Value = gh.Nam;
            command.Parameters.Add("@MaBLD", SqlDbType.VarChar).Value = gh.MaBLD;

            return command.ExecuteNonQuery();
        }

        public static int KiemTra(int Thang, int Nam)
        {
            string sql = "SELECT * FROM GiaHan WHERE Thang = @Thang AND Nam = @Nam";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@Thang", Thang);
            command.Parameters.AddWithValue("@Nam", Nam);
            var reader = command.ExecuteReader();
            int rowCount = 0;
            if (reader.Read())
            {
                rowCount++;
            }
            reader.Close();
            return rowCount;
        }
    }
}
