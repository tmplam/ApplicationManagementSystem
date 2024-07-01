using ApplicationManagementSystem.BusinessLogic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationManagementSystem.DataAccess
{
    internal class PhieuThongTinDangTuyen_DAO
    {
        public static int capNhapTinhTrangTT(Guid MaPhieu)
        {
            int rowsUpdated = 0;

            var command = new SqlCommand("CapNhapTinhTrangTT", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));

            rowsUpdated = command.ExecuteNonQuery();

            return rowsUpdated;
        }

        public static decimal tinhToanSoTienTT(Guid MaPhieu, string KieuTT)
        {
            decimal soTienCanThanhToanTiepTheo = 0;
            var command = new SqlCommand("TinhSoTienCanThanhToanTiepTheo", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));
            command.Parameters.Add(new SqlParameter("@KieuThanhToan", KieuTT));
            command.Parameters.Add(new SqlParameter("@SoTienCanThanhToanTiepTheo", System.Data.SqlDbType.Decimal));
            command.Parameters["@SoTienCanThanhToanTiepTheo"].Direction = System.Data.ParameterDirection.Output;

            command.ExecuteNonQuery();

            // Lấy giá trị đầu ra từ tham số @SoTienCanThanhToanTiepTheo
            soTienCanThanhToanTiepTheo = Convert.ToDecimal(command.Parameters["@SoTienCanThanhToanTiepTheo"].Value);
            return soTienCanThanhToanTiepTheo;
        }


        public static PhieuThongTinDangTuyen_BUS docThongTin(Guid MaPhieuTTDT)
        {
            PhieuThongTinDangTuyen_BUS phieuThongTin = null;
            try
            {
                string sql = "SELECT * FROM PhieuTTDangTuyen WHERE MaPhieu = @MaPhieuTTDT";
                var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
                command.Parameters.AddWithValue("@MaPhieuTTDT", MaPhieuTTDT);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    phieuThongTin = new PhieuThongTinDangTuyen_BUS
                    {
                        MaPhieu = (Guid)reader["MaPhieu"],
                        TenViTri = (string)reader["TenViTri"],
                        SoLuong = (int)reader["SoLuong"],
                        YeuCau = (string)reader["YeuCau"],
                        KhoangThoiGian = (string)reader["KhoangThoiGian"],
                        TinhTrang = (string)reader["TinhTrang"],
                        TinhTrangTT = (string)reader["TinhTrangTT"],
                        KieuTT = (string)reader["KieuTT"],
                        MaPhieuDKTV = (Guid)reader["MaPhieuDKTV"],
                        MaNhanVien = (string)reader["MaNhanVien"]
                    };
                }
                reader.Close(); // Đóng reader sau khi sử dụng  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                // Xử lý lỗi nếu cần thiết
            }
            return phieuThongTin;
        }

        public static decimal tinhToanSoTienChuaTT(Guid MaPhieu, string KieuTT)
        {
            decimal soTienChuaThanhToan = 0;
            var command = new SqlCommand("TinhSoTienChuaThanhToan", DbUtils.getInstance().Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Thêm các tham số và thiết lập giá trị cho stored procedure
            command.Parameters.Add(new SqlParameter("@MaPhieuTTDT", MaPhieu));
            command.Parameters.Add(new SqlParameter("@KieuThanhToan", KieuTT));
            command.Parameters.Add(new SqlParameter("@SoTienChuaThanhToan", System.Data.SqlDbType.Decimal));
            command.Parameters["@SoTienChuaThanhToan"].Direction = System.Data.ParameterDirection.Output;

            command.ExecuteNonQuery();

            // Lấy giá trị đầu ra từ tham số @SoTienCanThanhToanTiepTheo
            soTienChuaThanhToan = Convert.ToDecimal(command.Parameters["@SoTienChuaThanhToan"].Value);
            return soTienChuaThanhToan;
        }

        public static List<PhieuThongTinDangTuyen_BUS> layDSPhieuTTDT()
        {
            List<PhieuThongTinDangTuyen_BUS> list = new List<PhieuThongTinDangTuyen_BUS>();

            string sql = "SELECT * FROM PhieuTTDangTuyen";
            var command = new SqlCommand(sql, DbUtils.getInstance().Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhieuThongTinDangTuyen_BUS phieuThongTin = new PhieuThongTinDangTuyen_BUS
                {
                    MaPhieu = (Guid)reader["MaPhieu"],
                    TenViTri = (string)reader["TenViTri"],
                    SoLuong = (int)reader["SoLuong"],
                    YeuCau = (string)reader["YeuCau"],
                    KhoangThoiGian = (string)reader["KhoangThoiGian"],
                    TinhTrang = (string)reader["TinhTrang"],
                    TinhTrangTT = (string)reader["TinhTrangTT"],
                    KieuTT = (string)reader["KieuTT"],
                    MaPhieuDKTV = (Guid)reader["MaPhieuDKTV"],
                    MaNhanVien = (string)reader["MaNhanVien"]
                };
                list.Add(phieuThongTin);
            }
            reader.Close();
            return list;
        }


    }
}
