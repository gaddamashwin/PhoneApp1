using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(TextToWav.Properties.Settings.Default.ConnectionString);
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
                        var outFile = string.Format("{0}{1}{2}", Guid.NewGuid().ToString().Substring(0,9), dr["FileContenetID"].ToString(), ".wav");
                        var outURL = TextToWav.Properties.Settings.Default.URLPath + outFile;
                        c.TextToWav(dr["Content"].ToString(), TextToWav.Properties.Settings.Default.OutFilePath + outFile, dr["Voice"].ToString());
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
            SqlConnection connection = new SqlConnection(TextToWav.Properties.Settings.Default.ConnectionString);
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
