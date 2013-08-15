using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechApp.Service.WebService;
using SpeechApp.Service;

namespace SpeechApp
{
    public partial class EnterText : PhoneApplicationPage
    {

        public PhoneSvc phonesvc;
        ApplicationBarIconButton saveContentbutton;

        public EnterText()
        {
            InitializeComponent();
            phonesvc = new PhoneSvc();
            ApplicationBar = new ApplicationBar();

            //Save
            saveContentbutton = new ApplicationBarIconButton();
            saveContentbutton.IconUri = new Uri("/Images/save.png", UriKind.Relative);
            saveContentbutton.Text = "Save";
            saveContentbutton.Click += new EventHandler(SaveContent);
            
            //Home
            ApplicationBarIconButton homebutton = new ApplicationBarIconButton();
            homebutton.IconUri = new Uri("/Images/home.png", UriKind.Relative);
            homebutton.Text = "Home";
            homebutton.Click += new EventHandler(GoHome);

            //Focus Events
            Title.GotFocus += FocusEvents.TB_GotFocus;
            Title.LostFocus += FocusEvents.TB_LostFocus;
            Content.GotFocus += FocusEvents.TB_GotFocus;
            Content.LostFocus += FocusEvents.TB_LostFocus;

            ApplicationBar.Buttons.Add(saveContentbutton);
            ApplicationBar.Buttons.Add(homebutton);

            
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

        void svc_FileContentInsertCompleted(object sender, PhoneServiceRef.FileContentInsertCompletedEventArgs e)
        {
            try
            {
                Content.Text = Content.Name;
                Title.Text = Title.Name;
                //IsProgressBarVisible = false;
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
            finally
            {
                saveContentbutton.IsEnabled = true;
            }
        }

        private void SaveContent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Title.Text) || Title.Text == Title.Name) MessageBox.Show("Title is required.");
                else if (string.IsNullOrEmpty(Content.Text) || Content.Text == Content.Name) MessageBox.Show("Content is required.");
                else
                {
                    saveContentbutton.IsEnabled = false;
                    phonesvc.saveContent(Content.Text, Title.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            Service.WebService.PhoneSvc.FileContentInsertCompleted += svc_FileContentInsertCompleted;
        }

        private void LayoutRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            Service.WebService.PhoneSvc.FileContentInsertCompleted -= svc_FileContentInsertCompleted;
        }
    }
}