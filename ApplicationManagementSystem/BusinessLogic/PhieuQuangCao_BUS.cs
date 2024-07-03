using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class PhieuQuangCao_BUS
    {
        public Guid MaPhieu { get; set; }
        public Decimal TongTien { get; set; }
        public Guid HinhThuc { get; set; }
        public Guid MaPhieuTTDT { get; set; }
        public static List<PhieuQuangCao_BUS> LayDS(Guid maPhieuTTDT)
        {
            return PhieuQuangCao_DAO.LayDS(maPhieuTTDT);
        }

        public static int ThemPhieu(List<PhieuQuangCao_BUS> dsPhieu)
        {
            return PhieuQuangCao_DAO.ThemPhieu(dsPhieu);
        }
    }

}
