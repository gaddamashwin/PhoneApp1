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
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();

            this.DataContext = this;
            ApplicationBar = new ApplicationBar();
            AppBar = new ApplicationBarHelper();
            phonesvc = new PhoneSvc();

            ApplicationBar.IsVisible = false;
            if (App.myAuth.GetType().Equals(typeof(WindowsLive))) btnSkyDrive.IsEnabled = true; else btnSkyDrive.IsEnabled = false;
            
            AppBar.refreshContentbutton.Click += new EventHandler(RefreshContent);
        }

        #region "Properties"

        public bool IsProgressBarVisible
        {
            get { return (bool)GetValue(IsProgressBarVisibleProperty); }
            set { SetValue(IsProgressBarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsProgressBarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsProgressBarVisibleProperty =
            DependencyProperty.Register("IsProgressBarVisible", typeof(bool), typeof(HomePage), new PropertyMetadata(false));

        public List<PhoneServiceRef.FileContentColl> CollectionItems
        {
            get { return (List<PhoneServiceRef.FileContentColl>)GetValue(CollectionItemsProperty); }
            set { SetValue(CollectionItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionItemsProperty =
            DependencyProperty.Register("CollectionItems", typeof(List<PhoneServiceRef.FileContentColl>), typeof(HomePage), new PropertyMetadata(null));

        public ApplicationBarHelper AppBar;
        public PhoneSvc phonesvc;
        #endregion

        private void btnSkyDrive_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SkyDrive.xaml?link=me/skydrive/files", UriKind.Relative));
        }

        private void btnEnterText_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EnterText.xaml", UriKind.Relative));
        }

        private async void btnLogoff_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await App.myAuth.Logout();
                try { while (NavigationService.RemoveBackEntry() != null) ; }
                catch (System.NullReferenceException) { }
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

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
                        if (header.Contains("Home") || header.Contains("My Account") || header.Contains("about") || header.Contains("Convert"))
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

        void svc_FileContentMyCollSelectAllCompleted(object sender, PhoneServiceRef.FileContentMyCollSelectAllCompletedEventArgs e)
        {
            try
            {
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

        private async void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            Service.WebService.PhoneSvc.FileContentMyCollSelectAllCompleted += svc_FileContentMyCollSelectAllCompleted;
            var user = await Security.GetLoginUser();
            phonesvc.UpdateContentCollection(user.UserId);
            welcome.UserGreeting = string.Format("Hi {0},", user.UserName);
        }

        private void LayoutRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            Service.WebService.PhoneSvc.FileContentMyCollSelectAllCompleted -= svc_FileContentMyCollSelectAllCompleted;
        }
    }
}