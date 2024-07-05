using ApplicationManagementSystem.DataAccess;
using ApplicationManagementSystem.Views.Login;
using ApplicationManagementSystem.Views.Main.Pages;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationManagementSystem.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Page
        {
            Applicant,
            DoanhNghiep,
            AdmissionForm,
            Potential
        }

        private Page _currentPage = Page.Applicant;
        ObservableCollection<string>? NavItems = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavItems = new ObservableCollection<string>()
            {
                "Ứng Viên",
                "Doanh Nghiệp",
                "Phiếu Đăng Tuyển",
                "Tiềm Năng"
            };

            NavItemsListView.ItemsSource = NavItems;

            NavItemsListView.SelectedIndex = (int)_currentPage;
        }

        private void NavItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = NavItemsListView.SelectedIndex;
            changePage((Page)selectedIndex, e);
        }

        private void changePage(Page selectedIndex, SelectionChangedEventArgs e)
        {
            if (selectedIndex == Page.Applicant)
            {
                pageNavigation.NavigationService.Navigate(new Applicant(pageNavigation));
            }
            else if (selectedIndex == Page.DoanhNghiep)
            {
                pageNavigation.NavigationService.Navigate(new DoanhNghiep(pageNavigation));
            }
            else if (selectedIndex == Page.AdmissionForm)
            {
                pageNavigation.NavigationService.Navigate(new AdmissionForm(pageNavigation));
            }
            else if (selectedIndex == Page.Potential)
            {
                pageNavigation.NavigationService.Navigate(new TiemNang(pageNavigation));
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Cảnh báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                DbUtils.closeConnection();
                new LoginWindow().Show();
                Close();
            }

        }
    }
}
