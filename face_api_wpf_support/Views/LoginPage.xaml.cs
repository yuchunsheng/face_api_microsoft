using face_api_commons.Authentication;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;

            try
            {
                //Validate credentials through the authentication service
                AuthenticationService _authenticationService = new AuthenticationService();

                User_auth user = _authenticationService.AuthenticateUser(username, password);

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Email, user.Roles);
                                
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine( "Login failed! Please provide some valid credentials.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
            }

            
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                //MessageBox.Show("Login successful");
                ListViewPage first_page = new ListViewPage();
                first_page.Load_business_client();
                NavigationService.Navigate(first_page);
            }else
            {
                MessageBox.Show("Wrong username or password");
            }

            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            BusinessClientPage business_client_page = new BusinessClientPage();
            business_client_page.init();
            NavigationService.Navigate(business_client_page);
        }
    }
}
