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

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            await Security.SaveUserInfo(UserID.Text, Password.Text);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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
            txt.Foreground = txt.Foreground = (Brush)Application.Current.Resources["PhoneTextBoxForegroundColor"];

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
                txt.Foreground = txt.Foreground = (Brush)Application.Current.Resources["PhoneTextBoxForegroundColor"];
            }
        }
    }
}