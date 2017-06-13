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
    /// Interaction logic for ParrallelTasksView.xaml
    /// </summary>
    public partial class ParrallelTasksView : Page
    {
        public ParrallelTasksView()
        {
            InitializeComponent();
        }

        private void go_home(object sender, RoutedEventArgs e)
        {
            Page home = new ListViewPage();
            this.NavigationService.Navigate(home);
        }
    }
}
