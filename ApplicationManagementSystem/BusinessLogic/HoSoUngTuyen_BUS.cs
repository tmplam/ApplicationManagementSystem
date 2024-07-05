using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class HoSoUngTuyen_BUS
    {
        public Guid MaPhieu { get; set; }
        public string MaUV { get; set; }
        public Guid MaPhieuTTDT { get; set; }
        public DateTime NgayLap { get; set; }
        public string TinhTrangHS { get; set; }
        public int DoUuTien { get; set; }
        public string MaNhanVien { get; set; }

        public static List<HoSoUngTuyen_BUS> LayDSPhieuUngTuyen(Guid mahs)
        {
            return HoSoUngTuyen_DAO.XemDSPhieuUngTuyen(mahs); ;
        }

        public static List<HoSoUngTuyen_BUS> LocDSPhieuUngTuyen(Guid ma, string trangthai)
        {
            return HoSoUngTuyen_DAO.LocPhieuUngTuyen(ma, trangthai); ;
        }

        public static List<HoSoUngTuyen_BUS> TimKiemTheoTen(string keyword)
        {
            return HoSoUngTuyen_DAO.Tim(keyword);
        }

        public static int CapNhatTrangThai(Guid ma, string trangthai)
        {
            return HoSoUngTuyen_DAO.CapNhatTTPhieuUT(ma, trangthai);
        }

        public static int CapNhatDoUuTien(Guid ma, int uutien)
        {
            return HoSoUngTuyen_DAO.CapNhatUuTienPhieuUT(ma, uutien);
        }
    }
}
