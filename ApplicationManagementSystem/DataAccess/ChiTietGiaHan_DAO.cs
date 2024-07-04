using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ApplicationManagementSystem.DataAccess
{
    class ChiTietGiaHan_DAO
    {
        public static List<ChiTietGiaHan_BUS> LayDanhSach(int Thang, int Nam)
        {
            List<ChiTietGiaHan_BUS> list = new List<ChiTietGiaHan_BUS>();
            string sql = $"SELECT * FROM ChiTietGiaHan WHERE Thang = {Thang} AND Nam = {Nam}";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ChiTietGiaHan_BUS chiTiet = new();
                chiTiet.MaPhieuDKTV = (Guid)reader["MaPhieuDKTV"];
                chiTiet.Thang = Thang;
                chiTiet.Nam = Nam;
                chiTiet.PhanTramUuDai = (double)reader["PhanTramGiamGia"];
                list.Add(chiTiet);
            }
            reader.Close();

            return list;
        }

        public static int Them(ChiTietGiaHan_BUS ctgh)
        {
            string query = """
				INSERT INTO ChiTietGiaHan(MaPhieuDKTV,Thang, Nam, PhanTramGiamGia)
				VALUES(@MaPhieuDKTV, @Thang, @Nam, @PhanTramGiamGia);
				""";
            var command = new SqlCommand(query, DbUtils.getInstance().Connection);
            command.Parameters.Add("@MaPhieuDKTV", SqlDbType.UniqueIdentifier).Value = ctgh.MaPhieuDKTV;
            command.Parameters.Add("@Thang", SqlDbType.Int).Value = ctgh.Thang;
            command.Parameters.Add("@Nam", SqlDbType.Int).Value = ctgh.Nam;
            command.Parameters.Add("@PhanTramGiamGia", SqlDbType.Float).Value = ctgh.PhanTramUuDai;

            return command.ExecuteNonQuery();
        }

    }
}
