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
        public const string UserInfoFile = "UserDetails";

        public static async Task SaveUserInfo(string userName, string password)
        {
            StorageHelper storage = new StorageHelper();
            UserInfo user = new UserInfo();
            user.UserName = userName;
            await storage.WriteFromFile<UserInfo>(UserInfoFile, user);
        }

        private static UserInfo userInfo = null;
        public static async Task<UserInfo> GetUserInfo()
        {
            if (userInfo == null)
            {
                StorageHelper storage = new StorageHelper();
                userInfo = await storage.ReadFromFile<UserInfo>(UserInfoFile);
            }
            return userInfo;
        }

        public static async Task DeleteUserInfo()
        {
            StorageHelper storage = new StorageHelper();
            await storage.DeleteFile(UserInfoFile);
            userInfo = null;
        }
    }
}
