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
        public string HoTen { get; set; }
        public DateOnly NgayLap { get; set; }
        public string TinhTrangHS { get; set; }
        public string DoUuTien { get; set; }
        public DateOnly NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string MoTa { get; set; }
    }
}
