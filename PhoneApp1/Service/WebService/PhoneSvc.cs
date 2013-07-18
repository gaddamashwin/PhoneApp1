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
        public static PhoneSvcClient phoneSvcClient = new PhoneSvcClient();
     
        public async void saveContent(string content, string title)
        {
            UserInfo user = await Security.GetLoginUser();
            //IsProgressBarVisible = true;
            PhoneServiceRef.FileContentForInsert fileContent = new PhoneServiceRef.FileContentForInsert();
            fileContent.content = content;
            fileContent.speechRate = 3;
            fileContent.title = title;
            fileContent.userID = user.UserId;
            fileContent.voiceID = 1;

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
            phoneSvcClient.FileContentMyCollSelectAllAsync(userName);
            phoneSvcClient.CloseAsync();
        }
    }
}
