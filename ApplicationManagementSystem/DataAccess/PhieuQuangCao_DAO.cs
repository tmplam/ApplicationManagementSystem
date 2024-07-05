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
    public class PhieuQuangCao_DAO
    {
        public static List<PhieuQuangCao_BUS> LayDS(Guid maPhieuTTDT)
        {
            List<PhieuQuangCao_BUS> dsPhieuQuangCao = new List<PhieuQuangCao_BUS>();
            string query = """
				SELECT * FROM PhieuDangKyQuangCao WHERE MaPhieuTTDT=@MaPhieuTTDT; 
				""";
            var connection = DbUtils.getInstance().Connection;

            var command = new SqlCommand(query, connection);
            command.Parameters.Add("@MaPhieuTTDT", SqlDbType.UniqueIdentifier).Value = maPhieuTTDT;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PhieuQuangCao_BUS phieuQuangCao = new PhieuQuangCao_BUS();
                    phieuQuangCao.MaPhieu = reader.GetGuid(reader.GetOrdinal("maPhieu"));
                    phieuQuangCao.TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien"));
                    phieuQuangCao.MaPhieuTTDT = reader.GetGuid(reader.GetOrdinal("MaPhieuTTDT"));
                    phieuQuangCao.HinhThuc = reader.GetGuid(reader.GetOrdinal("MaHT"));

                    dsPhieuQuangCao.Add(phieuQuangCao);
                }
            }
            reader.Close();

            return dsPhieuQuangCao;
        }

        public static int ThemPhieu(List<PhieuQuangCao_BUS> dsPhieu)
        {
            var connection = DbUtils.getInstance().Connection;
            foreach (var phieu in dsPhieu)
            {
                string query = """
				INSERT INTO PhieuDangKyQuangCao(TongTien, MaPhieuTTDT, MaHT)
				VALUES(@TongTien, @MaPhieuTTDT, @MaHT);
				""";
                var command = new SqlCommand(query,connection);

                command.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = phieu.TongTien;
                command.Parameters.Add("@MaPhieuTTDT", SqlDbType.UniqueIdentifier).Value = phieu.MaPhieuTTDT;
                command.Parameters.Add("@MaHT", SqlDbType.UniqueIdentifier).Value = phieu.HinhThuc;

                command.ExecuteNonQuery();     
            }

            return 1;
        }
    }
}
