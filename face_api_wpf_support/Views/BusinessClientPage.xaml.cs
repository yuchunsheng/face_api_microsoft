using face_api_wpf_support.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for BusinessClientPage.xaml
    /// </summary>
    public partial class BusinessClientPage : Page
    {
        private ListViewModel list_view_model = new ListViewModel();

        public BusinessClientPage()
        {
            InitializeComponent();
            DataContext = list_view_model;
        }

        private async Task<BusinessClientPage> InitializeAsync()
        {
            await list_view_model.load_business_client();
            return this;
        }

        public static Task<BusinessClientPage> CreatePageAsync()
        {
            var ret = new BusinessClientPage();
            return ret.InitializeAsync();

        }

        private void go_home(object sender, System.Windows.RoutedEventArgs e)
        {
            Page home_page = new ListViewPage();
            this.NavigationService.Navigate(home_page);

        }

        private void add_business_client(object sender, System.Windows.RoutedEventArgs e)
        {
            PageFunction<String> add_item_page = new PageFunctionAddBussinessClient();
            add_item_page.Return += new ReturnEventHandler<String>(add_business_client_function_callback);

            this.NavigationService.Navigate(add_item_page);
        }

        private void add_business_client_function_callback(object sender, ReturnEventArgs<String> e)
        {
            String result = (String)e.Result;
            
            this.list_view_model.refresh();
            ICollectionView view = CollectionViewSource.GetDefaultView(list_view_model.business_client_list);
            view.Refresh();
        }
    }
}
