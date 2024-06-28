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
    /// Interaction logic for AdmissionForm.xaml
    /// </summary>
    public partial class AdmissionForm : Page
    {
        readonly Frame _pageNavigation;

        public AdmissionForm(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeComponent();
        }
    }
}
