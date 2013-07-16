using SpeechApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;

namespace SpeechApp.Service
{
    public class Security
    {

        #region "Live Authentication"

        /// <summary>
        ///     Defines the scopes the application needs.
        /// </summary>
        private static readonly string[] scopes = new string[] { "wl.signin", "wl.basic", "wl.offline_access" };

        /// <summary>
        ///     Stores the LiveAuthClient instance.
        /// </summary>
        private static LiveAuthClient authClient = new LiveAuthClient("0000000040101922");

        /// <summary>
        ///     Stores the LiveConnectClient instance.
        /// </summary>
        private static LiveConnectClient liveClient;

        /// <summary>
        ///     Calls LiveAuthClient.Initialize to get the user login status.
        ///     Retrieves user profile information if user is already signed in.
        /// </summary>
        private static async Task getLiveUserInfo()
        {
            LiveLoginResult loginResult = await authClient.InitializeAsync(scopes);
            if (loginResult.Status == LiveConnectSessionStatus.Connected)
            {
                liveClient = new LiveConnectClient(loginResult.Session);
                await GetMe();
            }
        }

        #endregion

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
               await getLiveUserInfo();
            }
            if (userInfo == null)
            {
                StorageHelper storage = new StorageHelper();
                userInfo = await storage.ReadFromFile<UserInfo>(UserInfoFile);
            }
        }

        //public static void SetUserInfo(UserInfo user)
        //{
        //    userInfo = user;
        //}

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
