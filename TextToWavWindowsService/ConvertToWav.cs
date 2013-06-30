using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TextToWavWindowsService
{
    public partial class ConvertToWav : ServiceBase
    {
        public ConvertToWav()
        {
            InitializeComponent();
        }

        public Timer timer;

        protected override void OnStart(string[] args)
        {
            timer = new Timer(60000);
            timer.Start();
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ConvertMain();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }

        void ConvertMain()
        {
            SqlConnection connection = new SqlConnection(TextToWavWindowsService.Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            DataSet ds = new DataSet();
            cmd.CommandText = "GetFileContentNew";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            connection.Open();
            ada.Fill(ds);
            connection.Close();
            connection.Dispose();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TextToWavLib.Converter c = new TextToWavLib.Converter();
                    try
                    {
                        var outFile = string.Format("{0}{1}{2}", Guid.NewGuid().ToString().Substring(0, 9), dr["FileContenetID"].ToString(), ".wav");
                        var outURL = TextToWavWindowsService.Properties.Settings.Default.URLPath + outFile;
                        c.TextToWav(dr["Content"].ToString(), TextToWavWindowsService.Properties.Settings.Default.OutFilePath + outFile, dr["Voice"].ToString());
                        UpdateStatus(dr["FileContenetID"].ToString(), "Completed", outURL);
                    }
                    catch (Exception ex)
                    {
                        UpdateStatus(dr["FileContenetID"].ToString(), "Error" + ex.Message, null);
                    }
                }
            }

        }

        private static void UpdateStatus(string FileContenetID, string status, string url)
        {
            SqlConnection connection = new SqlConnection(TextToWavWindowsService.Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "FileContentUpdateStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FileContenetID", FileContenetID);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@FileURL", url);
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();

        }
    }
}
