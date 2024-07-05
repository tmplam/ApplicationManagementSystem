using ApplicationManagementSystem.BusinessLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for AdmissionForm.xaml
    /// </summary>
    public partial class AdmissionForm : Page
    {
        readonly Frame _pageNavigation;

        public AdmissionForm(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();
            DataContext = PhieuThongTinDangTuyen_BUS.layDSPhieuTTDT();
        }

        private void ThanhToan_Click(object sender, RoutedEventArgs e)
        {
            if (PhieuTTDTListView.SelectedItem != null)
            {
                PhieuThongTinDangTuyen_BUS selectedPTTDT = (PhieuThongTinDangTuyen_BUS)PhieuTTDTListView.SelectedItem;
                Guid MaPhieuTTDT = selectedPTTDT.MaPhieu;
                _pageNavigation.Navigate(new GUI_ThanhToan(_pageNavigation, MaPhieuTTDT));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu để thanh toán.");
            }
        }
        private void dangKyButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.Navigate(new DangKyThongTinTuyenDung(_pageNavigation));
        }

        private void TrangThai_Selected(object sender, SelectionChangedEventArgs e)
        {
            // Lấy ComboBox hiện tại
            ComboBox comboBox = sender as ComboBox;

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();
                DataContext = PhieuThongTinDangTuyen_BUS.LayDSPhieuDangTuyen(selectedText);
            }
        }

        private void Input_ViTri(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string inputText = textBox.Text.ToString();
            DataContext = PhieuThongTinDangTuyen_BUS.TimPhieu(inputText);
        }

        private void PhieuDangTuyen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null && listViewItem.IsSelected)
            {
                var selectedItem = listViewItem.Content as PhieuThongTinDangTuyen_BUS;
                if (selectedItem != null)
                {
                    _pageNavigation.Navigate(new HoSoUngTuyen(_pageNavigation, selectedItem));
                }
            }
        }
    }
}