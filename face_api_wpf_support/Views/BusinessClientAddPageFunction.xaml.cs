using face_api_wpf_support.ViewModels;
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

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for BusinessClientAddPageFunction.xaml
    /// </summary>
    public partial class BusinessClientAddPageFunction : PageFunction<String>
    {
        private BusinessClientAddViewModel business_client_add_viewmodel = new BusinessClientAddViewModel();

        public BusinessClientAddPageFunction()
        {
            InitializeComponent();
            DataContext = business_client_add_viewmodel;
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            BusinessClientPage business_client_page = new BusinessClientPage();
            business_client_page.init();
            this.NavigationService.Navigate(business_client_page);
        }
    }
}
