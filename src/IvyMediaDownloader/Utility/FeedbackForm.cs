using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.Utility
{
	public partial class FeedbackForm : Form
	{
		public string strProductName { set; get; } = Application.ProductName;
		public string strVersion { set; get; } = "";


		public FeedbackForm()
		{
			InitializeComponent();

			textBoxMessage.TextChanged += delegate
			{
				buttonSend.Enabled = (textBoxMessage.Text != "");
			};
			buttonSend.Enabled = false;
		}



		private void buttonSend_Click(object sender, EventArgs e)
		{
			var message = textBoxMessage.Text;
			if (message == "")
				return;

			var ret = MessageBox.Show("Send?", "Feedback", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (ret != DialogResult.OK)
				return;

			var payload = new Dictionary<string, string>
			{
				{ "soft", strProductName },
				{ "ver", strVersion },
				{ "data", message }
			};

			//TODO: auto feedback url
			string url = "http://example.com/post.php";




			try
			{
				bool bSendSuccess = false;

				using (ProgressForm dlg = new ProgressForm())
				using (CancellationTokenSource cs = new CancellationTokenSource())
				{
					//TODO: add resource string
					dlg.strName = "Sending feedback...";
					dlg.strTitle = Application.ProductName;
					dlg.IsAutoProgress = true;
					dlg.EnableCancelButton = true;

					_ = Uty.SendPostAsync(url, payload, (sender, e) =>
					{
						bSendSuccess = e.Success;
						dlg.Close();
					}, cs.Token);

					dlg.ShowDialog();
					cs.Cancel();
				}

				if (bSendSuccess)
				{
					textBoxMessage.Text = "";
					Close();
				}

				//TODO: error or cancel

				//else
				//{
				//	//TODO: resource string
				//	MessageBox.Show("Failed to send feedback", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}
			}
			catch (Exception)
			{
			}
		}






		private void linkLabelUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//TODO: manual feedback url
			Uty.OpenURL("http://example.com/post.php");
		}
	}
}
