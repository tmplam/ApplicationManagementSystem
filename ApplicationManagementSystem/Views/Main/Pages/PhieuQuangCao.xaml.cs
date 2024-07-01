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
    /// Interaction logic for PhieuQuangCao.xaml
    /// </summary>
    public partial class PhieuQuangCao : Page
    {
        readonly Frame _pageNavigation;
        PhieuThongTinDangTuyen_BUS phieuTTDangTuyen;
        PhieuDangKyThanhVien_BUS doanhNghiep;
        List<PhieuQuangCao_BUS> dsPhieuQuangCao;
        int currentPage = 1;

        public PhieuQuangCao(Frame pageNavigation, PhieuDangKyThanhVien_BUS _doanhNghiep, Guid maPhieuTTDT)
        {
            _pageNavigation = pageNavigation;
            doanhNghiep = _doanhNghiep;
            phieuTTDangTuyen = PhieuThongTinDangTuyen_BUS.XemPhieu(maPhieuTTDT);
            dsPhieuQuangCao = PhieuQuangCao_BUS.LayDS(maPhieuTTDT);

            InitializeComponent();

            for (int i = 1; i <= dsPhieuQuangCao.Count; i++)
            {
                Button button = new Button();
                button.Content = i;
                button.Height = 30;
                button.Width = 40;
                button.Margin = new Thickness(4);
                button.Click += Button_Click;
                Pagination.Children.Add(button);
                if (i == 1)
                {
                    button.Background = Brushes.RoyalBlue;
                }
            }

            updateLabels();
        }

        void updateLabels()
        {
            DoanhNghiep.Content = doanhNghiep.TenCongTy;
            ViTriUngTuyen.Content = phieuTTDangTuyen.TenViTri;
            SoLuongTuyenDung.Content = phieuTTDangTuyen.SoLuong;
            YeuCauUngVien.Content = phieuTTDangTuyen.YeuCau;
            KhoangThoiGian.Content = phieuTTDangTuyen.KhoangThoiGian;
            ThoiGianBatDau.Content = dsPhieuQuangCao[currentPage - 1].NgayBatDau.ToString("dd/MM/yyyy");
            TongTien.Content = dsPhieuQuangCao[currentPage - 1].TongTien;

            HinhThucDangTuyen_BUS hinhthuc = HinhThucDangTuyen_BUS.XemHinhThuc(dsPhieuQuangCao[currentPage - 1].HinhThuc);
            HinhThuc.Content = hinhthuc.TenHinhThuc;
        }

        private void XacNhanButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.Navigate(new AdmissionForm(_pageNavigation));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if ((int)clickedButton.Content != currentPage)
            {
                clickedButton.Background = Brushes.RoyalBlue;
                Button previousClickedButton = Pagination.Children[currentPage - 1] as Button;
                previousClickedButton.Background = Brushes.White;
                currentPage = (int)clickedButton.Content;

                updateLabels();
            }


        }

    }
}
