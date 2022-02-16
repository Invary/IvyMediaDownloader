using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.Utility
{
	public partial class RegExpCheckForm : Form
	{
		public string RegExp { set; get; } = "";


		public RegExpCheckForm()
		{
			InitializeComponent();

			Load += delegate
			{
				textBoxRegexp.Text = RegExp;
			};

		}


		private void buttonExecute_Click(object sender, EventArgs e)
		{
			textBoxResult.Text = "";

			var text = textBoxText.Text;
			if (string.IsNullOrEmpty(text))
			{
				textBoxResult.Text = "Error: Text is empty.";
				return;
			}

			var strRegexp = textBoxRegexp.Text;
			if (string.IsNullOrEmpty(strRegexp))
			{
				textBoxResult.Text = "Error: RegExp string is empty.";
				return;
			}

			string result = "";

			var matchs = Regex.Matches(text, strRegexp);

			result += $"Hit count: {matchs.Count}\r\n";
			//result += $"\r\n";

			int n = 0;
			foreach (Match match in matchs)
			{
				n++;
				if (match.Groups.Count <= 1)
					continue;

				result += $"Group: {n}\r\n";
				for (int i = 1; i < match.Groups.Count; i++)
				{
					var hit = match.Groups[i].Value;
					result += $" Value: {hit}\r\n";
				}
				//result += $"\r\n";
			}

			textBoxResult.Text = result;
		}



		private void buttonOk_Click(object sender, EventArgs e)
		{
			RegExp = textBoxRegexp.Text;
			Close();
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
