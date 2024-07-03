using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class PhieuThongTinDangTuyen_BUS
    {
        public Guid MaPhieu { get; set; }
        public string TenViTri { get; set; }
        public string MaSoThue { get; set; }
        public int SoLuong { get; set; }
        public string YeuCau { get; set; }
        public int KhoangThoiGian { get; set; }
        public string TinhTrang { get; set; }
        public string TinhTrangTT { get; set; }
        public string KieuTT { get; set; }
        public Guid MaPhieuDKTV { get; set; }

        public DateTime NgayBatDau { get; set; }    
        public string MaNhanVien { get; set; }

        public static PhieuThongTinDangTuyen_BUS DangKy(PhieuThongTinDangTuyen_BUS pdk)
        {
            return PhieuThongTinDangTuyen_DAO.Them(pdk);
        }

        public static string? KiemTraDauVao(PhieuThongTinDangTuyen_BUS pdk)
        {
            if (string.IsNullOrEmpty(pdk.TenViTri))
            {
                return "Chưa nhập tên vị trí.";
            }
            else if (pdk.SoLuong <= 0)
            {
                return "Số lượng tuyển dụng không hợp lệ.";
            }
            else if (string.IsNullOrEmpty(pdk.YeuCau))
            {
                return "Chưa nhập yêu cầu tuyển dụng.";
            }
            else if (pdk.KhoangThoiGian <= 0)
            {
                return "Chưa chọn khoảng thời gian tuyển dụng.";
            }

            return null;
        }

        public static int capNhapTinhTrangTT(Guid MaPhieu)
        {
            return PhieuThongTinDangTuyen_DAO.capNhapTinhTrangTT(MaPhieu);
        }

        public static int capNhapKieuTT(Guid MaPhieu, string KTT)
        {
            return PhieuThongTinDangTuyen_DAO.capNhapKieuTT(MaPhieu, KTT);
        }

        public static decimal tinhToanSoTienTT(Guid MaPhieu, string KieuTT)
        {
            return PhieuThongTinDangTuyen_DAO.tinhToanSoTienTT(MaPhieu, KieuTT);
        }

        public static PhieuThongTinDangTuyen_BUS docThongTin(Guid MaPhieu)
        {
            return PhieuThongTinDangTuyen_DAO.docThongTin(MaPhieu);
        }

        public static List<PhieuThongTinDangTuyen_BUS> layDSPhieuTTDT()
        {
            return PhieuThongTinDangTuyen_DAO.layDSPhieuTTDT(); ;
        }

        public static decimal tinhToanSoTienChuaTT(Guid MaPhieu, string KieuTT)
        {
            return PhieuThongTinDangTuyen_DAO.tinhToanSoTienChuaTT(MaPhieu, KieuTT);
        }
    }
}