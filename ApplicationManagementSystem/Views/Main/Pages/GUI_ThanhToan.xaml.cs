using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.DataAccess;
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
    /// Interaction logic for GUI_ThanhToan.xaml
    /// </summary>
    public partial class GUI_ThanhToan : Page
    {
        readonly Frame _pageNavigation;
        private PhieuThongTinDangTuyen_BUS phieuTT;
        public GUI_ThanhToan(Frame pageNavigation, Guid MaPhieuTTDT)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();
            DataContext = HoaDon_BUS.layDSHoaDon(MaPhieuTTDT);
            phieuTT = PhieuThongTinDangTuyen_BUS.docThongTin(MaPhieuTTDT);
            txtTTTT.Text = phieuTT.TinhTrangTT;
            if (phieuTT.KieuTT != "Chưa chọn")
            {
                cbbKTT.Text = phieuTT.KieuTT;
                cbbKTT.IsEnabled = false;
                txtSTCTT.Text = PhieuThongTinDangTuyen_BUS.tinhToanSoTienTT(MaPhieuTTDT, cbbKTT.Text).ToString("N2");
            }
            //else
            //{
            //    if (phieuTT.KhoangThoiGian < 30)
            //    {
            //        cbbKTT.Text = "Toàn bộ";
            //        cbbKTT.IsEnabled = false;
            //        txtSTCTT.Text = PhieuThongTinDangTuyen_BUS.tinhToanSoTienTT(MaPhieuTTDT, cbbKTT.Text).ToString("N2");
            //    }
            //    else
            //    {
            //        cbbKTT.IsEnabled = true;
            //        txtSTCTT.Text = PhieuThongTinDangTuyen_BUS.tinhToanSoTienTT(MaPhieuTTDT, "Chưa chọn").ToString("N2");
            //    }
            //}
        }

        private void cbbKTT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy ComboBox hiện tại
            ComboBox comboBox = sender as ComboBox;

            // Lấy ComboBoxItem được chọn
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Lấy nội dung của ComboBoxItem được chọn
                string selectedText = selectedItem.Content.ToString();

                txtSTCTT.Text = PhieuThongTinDangTuyen_BUS.tinhToanSoTienTT(phieuTT.MaPhieu, selectedText).ToString("N2");
            }
        }


        private void btnTroVe_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void btnLapHD_Click(object sender, RoutedEventArgs e)
        {
            if (phieuTT.TinhTrangTT == "Hoàn thành")
            {
                MessageBox.Show("Phiếu thông tin đăng tuyển này đã hoàn tất thanh toán!");
                return;
            }

            if (cbbHTTT.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán!");
                return; 
            }

            _pageNavigation.Navigate(new GUI_LapHoaDon(_pageNavigation, phieuTT, PhieuThongTinDangTuyen_BUS.tinhToanSoTienTT(phieuTT.MaPhieu, cbbKTT.Text), cbbHTTT.Text, cbbKTT.Text));
        } 
    }
}
