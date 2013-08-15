using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechApp.Resources;
using SpeechApp.Service;
using System.Windows.Media;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using SpeechApp.DataModel;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;
using SpeechApp.Service.Authentication;
using SpeechApp.Service.WebService;

namespace SpeechApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void btnSkyDrive_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SkyDrive.xaml?link=me/skydrive/files", UriKind.Relative));
        }

        private async void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.RemoveBackEntry();
                StorageHelper sHelp = new StorageHelper();
                var userFromFile = await sHelp.ReadFromFile<UserInfo>(Constants.UserInfoFile);
                if (userFromFile != null && !string.IsNullOrEmpty(userFromFile.LoginSource.ToString()))
                {
                    if (userFromFile.LoginSource == Constants.SpeechSource) App.myAuth = new Custom();
                    else if (userFromFile.LoginSource == Constants.WindowsLiveSouce) App.myAuth = new WindowsLive();
                    if (App.myAuth != null)
                    {
                        var r = await App.myAuth.GetUser();
                        NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
                    }
                    else NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
                }
                else NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
    }
}