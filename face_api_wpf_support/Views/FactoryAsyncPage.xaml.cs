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
    /// Interaction logic for FactoryAsyncPage.xaml
    /// </summary>
    public partial class FactoryAsyncPage : Page
    {
        private ListViewModel list_view_model = new ListViewModel();

        public FactoryAsyncPage()
        {
            InitializeComponent();
            DataContext = list_view_model;
        }

        private async Task<FactoryAsyncPage> InitializeAsync()
        {
            await list_view_model.load_business_client();
            return this;
        }

        public static Task<FactoryAsyncPage> CreatePageAsync()
        {
            var ret = new FactoryAsyncPage();
            return ret.InitializeAsync();

        }

    }
}
