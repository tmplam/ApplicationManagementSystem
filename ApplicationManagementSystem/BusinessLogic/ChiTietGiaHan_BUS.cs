using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    class ChiTietGiaHan_BUS
    {
        public Guid MaPhieuDKTV { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public double PhanTramUuDai { get; set; }


        public static List<ChiTietGiaHan_BUS> LayDanhSach(int Thang, int Nam)
        {
            return ChiTietGiaHan_DAO.LayDanhSach(Thang, Nam);
        }

        public static int ThemCTGiaHan(ChiTietGiaHan_BUS ctgh)
        {
            return ChiTietGiaHan_DAO.Them(ctgh);
        }

    }
}
