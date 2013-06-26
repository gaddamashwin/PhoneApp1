using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PhoneService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPhoneSvc
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        IEnumerable<Voices> GetVoices();

        [OperationContract]
        IEnumerable<FileContentNew> GetFileContentNew();

        [OperationContract]
        void FileContentUpdateStatus(string status, int fileContenetID);

        [OperationContract]
        IEnumerable<FileContentColl> FileContentMyCollSelectAll(int userID);

        [OperationContract]
        void FileContentInsert(string title, string content, string url, string file, int userID, int speechRate, int voiceID);
    }

    [DataContract]
    public class FileContentColl 
    {
        [DataMember]
        public string ContentTitle {get;set;}
        [DataMember]
        public System.DateTime CreatedDatetime { get; set; }
        [DataMember]
        public string Download { get; set; }
        [DataMember]
        public string Filepath { get; set; }
    }
    
    [DataContract]
    public class Voices
    {

        [DataMember]
        public int VoiceID { get; set; }

        [DataMember]
        public string Voice { get; set; }
    }

    [DataContract]
    public class FileContentNew 
    {
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string OutFile { get; set; }
        [DataMember]
        public string Voice { get; set; }
        [DataMember]
        public int FileContenetID { get; set; }
    }
}
