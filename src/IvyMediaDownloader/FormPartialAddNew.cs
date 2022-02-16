using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{

	public partial class Form_Main : Form
	{
		void InitFormPartialAddNew()
		{
			listViewVideoInfo.Items.Clear();
			listViewVideoInfo.Columns.Clear();

			listViewVideoInfo.HeaderStyle = ColumnHeaderStyle.Clickable;
			listViewVideoInfo.View = View.Details;
			listViewVideoInfo.MultiSelect = true;
			listViewVideoInfo.GridLines = true;
			listViewVideoInfo.FullRowSelect = true;

			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Status, 40);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_No, 40);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Folder, 100);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Author, 50);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Title, 150);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Url, 300);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Args, 40);
			listViewVideoInfo.Columns.Add(ResourceSet.ListViewVideoInfo_Duration, 100);

			listViewVideoInfo.SmallImageList = imageListStatus;





			buttonAdd.Click += delegate
			{
				if (textBoxVideoId.Text == "")
					return;

				string url = textBoxVideoId.Text;

				if (url.Contains(':') || url.Contains('/') || url.Contains('.'))
				{
					//treat as normal url
				}
				else
				{
					//treat as youtube url
					url = "https://www.youtube.com/watch?v=" + url;
				}

				string folder = textBoxAddNewFolderName.Text;

				try
				{
					using (var dlg = new ProgressForm())
					using (var cancellationTokenSource = new CancellationTokenSource())
					{
						dlg.strName = ResourceSet.Progress_AddNew_Text;
						dlg.strTitle = Application.ProductName;
						dlg.IsAutoProgress = true;
						dlg.EnableCancelButton = true;

						_ = StartGetAddNewAsync(url, folder, delegate
							{
								dlg.Close();
							}, cancellationTokenSource.Token);

						dlg.ShowDialog();
						cancellationTokenSource.Cancel();
					}
				}
				catch(Exception)
				{
				}

				textBoxVideoId.Text = "";
			};


			var menu = new ContextMenuStrip();
			menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripAddNew_AddDownQue, null, OnAddNewMenu_Start));
			menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripAddNew_AddDownQueStartNow, null, OnAddNewMenu_StartNow));
			menu.Items.Add(new ToolStripSeparator());
			menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripAddNew_ClearSelected, null, OnAddNewMenu_ClearSelected));
			menu.Opening += OnAddNewMenu_ContextMenuOpening;
			listViewVideoInfo.ContextMenuStrip = menu;



			Setting.OnSettingChange += delegate
			{
				ReCreateCombobox_SelectArg(comboBoxAddNewArgSelect);
			};
			ReCreateCombobox_SelectArg(comboBoxAddNewArgSelect);
		}


		static void ReCreateCombobox_SelectArg(ComboBox combobox)
		{
			combobox.Items.Clear();
			foreach (var item in Setting.Current.listYtDlpArg)
			{
				combobox.Items.Add(item.strName);
			}
			combobox.SelectedIndex = 0;
		}





		class TagAddNew
		{
			public VideoInfo Info { set; get; } = null;
			public string FolderName { set; get; } = "";
			public int ArgIndex { set; get; } = 0;


			public TagAddNew()
			{
			}

			public TagAddNew(VideoInfo info, string folder, int argindex)
			{
				Info = info;
				FolderName = folder;
				ArgIndex = argindex;
			}


			public static (VideoInfo, string, int) Tag(ListViewItem lvItem)
			{
				var tag = (TagAddNew)lvItem.Tag;

				return (tag.Info, tag.FolderName, tag.ArgIndex);
			}
		}






		void OnAddNewMenu_ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Point p = listViewVideoInfo.PointToClient(Cursor.Position);
			ListViewHitTestInfo info = listViewVideoInfo.HitTest(p);

			if (info.Item == null)
				e.Cancel = true;
		}



		void OnAddNewMenu_Start(object sender, EventArgs e)
		{
			OnAddNewMenu_Start(false);
		}
		void OnAddNewMenu_StartNow(object sender, EventArgs e)
		{
			OnAddNewMenu_Start(true);
		}


		void OnAddNewMenu_Start(bool bStartNow)
		{
			//var mi = (ToolStripMenuItem)sender;

			var items = listViewVideoInfo.SelectedItems;

			List<DownloadItem> listItems = new List<DownloadItem>();

			for (int i = 0; i < items.Count; i++)
			{
				(var info, var folder, var argindex) = TagAddNew.Tag(items[i]);
				if (info == null)
					continue;

				var item = new DownloadItem();

				item.Info = info;
				item.strFolderName = folder;
				//TODO: check arg index? (button add -> remove some arg -> this method,  cause invalid arg index)
				item.nYtDlpArgs = argindex;
				listItems.Add(item);
			}

			////is item already downloaded?
			//{
			//	bool bAsked = false;

			//	for (int i = 0; i < listItems.Count; i++)
			//	{
			//		var find = DBUty.GetItemDB(listItems[i].strUrl);
			//		if (find == null)
			//			continue;

			//		//VideoInfo to latest one
			//		find.Info = listItems[i].Info;

			//		//use DownloadItem in db
			//		listItems[i] = find;
			//		if (find.enumStatus != DownloadItemStatus.Completed)
			//			continue;

			//		//ask re-download once
			//		if (bAsked == false)
			//		{
			//			var ret = MessageBox.Show(ResourceSet.MessageBoxAlreadyExistAskRedownload, ResourceSet.MessageBoxAlreadyExistAskRedownloadTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			//			if (ret != DialogResult.OK)
			//				return;

			//			bAsked = true;
			//		}

			//		//change status for re-download
			//		find.enumStatus = DownloadItemStatus.Unknown;
			//	}
			//}

			//add to donwnload list
			foreach (var item in listItems)
			{
				AddItem(item, true, bStartNow);
			}


			//remove from view
			for (int i = items.Count - 1; i >= 0; i--)
			{
				listViewVideoInfo.Items.Remove(items[i]);
			}
		}



		void OnAddNewMenu_ClearSelected(object sender, EventArgs e)
		{
			var items = listViewVideoInfo.SelectedItems;

			//remove from view
			for (int i = items.Count - 1; i >= 0; i--)
			{
				listViewVideoInfo.Items.Remove(items[i]);
			}
		}






		async Task StartGetAddNewAsync(List<string> listUrl, string strFolderName, EventHandler OnCompleted, CancellationToken ct)
		{
			foreach(var url in listUrl)
			{
				await StartGetAddNewAsync(url, strFolderName, null, ct);
			}
			OnCompleted?.Invoke(this, new EventArgs());
		}


		async Task StartGetAddNewAsync(string url, string strFolderName, EventHandler OnCompleted, CancellationToken ct)
		{
			try
			{
				if (url == null || url == "")
					return;

				var list = await YoutubeUty.GetVideoInfoAsync(url, ct);
				if (list != null && list.Count > 0)
				{
					//add video list, not original url
					foreach (var info in list)
					{
						AddNewInfoToListView(info, strFolderName);
					}
					return;
				}


				//register url
				{
					var info = new VideoInfo();
					info.strUrl = url;
					AddNewInfoToListView(info, strFolderName);
				}
			}
			catch(Exception)
			{
			}
			finally
			{
				OnCompleted?.Invoke(this, new EventArgs());
			}
		}



		void AddNewInfoToListView(VideoInfo info, string strFolderName)
		{
			Invoke((MethodInvoker)delegate
			{
				//dupe check
				for (int i = 0; i < listViewVideoInfo.Items.Count; i++)
				{
					(var check, var folder, var argindex) = TagAddNew.Tag(listViewVideoInfo.Items[i]);
					if (check == null)
						continue;

					//already in view
					if (check.strUrl == info.strUrl)
						return;
				}


				int nIndex = listViewVideoInfo.Items.Count + 1;

				ListViewItem lvItem = new ListViewItem();

				lvItem.Text = "";
				lvItem.SubItems.Add(nIndex.ToString()); //No.
				lvItem.SubItems.Add(strFolderName);
				lvItem.SubItems.Add(info.strAuthor);
				lvItem.SubItems.Add(info.strTitle);
				lvItem.SubItems.Add(info.strUrl);
				lvItem.SubItems.Add(comboBoxAddNewArgSelect.Text);
				lvItem.SubItems.Add((info.spanDuration != null) ? info.spanDuration.ToString() : "");
				lvItem.Tag = new TagAddNew(info, strFolderName, comboBoxAddNewArgSelect.SelectedIndex);

				var item = DBUty.GetItemDB(info.strUrl);
				if (item != null)
				{
					lvItem.ImageIndex = (int)item.enumStatus;
				}

				listViewVideoInfo.Items.Add(lvItem);
			});
		}





	}
}
