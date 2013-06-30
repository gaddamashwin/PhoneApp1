namespace TextToWavWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextToWavProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // TextToWavProcessInstaller
            // 
            this.TextToWavProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.TextToWavProcessInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstaller1});
            this.TextToWavProcessInstaller.Password = null;
            this.TextToWavProcessInstaller.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "Converts the text to the Wav file";
            this.serviceInstaller1.DisplayName = "Text to Wav";
            this.serviceInstaller1.ServiceName = "ConvertToWav";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TextToWavProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TextToWavProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}