using SpeechApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpeechApp.Service.Authentication
{
    public class Custom : Authenticate
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override Task SignMeIn()
        {
            //this.UserName = userName;
            //this.Password = password;
            AuthReference.AuthenticationServiceClient authService = new AuthReference.AuthenticationServiceClient();
            authService.LoginCompleted += authService_LoginCompleted;
            authService.LoginAsync(UserName, Password, "", true);
            return Task.FromResult(0);
        }

        async void authService_LoginCompleted(object sender, AuthReference.LoginCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null || e.Result == false)
                {
                    MessageBox.Show("Login failed.");
                }
                else
                {
                    StorageHelper storage = new StorageHelper();
                    user = new UserInfo();
                    user.UserName = UserName;
                    user.LoginSource = Constants.SpeechSource;
                    await storage.WriteFromFile<UserInfo>(Constants.UserInfoFile, user);
                    RefreshFunction(user);
                }
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.NetworkException);
            }
            catch (Exception ex)
            {
                MessageBox.Show(SpeechApp.Service.ExceptionHandler.ExceptionLog(ex));
            }
        }

        public async override Task Logout()
        {
            StorageHelper storage = new StorageHelper();
            await storage.WriteFromFile<UserInfo>(Constants.UserInfoFile, new UserInfo());
        }

        public override async Task<DataModel.UserInfo> GetUser()
        {
            if (user == null && string.IsNullOrEmpty(user.UserId))
            {
                StorageHelper storage = new StorageHelper();
                user = await storage.ReadFromFile<UserInfo>(Constants.UserInfoFile);
                if(RefreshFunction != null) RefreshFunction(user);
            }
            return user;
        }
    }
}
