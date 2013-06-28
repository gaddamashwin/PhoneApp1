using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace TextToWav
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button button2;
		private string outFilePath = String.Empty ;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnMem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		      
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button3 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnMem = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(24, 32);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(432, 248);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(24, 296);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(432, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(256, 376);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "Save File";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(136, 376);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Choose Output file";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 328);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(448, 40);
			this.label1.TabIndex = 4;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(8, 376);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(120, 24);
			this.button3.TabIndex = 5;
			this.button3.Text = "Choose Input File";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(120, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(248, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Paste Text below, or choose an input text file.";
			// 
			// btnMem
			// 
			this.btnMem.Location = new System.Drawing.Point(352, 376);
			this.btnMem.Name = "btnMem";
			this.btnMem.Size = new System.Drawing.Size(112, 24);
			this.btnMem.TabIndex = 7;
			this.btnMem.Text = "Save MemStream";
			this.btnMem.Click += new System.EventHandler(this.btnMem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 414);
			this.Controls.Add(this.btnMem);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.textBox1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			TextToWavLib.Converter c = new TextToWavLib.Converter();
			string[] s= c.getInstalledVoices();
			this.comboBox1.DataSource =s;
			
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			DialogResult res=this.saveFileDialog1.ShowDialog();
			if(res==DialogResult.OK)
				outFilePath =saveFileDialog1.FileName ;
			this.label1.Text = outFilePath;

			 
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(this.textBox1.Text=="")
			{
				MessageBox.Show("Please select input filename or paste some text in text area.");
				return;


			}
			if(this.label1.Text=="")
			{
				MessageBox.Show("Please select Output filename first.");
				return;


			}
			button1.Text="Working...";
			button1.Refresh();
			TextToWavLib.Converter c = new TextToWavLib.Converter();
			try
			{
				c.TextToWav(this.textBox1.Text,this.outFilePath,this.comboBox1.SelectedValue.ToString());
			}
			catch(Exception ex)
			{
			MessageBox.Show(ex.Message+ex.StackTrace);
			}
			button1.Text="DONE!";
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			DialogResult res=this.openFileDialog1.ShowDialog();
			if(res==DialogResult.OK)
			{
				string inFilePath =openFileDialog1.FileName ;
				System.IO.FileStream fs = new System.IO.FileStream(inFilePath,System.IO.FileMode.Open);
				byte[] b = new byte[(int)fs.Length ];
				fs.Read(b,0,b.Length);
				fs.Close();
				this.textBox1.Text=System.Text.Encoding.ASCII.GetString(b);
			

			}

		
		}

		private void btnMem_Click(object sender, System.EventArgs e)
		{
		
			byte[] b = null;
			if(this.textBox1.Text=="")
			{
				MessageBox.Show("Please select input filename or paste some text in text area.");
				return;


			}
			if(this.label1.Text=="")
			{
				MessageBox.Show("Please select Output filename first.");
				return;


			}
			btnMem.Text="Working...";
			btnMem.Refresh();
			TextToWavLib.Converter c = new TextToWavLib.Converter();
			try
			{
			  b=	c.TextToWav(this.textBox1.Text,this.comboBox1.SelectedValue.ToString());
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message+ex.StackTrace);
			}
			if(!outFilePath.EndsWith(".raw"))outFilePath+=".raw";
          
			FileStream fs = new FileStream(this.outFilePath,System.IO.FileMode.Create);
			fs.Write(b,0,(int)b.Length);
			fs.Close();
			btnMem.Text="DONE!";
		}
	}
}
