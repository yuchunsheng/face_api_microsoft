using face_api_wpf_support.ViewModels.business_face_search;
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

namespace face_api_wpf_support.Views.business_face_search
{
    /// <summary>
    /// Interaction logic for BusinessFaceSearchDetailPage.xaml
    /// </summary>
    public partial class BusinessFaceSearchDetailPage : Page
    {
        private BusinessFaceSearchDetailViewModel business_face_search_detail_view_model = new BusinessFaceSearchDetailViewModel();

        public BusinessFaceSearchDetailPage()
        {
            InitializeComponent();
            DataContext = business_face_search_detail_view_model;
        }

        public void load_item()
        {
            //business_face_search_view_model.load_item();
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(business_face_search_detail_view_model.next_page);
        }
    }
}
