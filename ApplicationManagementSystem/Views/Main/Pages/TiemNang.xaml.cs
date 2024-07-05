using ApplicationManagementSystem.BusinessLogic;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Media;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for TiemNang.xaml
    /// </summary>
    /// 
    public partial class TiemNang : Page
    {
        readonly Frame _pageNavigation;
        private GridView originalGridView;
        private Dictionary<Guid, int> PhanTram = new Dictionary<Guid, int>();

        public TiemNang(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;
            InitializeComponent();
            DateTime now = DateTime.Now;
            ThangTextBox.Text = now.Month.ToString();
            NamTextBox.Text = now.Year.ToString();
            Load_GridView(now.Month, now.Year);
            InitializeComboBox();
            //DataContext = ChiTietGiaHan_BUS.LayDanhSach();
        }

        private void InitializeComboBox()
        {
            List<string> options = BanLanhDao_BUS.LayDanhSachLanhDao();
            cbNguoiDuyet.ItemsSource = options;
        }

        private void Load_GridView(int Thang, int Nam)
        {
            DoanhNghiepTiemNangListView.Items.Clear();
            if (GiaHan_BUS.KiemTra(Thang, Nam) == 0)
            {
                cbNguoiDuyet.IsEnabled = false;
                if (DoanhNghiepTiemNangListView.View is GridView gridView)
                {
                    originalGridView = gridView;
                }
                DoanhNghiepTiemNangListView.View = null;
                var textBlock = new TextBlock
                {
                    Text = "Danh sách doanh nghiệp tiềm năng tháng này chưa được tạo",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    FontWeight = FontWeights.Bold
                };
                DoanhNghiepTiemNangListView.Items.Add(textBlock);
            }
            else
            {
                if (originalGridView != null)
                {
                    DoanhNghiepTiemNangListView.View = originalGridView;
                }
                List<ChiTietGiaHan_BUS> ct = ChiTietGiaHan_BUS.LayDanhSach(Thang, Nam);
                cbNguoiDuyet.IsEnabled = false;
                foreach (ChiTietGiaHan_BUS ChiTiet in ct)
                {
                    PhieuDangKyThanhVien_BUS phieuDangKy = PhieuDangKyThanhVien_BUS.LayThongTinPhieuDangKy(ChiTiet.MaPhieuDKTV);
                    var item = new
                    {
                        MaPhieuDKTV = ChiTiet.MaPhieuDKTV,
                        TenCongTy = phieuDangKy.TenCongTy,
                        Email = phieuDangKy.Email,
                        PhanTramUuDai = ChiTiet.PhanTramUuDai
                    };
                    DoanhNghiepTiemNangListView.Items.Add(item);
                }
                if (ct.Count > 0)
                {
                    cbNguoiDuyet.SelectedItem = BanLanhDao_BUS.LayThongTinLanhDao(GiaHan_BUS.LayThongTinGiaHan(ct[0].Thang, ct[0].Nam).MaBLD).HoTen.ToString();
                }
            }
            
        }

        private void Load_DSHetHanGridView(List<PhieuDangKyThanhVien_BUS> DSHetHan)
        {
            DoanhNghiepTiemNangListView.Items.Clear();
            foreach (PhieuDangKyThanhVien_BUS pdk in DSHetHan)
            {
                var item = new ListViewItem
                {
                    Content = new PhieuDangKyThanhVien_BUS
                    {
                        MaPhieu = pdk.MaPhieu,
                        TenCongTy = pdk.TenCongTy,
                        Email = pdk.Email,
                        MaSoThue = pdk.MaSoThue,
                        NguoiDaiDien = pdk.NguoiDaiDien,
                        DiaChi = pdk.DiaChi,
                        NgayTao = pdk.NgayTao,
                        TrangThai = pdk.TrangThai
                    },
                    Tag = false
                };
                DoanhNghiepTiemNangListView.Items.Add(item);
            }
            cbNguoiDuyet.SelectedItem = null;
            cbNguoiDuyet.IsEnabled = true;
        }

        private void TimKiemButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ThangTextBox.Text, out int Thang) && int.TryParse(NamTextBox.Text, out int Nam))
            {
                if (Thang < 0 || Thang > 12)
                {
                    MessageBox.Show("Tháng nhập vào không hợp lệ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);                  
                }
                else
                {
                    if (taoDanhSachButton.Visibility == Visibility.Hidden && XacNhanButton.Visibility == Visibility.Visible)
                    {
                        taoDanhSachButton.Visibility = Visibility.Visible;
                        XacNhanButton.Visibility = Visibility.Hidden;
                    }
                    Load_GridView(Thang, Nam);
                }               
            }
            else
            {
                MessageBox.Show("Kiểu dữ liệu không hợp lệ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void taoDanhSachButton_Click(object sender, RoutedEventArgs e)
        {
            int Thang = 0;
            int Nam = 0;
            if (int.TryParse(ThangTextBox.Text, out int Month) && int.TryParse(NamTextBox.Text, out int Year))
            {
                Thang = Month;
                Nam = Year;
            }
            if (GiaHan_BUS.KiemTra(Thang, Nam) == 0)
            {
                taoDanhSachButton.Visibility = Visibility.Hidden;
                XacNhanButton.Visibility = Visibility.Visible;
                GridView dsHetHan = new GridView();
                dsHetHan.Columns.Add(new GridViewColumn { Header = "Mã phiếu", DisplayMemberBinding = new System.Windows.Data.Binding("MaPhieu"), Width = 240 });
                dsHetHan.Columns.Add(new GridViewColumn { Header = "Tên công ty", DisplayMemberBinding = new System.Windows.Data.Binding("TenCongTy") });
                dsHetHan.Columns.Add(new GridViewColumn { Header = "Mã số thuế", DisplayMemberBinding = new System.Windows.Data.Binding("MaSoThue") });
                dsHetHan.Columns.Add(new GridViewColumn { Header = "Email", DisplayMemberBinding = new System.Windows.Data.Binding("Email") });
                //Thêm cột TextBox
                var gridViewColumnCheckBox = new GridViewColumn();
                var cellTemplate = new DataTemplate();
                var checkBoxFactory = new FrameworkElementFactory(typeof(CheckBox));
                checkBoxFactory.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                checkBoxFactory.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(CheckBox_Checked));
                checkBoxFactory.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(CheckBox_Unchecked));
                checkBoxFactory.SetValue(CheckBox.NameProperty, "chbTiemNang");
                cellTemplate.VisualTree = checkBoxFactory;
                gridViewColumnCheckBox.Header = "Tiềm năng";
                gridViewColumnCheckBox.CellTemplate = cellTemplate;
                dsHetHan.Columns.Add(gridViewColumnCheckBox);
                // Thêm cột ComboBox
                var gridViewColumnComboBox = new GridViewColumn();
                var comboBoxTemplate = new DataTemplate();
                var comboBoxFactory = new FrameworkElementFactory(typeof(ComboBox));
                comboBoxFactory.SetValue(ComboBox.ItemsSourceProperty, GetComboBoxItems());
                comboBoxFactory.AddHandler(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(ComboBox_SelectionChanged));
                comboBoxFactory.SetValue(ComboBox.IsEnabledProperty, false);
                comboBoxFactory.SetValue(ComboBox.NameProperty, "cbPhanTramUuDai");
                comboBoxTemplate.VisualTree = comboBoxFactory;
                gridViewColumnComboBox.Header = "Phần trăm ưu đãi";
                gridViewColumnComboBox.CellTemplate = comboBoxTemplate;
                dsHetHan.Columns.Add(gridViewColumnComboBox);

                DoanhNghiepTiemNangListView.View = dsHetHan;
                Load_DSHetHanGridView(PhieuDangKyThanhVien_BUS.LayDanhSachHetHan());
            }
            else
            {
                MessageBox.Show("Danh sách doanh nghiệp tiềm năng tháng này đã được tạo", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private static List<int> GetComboBoxItems()
        {
            return new List<int>
            {
                5,
                10, 
                15, 
                20
            };
        }

        private ListViewItem GetParentListViewItem(DependencyObject obj)
        {
            while (obj != null && !(obj is ListViewItem))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }
            return obj as ListViewItem;
        }

        private void EnableComboBoxInListViewItem(ListViewItem listViewItem, bool isEnabled)
        {
            var comboBox = FindVisualChild<ComboBox>(listViewItem);
            if (comboBox != null)
            {
                comboBox.IsEnabled = isEnabled;
            }
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            ListViewItem listViewItem = GetParentListViewItem(checkBox);
            if (listViewItem != null)
            {
                listViewItem.Tag = true; // Đánh dấu dòng này đã được chọn
                EnableComboBoxInListViewItem(listViewItem, true);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            ListViewItem listViewItem = GetParentListViewItem(checkBox);
            if (listViewItem != null)
            {
                listViewItem.Tag = false; // Đánh dấu dòng này chưa được chọn
                EnableComboBoxInListViewItem(listViewItem, false);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ListViewItem listViewItem = GetParentListViewItem(comboBox);
            if (listViewItem != null)
            {
                var data = listViewItem.Content as PhieuDangKyThanhVien_BUS;
                if (data != null && (int.TryParse(comboBox.SelectedItem.ToString(), out int PhanTramUD)))
                {
                    PhanTram[data.MaPhieu] = PhanTramUD;
                }
            }
        }

        private void XacNhanButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbNguoiDuyet.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn người duyệt danh sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DateTime now = DateTime.Now;
                GiaHan_BUS gh = new()
                {
                    Thang = now.Month,
                    Nam = now.Year,
                    MaBLD = BanLanhDao_BUS.LayMaBanLanhDao(cbNguoiDuyet.SelectedItem.ToString())
                };
                int themGH =  GiaHan_BUS.ThemGiaHan(gh);
                int ThemCT = 1;
                var checkedItems = DoanhNghiepTiemNangListView.Items.Cast<ListViewItem>()
                .Where(item => (bool)item.Tag)
                .Select(item => item.Content)
                .ToList();


                // Xử lý các dòng đã được chọn
                foreach (var item in checkedItems)
                {
                    dynamic data = item;
                    PhanTram.TryGetValue(data.MaPhieu, out int PhanTramUuDai);
                    ChiTietGiaHan_BUS ct = new()
                    {
                        MaPhieuDKTV = data.MaPhieu,
                        Thang = now.Month,
                        Nam = now.Year,
                        PhanTramUuDai = PhanTramUuDai
                    };
                    int flag = ChiTietGiaHan_BUS.ThemCTGiaHan(ct);
                    {
                        if (flag == 0)
                        {
                            ThemCT = 0;
                        }    
                    }
                    
                }
                if (themGH == 1 && ThemCT == 1)
                {
                    MessageBox.Show("Tạo danh sách thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    Load_GridView(now.Month, now.Year);
                    taoDanhSachButton.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
