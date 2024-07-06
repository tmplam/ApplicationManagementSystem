using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class NhanVien_BUS
    {
        public string MaNhanVien {  get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }

        public static NhanVien_BUS KiemTraTonTai(string maNV)
        {
            return NhanVien_DAO.KiemTra(maNV);
        }
    }
}
