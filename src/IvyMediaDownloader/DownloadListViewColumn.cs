using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invary.IvyMediaDownloader
{
	/*
	 * DownloadListViewColumnBase
	 * 
	 * DownloadListViewColumn_Status
	 * DownloadListViewColumn_Title
	 * DownloadListViewColumn_Url
	 * DownloadListViewColumn_Arg
	 * DownloadListViewColumn_Size
	 * DownloadListViewColumn_Speed
	 * DownloadListViewColumn_Progress
	 * DownloadListViewColumn_ETA
	 * DownloadListViewColumn_Folder
	 */


	abstract class DownloadListViewColumnBase
	{
		public string Name { get; set; } = "";
		public int Width { get; set; } = 50;
		public bool IsSupportSort { get; set; } = true;


		abstract public string GetValue(DownloadItem item);

		abstract public int Compare(DownloadItem a, DownloadItem b);
	}




	class DownloadListViewColumn_Status : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Status()
		{
			Name = ResourceSet.ListViewDownloadColumns_Status;
			Width = 30;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			return "";
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			if ((int)a.enumStatus > (int)b.enumStatus)
				return 1;
			if ((int)a.enumStatus < (int)b.enumStatus)
				return -1;
			return 0;
		}
	}




	class DownloadListViewColumn_Title : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Title()
		{
			Name = ResourceSet.ListViewDownloadColumns_Title;
			Width = 150;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			if (string.IsNullOrEmpty(item.strFile) == false)
				return item.strFile;

			return item.Info.strTitle;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}





	class DownloadListViewColumn_Url : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Url()
		{
			Name = ResourceSet.ListViewDownloadColumns_Url;
			Width = 300;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			return item.strUrl;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}

	class DownloadListViewColumn_Arg : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Arg()
		{
			Name = ResourceSet.ListViewDownloadColumns_Arg;
			Width = 50;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			if (item.nYtDlpArgs >= Setting.Current.listYtDlpArg.Count)
				return Setting.Current.listYtDlpArg[0].strName;
			return Setting.Current.listYtDlpArg[item.nYtDlpArgs].strName;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}



	class DownloadListViewColumn_Size : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Size()
		{
			Name = ResourceSet.ListViewDownloadColumns_Size;
			Width = 100;
			IsSupportSort = false;
		}


		public override string GetValue(DownloadItem item)
		{
			return item.strSize;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			//TODO: size compare is wrong! "400KB" > "1MB"
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}


	class DownloadListViewColumn_Speed : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Speed()
		{
			Name = ResourceSet.ListViewDownloadColumns_Speed;
			Width = 100;
			IsSupportSort = false;
		}


		public override string GetValue(DownloadItem item)
		{
			if (item.strSpeed == "")
				return "-";
			return item.strSpeed;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			//TODO: Speed compare is wrong! "400Kbps" > "1Mbps"
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}




	class DownloadListViewColumn_Progress : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Progress()
		{
			Name = ResourceSet.ListViewDownloadColumns_Progress;
			Width = 50;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			return $"{item.dProgress,4}%";
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			if (a.dProgress > b.dProgress)
				return 1;
			if (a.dProgress < b.dProgress)
				return -1;
			return 0;
		}
	}




	class DownloadListViewColumn_ETA : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_ETA()
		{
			Name = ResourceSet.ListViewDownloadColumns_ETA;
			Width = 100;
			IsSupportSort = false;
		}


		public override string GetValue(DownloadItem item)
		{
			if (item.strETA == "")
				return "-";
			return item.strETA;
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			//TODO: ETA compare is wrong! "01:12:34" > "03:12"
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}




	class DownloadListViewColumn_Folder : DownloadListViewColumnBase
	{
		public DownloadListViewColumn_Folder()
		{
			Name = ResourceSet.ListViewDownloadColumns_Folder;
			Width = 100;
			IsSupportSort = true;
		}


		public override string GetValue(DownloadItem item)
		{
			return item.GetFolderName();
		}

		public override int Compare(DownloadItem a, DownloadItem b)
		{
			string str1 = GetValue(a);
			string str2 = GetValue(b);
			return string.Compare(str1, str2);
		}
	}




}
