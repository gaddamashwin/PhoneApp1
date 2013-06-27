﻿using System;
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
using System.Threading.Tasks;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;

            ApplicationBar = new ApplicationBar();
        }

        #region "Properties"
        public List<PhoneServiceRef.FileContentColl> CollectionItems
        {
            get { return (List<PhoneServiceRef.FileContentColl>)GetValue(CollectionItemsProperty); }
            set { SetValue(CollectionItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionItemsProperty =
            DependencyProperty.Register("CollectionItems", typeof(List<PhoneServiceRef.FileContentColl>), typeof(MainPage), new PropertyMetadata(null));
        #endregion

        #region "UI Events"
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
            else
            {
                UpdateContentCollection(user.UserName);
                //Set the lst collection index to -1 so that no list item is seleted by default
                lstCollection.SelectedIndex = -1;
            }
        }

        private void lstCollection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (lstCollection.SelectedIndex != -1)
            {
                PhoneServiceRef.FileContentColl item = (PhoneServiceRef.FileContentColl)e.AddedItems[0];
                if (string.IsNullOrEmpty(item.Filepath)) NavigationService.Navigate(new Uri(string.Format("/Play.xaml?Description={0}&Link={1}", item.ContentTitle, item.Filepath), UriKind.Relative));
                else MessageBox.Show("Cannot play!");
            }
        }

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == txt.Name) txt.Text = "";
            txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
        }

        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == String.Empty)
            {
                txt.Text = txt.Name;
                txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
            }
        }

        private void SaveContent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Title.Text) || Title.Text == Title.Name) MessageBox.Show("Title is required.");
                else if (string.IsNullOrEmpty(Content.Text) || Content.Text == Content.Name) MessageBox.Show("Content is required.");
                else saveContent(Content.Text, Title.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myPivot.SelectedIndex == 2)
            {
                ApplicationBar.IsVisible = false;
            }
            else 
            {
                    ApplicationBar.IsVisible = true;
                    if(ApplicationBar.Buttons.Count > 0) ApplicationBar.Buttons.RemoveAt(0);
                    ApplicationBar.Buttons.Add(GetApplicationBarButton(myPivot.SelectedIndex));
            }
        }
        #endregion

        #region "Functions"

        private ApplicationBarIconButton refreshContentbutton = null;
        private ApplicationBarIconButton saveContentbutton = null;
        public ApplicationBarIconButton GetApplicationBarButton(int PivotIndex)
        {
            if (PivotIndex == 0)
            {
                if (refreshContentbutton == null)
                {
                    refreshContentbutton = new ApplicationBarIconButton();
                    refreshContentbutton.IconUri = new Uri("/Images/refresh.png", UriKind.Relative);
                    refreshContentbutton.Text = "Refresh";
                    refreshContentbutton.Click += new EventHandler(RefreshContent);
                }
                return refreshContentbutton;
            }
            else
            {
                if (saveContentbutton == null)
                {
                    saveContentbutton = new ApplicationBarIconButton();
                    saveContentbutton.IconUri = new Uri("/Images/save.png", UriKind.Relative);
                    saveContentbutton.Text = "Save";
                    saveContentbutton.Click += new EventHandler(SaveContent);
                }
                return saveContentbutton;
            }
        }

        void saveContent(string content, string title)
        {
            PhoneServiceRef.FileContentForInsert fileContent = new PhoneServiceRef.FileContentForInsert();
            fileContent.content = content;
            fileContent.speechRate = 3;
            fileContent.title = title;
            fileContent.userID = Security.GetUserInfo.UserName;
            fileContent.voiceID = 1;

            var svc = new PhoneServiceRef.PhoneSvcClient();
            svc.FileContentInsertAsync(fileContent);
            svc.FileContentInsertCompleted += svc_FileContentInsertCompleted;
            svc.CloseAsync();

            PhoneServiceRef.FileContentColl newFileContent = new PhoneServiceRef.FileContentColl();
            newFileContent.ContentTitle = title;
            newFileContent.CreatedDatetime = DateTime.Now;
            CollectionItems.Add(newFileContent);
        }

        private void RefreshContent(object sender, EventArgs e)
        {
            var user = Security.GetUserInfo;
            UpdateContentCollection(user.UserName);
        }

        void UpdateContentCollection(string userName)
        {
            var svc = new PhoneServiceRef.PhoneSvcClient();
            //Get the data for the list
            svc.FileContentMyCollSelectAllAsync(userName);
            svc.FileContentMyCollSelectAllCompleted += svc_FileContentMyCollSelectAllCompleted;
            svc.CloseAsync();
        }

        void svc_FileContentInsertCompleted(object sender, PhoneServiceRef.FileContentInsertCompletedEventArgs e)
        {
            //btnSubmit.IsEnabled = true;
            //btnSubmit.Content = "Submit";
            Content.Text = Content.Name;
            Title.Text = Title.Name;
            //myPivot.SelectedIndex = 0;
            MessageBox.Show("Successfully submitted");
        }

        void svc_FileContentMyCollSelectAllCompleted(object sender, PhoneServiceRef.FileContentMyCollSelectAllCompletedEventArgs e)
        {
            CollectionItems = e.Result.ToList();
        }
        #endregion

    }
}