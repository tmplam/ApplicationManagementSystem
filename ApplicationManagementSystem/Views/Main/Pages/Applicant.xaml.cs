using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.DataAccess;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Windows.Input;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for Applicant.xaml
    /// </summary>
    public partial class Applicant : Page
    {
        readonly Frame _pageNavigation;

        public Applicant(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();

            UngVienListView.ItemsSource = UngVien_BUS.LayDanhSachUngVien();
        }

        // Event handler for 'Thêm ứng viên' button click
        private void themUngVienButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to 'ThemDoanhNghiep' page
            _pageNavigation.Navigate(new ThemUngVien(_pageNavigation));
        }


        private void TimKiemUngVienTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = TimKiemUngVienTextBox.Text.Trim();
                UngVienListView.ItemsSource = UngVien_BUS.LayDanhSachUngVien(searchTerm);
            }
        }

    }
}
