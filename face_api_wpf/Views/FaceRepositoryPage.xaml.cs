using face_api_wpf.ViewModels;
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

namespace face_api_wpf.Views
{
    /// <summary>
    /// Interaction logic for FaceRepositoryPage.xaml
    /// </summary>
    public partial class FaceRepositoryPage : Page
    {
        private int test;

        FaceRepositoryViewModel face_repository = new FaceRepositoryViewModel();



        public FaceRepositoryPage()
        {
            InitializeComponent();
            DataContext = face_repository;
        }

        public FaceRepositoryPage(int test):this()
        {
            this.test = test;
        }

        private void onButton_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
