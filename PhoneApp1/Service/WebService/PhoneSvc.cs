using SpeechApp.DataModel;
using SpeechApp.PhoneServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechApp.Service.WebService
{
    public class PhoneSvc
    {
        
        private PhoneSvcClient GetPhoneSvcClient()
        {
            PhoneSvcClient _phoneSvcClient;    
            _phoneSvcClient = new PhoneSvcClient();
            _phoneSvcClient.FileContentInsertCompleted += FileContentInsertCompleted;
            _phoneSvcClient.FileContentMyCollSelectAllCompleted += FileContentMyCollSelectAllCompleted;
            return _phoneSvcClient;
        }

        public static event System.EventHandler<FileContentInsertCompletedEventArgs> FileContentInsertCompleted;
        public static event System.EventHandler<FileContentMyCollSelectAllCompletedEventArgs> FileContentMyCollSelectAllCompleted;

        public async void saveContent(string content, string title)
        {
            UserInfo user = await Security.GetLoginUser();
            PhoneSvcClient phoneSvcClient = GetPhoneSvcClient();
            //IsProgressBarVisible = true;
            PhoneServiceRef.FileContentForInsert fileContent = new PhoneServiceRef.FileContentForInsert();
            fileContent.content = content;
            fileContent.speechRate = 3;
            fileContent.title = title;
            fileContent.userID = user.UserId;
            fileContent.voiceID = 1;

            phoneSvcClient.OpenAsync();
            phoneSvcClient.FileContentInsertAsync(fileContent);
            phoneSvcClient.CloseAsync();
            
            PhoneServiceRef.FileContentColl newFileContent = new PhoneServiceRef.FileContentColl();
            newFileContent.ContentTitle = title;
            newFileContent.CreatedDatetime = DateTime.Now;
            //CollectionItems.Add(newFileContent);
        }

        public void UpdateContentCollection(string userName)
        {
            //IsProgressBarVisible = true;
            //Get the data for the list
            PhoneSvcClient phoneSvcClient = GetPhoneSvcClient();
            phoneSvcClient.OpenAsync();
            phoneSvcClient.FileContentMyCollSelectAllAsync(userName);
            phoneSvcClient.CloseAsync();
        }
    }
}
