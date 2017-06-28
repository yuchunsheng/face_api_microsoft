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

namespace face_api_wpf_support.Views.business_client
{
    /// <summary>
    /// Interaction logic for BusinessClientDetailsPageFunction.xaml
    /// </summary>
    public partial class BusinessClientDetailsPageFunction : PageFunction<String>
    {
        public BusinessClientDetailsViewModel details_model = new BusinessClientDetailsViewModel();
        public BusinessClientDetailsPageFunction()
        {
            InitializeComponent();

            DataContext = details_model;
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(details_model.next_page);
        }
    }
}
