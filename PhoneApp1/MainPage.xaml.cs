using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;
using PhoneApp1.Service;
using System.Windows.Media;

namespace PhoneApp1
{
    public class Testitems
    {
        public DateTime ItemId { get; set; }
        public string ItemDescription { get; set; }
        public string Link { get; set; }
        public Brush ForegroundBrush { get; set; }
        public bool IsEnabled { get; set; }

        public Testitems(int id, string des, string link, Brush foregroundBrush, bool isEnabled)
        {
            this.ItemId = DateTime.Now;
            this.ItemDescription = des;
            this.Link = link;
            this.ForegroundBrush = foregroundBrush;
            this.IsEnabled = isEnabled;
        }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = this;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            CollectionItems = new List<Testitems>();
            CollectionItems.Add(new Testitems(1, "Microsoft", "Sample3.wav", (Brush)Application.Current.Resources["PhoneContrastForegroundBrush"], false));
            CollectionItems.Add(new Testitems(2, "Google", "Sample1.mp3", (Brush)Application.Current.Resources["PhoneForegroundBrush"], true));
            CollectionItems.Add(new Testitems(3, "Apple", "Sample3.wav", (Brush)Application.Current.Resources["PhoneForegroundBrush"], true));
            CollectionItems.Add(new Testitems(4, "Facebook", "Sample3.wav", (Brush)Application.Current.Resources["PhoneForegroundBrush"], true));
            CollectionItems.Add(new Testitems(5, "Intel", "Sample3.wav", (Brush)Application.Current.Resources["PhoneForegroundBrush"], true));
            CollectionItems.Add(new Testitems(6, "GE", "Sample3.wav", (Brush)Application.Current.Resources["PhoneForegroundBrush"], true));
            CollectionItems.Add(new Testitems(7, "Linkedin", "Sample3.wav", (Brush)Application.Current.Resources["PhoneContrastForegroundBrush"], false));
        }

        private async void btnLogoff_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            await Security.DeleteUserInfo();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var user = Security.GetUserInfo;
            if (user == null || user.UserName == null)
            {
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
            lstCollection.SelectedIndex = -1;
        }

        public List<Testitems> CollectionItems { get; set; }

        private void lstCollection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (lstCollection.SelectedIndex != -1)
            {
                Testitems item = (Testitems)e.AddedItems[0];
                if (item.IsEnabled) NavigationService.Navigate(new Uri(string.Format("/Play.xaml?Description={0}&Link={1}", item.ItemDescription, item.Link), UriKind.Relative));
                else MessageBox.Show("Cannot play!");
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

        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Title.Text) || Title.Text == Title.Name) MessageBox.Show("Title is required.");
            if (string.IsNullOrEmpty(Content.Text) || Content.Text == Content.Name) MessageBox.Show("Content is required.");
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}