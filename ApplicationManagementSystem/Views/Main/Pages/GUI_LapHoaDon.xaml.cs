using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.DataAccess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection.Metadata;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for GUI_LapHoaDon.xaml
    /// </summary>
    public partial class GUI_LapHoaDon : Page
    {
        readonly Frame _pageNavigation;
        private PhieuThongTinDangTuyen_BUS phieuTT;
        decimal soTienDangTT;
        string hinhThucTT;
        string NoiDungDinhDangEmail;
        string Kieu2T;
        public GUI_LapHoaDon(Frame pageNavigation, PhieuThongTinDangTuyen_BUS phieu, decimal soTienTT, string HTTT, string KTT)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();
            // Lưu biến
            phieuTT = phieu;
            soTienDangTT = soTienTT;
            hinhThucTT = HTTT;
            Kieu2T= KTT;
            PhieuDangKyThanhVien_BUS pdktv = PhieuDangKyThanhVien_BUS.docThongTin(phieu.MaPhieuDKTV);
            List<HinhThucDangTuyen_BUS> dsHTDT = HinhThucDangTuyen_BUS.layDSHinhThuc(phieu.MaPhieu);
            // Load vô textbox
            txtEmail.Text = pdktv.Email;
            txtTieuDe.Text = "[HOÁ ĐƠN THANH TOÁN PHIẾU THÔNG TIN ĐĂNG TUYỂN]";
            // Load thông tin nội dung email
            string TenCongTy = pdktv.TenCongTy;
            string ViTri = phieu.TenViTri;
            string HinhThucDangTuyen = string.Join(", ", dsHTDT.Select(ht => ht.TenHinhThuc).ToArray());
            string KieuThanhToan = KTT;
            string NgayTT = DateTime.Now.ToString("dd/MM/yyyy");
            string SoTienDaTra = soTienTT.ToString();
            string HinhThucThanhToan = HTTT;
            string TienTTTL = PhieuThongTinDangTuyen_BUS.tinhToanSoTienChuaTT(phieu.MaPhieu, KieuThanhToan).ToString();

            string emailContent = $"Kính gửi {TenCongTy},\n\n" +
                          $"Chúng tôi xin gửi đến bạn thông tin hóa đơn thanh toán của phiếu thông tin đăng tuyển vị trí {ViTri} sử dụng hình thức đăng tuyển {HinhThucDangTuyen}:\n\n" +
                          $"Kiểu thanh toán: {KieuThanhToan}\n" +
                          $"ngày thanh toán: {NgayTT}\n" +
                          $"số tiền thanh toán: {SoTienDaTra}\n" +
                          $"hình thức thanh toán: {HinhThucThanhToan}\n" +
                          $"Số tiền còn lại cần thanh toán trong tương lai: {TienTTTL}\n\n" +
                          $"Cảm ơn bạn đã sử dụng dịch vụ của công ty.\n\n" +
                          $"Trân trọng,\n" +
                          $"Công ty ABC";

            NoiDungDinhDangEmail = $"<html>" +
                              $"<body style='font-family: Arial, sans-serif; color: black;'>" +
                              $"<p>Kính gửi {TenCongTy},</p>" +
                              $"<p>Chúng tôi xin gửi đến bạn thông tin hóa đơn thanh toán của phiếu thông tin đăng tuyển vị trí {ViTri} sử dụng hình thức đăng tuyển {HinhThucDangTuyen}:</p>" +
                              $"<ul>" +
                              $"<li>Kiểu thanh toán: {KieuThanhToan}</li>" +
                              $"<li>Ngày thanh toán: {NgayTT}</li>" +
                              $"<li>Số tiền thanh toán: {SoTienDaTra}</li>" +
                              $"<li>Hình thức thanh toán: {HinhThucThanhToan}</li>" +
                              $"<li>Số tiền còn lại cần thanh toán trong tương lai: {TienTTTL}</li>" +
                              $"</ul>" +
                              $"<p>Cảm ơn bạn đã sử dụng dịch vụ của công ty.</p>" +
                              $"<p>Trân trọng,<br/>Công ty ABC</p>" +
                              $"</body>" +
                              $"</html>";
            // Gọi hàm để đặt nội dung vào RichTextBox
            SetRichTextBoxContent(emailContent);
        }
        private void SetRichTextBoxContent(string content)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(content));
            document.Blocks.Add(paragraph);
            txtNoiDung.Document = document;
        }
        private void btnTroVe_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            //Cập nhập kiểu thanh toán
            var numOfRow1 = PhieuThongTinDangTuyen_BUS.capNhapKieuTT(phieuTT.MaPhieu, Kieu2T);
            //if (numOfRow1 > 0)
            //{
            //    MessageBox.Show("Cập nhập kiểu thanh toán thành công");
            //}
            //Thêm hoá đơn
            HoaDon_BUS hoaDon = new()
            {
                NgayThanhToan = DateTime.Now,
                SoTienThanhToan = soTienDangTT,
                HinhThucThanhToan = hinhThucTT,
                MaPhieuTTDT = phieuTT.MaPhieu,
                MaNhanVien = DbUtils._user
            };
            var numOfRow = HoaDon_BUS.themHoaDon(hoaDon);
            MessageBox.Show("Thêm hoá đơn thành công");
            //gửi hoá đơn
            TextRange textRange = new TextRange(txtNoiDung.Document.ContentStart, txtNoiDung.Document.ContentEnd);
            string noiDungEmail = textRange.Text;
            HoaDon_BUS.guiHoaDon(txtTieuDe.Text, NoiDungDinhDangEmail, txtEmail.Text);
            //Cập nhập tình trạng thanh toán
            var numOfRow2 = PhieuThongTinDangTuyen_BUS.capNhapTinhTrangTT(phieuTT.MaPhieu);
            if (numOfRow2 > 0)
            {
                MessageBox.Show("Cập nhập tình trạng thanh toán thành công");
            }
            //Sau khi gửi hoá đơn
            _pageNavigation.Navigate(new AdmissionForm(_pageNavigation));
        }
    }
}
