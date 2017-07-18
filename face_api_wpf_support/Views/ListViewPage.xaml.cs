using face_api_wpf_support.ViewModels;
using face_api_wpf_support.Views.repository;
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
    /// Interaction logic for ListViewPage.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class ListViewPage : Page
    {
        private ListViewModel list_view_model;

        public ListViewPage()
        {
            InitializeComponent();
            list_view_model = new ListViewModel();
            DataContext = list_view_model;
            
        }

        public async void Load_business_client()
        {
            await list_view_model.load_business_client();
            ICollectionView view = CollectionViewSource.GetDefaultView(list_view_model.business_client_list);
            view.Refresh();

        }

        private void add_button_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PageFunction<Object> add_item_page = new AddItemPageFunction();
            add_item_page.Return += new ReturnEventHandler<Object>(page_function_return);

            this.NavigationService.Navigate(add_item_page);

        }

        private void page_function_return(object sender, ReturnEventArgs<Object> e)
        {
            Item addedItem = (Item)e.Result;
            this.list_view_model.add_item_and_update(addedItem);

            ICollectionView view = CollectionViewSource.GetDefaultView(list_view_model.business_client_list);
            view.Refresh();
        }

        private void start_material_design_demo(object sender, System.Windows.RoutedEventArgs e)
        {
            Page materialDesignDemoPage = new MaterialDesignDemos();
            this.NavigationService.Navigate(materialDesignDemoPage);
        }

        private void start_task_parallel_library_demo(object sender, System.Windows.RoutedEventArgs e)
        {
            Page task_parallel_library_page = new TaskParallelLibraryView();
            this.NavigationService.Navigate(task_parallel_library_page);
        }

        private void async_load_employee_demo(object sender, System.Windows.RoutedEventArgs e)
        {
            Page async_employee_load_page = new WPFAsyncQueryView();
            this.NavigationService.Navigate(async_employee_load_page);
        }

        private async void async_load_page_demo(object sender, RoutedEventArgs e)
        {
            Page factory_async_load_page = await FactoryAsyncPage.CreatePageAsync();
            this.NavigationService.Navigate(factory_async_load_page);
        }

        private void load_business_client_page(object sender, RoutedEventArgs e)
        {
            BusinessClientPage business_client_page = new BusinessClientPage();
            business_client_page.init();
            this.NavigationService.Navigate(business_client_page);

        }

        private void manage_repository_page(object sender, RoutedEventArgs e)
        {
            ManageRepositoryPage mange_repository_page = new ManageRepositoryPage();
            mange_repository_page.load_item();
            this.NavigationService.Navigate(mange_repository_page);
        }
    }
}
