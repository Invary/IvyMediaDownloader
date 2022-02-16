using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	public partial class Form_Main : Form
	{
		void InitFormPartialClipboardWatch()
		{
			//Start clipboard monitoring
			Win32.AddClipboardFormatListener(Handle);
			_bClipboardWatchEnable = true;
		}


		void FormClosing_FormPartialClipboardWatch()
		{
			//Stop clipboard monitoring
			Win32.RemoveClipboardFormatListener(Handle);
		}






		/// <summary>
		/// Clipboard change detection
		/// </summary>
		void OnClipboardUpdate()
		{
			List<string> listClipText = ClipboardUty.GetText();

			List<string> listUrl= new List<string>();

			foreach (var text in listClipText)
			{
				if (string.IsNullOrEmpty(text))
					continue;

				{
					var matchs = Regex.Matches(text, Setting.Current.strUrlDetectRegExp);

					foreach (Match match in matchs)
					{
						if (match.Groups.Count < 2)
							continue;

						var url = match.Groups[1].Value;
						if (listUrl.Contains(url))
							continue;

						listUrl.Add(url);
					}
				}
			}

			if (listUrl.Count == 0)
				return;

			try
			{
				using (ProgressForm dlg = new ProgressForm())
				using (CancellationTokenSource cs = new CancellationTokenSource())
				{
					dlg.strName = ResourceSet.Progress_DetectClipboard_Text;
					dlg.strTitle = Application.ProductName;
					dlg.IsAutoProgress = true;
					dlg.EnableCancelButton = true;

					//TODO: output folder is default, at clipboard auto import
					_ = StartGetAddNewAsync(listUrl, "", delegate
					  {
						  dlg.Close();
					  }, cs.Token);

					dlg.ShowDialog();
					cs.Cancel();
				}
			}
			catch (Exception)
			{
			}
		}

		protected bool _bClipboardWatchEnable = true;



		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Win32.WM_CLIPBOARDUPDATE)
			{
				if (Setting.Current.bUseClipboarAutoImport)
				{
					if (_bClipboardWatchEnable)
						OnClipboardUpdate();
				}
				m.Result = IntPtr.Zero;
			}
			else
				base.WndProc(ref m);
		}

	}
}
