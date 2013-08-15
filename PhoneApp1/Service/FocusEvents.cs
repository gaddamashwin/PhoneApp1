using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpeechApp.Service
{
    public class FocusEvents
    {
        public static void TB_GotFocus(object sender, RoutedEventArgs e)
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

        public static void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text == String.Empty)
                {
                    txt.Text = txt.Name;
                    txt.Foreground = (SolidColorBrush)Application.Current.Resources["PhoneTextBoxReadOnlyBrush"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }
    }
}
