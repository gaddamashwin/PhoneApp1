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
            this.DataContext = this;

            ApplicationBar = new ApplicationBar();
            media.Volume = (double)volumeSlider.Value;

            ApplicationBarIconButton playbutton = new ApplicationBarIconButton();
            playbutton.IconUri = new Uri("/Images/play.png", UriKind.Relative);
            playbutton.Text = "play";
            ApplicationBar.Buttons.Add(playbutton);
            playbutton.Click += new EventHandler(PlayMedia);

        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(Play), new PropertyMetadata(""));

        public string Link
        {
            get { return (string)GetValue(LinkProperty); }
            set { SetValue(LinkProperty, value); }
        }

        public static readonly DependencyProperty LinkProperty = DependencyProperty.Register("Link", typeof(string), typeof(Play), new PropertyMetadata(""));

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Description = NavigationContext.QueryString["Description"];
            Link = NavigationContext.QueryString["Link"];
        }


        private void PlayMedia(object sender, EventArgs e)
        {
            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[0];

            if (btn.Text == "play")
            {
                media.Play();
                btn.Text = "pause";
                btn.IconUri = new Uri("/Images/pause.png", UriKind.Relative);
            }
            else if (btn.Text == "pause")
            {
                media.Pause();
                btn.Text = "play";
                btn.IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }            
        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if(volumeSlider != null) media.Volume = (double)volumeSlider.Value;
        }

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if (timelineSlider != null)
            {
                int SliderValue = (int)timelineSlider.Value;

                // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
                // Create a TimeSpan with miliseconds equal to the slider value.
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                media.Position = ts;
            }
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = media.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            timelineSlider.Value = timelineSlider.Maximum;
            media.Stop();
        }

    }
}