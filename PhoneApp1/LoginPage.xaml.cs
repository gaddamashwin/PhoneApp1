﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
//using Windows.Storage.Streams;
using System.Runtime.Serialization;
using Windows.Storage.Streams;
using System.IO;
using SpeechApp.Service;
using System.Windows.Media;
using Microsoft.Phone.Tasks;

namespace SpeechApp
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            //ApplicationBar = new ApplicationBar();

            //ApplicationBarIconButton signInbutton = new ApplicationBarIconButton();
            //signInbutton.IconUri = new Uri("/Images/accept.png", UriKind.Relative);
            //signInbutton.Text = "Login";
            //ApplicationBar.Buttons.Add(signInbutton);
            //signInbutton.Click += new EventHandler(SignIn);

            //ApplicationBarIconButton cancelInbutton = new ApplicationBarIconButton();
            //cancelInbutton.IconUri = new Uri("/Images/cancel.png", UriKind.Relative);
            //cancelInbutton.Text = "Cancel";
            //ApplicationBar.Buttons.Add(cancelInbutton);
            //cancelInbutton.Click += new EventHandler(Cancel);
        }

        private void SignIn(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID.Text) || UserID.Text == UserID.Name) MessageBox.Show("UserID is required.");
                else if (string.IsNullOrEmpty(Password.Password) || Password.Password == Password.Name) MessageBox.Show("Password is required.");
                else
                {
                    AuthReference.AuthenticationServiceClient authService = new AuthReference.AuthenticationServiceClient();
                    //cc = new CookieContainer();
                    //authService.CookieContainer = cc;
                    authService.LoginCompleted += authService_LoginCompleted;
                    authService.LoginAsync(UserID.Text, Password.Password, "", true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        async void authService_LoginCompleted(object sender, AuthReference.LoginCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null || e.Result == false)
                {
                    MessageBox.Show("Login failed..");
                }
                else
                {
                    //MembershipServiceReference.MembershipServiceClient helloService = new MembershipServiceReference.MembershipServiceClient();
                    ////helloService.CookieContainer = cc;
                    //helloService.IsAuthenticatedCompleted += helloService_IsAuthenticatedCompleted;
                    //helloService.IsAuthenticatedAsync();

                    await Security.SaveUserInfo(UserID.Text, Password.Password);
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
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

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = Security.GetUserInfo;
                if (user != null && user.UserName != null)
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SignIn(sender, e);
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
    }
}