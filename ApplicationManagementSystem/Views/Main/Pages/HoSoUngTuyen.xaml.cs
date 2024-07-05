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

        Guid currentMa;
        PhieuThongTinDangTuyen_BUS phieudangtuyen;
        public HoSoUngTuyen(Frame pageNavigation, PhieuThongTinDangTuyen_BUS _phieuDangTuyen)
        {
            phieudangtuyen = _phieuDangTuyen;

            _pageNavigation = pageNavigation;

            InitializeComponent();
            HoSoListView.DataContext = HoSoUngTuyen_BUS.LayDSPhieuUngTuyen(_phieuDangTuyen.MaPhieu);           
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
                HoSoListView.DataContext = HoSoUngTuyen_BUS.LocDSPhieuUngTuyen(phieudangtuyen.MaPhieu, selectedText);
            }
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.Navigate(new AdmissionForm(_pageNavigation));
        }

        private void HoSoUngTuyen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoUuTien.Visibility = Visibility.Visible;
            TinhTrang.Visibility = Visibility.Visible;
            ChiTietHoSo.Visibility = Visibility.Visible;
            ChiTietHoSoListView.Visibility = Visibility.Visible;

            var listViewItem = sender as ListViewItem;
            if (listViewItem != null && listViewItem.IsSelected)
            {
                var selectedItem = listViewItem.Content as HoSoUngTuyen_BUS;
                if (selectedItem != null)
                {
                    currentMa = selectedItem.MaPhieu;
                    ChiTietHoSoListView.DataContext = ChiTietHoSo_BUS.LayChiTietHoSo(currentMa);
                    //HoSoListView.DataContext = HoSoUngTuyen_BUS.LayDSPhieuUngTuyen(phieudangtuyen.MaPhieu);
                }
            }
        }

        private void TrangThai_Selected(object sender, SelectionChangedEventArgs e)
        {
            btnCapNhat.IsEnabled = true;

            // Lấy ComboBox hiện tại
            ComboBox comboBox = sender as ComboBox;

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();
                if (selectedText == "Đủ điều kiện")
                {
                    cbbDoUuTien.IsEnabled = true;
                    
                }
                else
                {
                    cbbDoUuTien.IsEnabled = false;
                }
            }
        }

        private void CapNhatButton_Click(object sender, RoutedEventArgs e)
        {

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = cbbTrangThai.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();
                int thongbao = HoSoUngTuyen_BUS.CapNhatTrangThai(currentMa, selectedText);
                if(thongbao > 0)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }    
            }
        }

        private void DoUuTien_Selected(object sender, SelectionChangedEventArgs e)
        {
            // Lấy ComboBox hiện tại
            ComboBox comboBox = sender as ComboBox;

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();
            }
        }
    }
}
