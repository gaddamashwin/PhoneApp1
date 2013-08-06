using SpeechApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpeechApp.Service
{
    public class Security
    {

        private static bool inProcessGetLoginUser = false;
        public async static Task<UserInfo> GetLoginUser()
        {
            UserInfo rt = null;
            if (!inProcessGetLoginUser)
            {
                inProcessGetLoginUser = true;
                //if (App.myAuth != null && App.getUserStatus.IsCompleted) rt = await App.myAuth.GetUser();
                if (App.myAuth != null) rt = await App.myAuth.GetUser();
                inProcessGetLoginUser = false;
            }
            return rt;
        }
        //public static async Task SaveUserInfo(string userName, string password)
        //{
        //    StorageHelper storage = new StorageHelper();
        //    UserInfo user = new UserInfo();
        //    user.UserName = userName;
        //    await storage.WriteFromFile<UserInfo>(UserInfoFile, user);
        //    userInfo = user;
        //}

        //public static async Task SetUserInfo()
        //{
        //    if (userInfo == null)
        //    {
        //        StorageHelper storage = new StorageHelper();
        //        userInfo = await storage.ReadFromFile<UserInfo>(Constants.UserInfoFile);
        //    }
        //}

        //public static void SetUserInfo(UserInfo user)
        //{
        //    userInfo = user;
        //}

        //public static UserInfo userInfo = null;
        
        //public static UserInfo GetUserInfo
        //{
        //    get { return userInfo; }
        //}
        
        //public static async Task DeleteUserInfo()
        //{
        //    userInfo = null;
        //    StorageHelper storage = new StorageHelper();
        //    await storage.WriteFromFile<UserInfo>(UserInfoFile, new UserInfo());
        //}
    }
}
