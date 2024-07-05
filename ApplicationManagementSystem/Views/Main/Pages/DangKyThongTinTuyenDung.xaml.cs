using ApplicationManagementSystem.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for DangKyThongTinTuyenDung.xaml
    /// </summary>
    public partial class DangKyThongTinTuyenDung : Page
    {
        readonly Frame _pageNavigation;
        public DangKyThongTinTuyenDung(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;    

            InitializeComponent();

            DataContext = PhieuDangKyThanhVien_BUS.LayDanhSachPhieuDangKy();
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null && listViewItem.IsSelected)
            {
                var selectedItem = listViewItem.Content as PhieuDangKyThanhVien_BUS;
                if (selectedItem != null)
                {
                    _pageNavigation.Navigate(new NhapThongTinTuyenDung(_pageNavigation, selectedItem));
                }
            }
        }

        private void TimKiemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
