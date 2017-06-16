using face_api_wpf_support.ViewModels;
using System;
using System.Windows.Navigation;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for AddItemPageFunction.xaml
    /// </summary>
    public partial class AddItemPageFunction : PageFunction<Object>
    {
        public AddItemPageFunction()
        {
            InitializeComponent();
        }

        private void add_button_click(object sender, System.Windows.RoutedEventArgs e)
        {
            Item new_item = new Item(text_box.Text);


            //Create instance of ReturnEventArgs to pass data back to caller page
            ReturnEventArgs<Object> return_object =
               new ReturnEventArgs<Object>((Object)new_item);

            //Call to PageFunction's OnReturn method and pass selected List
            OnReturn(return_object);
        }
    }
}
