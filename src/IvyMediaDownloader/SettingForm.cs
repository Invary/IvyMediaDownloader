using Invary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	public partial class SettingForm : Form
	{
		public SettingForm()
		{
			InitializeComponent();
		}

		private void SettingForm_Load(object sender, EventArgs e)
		{
			labelYtdlpPath.Text = Setting.Current.GetYtDlpExePath();
			labelWorkFolder.Text = Setting.Current.strYtDlpWorkPath;

			checkBoxUpdateCheck.Checked = Setting.Current.bUpdateCheck;
			checkBoxCheckUpdateYtdlp.Checked = Setting.Current.bUpdateCheckYtdlp;


			numericUpDownMaxDownload.Value = Setting.Current.nMaxDownloadCount;
			checkBoxUseClipboarAutoImport.Checked = Setting.Current.bUseClipboarAutoImport;
			textBoxUrlDetectRegExp.Text = Setting.Current.strUrlDetectRegExp;

			{
				listBoxArg.DoubleClick += delegate
				{
					var selected = (ArgInfo)listBoxArg.SelectedItem;
					EditArgInfo(selected);
				};

				for (int i = 0; i < Setting.Current.listYtDlpArg.Count; i++)
				{
					listBoxArg.Items.Add(Setting.Current.listYtDlpArg[i].Clone());
				}

				var menu = new ContextMenuStrip();
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.SettingForm_ArgAddNewMenu_AddNew, null, delegate
				{
					using (var dlg = new EditArgForm())
					{
						dlg.strTitle = ResourceSet.SettingForm_ArgAddNewMenu_AddNew_Title;
						dlg.strName = "";
						dlg.strArg = "%URL%";

						dlg.ShowDialog();
						if (dlg.IsDirty == false)
							return;

						var item = new ArgInfo();
						item.strName = dlg.strName;
						item.strArg = dlg.strArg;
						listBoxArg.Items.Add(item);
					}
				}));

				menu.Items.Add(new ToolStripMenuItem(ResourceSet.SettingForm_ArgAddNewMenu_Edit, null, delegate
				{
					if (listBoxArg.Items.Count <= 1)
						return;
					var selected = (ArgInfo)listBoxArg.SelectedItem;
					EditArgInfo(selected);
				}));
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.SettingForm_ArgAddNewMenu_Remove, null, delegate
				{
					if (listBoxArg.Items.Count <= 1)
						return;
					var selected = listBoxArg.SelectedItem;
					if (selected != null)
						listBoxArg.Items.Remove(selected);
				}));
				listBoxArg.ContextMenuStrip = menu;
			}


			{
				comboBoxLanguage.Items.Add(new LanguageItem("", "(default)"));
				comboBoxLanguage.Items.Add(new LanguageItem("en", "English"));
				comboBoxLanguage.Items.Add(new LanguageItem("ja", "Japanese"));

				comboBoxLanguage.SelectedIndex = 0;
				if (Setting.Current.strLanguage != "")
				{
					foreach(LanguageItem item in comboBoxLanguage.Items)
					{
						if (item.Name != Setting.Current.strLanguage)
							break;

						comboBoxLanguage.SelectedItem = item;
						break;
					}
				}
			}
		}



		class LanguageItem
		{
			public string Name { get; set; } = "";
			public string Display { get; set; } = "";

			public LanguageItem()
			{
			}

			public LanguageItem(string name, string display)
			{
				Name = name;
				Display = display;
			}

			public override string ToString()
			{
				return Display;
			}
		}








		void EditArgInfo(ArgInfo selected)
		{
			if (selected == null)
				return;

			using (var dlg = new EditArgForm())
			{
				dlg.strTitle = ResourceSet.SettingForm_ArgEditMenu_Title;
				dlg.strName = selected.strName;
				dlg.strArg = selected.strArg;

				dlg.ShowDialog();
				if (dlg.IsDirty == false)
					return;

				selected.strName = dlg.strName;
				selected.strArg = dlg.strArg;
				listBoxArg.Items[listBoxArg.SelectedIndex] = selected;
			}
		}




		private void buttonSelectYtdlpExe_Click(object sender, EventArgs e)
		{
			var folder = Path.GetDirectoryName(Setting.Current.GetYtDlpExePath());

			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.FileName = "yt-dlp.exe";
				ofd.InitialDirectory = folder;
				ofd.Filter = "yt-dlp.exe |*.exe";
				ofd.FilterIndex = 0;
				ofd.Title = ResourceSet.SettingForm_SelectYtdlpExe;
				ofd.RestoreDirectory = true;
				ofd.CheckFileExists = true;
				ofd.CheckPathExists = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					labelYtdlpPath.Text = ofd.FileName;
					_bYtdlpPathChange = true;
				}
			}
		}


		private void buttonEditWorkFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dlg = new FolderBrowserDialog())
			{
				dlg.Description = ResourceSet.SettingForm_SelectWorkFolder;
				dlg.InitialDirectory = labelWorkFolder.Text;
				dlg.SelectedPath = labelWorkFolder.Text;
				dlg.ShowNewFolderButton = true;

				var ret = dlg.ShowDialog();
				if (ret != DialogResult.OK)
					return;

				labelWorkFolder.Text = dlg.SelectedPath;
			}
		}




		public bool IsYtdlpPathChanged { private set; get; } = false;
		bool _bYtdlpPathChange = false;



		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (_bYtdlpPathChange)
			{
				Setting.Current.strYtDlpExePath = labelYtdlpPath.Text;
				IsYtdlpPathChanged = true;
			}

			Setting.Current.strYtDlpWorkPath = labelWorkFolder.Text;


			Setting.Current.bUpdateCheck = checkBoxUpdateCheck.Checked;
			Setting.Current.bUpdateCheckYtdlp = checkBoxCheckUpdateYtdlp.Checked;


			Setting.Current.nMaxDownloadCount = (int)numericUpDownMaxDownload.Value;
			Setting.Current.bUseClipboarAutoImport = checkBoxUseClipboarAutoImport.Checked;
			Setting.Current.strUrlDetectRegExp = textBoxUrlDetectRegExp.Text;


			List<ArgInfo> listArg = new List<ArgInfo>();
			foreach(ArgInfo item in listBoxArg.Items)
			{
				if (item == null)
					continue;
				listArg.Add(item);
			}
			Setting.Current.listYtDlpArg = listArg;

			{
				var item = comboBoxLanguage.SelectedItem as LanguageItem;
				Setting.Current.strLanguage = item.Name;
			}


			Setting.FireSettingChange();
			Close();
		}

		private void buttonCheckRegexp_Click(object sender, EventArgs e)
		{
			using (var dlg = new RegExpCheckForm())
			{
				dlg.RegExp = textBoxUrlDetectRegExp.Text;
				dlg.ShowDialog();
				textBoxUrlDetectRegExp.Text = dlg.RegExp;
			}
		}
	}
}
