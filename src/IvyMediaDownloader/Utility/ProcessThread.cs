using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invary.Utility
{
	public class OutputLineEventArg : EventArgs
	{
		public string Line { set; get; } = null;

		public OutputLineEventArg()
		{
		}
		public OutputLineEventArg(string line)
		{
			Line = line;
		}
	}




	public class ProcessThread : IDisposable
	{


		public EventHandler OnStart { set; get; } = null;
		public EventHandler OnEnd { set; get; } = null;


		public EventHandler<OutputLineEventArg> OnErrorOutput { set; get; } = null;
		public EventHandler<OutputLineEventArg> OnStandardOutput { set; get; } = null;


		public bool Start(string strExePath, string strArg, string strWorkDir)
		{
			if (IsRunning)
				return false;

			_cs = new CancellationTokenSource();

			_exe = strExePath;
			_arg = strArg;
			_work = strWorkDir;
			_ct = _cs.Token;

			_thread = new Thread(DownloadThread);
			_thread.Start();

			return true;
		}


		public void Abort()
		{
			if (IsRunning == false)
				return;

			_cs.Cancel();
		}

		public void Kill()
		{
			if (IsRunning == false)
				return;

			_cs.Cancel();
			Thread.Sleep(100);
			_thread.Interrupt();
			_thread.Join();
			_thread = null;
		}





		public void Dispose()
		{
			Kill();

			_cs.Dispose();
			_cs = null;
		}









		public bool IsRunning { get; private set; } = false;


		Thread _thread = null;

		public string _arg { get; private set; } = "";
		public string _exe { get; private set; } = "";
		public string _work { get; private set; } = "";
		CancellationToken _ct;
		CancellationTokenSource _cs = null;



		void DownloadThread()
		{
			IsRunning = true;

			try
			{
				OnStart?.Invoke(this, new EventArgs());

				var startInfo = new ProcessStartInfo()
				{
					FileName = _exe,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,
					Arguments = _arg,
					WorkingDirectory = _work,
				};


				using (var process = new Process())
				{
					process.StartInfo = startInfo;


					process.OutputDataReceived += (sender, e) =>
					{
						if (e.Data != null)
							OnStandardOutput?.Invoke(this, new OutputLineEventArg(e.Data));
					};
					process.ErrorDataReceived += (sender, e) =>
					{
						if (e.Data != null)
							OnErrorOutput?.Invoke(this, new OutputLineEventArg(e.Data));
					};

					process.Start();
					process.BeginOutputReadLine();
					process.BeginErrorReadLine();

					//process.WaitForExit();
					while (process.HasExited == false)
					{
						if (_ct.IsCancellationRequested)
						{
							process.Kill();
							return;
						}

						Thread.Sleep(50);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				IsRunning = false;
				OnEnd?.Invoke(this, new EventArgs());
			}
		}

	}
}
