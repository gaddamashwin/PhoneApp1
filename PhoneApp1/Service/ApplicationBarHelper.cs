﻿using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service
{
    public class ApplicationBarHelper
    {
        public ApplicationBarIconButton refreshContentbutton = new ApplicationBarIconButton();
        public ApplicationBarIconButton saveContentbutton = new ApplicationBarIconButton();
        
        public ApplicationBarIconButton GetApplicationBarButton(string header)
        {
            if (header.Contains("Collections"))
            {
                if (refreshContentbutton.IconUri == null)
                {
                    //refreshContentbutton = new ApplicationBarIconButton();
                    refreshContentbutton.IconUri = new Uri("/Images/refresh.png", UriKind.Relative);
                    refreshContentbutton.Text = "Refresh";
                }
                return refreshContentbutton;
            }
            else if (header.Contains("Convert"))
            {
                if (saveContentbutton.IconUri == null)
                {
                    //saveContentbutton = new ApplicationBarIconButton();
                    saveContentbutton.IconUri = new Uri("/Images/save.png", UriKind.Relative);
                    saveContentbutton.Text = "Save";
                }
                return saveContentbutton;
            }
            return null;
        }
    }
}
