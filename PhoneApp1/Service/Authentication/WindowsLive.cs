using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;
using SpeechApp.DataModel;

namespace SpeechApp.Service.Authentication
{
    public class WindowsLive : Authenticate
    {
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
        private static LiveConnectClient _liveClient = null;

        private async Task<LiveConnectClient> getLiveClient()
        {
            if (_liveClient == null)
            {
                LiveLoginResult loginResult = await authClient.InitializeAsync(scopes);
                if (loginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    _liveClient = new LiveConnectClient(loginResult.Session);
                    SaveUser();
                    await UpdateUserRefresh(_liveClient);
                }
            }
            return _liveClient;
        }

        private async Task UpdateUserRefresh(LiveConnectClient liveClient)
        {
            LiveOperationResult operationResult = await liveClient.GetAsync("me");
            dynamic properties = operationResult.Result;
            user = new UserInfo();
            user.UserId = "Live" + properties.id;
            user.UserName = properties.name;
            user.LoginSource = Constants.WindowsLiveSouce;

            if (RefreshFunction != null) RefreshFunction(user);
        }


        public override async Task SignMeIn()
        {
            LiveLoginResult loginResult = await authClient.LoginAsync(scopes);
            if (loginResult.Status == LiveConnectSessionStatus.Connected)
            {
                _liveClient = new LiveConnectClient(loginResult.Session);
                SaveUser();
                await UpdateUserRefresh(_liveClient);
            }
            else
            {
                RefreshFunction(user);
            }

        }

        public override Task Logout()
        {
            authClient.Logout();
            if (RefreshFunction != null) RefreshFunction(null);
            user = null;
            return Task.FromResult<object>(null);
        }

        public override async Task<DataModel.UserInfo> GetUser()
        {
            if (user==null)
            {
                LiveConnectClient liveClient = await getLiveClient();
                if (liveClient != null)
                {
                    await UpdateUserRefresh(liveClient);
                }
            }
            return user;
        }

        public async void SaveUser()
        {
            StorageHelper storage = new StorageHelper();
            UserInfo user = new UserInfo();
            user.LoginSource = Constants.WindowsLiveSouce;
            await storage.WriteFromFile<UserInfo>(Constants.UserInfoFile, user);
        }
    }
}
