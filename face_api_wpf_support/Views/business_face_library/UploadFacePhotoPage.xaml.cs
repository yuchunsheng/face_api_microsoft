using System.Windows;
using System.Windows.Controls;

using face_api_wpf_support.ViewModels.business_face_library;
using System.Security.Permissions;

namespace face_api_wpf_support.Views.business_face_library
{
    /// <summary>
    /// Interaction logic for upload_face_photo_business.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class UploadFacePhotoPage : Page
    {
        private UploadBusinessFacePhotoViewModel  upload_face_photo_view_model = new UploadBusinessFacePhotoViewModel();

        public UploadFacePhotoPage()
        {
            InitializeComponent();
            DataContext = upload_face_photo_view_model;
        }

        public void load_item()
        {
            upload_face_photo_view_model.init();
        }

        private void next_page_checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(upload_face_photo_view_model.next_page);
        }
    }
}
