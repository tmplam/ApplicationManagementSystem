using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class ChiTietHoSo_BUS
    {
        public Guid MaHoSo { get; set; }
        public int STT { get; set; }
        public string TenChiTiet { get; set; }
        public string MoTa { get; set; }
        public static List<ChiTietHoSo_BUS> LayChiTietHoSo(Guid ma)
        {
            return ChiTietHoSo_DAO.XemCTHoSo(ma);
        }
    }
}
