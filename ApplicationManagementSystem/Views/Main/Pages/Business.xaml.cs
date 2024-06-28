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
    /// Interaction logic for Business.xaml
    /// </summary>
    public partial class Business : Page
    {
        readonly Frame _pageNavigation;

        public Business(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeComponent();
        }
    }
}
