using ApplicationManagementSystem.BusinessLogic;
using System.Windows;
using System.Windows.Controls;

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
    }
}