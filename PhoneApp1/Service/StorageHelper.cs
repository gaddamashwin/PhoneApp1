using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PhoneApp1
{
    public class StorageHelper
    {
        public async Task<T> ReadFromFile<T>(string filename)
        {
            T result = default(T);
            StorageFile sampleFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename,CreationCollisionOption.OpenIfExists);
            using (IRandomAccessStream readStream = await sampleFile.OpenAsync(FileAccessMode.Read))
            {
                if (readStream.Size > 0)
                {
                    IInputStream inputStream = readStream.GetInputStreamAt(0);
                    DataContractSerializer serializer1 = new DataContractSerializer(typeof(T));
                    result = (T)serializer1.ReadObject(inputStream.AsStreamForRead());
                }
                readStream.Dispose();
            }
            
            return result;
        }

        public async Task WriteFromFile<T>(string filename, T info)
        {
            StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);
            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                // Serialize the Session State.
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(outStream.AsStreamForWrite(), info);
                await outStream.FlushAsync();
                outStream.Dispose();
            }
        }

        public async Task DeleteFile(string filename)
        {
            try
            {
                var ret = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                await ret.DeleteAsync();
            }
            catch (Exception)
            {                
             
            }
        }
    }
}
