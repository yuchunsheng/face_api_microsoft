using face_api_wpf_support.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for ListViewPage.xaml
    /// </summary>
    public partial class ListViewPage : Page
    {
        private ListViewModel list_view_model = new ListViewModel();

        public ListViewPage()
        {
            InitializeComponent();
            DataContext = list_view_model;
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
            this.list_view_model.add_item(addedItem);

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
    }
}
