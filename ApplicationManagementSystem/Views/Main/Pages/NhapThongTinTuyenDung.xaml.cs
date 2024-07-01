using ApplicationManagementSystem.BusinessLogic;
using ApplicationManagementSystem.Utils.StaticDetails;
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
    /// Interaction logic for NhapThongTinTuyenDung.xaml
    /// </summary>
    public partial class NhapThongTinTuyenDung : Page
    {
        readonly Frame _pageNavigation;

        string khoangThoiGianDangTuyen = "";

        List<HinhThucDangTuyen_BUS> dsHinhThuc;

        PhieuDangKyThanhVien_BUS doanhNghiep;

        string maNhanVien = "4561023897412";
        public NhapThongTinTuyenDung(Frame pageNavigation, PhieuDangKyThanhVien_BUS _doanhNghiep)
        {
            doanhNghiep = _doanhNghiep;
            
            _pageNavigation = pageNavigation;

            dsHinhThuc = HinhThucDangTuyen_BUS.LayDSHinhThuc();

            InitializeComponent();

            for (int i = 1; i <= 8; i++)
            {
                KhoangThoiGianDangTuyenCBB.Items.Add(i * 5 + " ngày");
            }

            KhoangThoiGianDangTuyenCBB.SelectedIndex = 0;

            TitleTextBox.Text += doanhNghiep.TenCongTy;

            foreach (var hinhThuc in dsHinhThuc)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = hinhThuc.TenHinhThuc;
                checkBox.Tag = hinhThuc.MaHinhThuc;
                checkBox.FontSize = 16;
                Border border = new Border();
                border.Child = checkBox;
                border.Margin = new Thickness(36, 0, 0, 0);
                HinhThucDangTuyenCheckBoxes.Children.Add(border);
            }
        }

        private void KhoangThoiGianDangTuyenCBB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            khoangThoiGianDangTuyen = KhoangThoiGianDangTuyenCBB.SelectedItem.ToString();
        }

        private void TroVeButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.GoBack();
        }

        private void DangKyButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SoLuongTuyenDungTextBox.Text.Trim(), out _) == false)
            {
                MessageBox.Show("Số lượng tuyển dụng không hợp lệ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ThoiGianBatDauDangTuyenTimePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Chưa chọn thời gian bắt đầu đăng tuyển", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ThoiGianBatDauDangTuyenTimePicker.SelectedDate.Value <= DateTime.Now)
            {
                MessageBox.Show("Thời gian bắt đầu đăng tuyển không hợp lệ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime thoiGianBatDauDangTuyen = ThoiGianBatDauDangTuyenTimePicker.SelectedDate.Value;

            List<Guid> dsHinhThucDangTuyen = new List<Guid>();

            foreach (var child in HinhThucDangTuyenCheckBoxes.Children)
            {
                Border border = (Border)child;
                CheckBox checkBox = (CheckBox)border.Child;

                if ((bool)checkBox.IsChecked)
                {
                    dsHinhThucDangTuyen.Add((Guid)checkBox.Tag);
                }
            }

            if (dsHinhThucDangTuyen.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hình thức đăng tuyển", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PhieuThongTinDangTuyen_BUS phieuTTDangTuyen = new PhieuThongTinDangTuyen_BUS()
            {
                SoLuong = int.Parse(SoLuongTuyenDungTextBox.Text.Trim()),
                TenViTri = ViTriUngTuyenTextBox.Text.Trim(),
                YeuCau = YeuCauUngVienTextBox.Text.Trim(),
                KhoangThoiGian = khoangThoiGianDangTuyen,
                MaNhanVien = maNhanVien,
                MaPhieuDKTV = doanhNghiep.MaPhieu,
            };



            var messageError = PhieuThongTinDangTuyen_BUS.KiemTraDauVao(phieuTTDangTuyen);
            if (string.IsNullOrEmpty(messageError))
            {
                // Không có lỗi, xử lí thêm phiếu
                var phieuTTDT = PhieuThongTinDangTuyen_BUS.DangKy(phieuTTDangTuyen);

                List <PhieuQuangCao_BUS> dsPhieuQuangCao = new List<PhieuQuangCao_BUS>();
                foreach (var hinhThuc in dsHinhThucDangTuyen)
                {
                    PhieuQuangCao_BUS phieuQuangCao = new PhieuQuangCao_BUS()
                    {
                        NgayBatDau = thoiGianBatDauDangTuyen,
                        TongTien = dsHinhThuc.FirstOrDefault(item => item.MaHinhThuc == hinhThuc).GiaTien * int.Parse(khoangThoiGianDangTuyen.Split(" ")[0]),
                        MaPhieuTTDT = phieuTTDT.MaPhieu,
                        HinhThuc = hinhThuc
                    };
                    dsPhieuQuangCao.Add(phieuQuangCao);
                }

                PhieuQuangCao_BUS.ThemPhieu(dsPhieuQuangCao);

                _pageNavigation.Navigate(new PhieuQuangCao(_pageNavigation, doanhNghiep, phieuTTDT.MaPhieu));
            }
            else
            {
                MessageBox.Show(messageError, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
