using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ApplicationManagementSystem.DataAccess
{
    class BanLanhDao_DAO
    {
        public static BanLanhDao_BUS DocThongTinLanhDao(string MaBLD)
        {
            BanLanhDao_BUS bld = new BanLanhDao_BUS();
            string sql = "SELECT * FROM BanLanhDao WHERE MaBLD = @MaBLD";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaBLD", MaBLD);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                bld.MaBLD = (string)reader["MaBLD"];
                bld.HoTen = (string)reader["HoTen"];
                bld.ChucVu = (string)reader["ChucVu"];
                bld.SDT = (string)reader["SDT"];
            }
            reader.Close();
            return bld;
        }

        public static List<string> DocDanhSachLanhDao()
        {
            List<string> list = new List<string>();
            string sql = "SELECT * FROM BanLanhDao";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string HoTen = (string)reader["HoTen"];
                list.Add(HoTen);
            }
            reader.Close();
            return list;
        }

        public static string DocMaBanLanhDao(string Hoten)
        {
            string MaBLD = "";
            string sql = "SELECT MaBLD FROM BanLanhDao WHERE HoTen = @HoTen";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@HoTen", Hoten);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                MaBLD = (string)reader["MaBLD"];
            }
            reader.Close();
            return MaBLD;
        }
    }
}
