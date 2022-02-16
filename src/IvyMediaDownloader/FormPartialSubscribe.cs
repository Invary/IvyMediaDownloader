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
		void InitFormPartialSubscribe()
		{

			{
				listViewSubscribePrep.HeaderStyle = ColumnHeaderStyle.Clickable;
				listViewSubscribePrep.View = View.Details;
				listViewSubscribePrep.MultiSelect = true;
				listViewSubscribePrep.GridLines = true;
				listViewSubscribePrep.FullRowSelect = true;

				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Status, 40);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_No, 40);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_FolderName, 100);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Author, 50);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Title, 150);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Url, 300);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Args, 40);
				listViewSubscribePrep.Columns.Add(ResourceSet.ListViewSubscribePrepColumns_Description, 100);

				//listView1.SmallImageList = imageListStatus;


				var menu = new ContextMenuStrip();
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripSubscribedPrep_Subscribe, null, OnSubscribePrepMenu_Subscribe, "Subscribe"));
				menu.Items.Add(new ToolStripSeparator());
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripSubscribedPrep_ClearSelected, null, OnSubscribePrepMenu_ClearSelected, "ClearSelected"));
				menu.Opening += OnSubscribePrepMenu_ContextMenuOpening;
				listViewSubscribePrep.ContextMenuStrip = menu;
			}

			{
				listViewSubscribed.HeaderStyle = ColumnHeaderStyle.Clickable;
				listViewSubscribed.View = View.Details;
				listViewSubscribed.MultiSelect = true;
				listViewSubscribed.GridLines = true;
				listViewSubscribed.FullRowSelect = true;

				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Status, 40);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_No, 40);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_FolderName, 100);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Author, 50);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Title, 150);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Url, 300);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Args, 40);
				listViewSubscribed.Columns.Add(ResourceSet.ListViewSubscribedColumns_Description, 100);

				//listView2.SmallImageList = imageListStatus;


				var menu = new ContextMenuStrip();
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripSubscribed_AddDownQue, null, OnSubscribeMenu_AddToDownloadQue));
				menu.Items.Add(new ToolStripSeparator());
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripMain_EditFolderName, null, OnSubscribeMenu_EditFolderName));
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripMain_CopyUrl, null, OnSubscribeMenu_CopyURL));
				menu.Items.Add(new ToolStripSeparator());
				menu.Items.Add(new ToolStripMenuItem(ResourceSet.ContextMenuStripSubscribed_Unsubscribe, null, OnSubscribeMenu_Unsubscribe));
				menu.Opening += OnSubscribeMenu_ContextMenuOpening;
				listViewSubscribed.ContextMenuStrip = menu;

				//owner draw for modify bg color
				{
					listViewSubscribed.OwnerDraw = true;

					listViewSubscribed.DrawColumnHeader += (sender, e) =>
					{
						e.DrawDefault = true;
					};
					listViewSubscribed.DrawSubItem += (sender, e) =>
					{
						e.DrawDefault = true;
					};
				}

			}


			{
				var items = DBUty.GetSubscribeItemsDB();

				foreach (var item in items)
				{
					AddNewSubscribeInfoToListView(item);
				}
			}


			splitContainerSubscribeInner.Panel1.ClientSizeChanged += (sender, e) =>
			{
				OnClientSizeChanged_splitContainerSubscribeInnerPanel1(sender, e);
			};

			OnClientSizeChanged_splitContainerSubscribeInnerPanel1(null, null);



			Setting.OnSettingChange += delegate
			{
				ReCreateCombobox_SelectArg(comboBoxSubscribeArgSelect);
			};
			ReCreateCombobox_SelectArg(comboBoxSubscribeArgSelect);
		}




		void OnClientSizeChanged_splitContainerSubscribeInnerPanel1(object sender, EventArgs e)
		{
			int width = splitContainerSubscribeInner.Panel1.Width;
			int height = splitContainerSubscribeInner.Panel1.Height - listViewSubscribePrep.Location.Y - 70;
			listViewSubscribePrep.Size = new Size(width, height);

			int x = splitContainerSubscribeInner.Panel1.Width / 2 - buttonAddToSubscribe.Width / 2; ;
			int y = listViewSubscribePrep.Location.Y + listViewSubscribePrep.Height + 10;
			buttonAddToSubscribe.Location = new Point(x, y);
		}








		void OnSubscribeMenu_ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Point p = listViewSubscribed.PointToClient(Cursor.Position);
			ListViewHitTestInfo info = listViewSubscribed.HitTest(p);

			if (info.Item == null)
				e.Cancel = true;
		}


		void OnSubscribeMenu_Unsubscribe(object sender, EventArgs e)
		{
			var items = listViewSubscribed.SelectedItems;

			if (items.Count == 0)
				return;

			var ret = MessageBox.Show(ResourceSet.MessageBoxAskDelete, ResourceSet.MessageBoxAskDeleteTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			if (ret != DialogResult.OK)
				return;

			for (int i = items.Count - 1; i >= 0; i--)
			{
				//DBから削除
				var item = (SubscribeItem)items[i].Tag;
				if (item != null)
					DBUty.RemoveSubscribeItemDB(item);

				//リストビューから削除
				listViewSubscribed.Items.Remove(items[i]);
			}
		}




		void OnSubscribeMenu_CopyURL(object sender, EventArgs e)
		{
			var items = listViewSubscribed.SelectedItems;

			string text = "";

			for (int i = 0; i < items.Count; i++)
			{
				var item = (SubscribeItem)items[i].Tag;
				if (item == null)
					continue;

				if (text.Length >= 0)
					text += "\n";

				text += item.strUrl;
			}

			_bClipboardWatchEnable = false;
			ClipboardUty.SetText(text);
			_bClipboardWatchEnable = true;
		}




		void OnSubscribeMenu_EditFolderName(object sender, EventArgs e)
		{
			var items = listViewSubscribed.SelectedItems;

			List<SubscribeItem> list = new List<SubscribeItem>();

			for (int i = 0; i < items.Count; i++)
			{
				var item = (SubscribeItem)items[i].Tag;
				if (item == null)
					continue;

				list.Add(item);
			}

			using(var dlg = new InputTextForm())
			{
				dlg.strTitle = ResourceSet.EditFolderNameForm_Title;
				dlg.strName = ResourceSet.EditFolderNameForm_Name;
				dlg.strDescription = ResourceSet.EditFolderNameForm_Description;
				dlg.strText = list[0].strFolderName;

				dlg.ShowDialog();
				if (dlg.IsChange == false)
					return;

				foreach (var item in list)
				{
					if (item.strFolderName == dlg.strText)
						continue;

					item.strFolderName = dlg.strText;
					DBUty.SetSubscribeItemDB(item);
				}

				for (int i = 0; i < items.Count; i++)
				{
					items[i].SubItems[2].Text = dlg.strText;
				}

				//TODO: change displayed folder name in download listview
			}

		}




		/// <summary>
		/// add to download que, not prep
		/// </summary>
		void OnSubscribeMenu_AddToDownloadQue(object sender, EventArgs e)
		{
			var items = listViewSubscribed.SelectedItems;

			List<SubscribeItem> list = new List<SubscribeItem>();

			for (int i = 0; i < items.Count; i++)
			{
				var item = (SubscribeItem)items[i].Tag;
				if (item == null)
					continue;

				list.Add(item);
			}



			try
			{
				using(ProgressForm dlg = new ProgressForm())
				using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
				{
					dlg.strName = ResourceSet.Progress_Subscribe_Text;
					dlg.strTitle = Application.ProductName;
					dlg.IsAutoProgress = true;
					dlg.EnableCancelButton = true;

					_ = StartGetInfoAndAddAsync(list, delegate
					  {
						  dlg.Close();
					  }, cancellationTokenSource.Token);

					dlg.ShowDialog();
					cancellationTokenSource.Cancel();
				}
			}
			catch (Exception)
			{
			}
		}


		async Task StartGetInfoAndAddAsync(List<SubscribeItem> listItems, EventHandler OnCompleted, CancellationToken ct)
		{
			try
			{
				foreach (var item in listItems)
				{
					if (ct.IsCancellationRequested)
						return;

					//get video list in channel
					var list = await YoutubeUty.GetVideoInfoAsync(item.strUrl, ct);

					if (list == null)
					{
						// not youtube?

						var dlItem = DBUty.GetItemDB(item.strUrl);

						if (dlItem == null)
						{
							VideoInfo info = new VideoInfo();
							info.strUrl = item.strUrl;

							dlItem = new DownloadItem();
							dlItem.Info = info;
						}
						if (dlItem.enumStatus == DownloadItemStatus.Completed)
							dlItem.enumStatus = DownloadItemStatus.Unknown;
						dlItem.nYtDlpArgs = item.nYtDlpArgs;

						if (dlItem.listSubscribeUrl.Contains(item.strUrl) == false)
							dlItem.listSubscribeUrl.Add(item.strUrl);
						if (dlItem.listSubscribeItem.Contains(item) == false)
							dlItem.listSubscribeItem.Add(item);

						//add to download que
						AddItem(dlItem, true);
						return;
					}

					//
					//youtube with video list
					//
					if (list.Count == 0)
						return;
					if (item.listInfo.Count == 0)
					{
						item.listInfo.AddRange(list);
					}
					else
					{
						foreach (var info in list)
						{
							if (IsContain(item.listInfo, info.strUrl))
								continue;

							item.listInfo.Add(info);
						}
					}

					DBUty.SetSubscribeItemDB(item);

					foreach (var info in list)
					{
						var dlItem = DBUty.GetItemDB(info.strUrl);
						if (dlItem != null && dlItem.enumStatus == DownloadItemStatus.Completed)
							continue;

						if (dlItem == null)
						{
							dlItem = new DownloadItem();
							dlItem.Info = info;
						}

						if (dlItem.listSubscribeUrl.Contains(item.strUrl) == false)
							dlItem.listSubscribeUrl.Add(item.strUrl);
						if (dlItem.listSubscribeItem.Contains(item) == false)
							dlItem.listSubscribeItem.Add(item);
						dlItem.nYtDlpArgs = item.nYtDlpArgs;


						//add to download que
						AddItem(dlItem, true);
					}

				}
			}
			catch (Exception)
			{
			}
			finally
			{
				OnCompleted?.Invoke(this, new EventArgs());
			}
		}

		static bool IsContain(List<VideoInfo> listInfo, string url)
		{
			foreach(var item in listInfo)
			{
				if (item.strUrl == url)
					return true;
			}

			return false;
		}






		void OnSubscribePrepMenu_ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Point p = listViewSubscribePrep.PointToClient(Cursor.Position);
			ListViewHitTestInfo info = listViewSubscribePrep.HitTest(p);

			if (info.Item == null)
				e.Cancel = true;
		}


		void OnSubscribePrepMenu_ClearSelected(object sender, EventArgs e)
		{
			var items = listViewSubscribePrep.SelectedItems;

			//remove from view
			for (int i = items.Count - 1; i >= 0; i--)
			{
				listViewSubscribePrep.Items.Remove(items[i]);
			}
		}



		void OnSubscribePrepMenu_Subscribe(object sender, EventArgs e)
		{
			var items = listViewSubscribePrep.SelectedItems;

			for (int i = 0; i < items.Count; i++)
			{
				var item = (SubscribeItem)listViewSubscribePrep.Items[i].Tag;
				if (item == null)
					continue;

				DBUty.SetSubscribeItemDB(item);
				AddNewSubscribeInfoToListView(item);
			}
		}



		void AddNewSubscribeInfoToListView(SubscribeItem info)
		{
			int nIndex = listViewSubscribed.Items.Count + 1;

			ListViewItem lvItem = new ListViewItem();

			lvItem.Text = "";
			lvItem.SubItems.Add(nIndex.ToString()); //No.
			lvItem.SubItems.Add(info.strFolderName);
			lvItem.SubItems.Add(info.strAuthor);
			lvItem.SubItems.Add(info.strTitle);
			lvItem.SubItems.Add(info.strUrl);
			lvItem.SubItems.Add(Setting.GetArgsName(info.nYtDlpArgs));
			lvItem.SubItems.Add(info.strDescription);
			lvItem.Tag = info;

			//change bg color
			foreach (ListViewItem.ListViewSubItem subitem in lvItem.SubItems)
			{
				subitem.BackColor = Setting.Current.colorSubscribeBackGround;
			}


			listViewSubscribed.Items.Add(lvItem);
		}

























		/// <summary>
		/// add to prep
		/// </summary>
		void buttonSubscribeAdd_Click(object sender, EventArgs e)
		{
			var url = textBoxSubscribe.Text;
			textBoxSubscribe.Text = "";

			var folder = textBoxSubscribeFolder.Text;
			textBoxSubscribeFolder.Text = "";

			var argindex = comboBoxSubscribeArgSelect.SelectedIndex;

			try
			{
				using (ProgressForm dlg = new ProgressForm())
				using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
				{
					dlg.strName = ResourceSet.Progress_SubscribePrep_Text;
					dlg.strTitle = Application.ProductName;
					dlg.IsAutoProgress = true;
					dlg.EnableCancelButton = true;

					_ = GetAndAddPrepChannelInfoAsync(url, folder, argindex, delegate
					{
						dlg.Close();
					}, cancellationTokenSource.Token);

					dlg.ShowDialog();
					cancellationTokenSource.Cancel();
				}
			}
			catch (Exception)
			{
			}
		}




		async Task GetAndAddPrepChannelInfoAsync(string url, string folder, int argindex, EventHandler OnCompleted, CancellationToken ct)
		{
			try
			{
				if (url == "")
					return;

				var item = await YoutubeUty.GetChannelInfoAsync(url, ct);
				if (item == null)
				{
					item = new SubscribeItem();
					item.strUrl = url;
					item.strTitle = url;
				}
				if (string.IsNullOrEmpty(folder) == false)
					item.strFolderName = folder;
				item.nYtDlpArgs = argindex;

				Invoke((MethodInvoker)delegate
				{
					AddNewSubscribeInfoToPrepListView(item, argindex);
				});
			}
			catch(Exception)
			{
			}
			finally
			{
				OnCompleted?.Invoke(this, new EventArgs());
			}
		}





		void AddNewSubscribeInfoToPrepListView(SubscribeItem info, int argindex)
		{
			//check dupe
			for (int i = 0; i < listViewSubscribePrep.Items.Count; i++)
			{
				var check = (SubscribeItem)listViewSubscribePrep.Items[i].Tag;
				if (check == null)
					continue;

				//already exists in view
				if (check.strUrl == info.strUrl)
					return;
			}
			{
				//already exists in DB
				var item = DBUty.GetSubscribeItemDB(info.strUrl);
				if (item != null)
					return;
			}



			int nIndex = listViewSubscribePrep.Items.Count + 1;

			ListViewItem lvItem = new ListViewItem();

			lvItem.Text = "";
			lvItem.SubItems.Add(nIndex.ToString()); //No.
			lvItem.SubItems.Add(info.strFolderName);
			lvItem.SubItems.Add(info.strAuthor);
			lvItem.SubItems.Add(info.strTitle);
			lvItem.SubItems.Add(info.strUrl);
			lvItem.SubItems.Add(Setting.GetArgsName(info.nYtDlpArgs));
			lvItem.SubItems.Add(info.strDescription);
			lvItem.Tag = info;

			//var item = DBUty.GetItemDB(info.strUrl);
			//if (item != null)
			//{
			//	lvItem.ImageIndex = (int)item.enumStatus;
			//}

			listViewSubscribePrep.Items.Add(lvItem);
		}







	}
}
