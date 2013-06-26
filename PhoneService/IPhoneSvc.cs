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
        string FileContentUpdateStatus(string status, int fileContenetID);

        [OperationContract]
        IEnumerable<FileContentColl> FileContentMyCollSelectAll(string userID);

        [OperationContract]
        string FileContentInsert(FileContentForInsert fileContent);
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

    public class FileContentForInsert
    { 
        public string title { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string file { get; set; }
        public string userID { get; set; }
        public int speechRate { get; set; }
        public int voiceID { get; set; }
    }
}
