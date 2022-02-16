using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invary.IvyMediaDownloader
{
	class DBUty
	{


		static readonly object _lockDB = new object();

		const string _strDBNameSubscribe = "subscribe";
		const string _strDBNameDownload = "video";



		public static SubscribeItem GetSubscribeItemDB(string url)
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<SubscribeItem>(_strDBNameSubscribe);
					dbColl.EnsureIndex(x => x._id);

					return dbColl.FindOne(x => x.strUrl == url);
				}
			}
		}


		public static List<SubscribeItem> GetSubscribeItemsDB()
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<SubscribeItem>(_strDBNameSubscribe);
					dbColl.EnsureIndex(x => x._id);

					var query = dbColl.Query();
					if (query.Count() == 0)
						return new List<SubscribeItem>();

					return query.OrderBy(x => x.dtAdd)
						.ToList();
				}
			}
		}



		public static void SetSubscribeItemDB(SubscribeItem item)
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<SubscribeItem>(_strDBNameSubscribe);
					dbColl.EnsureIndex(x => x._id);

					SubscribeItem find = null;

					find = dbColl.FindOne(x => x.strUrl == item.strUrl);

					if (find != null)
					{
						if (find._id == item._id)
						{
							dbColl.Update(item);
							return;
						}

						dbColl.Delete(find._id);
					}

					item.dtAdd = DateTime.Now;
					dbColl.Insert(item);
				}
			}
		}


		public static void RemoveSubscribeItemDB(SubscribeItem item)
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<SubscribeItem>(_strDBNameSubscribe);
					dbColl.EnsureIndex(x => x._id);
					dbColl.Delete(item._id);
				}
			}
		}







		public static List<DownloadItem> GetItemsDB()
		{
			List<DownloadItem> list = null;

			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<DownloadItem>(_strDBNameDownload);
					dbColl.EnsureIndex(x => x._id);

					var query = dbColl.Query();
					if (query.Count() == 0)
						return new List<DownloadItem>();
					query = query.Where(x => x.enumStatus != DownloadItemStatus.Completed);
					if (query.Count() == 0)
						return new List<DownloadItem>();

					list =  query.OrderBy(x => x.dtAdd)
						.ToList();
					if (list == null)
						return new List<DownloadItem>();

					foreach (var item in list)
					{
						if (item.enumStatus == DownloadItemStatus.Start || item.enumStatus == DownloadItemStatus.Pause)
							item.enumStatus = DownloadItemStatus.Stop;
					}
				}
			}

			foreach (var item in list)
			{
				SetListSubscribeItemToDownloadItem(item);
			}

			return list;
		}



		public static void RemoveItemDB(DownloadItem item)
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<DownloadItem>(_strDBNameDownload);
					dbColl.EnsureIndex(x => x._id);

					dbColl.Delete(item._id);
				}
			}
		}


		public static DownloadItem GetItemDB(string url)
		{
			DownloadItem item = null;

			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<DownloadItem>(_strDBNameDownload);
					dbColl.EnsureIndex(x => x._id);

					item =  dbColl.FindOne(x => x.Info.strUrl == url);
					if (item == null)
						return null;

					if (item.enumStatus == DownloadItemStatus.Start || item.enumStatus == DownloadItemStatus.Pause)
						item.enumStatus = DownloadItemStatus.Stop;
				}
			}

			SetListSubscribeItemToDownloadItem(item);
			return item;
		}


		static void SetListSubscribeItemToDownloadItem(DownloadItem item)
		{
			if (item == null)
				return;

			if (item.listSubscribeItem.Count != item.listSubscribeUrl.Count)
			{
				item.listSubscribeItem.Clear();
				for (int i = 0; i < item.listSubscribeUrl.Count; i++)
				{
					item.listSubscribeItem.Add(null);
				}
			}

			for (int i = 0; i < item.listSubscribeUrl.Count; i++)
			{
				var subscribe = GetSubscribeItemDB(item.listSubscribeUrl[i]);
				item.listSubscribeItem[i] = subscribe;
			}
		}






		public static void SetItemDB(DownloadItem item)
		{
			lock (_lockDB)
			{
				using (var db = new LiteDatabase(Setting.Current.GetDBPath()))
				{
					var dbColl = db.GetCollection<DownloadItem>(_strDBNameDownload);
					dbColl.EnsureIndex(x => x._id);

					DownloadItem find = null;

					find = dbColl.FindOne(x => x.Info.strUrl == item.Info.strUrl);

					if (find != null)
					{
						if (find._id == item._id)
						{
							dbColl.Update(item);
							return;
						}

						dbColl.Delete(find._id);
					}

					item.dtAdd = DateTime.Now;
					dbColl.Insert(item);
				}
			}
		}





	}
}
