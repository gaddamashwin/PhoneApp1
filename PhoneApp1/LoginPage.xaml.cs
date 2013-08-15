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
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
            AppBar = new ApplicationBarHelper();
            phonesvc = new PhoneSvc();

            UserID.GotFocus += FocusEvents.TB_GotFocus;
            UserID.LostFocus += FocusEvents.TB_LostFocus;
        }

        public ApplicationBarHelper AppBar;
        public PhoneSvc phonesvc;

        private async void btnLiveSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCustomSignIn.IsEnabled = false;
                btnLiveSignIn.IsEnabled = false;
                var liveLogin = new WindowsLive();
                App.myAuth = liveLogin;
                await App.myAuth.SignMeIn();
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            catch (LiveAuthException authExp)
            {
                //this.tbResponse.Text = authExp.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
            finally
            {
                btnCustomSignIn.IsEnabled = true;
                btnLiveSignIn.IsEnabled = true;
            }
        }

        private void btnCustomSignIn_Click(object sender, RoutedEventArgs e)
        {
            panelSignin1.Visibility = System.Windows.Visibility.Collapsed;
            panelSignin2.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnCancelSignIn_Click(object sender, RoutedEventArgs e)
        {
            panelSignin1.Visibility = System.Windows.Visibility.Visible;
            panelSignin2.Visibility = System.Windows.Visibility.Collapsed;
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID.Text) || UserID.Text == UserID.Name) MessageBox.Show("UserID is required.");
                else if (string.IsNullOrEmpty(Password.Password) || Password.Password == Password.Name) MessageBox.Show("Password is required.");
                else
                {
                    var auth = new Custom();
                    App.myAuth = auth;
                    auth.UserName = UserID.Text;
                    auth.Password = Password.Password;
                    await auth.SignMeIn();
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebBrowserTask wtb = new WebBrowserTask();
                wtb.Uri = new Uri("http://readtome.azurewebsites.net/Mobile/Register.aspx", UriKind.Absolute);
                wtb.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
   
        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            var passwordEmpty = string.IsNullOrEmpty(Password.Password);
            PasswordWatermark.Opacity = passwordEmpty ? 100 : 0;
            Password.Opacity = passwordEmpty ? 0 : 100;
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Opacity = 0;
            Password.Opacity = 100;
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }
    }
}