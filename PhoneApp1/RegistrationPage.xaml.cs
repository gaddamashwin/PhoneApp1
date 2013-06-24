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

namespace PhoneApp1
{
    public partial class Registration : PhoneApplicationPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        //The foreground color of the text in SearchTB is set to Magenta when SearchTB
        //gets focus.
        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == txt.Name) txt.Text = "";
            SolidColorBrush Brush1 = new SolidColorBrush();
            Brush1.Color = Colors.Black;
            txt.Foreground = Brush1;

        }

        //The foreground color of the text in SearchTB is set to Blue when SearchTB
        //loses focus. Also, if SearchTB loses focus and no text is entered, the
        //text "Search" is displayed.
        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == String.Empty)
            {
                txt.Text = txt.Name;
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Black;
                txt.Foreground = Brush2;
            }
        }
    }
}