using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.Utils.StaticDetails;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for ThemDoanhNghiep.xaml
    /// </summary>
    public partial class ThemDoanhNghiep : Page
    {
        readonly Frame _pageNavigation;

        public ThemDoanhNghiep(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeComponent();
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void DangKyButton_Click(object sender, RoutedEventArgs e)
        {
            PhieuDangKyThanhVien_BUS phieuDangKy = new ()
            {
                TenCongTy = TenCongTyTextBox.Text.Trim(),
                MaSoThue = MaSoThueTextBox.Text.Trim(),
                NguoiDaiDien = NguoiDaiDienTextBox.Text.Trim(),
                DiaChi = DiaChiTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                NgayTao = DateTime.Now,
                TrangThai = TrangThaiPhieuDKTV.ChoXacThuc
            };

            var messageError = PhieuDangKyThanhVien_BUS.KiemTraDauVao(phieuDangKy);
            if (string.IsNullOrEmpty(messageError))
            {
                var existing = PhieuDangKyThanhVien_BUS.KiemTraTonTai(phieuDangKy.MaSoThue);
                if (existing)
                {
                    MessageBox.Show("Mã số thuế doanh nghiệp đã tồn tại.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    // Không có lỗi, xử lí thêm
                    var numOfRow = PhieuDangKyThanhVien_BUS.ThemPhieuDangKy(phieuDangKy);
                    // Thêm thành công
                    MessageBox.Show($"Thêm phiếu đăng ký thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    _pageNavigation.Navigate(new DoanhNghiep(_pageNavigation));
                }
            }
            else
            {
                MessageBox.Show(messageError, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
