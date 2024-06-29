using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace ApplicationManagementSystem.DataAccess
{
    public class UngVien_DAO
    {
        public static List<UngVien_BUS> LayDanhSach(string name = null)
        {
            List<UngVien_BUS> list = new List<UngVien_BUS>();

            string sql = "SELECT * FROM UngVien";
            if (name != null)
            {
                sql += " WHERE HoTen LIKE @HoTen";
            }

            using (var command = new SqlCommand(sql, DbUtils.getInstance().Connection))
            {
                if (name != null)
                {
                    command.Parameters.AddWithValue("@HoTen", "%" + name + "%");
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UngVien_BUS ungVien = new UngVien_BUS();
                        ungVien.MaUngVien = (string)reader["MaUngVien"];
                        ungVien.HoTen = (string)reader["HoTen"];
                        ungVien.NgaySinh = (DateTime)reader["NgaySinh"];
                        ungVien.SoDienThoai = (string)reader["SoDienThoai"];
                        ungVien.Email = (string)reader["Email"];
                        list.Add(ungVien);
                    }
                }
            }

            return list;
        }

        public static int Them(UngVien_BUS pdk)
        {
            string query = @"
            INSERT INTO UngVien(HoTen, NgaySinh, SoDienThoai, Email)
            VALUES(@HoTen, @NgaySinh, @SoDienThoai, @Email);
            ";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);

            command.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = pdk.HoTen;
            command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = pdk.NgaySinh;
            command.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar).Value = pdk.SoDienThoai;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = pdk.Email;

            return command.ExecuteNonQuery();
        }

    }
}
