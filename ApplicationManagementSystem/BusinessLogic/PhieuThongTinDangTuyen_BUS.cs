using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    internal class PhieuThongTinDangTuyen_BUS 
    {
        public Guid MaPhieu { get; set; }
        public string TenViTri { get; set; }
        public int SoLuong { get; set; }
        public string YeuCau { get; set; }
        public string KhoangThoiGian { get; set; }
        public string TinhTrang{ get; set; }
        public string TinhTrangThanhToan { get; set; }
        public string KieuThanhToan { get; set; }
        public Guid MaPhieuDKTV { get; set; }
        public string MaNhanVien { get; set; }

        public static int DangKy(PhieuThongTinDangTuyen_BUS pdk)
        {
            return PhieuThongTinDangTuyen_DAO.Them(pdk);
        }
    }
}
