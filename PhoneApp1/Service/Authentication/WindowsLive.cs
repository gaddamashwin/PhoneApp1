using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;
using SpeechApp.DataModel;
using System.Threading;
using SpeechApp.Service.WebService;

namespace SpeechApp.Service.Authentication
{
    public class WindowsLive : Authenticate
    { 
        /// <summary>
        ///     Defines the scopes the application needs.
        /// </summary>
        private static readonly string[] scopes = new string[] { "wl.signin", "wl.basic", "wl.offline_access, wl.skydrive, wl.skydrive_update" };

        /// <summary>
        ///     Stores the LiveAuthClient instance.
        /// </summary>
        private static LiveAuthClient authClient = new LiveAuthClient("0000000040101922");

        /// <summary>
        ///     Stores the LiveConnectClient instance.
        /// </summary>
        private static LiveConnectClient _liveClient = null;

        private static bool inProgressLiveClient = false;
        private async Task<LiveConnectClient> getLiveClient()
        {
            if (_liveClient == null && !inProgressLiveClient)
            {
                inProgressLiveClient = true;
                LiveLoginResult loginResult = await authClient.InitializeAsync(scopes);
                if (loginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    _liveClient = new LiveConnectClient(loginResult.Session);
                    SaveUser();
                    await UpdateUserRefresh(_liveClient);
                }
                inProgressLiveClient = false;
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

            //if (RefreshFunction != null) RefreshFunction(user);
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
                //RefreshFunction(user);
            }

        }

        public async override Task Logout()
        {
            StorageHelper storage = new StorageHelper();
            authClient.Logout();
            //if (RefreshFunction != null) RefreshFunction(null);
            user = null;
            await storage.WriteFromFile<UserInfo>(Constants.UserInfoFile, new UserInfo());           
        }

        public override async Task<DataModel.UserInfo> GetUser()
        {
            if (user == null) { LiveConnectClient liveClient = await getLiveClient(); }
            return user;
        }

        public async void SaveUser()
        {
            StorageHelper storage = new StorageHelper();
            UserInfo user = new UserInfo();
            user.LoginSource = Constants.WindowsLiveSouce;
            await storage.WriteFromFile<UserInfo>(Constants.UserInfoFile, user);
        }

        public async Task<List<SkyDriveFile>> GetFilesSkyDrive(string path)
        {
            var liveClient = await getLiveClient();
            LiveOperationResult operationResult = await liveClient.GetAsync(path);
            //LiveOperationResult operationResult = await liveClient.GetAsync("me/skydrive/files");
            //LiveOperationResult operationResult = await liveClient.GetAsync("folder.f7d511d38e61d6ed.F7D511D38E61D6ED!338/files");
            var fileList = new List<SkyDriveFile>();
            dynamic data = operationResult.Result;
            foreach (var dd in data)
            {
                foreach (var d in dd.Value)
                {
                    fileList.Add(new SkyDriveFile(d));
                }
            }
            return fileList;
        }

        public async Task DownloadFileSkyDrive(string fileId, string fileName)
        {
            PhoneSvc svc = new PhoneSvc();
            var liveClient = await getLiveClient();
            LiveDownloadOperationResult operationResult = await liveClient.DownloadAsync(fileId + "/content");
            var stream = operationResult.Stream;
            int streamlen = (int)stream.Length;
            if (streamlen <= 80000)
            {
                var result = new byte[streamlen];
                await stream.ReadAsync(result, 0, streamlen);
                svc.saveContent(Encoding.UTF8.GetString(result, 0, streamlen), fileName);
            }
            else 
            { 
                
            }
        }

    
    }
}
