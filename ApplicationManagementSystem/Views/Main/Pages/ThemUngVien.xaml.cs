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
            UngVien_BUS ungVien = new UngVien_BUS
            {
                HoTen = HoTenUngVienTextBox.Text.Trim(),
                NgaySinh = NgaySinhDatePicker.SelectedDate.GetValueOrDefault(),
                SoDienThoai = SoDienThoaiTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
            };

            var messageError = UngVien_BUS.KiemTraDauVao(ungVien);

            if (!string.IsNullOrEmpty(messageError))
            {
                MessageBox.Show(messageError, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int numOfRow = UngVien_BUS.ThemPhieuDangKy(ungVien);

                if (numOfRow > 0)
                {
                    MessageBox.Show("Thêm ứng viên thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    _pageNavigation.Navigate(new Applicant(_pageNavigation)); // Navigate to a new page or refresh current view
                }
                else
                {
                    MessageBox.Show("Thêm ứng viên không thành công", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
