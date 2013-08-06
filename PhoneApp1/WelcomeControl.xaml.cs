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
using SpeechApp.DataModel;

namespace SpeechApp
{
    public partial class WelcomeControl : UserControl
    {
        public WelcomeControl()
        {
            InitializeComponent();
        }


        public string UserGreeting
        {
            get { return (string)GetValue(UserGreetingProperty); }
            set { txt1.Text = value; }
        }

        // Using a DependencyProperty as the backing store for UserGreeting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserGreetingProperty =
            DependencyProperty.Register("UserGreeting", typeof(string), typeof(string), new PropertyMetadata(0));

        
        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //var user = await Security.GetLoginUser();
            //if (user != null && user.UserName != null)
            //{
            //    txt1.Text = string.Format("Hi {0},", user.UserName);
            //}
        }
    }
}
