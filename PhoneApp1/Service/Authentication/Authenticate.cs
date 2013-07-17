using SpeechApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service.Authentication
{
    public abstract class Authenticate
    {
        protected static UserInfo user { get; set; }
        public Action<UserInfo> RefreshFunction { get; set; }
        public abstract Task<bool> SignMeIn();
        public abstract Task Logout();
        public abstract Task<UserInfo> GetUser();
        //public abstract void SaveUser();
    }
}
