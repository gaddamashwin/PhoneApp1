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
        //private UserInfo user;
        // Constructor
        public MainPage()
        {
            try
            {
                InitializeComponent();
                
                App.onUserUpdated = new Action<UserInfo>((user) =>
                {
                    if (App.myAuth == null) refreshControls(null);
                    else refreshControls(user);
                    if (user != null && user.UserId != null)
                    {
                        App.myAuth.RefreshFunction = refreshControls;
                        //lstCollection.SelectedIndex = -1;
                    }
                    myPivot.Visibility = System.Windows.Visibility.Visible;
                    mainProgress.Visibility = System.Windows.Visibility.Collapsed;
                }
                );

                this.DataContext = this;
                ApplicationBar = new ApplicationBar();
                AppBar = new ApplicationBarHelper();
                phonesvc = new PhoneSvc();
                ApplicationBar.IsVisible = false;


                Service.WebService.PhoneSvc.FileContentInsertCompleted += svc_FileContentInsertCompleted;
                Service.WebService.PhoneSvc.FileContentMyCollSelectAllCompleted += svc_FileContentMyCollSelectAllCompleted;
                AppBar.refreshContentbutton.Click += new EventHandler(RefreshContent);
                AppBar.saveContentbutton.Click += new EventHandler(SaveContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        #region "Properties"

        public bool IsProgressBarVisible
        {
            get { return (bool)GetValue(IsProgressBarVisibleProperty); }
            set { SetValue(IsProgressBarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsProgressBarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsProgressBarVisibleProperty =
            DependencyProperty.Register("IsProgressBarVisible", typeof(bool), typeof(MainPage), new PropertyMetadata(false));

        public List<PhoneServiceRef.FileContentColl> CollectionItems
        {
            get { return (List<PhoneServiceRef.FileContentColl>)GetValue(CollectionItemsProperty); }
            set { SetValue(CollectionItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionItemsProperty =
            DependencyProperty.Register("CollectionItems", typeof(List<PhoneServiceRef.FileContentColl>), typeof(MainPage), new PropertyMetadata(null));

        public ApplicationBarHelper AppBar;
        public PhoneSvc phonesvc;
        #endregion

        #region "Authentication Events"
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID.Text) || UserID.Text == UserID.Name) MessageBox.Show("UserID is required.");
                else if (string.IsNullOrEmpty(Password.Password) || Password.Password == Password.Name) MessageBox.Show("Password is required.");
                else
                {
                    var auth = new Custom();
                    App.myAuth = auth;
                    auth.RefreshFunction = refreshControls;
                    auth.UserName = UserID.Text;
                    auth.Password = Password.Password;
                    auth.SignMeIn();
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
       
        private async void btnLiveSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCustomSignIn.IsEnabled = false;
                btnLiveSignIn.IsEnabled = false;
                var liveLogin = new WindowsLive();
                App.myAuth = liveLogin;
                App.myAuth.RefreshFunction = refreshControls;
                await App.myAuth.SignMeIn();
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

        private async void btnLogoff_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await App.myAuth.Logout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
        
        #endregion

        #region "UI Events Other"

        private void lstCollection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if (lstCollection.SelectedIndex != -1)
                //{
                    PhoneServiceRef.FileContentColl item = (PhoneServiceRef.FileContentColl)e.AddedItems[0];
                    if (!string.IsNullOrEmpty(item.Filepath)) NavigationService.Navigate(new Uri(string.Format("/Play.xaml?Description={0}&Link={1}", item.ContentTitle, item.Filepath), UriKind.Relative));
                    else MessageBox.Show("Conversion in progress...");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void SaveContent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Title.Text) || Title.Text == Title.Name) MessageBox.Show("Title is required.");
                else if (string.IsNullOrEmpty(Content.Text) || Content.Text == Content.Name) MessageBox.Show("Content is required.");
                else phonesvc.saveContent(Content.Text, Title.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (myPivot.SelectedIndex >= 0)
                {
                    var pItem = (PivotItem)myPivot.SelectedItem;
                    if (pItem != null)
                    {
                        string header = pItem.Header.ToString();
                        if (header.Contains("Home") || header.Contains("My Account") || header.Contains("about"))
                        {
                            ApplicationBar.IsVisible = false;
                        }
                        else
                        {
                            var myButton = AppBar.GetApplicationBarButton(header);
                            if (myButton != null)
                            {
                                ApplicationBar.IsVisible = true;
                                if (ApplicationBar.Buttons.Count > 0) ApplicationBar.Buttons.RemoveAt(0);
                                ApplicationBar.Buttons.Add(myButton);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private async void RefreshContent(object sender, EventArgs e)
        {
            try
            {
                var usr = await App.myAuth.GetUser();
                phonesvc.UpdateContentCollection(usr.UserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
        
        void svc_FileContentInsertCompleted(object sender, PhoneServiceRef.FileContentInsertCompletedEventArgs e)
        {
            try
            {
                Content.Text = Content.Name;
                Title.Text = Title.Name;
                IsProgressBarVisible = false;
                MessageBox.Show("Successfully submitted");
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.NetworkException);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        void svc_FileContentMyCollSelectAllCompleted(object sender, PhoneServiceRef.FileContentMyCollSelectAllCompletedEventArgs e)
        {
            try
            {
                Title.Text = Title.Name;
                IsProgressBarVisible = false;
                var rtList = e.Result.ToList();
                CollectionItems = rtList;
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.NetworkException);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
        
        #endregion

        #region TextBox background Text

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text == txt.Name) txt.Text = "";
                txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text == String.Empty)
                {
                    txt.Text = txt.Name;
                    txt.Foreground = (SolidColorBrush)Application.Current.Resources["PhoneTextBoxReadOnlyBrush"];
                }
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
        #endregion

        #region "Functions"

        public void refreshControls(UserInfo user)
        {
            myPivot.SelectionChanged -= myPivot_SelectionChanged;
            if (user == null || user.UserId == null)
            {
                if (!myPivot.Items.Contains(pivotHome)) myPivot.Items.Add(pivotHome);
                if (!myPivot.Items.Contains(pivotLogin)) myPivot.Items.Add(pivotLogin);
                if (myPivot.Items.Contains(pivotCollections)) myPivot.Items.Remove(pivotCollections);
                if (myPivot.Items.Contains(pivotConvert)) myPivot.Items.Remove(pivotConvert);
                if (myPivot.Items.Contains(pivotAccount)) myPivot.Items.Remove(pivotAccount);
                if (myPivot.Items.Contains(pivotAbout))
                {
                    myPivot.Items.Remove(pivotAbout);
                    myPivot.Items.Add(pivotAbout);
                }
                if (myPivot.Items.Contains(pivotWelome)) myPivot.Items.Remove(pivotWelome);
            }
            else
            {
                phonesvc.UpdateContentCollection(user.UserId);
                if (myPivot.Items.Contains(pivotHome)) myPivot.Items.Remove(pivotHome);
                if (myPivot.Items.Contains(pivotLogin)) myPivot.Items.Remove(pivotLogin);
                if (!myPivot.Items.Contains(pivotWelome)) myPivot.Items.Add(pivotWelome);
                if (!myPivot.Items.Contains(pivotCollections)) myPivot.Items.Add(pivotCollections);
                if (!myPivot.Items.Contains(pivotConvert)) myPivot.Items.Add(pivotConvert);
                if (!myPivot.Items.Contains(pivotAccount)) myPivot.Items.Add(pivotAccount);
                if (myPivot.Items.Contains(pivotAbout))
                {
                    myPivot.Items.Remove(pivotAbout);
                    myPivot.Items.Add(pivotAbout);
                }
                welcome.UserGreeting = string.Format("Hi {0},", user.UserName);
            }
            myPivot.SelectedIndex = 0;
            myPivot.SelectionChanged += myPivot_SelectionChanged;

            UserID.Text = UserID.Name;
            Password.Password = "";
            TB_LostFocus(UserID, null);
            PasswordLostFocus(null,null);
        }

        #endregion

        private void btnSkyDrive_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SkyDrive.xaml?link=me/skydrive/files", UriKind.Relative));
        }

    }
}