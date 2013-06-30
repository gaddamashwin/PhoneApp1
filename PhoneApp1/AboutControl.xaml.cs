using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Tasks;
using System.IO;

namespace SpeechApp
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
            UpdateVersion();
        }

        void UpdateVersion()
        {
            try
            {
                Uri manifest = new Uri("WMAppManifest.xml", UriKind.Relative);
                var si = Application.GetResourceStream(manifest);
                if (si != null)
                {
                    using (StreamReader sr = new StreamReader(si.Stream))
                    {
                        bool haveApp = false;
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (!haveApp)
                            {
                                int i = line.IndexOf("AppPlatformVersion=\"", StringComparison.InvariantCulture);
                                if (i >= 0)
                                {
                                    haveApp = true;
                                    line = line.Substring(i + 20);

                                    int z = line.IndexOf("\"");
                                    if (z >= 0)
                                    {
                                        // if you're interested in the app plat version at all
                                        // AppPlatformVersion = line.Substring(0, z);
                                    }
                                }
                            }

                            int y = line.IndexOf("Version=\"", StringComparison.InvariantCulture);
                            if (y >= 0)
                            {
                                int z = line.IndexOf("\"", y + 9, StringComparison.InvariantCulture);
                                if (z >= 0)
                                {
                                    // We have the version, no need to read on.
                                    _versionText.Text = line.Substring(y + 9, z - y - 9);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    _versionText.Text = "Unknown";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = ((ButtonBase)sender).Tag as string;

                switch (s)
                {
                    case "Review":
                        var task = new MarketplaceReviewTask();
                        task.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
    }
}
