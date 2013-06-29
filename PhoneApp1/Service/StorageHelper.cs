using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SpeechApp
{
    public class StorageHelper
    {
        public async Task<T> ReadFromFile<T>(string filename)
        {
            T result = default(T);
            //StorageFile sampleFile;
            try
            {
                using (Stream raStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    result = (T)serializer.ReadObject(raStream);
                    await raStream.FlushAsync();
                    raStream.Dispose();
                }
            }
            catch (SerializationException)
            {}
            catch (FileNotFoundException)
            {}
            catch (Exception ex)
            {
                string st = ex.Message;
            }
            //try
            //{

            //   sampleFile = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            //}
            //catch (FileNotFoundException)
            //{
            //    sampleFile = null;
            //}

            //if (sampleFile != null)
            //{
            //    using (IRandomAccessStream readStream = await sampleFile.OpenAsync(FileAccessMode.Read))
            //    {
            //        if (readStream.Size > 0)
            //        {
            //            IInputStream inputStream = readStream.GetInputStreamAt(0);
            //            DataContractSerializer serializer1 = new DataContractSerializer(typeof(T));
            //            try
            //            {
            //                result = (T)serializer1.ReadObject(inputStream.AsStreamForRead());
            //            }
            //            catch (SerializationException)
            //            {
            //                result = default(T);
            //            }
                        
            //        }
            //        readStream.Dispose();
            //    }
            //}
            return result;
        }

        public async Task WriteFromFile<T>(string filename, T info)
        {
            try
            {
                using (Stream raStream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(filename, CreationCollisionOption.ReplaceExisting))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(raStream, info);
                    await raStream.FlushAsync();
                    raStream.Dispose();
                }
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }

            //StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            //IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);
            //using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            //{
            //    // Serialize the Session State.
            //    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            //    serializer.WriteObject(outStream.AsStreamForWrite(), info);
            //    await outStream.FlushAsync();
            //    outStream.Dispose();
            //}
        }

        public async Task DeleteFile(string filename)
        {
            try
            {
                var ret = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                await ret.DeleteAsync();
            }
            catch (FileNotFoundException)
            {                
             
            }
        }
    }
}
