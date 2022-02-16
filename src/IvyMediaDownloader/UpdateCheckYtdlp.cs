using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Invary.IvyMediaDownloader
{
	enum VerType
	{
		Online,
		Local,
	}

	class VersionEventArg : EventArgs
	{
		public string Version { get; set; } = "";
		public VerType enumType { get; set; } = VerType.Local;

		public VersionEventArg()
		{
		}

		public VersionEventArg(string ver, VerType type)
		{
			Version = ver;
			enumType = type;
		}
	}


	class VersionCheckEventArg : EventArgs
	{
		public string OnlineVersion { get; set; } = "";
		public string LocalVersion { get; set; } = "";

		public VersionCheckEventArg()
		{
		}

		public VersionCheckEventArg(string verOnline, string verLocal)
		{
			OnlineVersion = verOnline;
			LocalVersion = verLocal;
		}
	}




	internal class UpdateCheckYtdlp
	{



		public static EventHandler<VersionEventArg> OnVersionReceived = null;
		public static EventHandler<VersionCheckEventArg> OnCheckCompleted = null;


		static ProcessThread _threadCheckYtDlpVersion = null;

		static string _verOnlineYtdlp = "";
		static string _verLocalYtdlp = "";




		public static void Destroy()
		{
			try
			{
				if (_threadCheckYtDlpVersion != null)
				{
					if (_threadCheckYtDlpVersion.IsRunning)
					{
						_threadCheckYtDlpVersion.Kill();
						_threadCheckYtDlpVersion.Dispose();
					}
					_threadCheckYtDlpVersion = null;
				}
			}
			catch (Exception)
			{
			}
		}



		public static void Check()
		{
			Destroy();

			try
			{
				using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
				{
					StartLocalVersionCheck();
					CheckYtDlgVersionAsync(cancellationTokenSource.Token);
				}
			}
			catch (Exception)
			{
			}
		}




		static void StartLocalVersionCheck()
		{
			_threadCheckYtDlpVersion = new ProcessThread();

			_threadCheckYtDlpVersion.OnStandardOutput += (sender, e) =>
			{
				if (string.IsNullOrEmpty(e.Line))
					return;

				_verLocalYtdlp = e.Line;
				OnVersionReceived?.Invoke(null, new VersionEventArg(_verLocalYtdlp, VerType.Local));
				OnReceivedYtdlpVer(VerType.Local, _verLocalYtdlp);
			};

			_threadCheckYtDlpVersion.Start(Setting.Current.GetYtDlpExePath(), "--version", Setting.Current.strYtDlpWorkPath);
		}






		static void CheckYtDlgVersionAsync(CancellationToken ct)
		{
			if (Setting.Current.bUpdateCheckYtdlp == false)
				return;

			TimeSpan span = DateTime.Now - Setting.Current.dtLastUpdateCheckYtdlp;
			if (span.TotalDays < 1.0)
				return;

			//Update the date regardless of the result.
			Setting.Current.dtLastUpdateCheckYtdlp = DateTime.Now;

			Task.Factory.StartNew(async delegate
			{
				//TODO: timeout 30sec 
				var html = await Uty.DownloadTextAsync("https://raw.githubusercontent.com/yt-dlp/yt-dlp/master/yt_dlp/version.py", 30, ct);

				string ver = "";
				{
					var matchs = Regex.Matches(html, "__version__ = '(.+?)'");

					foreach (Match match in matchs)
					{
						if (match.Groups.Count < 2)
							continue;

						ver = match.Groups[1].Value;
						break;
					}
				}
				if (ver != "")
				{
					_verOnlineYtdlp = ver;
					OnVersionReceived?.Invoke(null, new VersionEventArg(ver, VerType.Online));
					OnReceivedYtdlpVer(VerType.Online, ver);
				}
			});


		}







		static void OnReceivedYtdlpVer(VerType type, string version)
		{
			if (_verLocalYtdlp == "" || _verOnlineYtdlp == "")
				return;

			OnCheckCompleted?.Invoke(null, new VersionCheckEventArg(_verOnlineYtdlp, _verLocalYtdlp));
		}





	}
}
