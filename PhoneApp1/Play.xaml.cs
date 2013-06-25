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
using Microsoft.Phone.Shell;

namespace PhoneApp1
{
    public partial class Play : PhoneApplicationPage
    {
        public Play()
        {
            InitializeComponent();
            this.DataContext = this;

            ApplicationBar = new ApplicationBar();

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

    }
}