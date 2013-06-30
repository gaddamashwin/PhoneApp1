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

namespace SpeechApp
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
        #endregion

        #region "UI Events"
        private async void btnLogoff_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
                await Security.DeleteUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void lstCollection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstCollection.SelectedIndex != -1)
                {
                    PhoneServiceRef.FileContentColl item = (PhoneServiceRef.FileContentColl)e.AddedItems[0];
                    if (!string.IsNullOrEmpty(item.Filepath)) NavigationService.Navigate(new Uri(string.Format("/Play.xaml?Description={0}&Link={1}", item.ContentTitle, item.Filepath), UriKind.Relative));
                    else MessageBox.Show("Conversion in progress...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
            
        }

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
                    txt.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneTextBoxForegroundColor"]);
                }
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
                else saveContent(Content.Text, Title.Text);
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
                            var myButton = GetApplicationBarButton(header);
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
        #endregion

        #region "Functions"

        private ApplicationBarIconButton refreshContentbutton = null;
        private ApplicationBarIconButton saveContentbutton = null;
        public ApplicationBarIconButton GetApplicationBarButton(string header)
        {
            if (header.Contains("Collections"))
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
            else if (header.Contains("Convert"))
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
            return null;
        }

        void saveContent(string content, string title)
        {
            IsProgressBarVisible = true;
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
            IsProgressBarVisible = true;
            var svc = new PhoneServiceRef.PhoneSvcClient();
            //Get the data for the list
            svc.FileContentMyCollSelectAllAsync(userName);
            svc.FileContentMyCollSelectAllCompleted += svc_FileContentMyCollSelectAllCompleted;
            svc.CloseAsync();
        }

        void svc_FileContentInsertCompleted(object sender, PhoneServiceRef.FileContentInsertCompletedEventArgs e)
        {
            Content.Text = Content.Name;
            Title.Text = Title.Name;
            IsProgressBarVisible = false;
            MessageBox.Show("Successfully submitted");
        }

        void svc_FileContentMyCollSelectAllCompleted(object sender, PhoneServiceRef.FileContentMyCollSelectAllCompletedEventArgs e)
        {
            Title.Text = Title.Name;
            IsProgressBarVisible = false;
            CollectionItems = e.Result.ToList();
        }
        #endregion

    }
}