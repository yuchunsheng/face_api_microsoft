using face_api_wpf_support.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for BusinessClientPage.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
    public partial class BusinessClientPage : Page
    {
        private BusinessClientViewModel business_client_viewmodel ;

        public BusinessClientPage()
        {
            InitializeComponent();
            business_client_viewmodel = new BusinessClientViewModel();
            DataContext = business_client_viewmodel;
        }

        
        private void go_home(object sender, System.Windows.RoutedEventArgs e)
        {
            ListViewPage home_page = new ListViewPage();
            home_page.Load_business_client();
            this.NavigationService.Navigate(home_page);

        }

        public void init()
        {
            business_client_viewmodel.load_business_client();
        }

        //private void details_clicked(object sender, RoutedEventArgs e)
        //{
        //    var myValue = ((Button)sender).Tag;
        //}

        
        private void page_function_return(object sender, ReturnEventArgs<string> e)
        {
            this.business_client_viewmodel.load_business_client();
        }

        private void add_page_checked(object sender, RoutedEventArgs e)
        {
            PageFunction<string> add_business_client = new BusinessClientAddPageFunction();
            add_business_client.Return += new ReturnEventHandler<string>(page_function_return);

            this.NavigationService.Navigate(add_business_client);
            
        }

        private void details_page_checked(object sender, RoutedEventArgs e)
        {
            PageFunction<string> details_page = business_client_viewmodel.next_page;
            details_page.Return += new ReturnEventHandler<string>(page_function_return);

            this.NavigationService.Navigate(details_page);
        }
    }
}
