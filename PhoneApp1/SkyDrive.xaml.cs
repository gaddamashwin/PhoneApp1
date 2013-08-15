using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechApp.Service.Authentication;
using SpeechApp.DataModel;

namespace SpeechApp
{
    public partial class SkyDrive : PhoneApplicationPage
    {
        public SkyDrive()
        {
            try
            {
                InitializeComponent();
                ApplicationBar = new ApplicationBar();
                ApplicationBarIconButton homebutton = new ApplicationBarIconButton();
                homebutton.IconUri = new Uri("/Images/home.png", UriKind.Relative);
                homebutton.Text = "Home";
                homebutton.Click += new EventHandler(GoHome);
                ApplicationBar.Buttons.Add(homebutton);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
            
        }

        private void GoHome(object sender, EventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        string link;

        public List<SkyDriveFile> CollectionItems
        {
            get { return (List<SkyDriveFile>)GetValue(CollectionItemsProperty); }
            set { SetValue(CollectionItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionItemsProperty =
            DependencyProperty.Register("CollectionItems", typeof(List<SkyDriveFile>), typeof(SkyDrive), new PropertyMetadata(null));

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
                link = NavigationContext.QueryString["link"];
                if (App.myAuth.GetType().Equals(typeof(WindowsLive))) { var r = (WindowsLive)App.myAuth; CollectionItems = await r.GetFilesSkyDrive(link); }
                lstCollection.ItemsSource = CollectionItems;
                lstCollection.Visibility = System.Windows.Visibility.Visible;
                mainProgress.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                SkyDriveFile dataItem = (SkyDriveFile)((FrameworkElement)sender).DataContext;
                dataItem.checkboxEnabled = false;
                if (App.myAuth.GetType().Equals(typeof(WindowsLive))) { var r = (WindowsLive)App.myAuth; await r.DownloadFileSkyDrive(dataItem.ID, dataItem.Name); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
    }
}