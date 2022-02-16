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
	public partial class DonationControl : UserControl
	{
		public DonationControl()
		{
			InitializeComponent();

			pictureBoxKofi.Click += delegate
			{
				Uty.OpenURL("https://ko-fi.com/E1E7AC6QH");
			};

			pictureBoxQR1.Click += delegate
			{
				ClipboardUty.SetText("0xCbd4355d13CEA25D87F324E9f35A075adce6507c");
			};
			labelQR1_Address.Click += delegate
			{
				ClipboardUty.SetText("0xCbd4355d13CEA25D87F324E9f35A075adce6507c");
			};

			pictureBoxQR2.Click += delegate
			{
				ClipboardUty.SetText("1FvzxYriyNDdeA12eaUGXTGSJxkzpQdxPd");
			};
			if (labelQR2_Address.Text != "")
			{
				labelQR2_Address.Click += delegate
				{
					ClipboardUty.SetText("1FvzxYriyNDdeA12eaUGXTGSJxkzpQdxPd");
				};
			}

			if (textBoxQR1_Description.Text != "")
				toolTip1.SetToolTip(textBoxQR1_Description, textBoxQR1_Description.Text);
			if (textBoxQR2_Description.Text != "")
				toolTip1.SetToolTip(textBoxQR2_Description, textBoxQR2_Description.Text);

		}
	}
}
