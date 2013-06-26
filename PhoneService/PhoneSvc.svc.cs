using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PhoneService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PhoneSvc : IPhoneSvc
    {
        private static T CopyProperties<T>(object src) where T : new()
        {
            var dst = new T();
            IEnumerable<PropertyInfo> srcProperties = src.GetType().GetRuntimeProperties();
            dynamic dstType = dst.GetType();

            if (srcProperties == null | dstType.GetProperties() == null)
            {
                return dst;
            }

            foreach (PropertyInfo srcProperty in srcProperties)
            {
                PropertyInfo dstProperty = dstType.GetProperty(srcProperty.Name);

                if (dstProperty != null)
                {
                    if (dstProperty.CanWrite == true)
                    {
                        dstProperty.SetValue(dst, srcProperty.GetValue(src, null), null);
                    }
                }
            }
            return dst;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public IEnumerable<Voices> GetVoices()
        {
            ISTORE2Entities entity = new ISTORE2Entities();
            return entity.GetVoices().ToList().ConvertAll<Voices>(i => CopyProperties<Voices>(i));
        }

        public IEnumerable<FileContentNew> GetFileContentNew()
        {
            ISTORE2Entities entity = new ISTORE2Entities();
            return entity.GetFileContentNew().ToList().ConvertAll<FileContentNew>(i => CopyProperties<FileContentNew>(i));
        }

        public void FileContentUpdateStatus(string status, int fileContenetID)
        {
            ISTORE2Entities entity = new ISTORE2Entities();
            entity.FileContentUpdateStatus(status, fileContenetID);
        }

        public IEnumerable<FileContentColl> FileContentMyCollSelectAll(int userID)
        {
            ISTORE2Entities entity = new ISTORE2Entities();
            return entity.FileContentMyCollSelectAll(userID).ToList().ConvertAll<FileContentColl>(i => CopyProperties<FileContentColl>(i));
        }

        public void FileContentInsert(string title, string content, string url, string file, int userID, int speechRate, int voiceID)
        {
            ISTORE2Entities entity = new ISTORE2Entities();
            entity.FileContentInsert(title,content,url,file,userID,speechRate,voiceID);
        }
    }
}
