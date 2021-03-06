﻿using face_api_wpf.ViewModels;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel login_view_model = new LoginViewModel(); 
        public LoginPage()
        {
            InitializeComponent();
            DataContext = login_view_model;

        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FaceRepositoryPage(10));
        }
    }
}
