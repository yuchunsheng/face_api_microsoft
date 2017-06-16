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
    /// Interaction logic for PageFunctionAddBussinessClient.xaml
    /// </summary>
    public partial class PageFunctionAddBussinessClient : PageFunction<String>
    {
        public PageFunctionAddBussinessClient()
        {
            InitializeComponent();
        }

        private void add_bussiness_client(object sender, RoutedEventArgs e)
        {
            Item new_item = new Item(text_box.Text);
            Task save_item_task = Task.Factory.StartNew(
                () =>
                {
                    ListViewModel list_view_model = new ListViewModel();
                    list_view_model.save_item(new_item);
                }
                );

            save_item_task.Wait();

            //Create instance of ReturnEventArgs to pass data back to caller page
            ReturnEventArgs<String> return_object =
               new ReturnEventArgs<String>("Done");

            //Call to PageFunction's OnReturn method and pass selected List
            OnReturn(return_object);

        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            //Create instance of ReturnEventArgs to pass data back to caller page
            ReturnEventArgs<String> return_object =
               new ReturnEventArgs<String>("Cancel");

            //Call to PageFunction's OnReturn method and pass selected List
            OnReturn(return_object);
        }
    }
}
