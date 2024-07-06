using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.DataAccess
{
    public class NhanVien_DAO
    {
        public static NhanVien_BUS KiemTra(string maNV)
        {
            NhanVien_BUS nv = new NhanVien_BUS();
            string sql = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            command.Parameters.AddWithValue("@MaNhanVien", maNV);
            var reader = command.ExecuteReader();         
            if (reader.Read())
            {
                nv.MaNhanVien = (string)reader["MaNhanVien"];
                nv.HoTen = (string)reader["HoTen"];
                nv.SoDienThoai = (string)reader["SoDienThoai"];
                reader.Close();
                return nv;
            }
            reader.Close();
            return null;
        }
    }
}
