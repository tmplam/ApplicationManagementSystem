using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.Utils.StaticDetails;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for ThemDoanhNghiep.xaml
    /// </summary>
    public partial class ThemUngVien : Page
    {
        readonly Frame _pageNavigation;

        public ThemUngVien(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeComponent();
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void ThemUngVienButton_Click(object sender, RoutedEventArgs e)
        {
            UngVien_BUS ungVien = new ()
            {
                HoTen = HoTenUngVienTextBox.Text.Trim(),
                NgaySinh = DateTime.ParseExact(NgaySinhTextBox.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                SoDienThoai = SoDienThoaiTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
            };

            var messageError = UngVien_BUS.KiemTraDauVao(ungVien);
            if (string.IsNullOrEmpty(messageError))
            {
                // Không có lỗi, xử lí thêm
                var numOfRow = UngVien_BUS.ThemPhieuDangKy(ungVien);
                // Thêm thành công
                MessageBox.Show($"Thêm ứng viên thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                _pageNavigation.Navigate(new Applicant(_pageNavigation));
            }
            else
            {
                MessageBox.Show(messageError, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
