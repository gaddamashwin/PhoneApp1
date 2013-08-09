using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service.Authentication
{
    public class FaceBook : Authenticate
    {
        private const string AppId = "";

        private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream";

        private readonly FacebookClient _fb = new FacebookClient();

        public override Task SignMeIn()
        {
            throw new NotImplementedException();
        }

        public override Task Logout()
        {
            throw new NotImplementedException();
        }

        public override Task<DataModel.UserInfo> GetUser()
        {
            throw new NotImplementedException();
        }
    }
}
