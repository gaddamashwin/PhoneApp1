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

                //if (App.onUserUpdated == null)
                //{
                //    App.onUserUpdated = new Action<UserInfo>((user) =>
                //    {
                //        //if (App.myAuth == null) refreshControls(null);
                //        //else refreshControls(user);
                //        //if (user != null && user.UserId != null)
                //        //{
                //        //    App.myAuth.RefreshFunction = refreshControls;
                //        //    //lstCollection.SelectedIndex = -1;
                //        //}
                //        //myPivot.Visibility = System.Windows.Visibility.Visible;
                //        //mainProgress.Visibility = System.Windows.Visibility.Collapsed;       
                //    }
                //    );
                //}

                //this.DataContext = this;
                //ApplicationBar = new ApplicationBar();
                //AppBar = new ApplicationBarHelper();
                //phonesvc = new PhoneSvc();
                //ApplicationBar.IsVisible = false;


                //Service.WebService.PhoneSvc.FileContentInsertCompleted += svc_FileContentInsertCompleted;
                //Service.WebService.PhoneSvc.FileContentMyCollSelectAllCompleted += svc_FileContentMyCollSelectAllCompleted;
                //AppBar.refreshContentbutton.Click += new EventHandler(RefreshContent);
                //AppBar.saveContentbutton.Click += new EventHandler(SaveContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        //public void refreshControls(UserInfo user)
        //{
        //    myPivot.SelectionChanged -= myPivot_SelectionChanged;
        //    if (user == null || user.UserId == null)
        //    {
        //        if (!myPivot.Items.Contains(pivotHome)) myPivot.Items.Add(pivotHome);
        //        if (!myPivot.Items.Contains(pivotLogin)) myPivot.Items.Add(pivotLogin);
        //        if (myPivot.Items.Contains(pivotCollections)) myPivot.Items.Remove(pivotCollections);
        //        if (myPivot.Items.Contains(pivotConvert)) myPivot.Items.Remove(pivotConvert);
        //        if (myPivot.Items.Contains(pivotAccount)) myPivot.Items.Remove(pivotAccount);
        //        if (myPivot.Items.Contains(pivotAbout))
        //        {
        //            myPivot.Items.Remove(pivotAbout);
        //            myPivot.Items.Add(pivotAbout);
        //        }
        //        if (myPivot.Items.Contains(pivotWelome)) myPivot.Items.Remove(pivotWelome);
        //    }
        //    else
        //    {
        //        phonesvc.UpdateContentCollection(user.UserId);
        //        if (myPivot.Items.Contains(pivotHome)) myPivot.Items.Remove(pivotHome);
        //        if (myPivot.Items.Contains(pivotLogin)) myPivot.Items.Remove(pivotLogin);
        //        if (!myPivot.Items.Contains(pivotWelome)) myPivot.Items.Add(pivotWelome);
        //        if (!myPivot.Items.Contains(pivotCollections)) myPivot.Items.Add(pivotCollections);
        //        if (!myPivot.Items.Contains(pivotConvert)) myPivot.Items.Add(pivotConvert);
        //        if (!myPivot.Items.Contains(pivotAccount)) myPivot.Items.Add(pivotAccount);
        //        if (myPivot.Items.Contains(pivotAbout))
        //        {
        //            myPivot.Items.Remove(pivotAbout);
        //            myPivot.Items.Add(pivotAbout);
        //        }
        //        welcome.UserGreeting = string.Format("Hi {0},", user.UserName);
        //    }
        //    myPivot.SelectedIndex = 0;
        //    myPivot.SelectionChanged += myPivot_SelectionChanged;

        //    UserID.Text = UserID.Name;
        //    Password.Password = "";
        //    TB_LostFocus(UserID, null);
        //    PasswordLostFocus(null, null);
        //}
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