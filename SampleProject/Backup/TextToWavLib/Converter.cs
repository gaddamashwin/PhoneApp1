using SpeechLib;
using System;
using System.IO;
using System.Threading;

namespace TextToWavLib
{	
	public class Converter
	{
		public Converter()
		{			 
		}		
		public string[] getInstalledVoices( )
		{
			SpVoice speech = new SpVoice();
			ISpeechObjectTokens sot = speech.GetVoices("","");
			string[] voiceIds = new string[sot.Count];
			for(int i = 0;i < sot.Count ;i++)
			voiceIds[i] = sot.Item(i).GetDescription(1033) ;
			return voiceIds;
		}
		
		public void TextToWav(string inputText, string filePath, string voiceIdString)
		{
			try 
			{
				System.Web.HttpContext ctx = System.Web.HttpContext.Current;
				
				if(ctx != null)
				{
					DirectoryInfo di = new DirectoryInfo(ctx.Server.MapPath("."));
					FileInfo[] fi = di.GetFiles("*.wav");
					foreach(FileInfo f in fi)
					File.Delete(ctx.Server.MapPath(f.Name));
				}
				
				SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync; 
				SpVoice speech = new SpVoice();
				
				if(voiceIdString != String.Empty)
				{
					ISpeechObjectTokens sot = speech.GetVoices("","");
					string[] voiceIds = new string[sot.Count];
					for(int i = 0;i < sot.Count ;i++)
					{
						voiceIds[i] = sot.Item(i).GetDescription(1033) ;
						if(voiceIds[i] == voiceIdString)
						speech.Voice = sot.Item(i);						
					}
				}
				SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
				SpFileStream SpFileStream = new SpFileStream();
				SpFileStream.Format.Type = SpeechAudioFormatType.SAFT11kHz8BitMono;
				 if( ! filePath.ToLower().EndsWith(".wav"))filePath += ".wav";
				SpFileStream.Open(filePath, SpFileMode, false);
				speech.AudioOutputStream = SpFileStream;
				speech.Speak(inputText, SpFlags);
				speech.WaitUntilDone(Timeout.Infinite);
				SpFileStream.Close();				
			}
			catch
			{
				throw;
			}
		}
		
		public byte[] TextToWav(string inputText, string voiceIdString)
		{
			byte[] b = null;
			try 
			{				
				SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync; 
				SpVoice speech = new SpVoice();				
				if(voiceIdString != String.Empty)
				{
					ISpeechObjectTokens sot = speech.GetVoices("","");
					string[] voiceIds = new string[sot.Count];
					for(int i = 0;i < sot.Count ;i++)
					{
						voiceIds[i] = sot.Item(i).GetDescription(1033) ;
						if(voiceIds[i] == voiceIdString)
						speech.Voice = sot.Item(i);						
					}
				}
				 
				SpMemoryStream spMemStream = new SpMemoryStream();
				spMemStream.Format.Type = SpeechAudioFormatType.SAFT11kHz8BitMono; 
				object buf = new object();
				speech.AudioOutputStream = spMemStream; 
				int r = speech.Speak(inputText, SpFlags);
				speech.WaitUntilDone(Timeout.Infinite);
				spMemStream.Seek(0,SpeechStreamSeekPositionType.SSSPTRelativeToStart);
				buf = spMemStream.GetData(); 
				b = (byte[])buf;
			}
			catch
			{
				throw;
			}
			return b;
		}
	}
}
