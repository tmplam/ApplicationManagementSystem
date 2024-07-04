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
    /// Interaction logic for HoSoUngTuyen.xaml
    /// </summary>
    public partial class HoSoUngTuyen : Page
    {
        readonly Frame _pageNavigation;

        List<HinhThucDangTuyen_BUS> dsHinhThuc;

        PhieuThongTinDangTuyen_BUS phieudangtuyen;

        string maNhanVien = "4561023897412";
        public HoSoUngTuyen(Frame pageNavigation, PhieuThongTinDangTuyen_BUS _phieuDangTuyen)
        {
            phieudangtuyen = _phieuDangTuyen;

            _pageNavigation = pageNavigation;

            dsHinhThuc = HinhThucDangTuyen_BUS.LayDSHinhThuc();

            InitializeComponent();
        }

        private void TrangThaiHS_Selected(object sender, SelectionChangedEventArgs e)
        {
            // Lấy ComboBox hiện tại
            ComboBox comboBox = sender as ComboBox;

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();
                //DataContext = HoSoUngTuyen_BUS.LayDSPhieuDangTuyen(selectedText);
            }
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }
    }
}
