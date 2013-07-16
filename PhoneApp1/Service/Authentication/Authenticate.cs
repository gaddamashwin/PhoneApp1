using SpeechApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service.Authentication
{
    abstract class Authenticate
    {
        private Action<UserInfo> RefreshFunction;
        public Authenticate(Action<UserInfo> refresh)
        {
            RefreshFunction = refresh;
        }
        public void Sign
        private abstract void SignMeIn();

    }
}
