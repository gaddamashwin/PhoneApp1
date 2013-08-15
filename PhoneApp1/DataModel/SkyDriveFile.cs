using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpeechApp.DataModel
{
    public class SkyDriveFile
    {
        public string url { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public Visibility hyperlinkVisibility { get; set; }
        public Visibility checkboxVisibility { get; set; }
        public DateTime CreationDate { get; set; }
        public bool checkboxEnabled { get; set; }
        public SkyDriveFile(dynamic fileData)
        {
            this.Name = fileData.name;
            this.ID = fileData.id;
            this.Link = fileData.link;
            this.Type = fileData.type;
            this.CreationDate = Convert.ToDateTime(fileData.created_time);
            if (this.Type.Contains("folder") || this.Type.Contains("album"))
            {
                this.url = string.Concat("/SkyDrive.xaml?link=", fileData.id, "/files");
                this.checkboxEnabled = false;
                this.hyperlinkVisibility = Visibility.Visible;
                this.checkboxVisibility = Visibility.Collapsed;
            }
            //else if (this.Type.Contains("photo") || this.Name.EndsWith(".exe") || this.Name.EndsWith(".zip"))
            //{
            //    this.checkboxEnabled = false;
            //    this.hyperlinkVisibility = Visibility.Collapsed;
            //    this.checkboxVisibility = Visibility.Visible;
            //}
            else if (this.Name.ToLower().EndsWith(".txt") || this.Name.ToLower().EndsWith(".htm") || this.Name.ToLower().EndsWith(".html"))
            {
                this.checkboxEnabled = true;
                this.hyperlinkVisibility = Visibility.Collapsed;
                this.checkboxVisibility = Visibility.Visible;
            }
            else
            {
                this.checkboxEnabled = false;
                this.hyperlinkVisibility = Visibility.Collapsed;
                this.checkboxVisibility = Visibility.Visible;
            }
        }
    } 
}
