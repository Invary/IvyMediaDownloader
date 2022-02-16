using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	public partial class EditArgForm : Form
	{

		public string strTitle { set; get; } = "";

		public string strName { set; get; } = "";
		public string strArg { set; get; } = "";
		public bool IsDirty { private set; get; } = false;



		public EditArgForm()
		{
			InitializeComponent();

			Load += OnFormLoad;
		}



		private void OnFormLoad(object sender, EventArgs e)
		{
			Text = strTitle;	
			textBoxArgName.Text = strName;
			textBoxArg.Text = strArg;
			IsDirty = false;

			textBoxArgName.TextChanged += OnTextboxChanged;
			textBoxArg.TextChanged += OnTextboxChanged;

			OnTextboxChanged(null, null);
		}


		/// <summary>
		/// refresh button state at textbox changed
		/// </summary>
		private void OnTextboxChanged(object sender, EventArgs e)
		{
			if (textBoxArgName.Text == "" || textBoxArgName.Text == "")
				buttonOk.Enabled = false;
			else
				buttonOk.Enabled = true;
		}



		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (strName != textBoxArgName.Text)
			{
				strName = textBoxArgName.Text;
				IsDirty = true;
			}

			if (strArg != textBoxArg.Text)
			{
				strArg = textBoxArg.Text;
				IsDirty = true;
			}

			Close();
		}
	}
}
