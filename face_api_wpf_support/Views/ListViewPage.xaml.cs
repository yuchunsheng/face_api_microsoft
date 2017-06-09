using face_api_wpf_support.ViewModels;
using System.Windows.Controls;

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
    }
}
