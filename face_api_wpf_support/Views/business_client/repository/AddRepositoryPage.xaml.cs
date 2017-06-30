using face_api_wpf_support.ViewModels.business_client.repository;
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

namespace face_api_wpf_support.Views.business_client.repository
{
    /// <summary>
    /// Interaction logic for AddRepositoryPage.xaml
    /// </summary>
    public partial class AddRepositoryPage : Page
    {
        private AddRepositoryViewModel repository_view_model = new AddRepositoryViewModel();

        public AddRepositoryPage()
        {
            InitializeComponent();
            DataContext = repository_view_model;
        }

        public void load_item(object business_client_id)
        {
            repository_view_model.init(business_client_id);
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(repository_view_model.next_page);
        }
    }
}
