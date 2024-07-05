using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace ApplicationManagementSystem.BusinessLogic
{
    class BanLanhDao_BUS
    {
        public string MaBLD {  get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string SDT { get; set; }

        public static BanLanhDao_BUS LayThongTinLanhDao(string MaBLD)
        {
            return BanLanhDao_DAO.DocThongTinLanhDao(MaBLD);
        }

        public static List<string> LayDanhSachLanhDao()
        {
            return BanLanhDao_DAO.DocDanhSachLanhDao();
        }

        public static string LayMaBanLanhDao(string HoTen)
        {
            return BanLanhDao_DAO.DocMaBanLanhDao(HoTen);
        }
    }
}
