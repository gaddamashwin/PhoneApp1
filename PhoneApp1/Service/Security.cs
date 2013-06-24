using PhoneApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp1.Service
{
    public class Security
    {
        public const string UserInfoFile = "UserDetails1";

        public static async Task SaveUserInfo(string userName, string password)
        {
            StorageHelper storage = new StorageHelper();
            UserInfo user = new UserInfo();
            user.UserName = userName;
            await storage.WriteFromFile<UserInfo>(UserInfoFile, user);
            userInfo = user;
        }

        public static async Task SetUserInfo()
        {
            if (userInfo == null)
            {
                StorageHelper storage = new StorageHelper();
                userInfo = await storage.ReadFromFile<UserInfo>(UserInfoFile);
            }
        }

        private static UserInfo userInfo = null;
        
        public static UserInfo GetUserInfo
        {
            get { return userInfo; }
        }
        
        public static async Task DeleteUserInfo()
        {
            userInfo = null;
            StorageHelper storage = new StorageHelper();
            await storage.WriteFromFile<UserInfo>(UserInfoFile, new UserInfo());
        }
    }
}
