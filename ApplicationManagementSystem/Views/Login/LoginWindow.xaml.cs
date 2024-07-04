using ApplicationManagementSystem.DataAccess;
using ApplicationManagementSystem.Views.Main;
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
using System.Windows.Shapes;

namespace ApplicationManagementSystem.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void DangNhapButton_Click(object sender, RoutedEventArgs e)
        {
            string username = TaiKhoanTextBox.Text;
            string password = MatKhauTextBox.Text;
            DbUtils._user = username;
            DbUtils._password = password;
            var connection = DbUtils.getInstance().Connection;
            if (DbUtils._isConnected)
            {
                var window = new MainWindow();
                window.Show();
            }
        }
    }
}
