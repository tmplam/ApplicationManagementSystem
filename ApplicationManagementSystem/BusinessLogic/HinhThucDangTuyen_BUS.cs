using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationManagementSystem.DataAccess;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class HinhThucDangTuyen_BUS
    {

        public Guid MaHinhThuc { get; set; }
        public string TenHinhThuc { get; set; }
        public decimal GiaTien { get; set; }

        public static List<HinhThucDangTuyen_BUS> layDSHinhThuc(Guid MaPhieuTTDT)
        {
            return HinhThucDangTuyen_DAO.layDSHinhThuc(MaPhieuTTDT);
        }

        public static List<HinhThucDangTuyen_BUS> LayDSHinhThuc()
        {
            return HinhThucDangTuyen_DAO.LayDS();
        }

        public static HinhThucDangTuyen_BUS XemHinhThuc(Guid maHinhThuc)
        {
            return HinhThucDangTuyen_DAO.Xem(maHinhThuc);
        }
    }

}
