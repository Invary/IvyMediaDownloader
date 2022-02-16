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
	public partial class ProgressForm : Form
	{
		public bool EnableCancelButton { set; get; } = false;
		public string strTitle { set; get; } = "";
		public string strName { set; get; } = "";

		public int nMin { set; get; } = 0;
		public int nMax { set; get; } = 100;


		public bool IsAutoProgress { set; get; } = false;
		Timer _timer = null;



		public ProgressForm()
		{
			InitializeComponent();

			Load += OnFormLoad;
		}

		void OnFormLoad(object sender, EventArgs e)
		{
			Text = strTitle;
			labelText.Text = strName;
			buttonCancel.Enabled = EnableCancelButton;

			progressBar.Minimum = nMin;
			progressBar.Maximum = nMax;

			if (IsAutoProgress)
			{
				Random rnd = new Random();

				_timer = new Timer();
				_timer.Interval = 100;
				_timer.Tick += delegate
				{
					try
					{
						int n = (nMax - nMin) / 10 + nMin;
						if (n < 0)
							n = 1;
						int value = progressBar.Value + rnd.Next(nMin, n);
						if (value > nMax)
							value = nMin;
						progressBar.Value = value;
					}
					catch(Exception)
					{
					}
				};

				FormClosing += delegate
				{
					if (_timer == null)
						return;
					_timer.Stop();
					_timer.Dispose();
					_timer = null;
				};

				_timer.Start();
			}

			if (_bCanceled)
				Close();
		}


		bool _bCanceled = false;

		public new void Close()
		{
			_bCanceled = true;

			try
			{
				Invoke((MethodInvoker)delegate
				{
					base.Close();
				});
			}
			catch (Exception)
			{
			}
		}



		public new DialogResult ShowDialog()
		{
			if (_bCanceled)
				return DialogResult.Cancel;
			var ret = base.ShowDialog();
			if (_bCanceled)
				return DialogResult.Cancel;
			return ret;
		}
		public new DialogResult ShowDialog(IWin32Window parent)
		{
			if (_bCanceled)
				return DialogResult.Cancel;
			var ret =  base.ShowDialog(parent);
			if (_bCanceled)
				return DialogResult.Cancel;
			return ret;
		}





		public void SetProgress(int value)
		{
			if (IsAutoProgress)
				return;

			try
			{
				Invoke((MethodInvoker)delegate
				{
					progressBar.Value = value;
				});
			}
			catch(Exception)
			{
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
