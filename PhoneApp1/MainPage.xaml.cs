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

        public Testitems(int id, string des, string link)
        {
            this.ItemId = DateTime.Now;
            this.ItemDescription = des;
            this.Link = link;
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
            CollectionItems.Add(new Testitems(1, "Microsoft", "www.google.com"));
            CollectionItems.Add(new Testitems(2, "Google", "www.google.com"));
            CollectionItems.Add(new Testitems(3, "Apple", "www.google.com"));
            CollectionItems.Add(new Testitems(4, "Facebook", "www.google.com"));
            CollectionItems.Add(new Testitems(5, "Intel", "www.google.com"));
            CollectionItems.Add(new Testitems(6, "GE", "www.google.com"));
            CollectionItems.Add(new Testitems(7, "Linkedin", "www.google.com"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
        }

        public List<Testitems> CollectionItems { get; set; }

        private void lstCollection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Testitems item = (Testitems)e.AddedItems[0];
            NavigationService.Navigate(new Uri("/Play.xaml", UriKind.Relative));
        }

        //The foreground color of the text in SearchTB is set to Magenta when SearchTB
        //gets focus.
        private void SearchTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == txt.Name) txt.Text = "";
            SolidColorBrush Brush1 = new SolidColorBrush();
            Brush1.Color = Colors.Black;
            txt.Foreground = Brush1;

        }

        //The foreground color of the text in SearchTB is set to Blue when SearchTB
        //loses focus. Also, if SearchTB loses focus and no text is entered, the
        //text "Search" is displayed.
        private void SearchTB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == String.Empty)
            {
                txt.Text = txt.Name;
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Black;
                txt.Foreground = Brush2;
            }
        }

        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogoff_Click_2(object sender, RoutedEventArgs e)
        {

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