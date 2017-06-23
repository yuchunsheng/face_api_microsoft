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
    /// Interaction logic for ParrallelTasksView.xaml
    /// </summary>
    public partial class MaterialDesignDemos : Page
    {
        private ListViewModel list_view_model;
        private List<User> users = new List<User>();

        public MaterialDesignDemos()
        {
            InitializeComponent();
            list_view_model = new ListViewModel();
            DataContext = list_view_model;
            add_users();
            dgUsers.ItemsSource = users;


        }

        private void add_users()
        {
            users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23), ImageUrl = "http://www.wpf-tutorial.com/images/misc/john_doe.jpg" });
            users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

        }

        private void go_home(object sender, RoutedEventArgs e)
        {
            Page home = new ListViewPage();
            this.NavigationService.Navigate(home);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string ImageUrl { get; set; }
    }
}
