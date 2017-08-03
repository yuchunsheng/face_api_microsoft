using face_api_wpf_support.ViewModels.business_face_search;
using System.Windows;
using System.Windows.Controls;

namespace face_api_wpf_support.Views.business_face_search
{
    /// <summary>
    /// Interaction logic for BusinessFaceSearchPage.xaml
    /// </summary>
    public partial class BusinessFaceSearchPage : Page
    {
        private BusinessFaceSearchViewModel business_face_search_view_model = new BusinessFaceSearchViewModel();
        public BusinessFaceSearchPage()
        {
            InitializeComponent();
            DataContext = business_face_search_view_model;
        }

        public void load_item()
        {
            //business_face_search_view_model.load_item();
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(business_face_search_view_model.next_page);
        }
    }
}
