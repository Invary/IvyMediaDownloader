using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	static class Program
	{



		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				//check multi execution
				var check = CheckDupeExecute.StartAndCheck(Application.ProductName);
				if (check == false)
				{
					MessageBox.Show(ResourceSet.MessageBox_DupeExecute.Replace("%APP%", Application.ProductName), ResourceSet.MessageBox_DupeExecuteTitle.Replace("%APP%", Application.ProductName), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				//parse command line options
				{
					var ret = CheckAndApplyCommandLineArgs();
					if (ret == false)
					{
						var message = ResourceSet.InvalidCommandlineOption + "\n\n";

						var options = GetCommandLineOptions();
						foreach (var option in options)
						{
							message += option.Help += "\n";
						}

						MessageBox.Show(message, ResourceSet.InvalidCommandlineOption, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}

				//load settings
				{
					Setting.Current = Setting.Load();
					if (Setting.Current == null)
					{
						var ret = MessageBox.Show(ResourceSet.MessageBoxInitSetting_Text, ResourceSet.MessageBoxInitSetting_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (ret != DialogResult.Yes)
							return;

						Setting.Current = Setting.CreateNewSetting();

						var saved = Setting.Current.Save();
						if (saved == false)
						{
							//TODO: resource string
							MessageBox.Show("Initialize setting file failed.\nSetting file cannot save to folder.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
				}

				//Switch UI language
				SwitchLanguage();

				try
				{
					// To customize application configuration such as set high DPI settings or default font,
					// see https://aka.ms/applicationconfiguration.
					ApplicationConfiguration.Initialize();

					//Application.SetHighDpiMode(HighDpiMode.SystemAware);
					//Application.EnableVisualStyles();
					//Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Form_Main());
				}
				catch
				{
				}
				finally
				{
					Setting.Current.Save();
				}
			}
			catch
			{
			}
			finally
			{
				CheckDupeExecute.Release();
			}
		}





		static bool CheckAndApplyCommandLineArgs()
		{
			var options = GetCommandLineOptions();

			var ret = CommandLineOption.AnalyzeCommandLine(options);
			if (ret == false)
				return false;

			foreach(var option in options)
			{
				option.Apply();
			}

			return true;
		}





		static List<CommandLineOption> GetCommandLineOptions()
		{
			List<CommandLineOption> options = new List<CommandLineOption>();

			options.Add(new CommandLineOption("--settingfolder", 1, true) 
			{
				Help = "--settingfolder {folder path}",
				OnApply = (option) =>
				{
					//change setting file folder
					Setting.SetSettingFileFolder(option.Values[0]);
				}
			});


			return options;
		}







		static void SwitchLanguage()
		{
			if (string.IsNullOrEmpty(Setting.Current.strLanguage) == false)
			{
				try
				{
					Thread.CurrentThread.CurrentUICulture = new CultureInfo(Setting.Current.strLanguage, false);
				}
				catch (Exception)
				{
					//language not found, set to default
					Setting.Current.strLanguage = "";
				}
			}
		}


	}
}
