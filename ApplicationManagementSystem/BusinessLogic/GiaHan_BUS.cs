using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace ApplicationManagementSystem.BusinessLogic
{
    class GiaHan_BUS
    {
        public int Thang {  get; set; }
        public int Nam { get; set; }
        public string MaBLD { get; set; }

        public static GiaHan_BUS LayThongTinGiaHan(int Thang, int Nam)
        {
            return GiaHan_DAO.DocThongTinGiaHan(Thang, Nam);
        }

        public static int ThemGiaHan(GiaHan_BUS gh)
        {
            return GiaHan_DAO.Them(gh);
        }

        public static int KiemTra(int Thang, int Nam)
        {
            return GiaHan_DAO.KiemTra(Thang, Nam);
        }
    }
}
