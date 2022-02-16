using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.Utility
{
	public partial class InputTextForm : Form
	{

		public string strText { set; get; } = "";
		public string strDescription { set; get; } = "";
		public string strName { set; get; } = "";
		public string strTitle { set; get; } = "";

		public bool IsChange { private set; get; } = false;


		string _strInit = "";



		public InputTextForm()
		{
			InitializeComponent();

			Load += delegate
			{

				_strInit = strText;
				textBoxText.Text = strText;

				labelName.Text = strName;
				labelDescription.Text = strDescription;
				Name = strTitle;
			};
		}



		private void buttonOk_Click(object sender, EventArgs e)
		{
			strText = textBoxText.Text;

			if (_strInit != strText)
				IsChange = true;

			Close();
		}
	}
}
