using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TextToWavLib;


namespace TextToWavWeb
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
			string theText=this.TextBox1.Text;
			TextToWavLib.Converter c= new Converter();
			string unique=DateTime.Now.Ticks.ToString() +".wav";
			string thePath =Server.MapPath(unique);
			c.TextToWav(theText,thePath,"");
		
			HtmlGenericControl ctrl = new HtmlGenericControl("BGSOUND");
			ctrl.Attributes.Add("src",thePath);

			this.PlaceHolder1.Controls.Add(ctrl);
			HyperLink lnk = new HyperLink();
			lnk.NavigateUrl= thePath;
			lnk.Text ="Download Wav File";
			this.PlaceHolder1.Controls.Add(lnk);






		}
	}
}
