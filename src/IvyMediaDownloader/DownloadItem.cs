using Invary.Utility;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invary.IvyMediaDownloader
{

	/// <summary>
	/// number is index to ImageListStatus
	/// </summary>
	public enum DownloadItemStatus
	{
		Unknown = 0,
		Start = 1,
		Pause = 2,
		Stop = 3,
		Completed = 4,
		Error = 5,

		_MAX_ = Error,
		_COUNT_ = _MAX_ + 1
	}







	public class VideoInfo
	{
		public string strUrl { set; get; } = "";
		public string strTitle { set; get; } = "";
		public string strId { set; get; } = "";
		public TimeSpan? spanDuration { set; get; } = null;
		public string strAuthor { set; get; } = "";
		public string strAuthorId { set; get; } = "";

		public string strThumbnail { set; get; } = "";

	}



	public class SubscribeItem
	{
		[BsonId]
		public ObjectId _id { get; set; }



		public DateTime dtAdd { set; get; } = DateTime.MinValue;
		public DateTime dtLastDownload { set; get; } = DateTime.MinValue;


		public string strAuthor { set; get; } = "";
		public string strAuthorId { set; get; } = "";
		public string strTitle { set; get; } = "";
		public string strUrl { set; get; } = "";
		public string strDescription { set; get; } = "";
		public string strThumbnail { set; get; } = "";

		public int nYtDlpArgs { set; get; } = 0;

		public List<VideoInfo> listInfo { set; get; } = new List<VideoInfo>();

		public string strFolderName { set; get; } = "";
	}





	public class DownloadItem
	{
		[BsonId]
		public ObjectId _id { get; set; }



		public DateTime dtAdd { set; get; } = DateTime.MinValue;

		[BsonIgnore]
		public string strUrl
		{
			get
			{
				if (Info == null)
					return "";
				return Info.strUrl;
			}
		}
		public int nYtDlpArgs { set; get; } = 0;


		public string strFile { set; get; } = "";


		public VideoInfo Info { set; get; } = null;



		public DownloadItemStatus enumStatus { set; get; } = DownloadItemStatus.Unknown;


		public string strSize { set; get; } = "";

		public decimal dProgress { set; get; } = 0;

		[BsonIgnore]
		public string strSpeed { set; get; } = "";

		[BsonIgnore]
		public string strETA { set; get; } = "";


		public string strLogMessage { set; get; } = "";

		public List<string> listSubscribeUrl { set; get; } = new List<string>();

		[BsonIgnore]
		public List<SubscribeItem> listSubscribeItem { set; get; } = new List<SubscribeItem>();




		public string strFolderName { set; get; } = "";





		public string GetFolderName()
		{
			foreach (var item in listSubscribeItem)
			{
				if (item == null)
					continue;
				if (string.IsNullOrEmpty(item.strFolderName))
					continue;

				return item.strFolderName;
			}

			return strFolderName;
		}







		public string GetSafeFolderFullPath()
		{
			string folderBase = Setting.Current.strYtDlpWorkPath;

			string folder = GetFolderName();

			if (folder != null)
				folder = Uty.ReplaceInvalidPathChar(folder, '_');

			if (Directory.Exists(folderBase) == false)
				throw new Exception();

			if (string.IsNullOrEmpty(folder))
				folder = folderBase;
			else
				folder = Path.Combine(folderBase, folder);
			if (Directory.Exists(folder) == false)
				Directory.CreateDirectory(folder);

			return folder;
		}




		[BsonIgnore]
		public ProcessThread _process { set; get; } = null;

	}
}
