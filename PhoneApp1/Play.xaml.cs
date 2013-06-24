using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Threading;

namespace PhoneApp1
{
    public partial class Play : PhoneApplicationPage
    {
        public Play()
        {
            InitializeComponent();
            //this.DataContext = this;
            //btnPlay.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];
            //media.CurrentStateChanged += new RoutedEventHandler(mediaPlayer_CurrentStateChanged);
        }

        //public double Duration { get; set; }

        //DependencyProperty property = DependencyProperty.Register("Progress", typeof(double), typeof(Play), new PropertyMetadata(0.0));

        //public double Progress
        //{
        //    get
        //    {
        //        return (double)GetValue(property);
        //    }
        //    set
        //    {
        //        SetValue(property, value);
        //    }
        //}

        //void mediaPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        //{
        //    if (media.CurrentState == MediaElementState.Playing)
        //    {
        //        Duration = media.NaturalDuration.TimeSpan.TotalSeconds;
        //        ThreadPool.QueueUserWorkItem(o =>
        //        {
        //            while (true)
        //            {
        //                Dispatcher.BeginInvoke(new Action(() => Progress = media.Position.TotalSeconds * 100 / Duration));
        //                Thread.Sleep(0);
        //            }
        //        });
        //    }
        //}

        private void StopMedia(object sender, RoutedEventArgs e)
        {
            media.Stop();
            btnStop.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];
            btnPause.Background = (Brush)Application.Current.Resources["Transparent"];
            btnPlay.Background = (Brush)Application.Current.Resources["Transparent"]; 
        }
        private void PauseMedia(object sender, RoutedEventArgs e)
        {
            media.Pause();
            btnStop.Background = (Brush)Application.Current.Resources["Transparent"];
            btnPause.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];
            btnPlay.Background = (Brush)Application.Current.Resources["Transparent"]; 
        }
        private void PlayMedia(object sender, RoutedEventArgs e)
        {
            media.Play();
            btnStop.Background = (Brush)Application.Current.Resources["Transparent"];
            btnPause.Background = (Brush)Application.Current.Resources["Transparent"];
            btnPlay.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"]; 
        }

        //private void media_CurrentStateChanged(object sender, RoutedEventArgs e)
        //{
        //    btnStop.Background = (Brush)Application.Current.Resources["Transparent"];
        //    btnPause.Background = (Brush)Application.Current.Resources["Transparent"];
        //    btnPlay.Background = (Brush)Application.Current.Resources["Transparent"];
        //    if (media.CurrentState == MediaElementState.Playing) btnPlay.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];
        //    if (media.CurrentState == MediaElementState.Paused) btnPause.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];
        //    if (media.CurrentState == MediaElementState.Stopped) btnStop.Background = (Brush)Application.Current.Resources["PhoneAccentBrush"];

        //} 
    }
}