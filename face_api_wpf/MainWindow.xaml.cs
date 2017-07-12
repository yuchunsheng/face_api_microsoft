using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Configuration;

using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using face_api_wpf.Views;

namespace face_api_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["test"] ?? "Not Found";
            Title = String.Format("App Setting {0}", result);
            main_frame.NavigationService.Navigate(new LoginPage());
        }

        //private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("36d7fbe10dfc46e7887edde19be2214b",
        //    "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

       

        
    }
}
