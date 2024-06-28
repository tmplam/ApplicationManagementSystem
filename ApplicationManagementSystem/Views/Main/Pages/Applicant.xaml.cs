using System.Windows.Controls;


namespace ApplicationManagementSystem.Views.Main.Pages
{
    /// <summary>
    /// Interaction logic for Applicant.xaml
    /// </summary>
    public partial class Applicant : Page
    {
        readonly Frame _pageNavigation;

        public Applicant(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeComponent();
        }
    }
}
