using face_api_wpf_support.ViewModels.repository;
using System.Windows;
using System.Windows.Controls;

namespace face_api_wpf_support.Views.repository
{
    /// <summary>
    /// Interaction logic for ManageRepositoryPage.xaml
    /// </summary>
    public partial class ManageRepositoryPage : Page
    {
        private ManageRepositoryViewModel repository_view_model = new ManageRepositoryViewModel();

        public ManageRepositoryPage()
        {
            InitializeComponent();
            DataContext = repository_view_model;
        }

        public void load_item()
        {
            repository_view_model.init();
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(repository_view_model.next_page);
        }
    }
}
