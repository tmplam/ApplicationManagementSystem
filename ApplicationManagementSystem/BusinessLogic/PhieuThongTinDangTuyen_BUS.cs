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

        public static string? KiemTraDauVao(PhieuThongTinDangTuyen_BUS pdk)
        {
            if (string.IsNullOrEmpty(pdk.TenViTri))
            {
                return "Chưa nhập tên vị trí.";
            }
            else if (pdk.SoLuong <= 0)
            {
                return "Số lượng tuyển dụng không hợp lệ.";
            }
            else if (string.IsNullOrEmpty(pdk.YeuCau))
            {
                return "Chưa nhập yêu cầu tuyển dụng.";
            }
            else if (string.IsNullOrEmpty(pdk.KhoangThoiGian))
            {
                return "Chưa chọn khoảng thời gian tuyển dụng.";
            }

            return null;
        }
    }
}
