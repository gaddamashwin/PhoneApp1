using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
//using Windows.Storage.Streams;
using System.Runtime.Serialization;
using Windows.Storage.Streams;
using System.IO;
using PhoneApp1.Service;
using System.Windows.Media;

namespace PhoneApp1
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private CookieContainer cc;
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(UserID.Text) || UserID.Text == UserID.Name) MessageBox.Show("UserID is required.");
            else if (string.IsNullOrEmpty(Password.Password) || Password.Password == Password.Name) MessageBox.Show("Password is required.");
            else
            {
                AuthReference.AuthenticationServiceClient authService = new AuthReference.AuthenticationServiceClient();
                cc = new CookieContainer();
                authService.CookieContainer = cc;
                authService.LoginCompleted += authService_LoginCompleted;
                authService.LoginAsync(UserID.Text, Password.Password, "", true);
            }
        }

        async void authService_LoginCompleted(object sender, AuthReference.LoginCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Login failed, you Jackwagon.");
            }
            else
            {
                MembershipServiceReference.MembershipServiceClient helloService = new MembershipServiceReference.MembershipServiceClient();
                helloService.CookieContainer = cc;
                helloService.IsAuthenticatedCompleted += helloService_IsAuthenticatedCompleted;
                helloService.IsAuthenticatedAsync();

                await Security.SaveUserInfo(UserID.Text, Password.Password);
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        void helloService_IsAuthenticatedCompleted(object sender, MembershipServiceReference.IsAuthenticatedCompletedEventArgs e)
        {
            MessageBox.Show("You're logged in, results from svc: " + e.Result);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegistrationPage.xaml", UriKind.Relative));
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var user = Security.GetUserInfo;
            if (user != null && user.UserName !=null)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        //The foreground color of the text in SearchTB is set to Magenta when SearchTB
        //gets focus.
        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == txt.Name) txt.Text = "";
            txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
        }

        //The foreground color of the text in SearchTB is set to Blue when SearchTB
        //loses focus. Also, if SearchTB loses focus and no text is entered, the
        //text "Search" is displayed.
        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == String.Empty)
            {
                txt.Text = txt.Name;
                txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
            }
        }
    }
}