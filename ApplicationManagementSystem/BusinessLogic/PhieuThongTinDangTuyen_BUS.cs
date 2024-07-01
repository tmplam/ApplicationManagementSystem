using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationManagementSystem.DataAccess;
using System.Text.RegularExpressions;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class PhieuThongTinDangTuyen_BUS
    {
        public Guid MaPhieu { get; set; }
        public string TenViTri { get; set; }
        public string MaSoThue { get; set; }
        public int SoLuong { get; set; }
        public string YeuCau { get; set; }
        public string KhoangThoiGian { get; set; }
        public string TinhTrang { get; set; }
        public string TinhTrangTT { get; set; }
        public string KieuTT { get; set; }
        public DateTime NgayTao { get; set; }
        public Guid MaPhieuDKTV { get; set; }
        public string MaNhanVien { get; set; }

        public static int capNhapTinhTrangTT(Guid MaPhieu)
        {
            return PhieuThongTinDangTuyen_DAO.capNhapTinhTrangTT(MaPhieu); 
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
