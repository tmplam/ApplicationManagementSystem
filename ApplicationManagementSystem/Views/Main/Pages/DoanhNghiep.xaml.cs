using ApplicationManagementSystem.BusinessLogic;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for Business.xaml
    /// </summary>
    public partial class DoanhNghiep : Page
    {
        readonly Frame _pageNavigation;

        public DoanhNghiep(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();
            DataContext = PhieuDangKyThanhVien_BUS.LayDanhSachPhieuDangKy();
        }

        private void themDoanhNghiepButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.Navigate(new ThemDoanhNghiep(_pageNavigation));
        }
    }
}
