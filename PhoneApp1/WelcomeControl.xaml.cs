using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechApp.Service;

namespace SpeechApp
{
    public partial class WelcomeControl : UserControl
    {
        public WelcomeControl()
        {
            InitializeComponent();
            
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var user = Security.GetUserInfo;
            if (user != null && user.UserName != null)
            {
                txt1.Text = string.Format("Hi {0},", user.UserName);
            }
        }
    }
}
