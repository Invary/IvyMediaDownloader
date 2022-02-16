using Invary.Utility;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	public partial class Form_Main : Form
	{
		public Form_Main()
		{
			InitializeComponent();

			Text = $"{Application.ProductName} {Setting.strVersion}";

			Load += OnFormLoad;
		}




		void OnFormLoad(object sender, EventArgs e)
		{
			splitContainerStatusbar.Items.Add("ready");



			InitFormPartialAddNew();
			InitFormPartialDownloading();
			InitFormPartialSubscribe();
			InitFormPartialClipboardWatch();


			pictureBoxNewVersionExists.Visible = false;
			pictureBoxNewVersionExistsYtdlp.Visible = false;




			if (Setting.Current.bUpdateCheck)
			{
				TimeSpan span = DateTime.Now - Setting.Current.dtLastUpdateCheck;
				if (span.TotalDays > 1.0)
				{
					//Update the date regardless of the result.
					Setting.Current.dtLastUpdateCheck = DateTime.Now;

					try
					{
						using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
						{
							UpdateStatus.CheckUpdate((sender, e) =>
							{
								if (e.IsNewVersiionExists)
								{
									try
									{
										Invoke((MethodInvoker)delegate
										{
											pictureBoxNewVersionExists.Visible = true;
											pictureBoxNewVersionExists.Click += delegate
											{
												Uty.OpenURL(Setting.strDownloadUrl);
											};
										});
									}
									catch (Exception)
									{
									}
								}
							//TODO: timeout 30sec 
							}, 30, cancellationTokenSource.Token);
						}
					}
					catch (Exception)
					{
					}
				}
			}





			UpdateCheckYtdlp.OnVersionReceived += (sender, e) =>
			{
				if (e.enumType != VerType.Local)
					return;

				Invoke((MethodInvoker)delegate
				{
					labelYtDlpVersion.Text = $"Ver: {e.Version}";
				});
			};

			UpdateCheckYtdlp.OnCheckCompleted += (sender, e) =>
			{
				Invoke((MethodInvoker)delegate
				{
					if (e.OnlineVersion == e.LocalVersion)
					{
						//current yt-dlp version is up to date

					}
					else
					{
						//current yt-dlp version is old

						pictureBoxNewVersionExistsYtdlp.Visible = true;
						pictureBoxNewVersionExistsYtdlp.Click += delegate
						{
							Uty.OpenURL("https://github.com/yt-dlp/yt-dlp/releases");
						};

						//_threadCheckYtDlpUpdate.OnEnd += delegate
						//{
						//	//Check Current Version
						//	CheckYtDlpVersionAsync();
						//};
						//_threadCheckYtDlpUpdate.Start(Setting.Current.strYtDlpExePath, "-U", Setting.Current.strYtDlpWorkPath);
					}
				});
			};




			tabControl1.SelectedTab = tabPage4_Information;


			linkLabelIvyMediaDownloader1.Click += delegate
			{
				Uty.OpenURL(@"https://github.com/Invary/IvyMediaDownloader");
			};
			linkLabelIvyMediaDownloader2.Click += delegate
			{
				Uty.OpenURL(@"https://github.com/Invary/IvyMediaDownloader");
			};


			linkLabelYtDlpDownload.Click += delegate
			{
				Uty.OpenURL(@"https://github.com/yt-dlp/yt-dlp/releases");
			};

			linkLabelDownloadFfmpeg.Click += delegate
			{
				Uty.OpenURL(@"https://ffmpeg.org/download.html");
			};



			FormClosing += delegate
			{
				UpdateCheckYtdlp.Destroy();

				FormClosing_FormPartialClipboardWatch();
				FormClosing_FormPartialDownloading();
			};


			CheckEnv();
		}




		void CheckEnv()
		{
			if (File.Exists(Setting.Current.GetYtDlpExePath()))
			{
				labelYtDlp.Text = ResourceSet.LabelYtDlp_Found;
				labelYtDlpVersion.Text = "";
				labelYtDlp.BackColor = SystemColors.Control;
				pictureBoxCriticalErrorYtDlp.Visible = false;

				//Check local/online version start
				UpdateCheckYtdlp.Check();
			}
			else
			{
				labelYtDlp.Text = ResourceSet.LabelYtDlp_Error;
				labelYtDlp.BackColor = Color.LightPink;
				labelYtDlpVersion.Text = "";
				pictureBoxCriticalErrorYtDlp.Visible = true;
			}

			var ffmpwgexe = Path.GetDirectoryName(Setting.Current.GetYtDlpExePath());
			ffmpwgexe = Path.Combine(ffmpwgexe, "ffmpeg.exe");
			if (File.Exists(ffmpwgexe))
			{
				labelFfmpeg.Text = ResourceSet.LabelFFmpeg_Found;
				labelFfmpeg.BackColor = SystemColors.Control;
				pictureBoxCriticalErrorFFmpeg.Visible = false;
			}
			else
			{
				labelFfmpeg.Text = ResourceSet.LabelFFmpeg_Error;
				labelFfmpeg.BackColor = Color.LightPink;
				pictureBoxCriticalErrorFFmpeg.Visible = true;
			}

		}






		private void buttonOpenSetting_Click(object sender, EventArgs e)
		{
			using (var dlg = new SettingForm())
			{
				dlg.ShowDialog();

				if (dlg.IsYtdlpPathChanged)
				{
					CheckEnv();
				}
			}
		}

		private void buttonFeedback_Click(object sender, EventArgs e)
		{
			using (var dlg = new FeedbackForm())
			{
				dlg.ShowDialog();
			}
		}
	}
}
