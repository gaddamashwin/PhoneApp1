using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service
{
    public class ExceptionHandler
    {
        public static string NetworkException = "please check your network connection and try again";

        public static string ExceptionLog(Exception ex)
        {
            return "An error occurred. Please try again later.";
        }
    }
}
