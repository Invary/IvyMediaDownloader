using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Invary.Utility;

namespace Invary.IvyMediaDownloader
{
	public partial class Form_Main : Form
	{
		readonly List<DownloadItem> _listItem = new List<DownloadItem>();
		readonly List<RemoveInfo> _listRemoveItem = new List<RemoveInfo>();
		readonly List<DownloadListViewColumnBase> _listColumn = new List<DownloadListViewColumnBase>();
		readonly List<MainMenuBase> _listMenu = new List<MainMenuBase>();
		readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();


		class RemoveInfo
		{
			public DownloadItem Item { set; get; } = null;
			public int TimerCount { set; get; } = 0;

			public RemoveInfo()
			{
			}
			public RemoveInfo(DownloadItem item)
			{
				Item = item;
			}
		}


		void InitFormPartialDownloading()
		{
			listViewDownload.Items.Clear();
			listViewDownload.Columns.Clear();

			listViewDownload.HeaderStyle = ColumnHeaderStyle.Clickable;
			listViewDownload.View = View.Details;
			listViewDownload.MultiSelect = true;
			listViewDownload.GridLines = true;
			listViewDownload.FullRowSelect = true;

			_listColumn.Add(new DownloadListViewColumn_Status());
			_listColumn.Add(new DownloadListViewColumn_Folder());
			_listColumn.Add(new DownloadListViewColumn_Title());
			_listColumn.Add(new DownloadListViewColumn_Url());
			_listColumn.Add(new DownloadListViewColumn_Arg());
			_listColumn.Add(new DownloadListViewColumn_Size());
			_listColumn.Add(new DownloadListViewColumn_Speed());
			_listColumn.Add(new DownloadListViewColumn_Progress());
			_listColumn.Add(new DownloadListViewColumn_ETA());

			foreach(var item in _listColumn)
			{
				listViewDownload.Columns.Add(item.Name, item.Width);
			}


			listViewDownload.SmallImageList = imageListStatus;

			listViewDownload.VirtualMode = true;

			listViewDownload.RetrieveVirtualItem += listViewDownload_RetrieveVirtualItem;

			

			listViewDownload.ColumnClick += (sender, e) =>
			{
				if (e.Column >= _listColumn.Count)
					return;
				if (_listColumn[e.Column].IsSupportSort == false)
					return;

				var bAscend = (bool?)listViewDownload.Columns[e.Column].Tag;
				if (bAscend == null)
					bAscend = true;
				listViewDownload.Columns[e.Column].Tag = !bAscend;

				_listItem.Sort((a, b) =>
				{
					if (bAscend == true)
						return _listColumn[e.Column].Compare(a, b);
					else
						return _listColumn[e.Column].Compare(b, a);
				});
				listViewDownload.Invalidate();
			};


			listViewDownload.SelectedIndexChanged += delegate
			{
				RefreshTextBox();
			};

			textBoxLog.ScrollBars = ScrollBars.Vertical;



			var items = DBUty.GetItemsDB();

			foreach (var item in items)
			{
				if (item.strLogMessage != "")
					item.strLogMessage += "\r\n";

				if (item.enumStatus == DownloadItemStatus.Start)
					item.enumStatus = DownloadItemStatus.Stop;
				if (item.enumStatus == DownloadItemStatus.Pause)
					item.enumStatus = DownloadItemStatus.Stop;
				if (item.dProgress >= 100m)
					item.enumStatus = DownloadItemStatus.Completed;

				AddItem(item, false);
			}



			_listMenu.Add(new MainMenu_StartSelected(OnMenu_StartSelected));
			_listMenu.Add(new MainMenu_StartAll(OnMenu_StartAll));
			_listMenu.Add(new MainMenu_StopSelected(OnMenu_StopSelected));
			_listMenu.Add(new MainMenu_StopAll(OnMenu_StopAll));
			_listMenu.Add(new MainMenu_SetCompletedFlag(OnMenu_SetCompletedFlag));
			_listMenu.Add(new MainMenu_ResetCompletedFlag(OnMenu_ResetCompletedFlag));
			_listMenu.Add(new MainMenu_ClearCompleted(OnMenu_ClearCompleted));
			_listMenu.Add(new MainMenu_DeleteDB(OnMenu_DeleteFromDB));
			_listMenu.Add(new MainMenu_DeleteView(OnMenu_DeleteFromView));
			_listMenu.Add(new MainMenu_KillSelected(OnMenu_Kill));
			_listMenu.Add(new MainMenu_SelectAll(OnMenu_SelectAll));
			_listMenu.Add(new MainMenu_EditFolderName(OnMenu_EditFolderName));
			_listMenu.Add(new MainMenu_CopyUrl(OnMenu_CopyUrl));
			
			foreach (var item in _listMenu)
			{
				var menu = item.CreateForToolbar();
				toolStripDownloadTab.Items.Add(menu);
			}



			listViewDownload.SelectedIndexChanged += delegate
			{
				OnMenuOpening(toolStripDownloadTab.Items);
			};

			listViewDownload.KeyUp += (sender, e) =>
			{
				if (e.KeyCode == Keys.Delete)
				{
					OnDeleteKey();
					return;
				}

				if (e.KeyCode == Keys.Home)
				{
					if (listViewDownload.Items.Count > 0)
						listViewDownload.EnsureVisible(0);
					return;
				}
				if (e.KeyCode == Keys.End)
				{
					if (listViewDownload.Items.Count > 0)
						listViewDownload.EnsureVisible(listViewDownload.Items.Count - 1);
					return;
				}

				if (e.KeyCode == Keys.A && e.Control)
				{
					if (listViewDownload.Items.Count > 0)
						OnMenu_SelectAll(this, new EventArgs());
					return;
				}
			};




			Setting.OnSettingChange += delegate
			{
				CreateContextMenu_Main();
			};
			CreateContextMenu_Main();


			//owner draw for changing bg color
			{
				listViewDownload.OwnerDraw = true;

				listViewDownload.DrawColumnHeader += (sender, e) =>
				{
					e.DrawDefault = true;
				};
				listViewDownload.DrawSubItem += (sender, e) =>
				{
					e.DrawDefault = true;

					//need to draw icon image
					//if (e.Item.Selected)
					//	e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, e.Bounds);
					//else
					//	e.DrawBackground();
					//e.DrawText();
				};
			}


			splitContainerMain.Panel1.ClientSizeChanged += (sender, e) =>
			{
				OnClientSizeChanged_splitContainerMainPanel1(sender, e);
			};

			OnClientSizeChanged_splitContainerMainPanel1(null, null);


			_timer.Interval = 1000;
			_timer.Tick += delegate
			{
				var count = _listRemoveItem.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					var item = _listRemoveItem[i];
					item.TimerCount++;

					try
					{
						if (item.Item._process == null || item.Item._process.IsRunning == false)
						{
							_listRemoveItem.RemoveAt(i);
							item.Item?._process.Dispose();
							continue;
						}

						if (item.TimerCount > 10)
						{
							item.Item?._process.Kill();
							item.TimerCount = 0;
						}
					}
					catch (Exception)
					{
					}
				}

			};
			_timer.Start();

		}




		void OnClientSizeChanged_splitContainerMainPanel1(object sender, EventArgs e)
		{
			int x = listViewDownload.Location.X;
			int y = toolStripDownloadTab.Location.Y + toolStripDownloadTab.Height + 10;

			int width = splitContainerMain.Panel1.Width;
			int height = splitContainerMain.Panel1.Bottom - y;

			listViewDownload.Location = new Point(x, y);
			listViewDownload.Size = new Size(width, height);
		}






		void CreateContextMenu_Main()
		{
			if (listViewDownload.ContextMenuStrip != null)
			{
				var tmp = listViewDownload.ContextMenuStrip;
				listViewDownload.ContextMenuStrip = null;
				tmp.Dispose();
			}

			var menu = new ContextMenuStrip();

			foreach (var item in _listMenu)
			{
				menu.Items.Add(item.CreateForContextMenu());
			}

			var menuArgs = new ToolStripMenuItem(ResourceSet.ContextMenuStripMain_Args);
			for (int i = 0; i < Setting.Current.listYtDlpArg.Count; i++)
			{
				var item = Setting.Current.listYtDlpArg[i];
				menuArgs.DropDownItems.Add(new ToolStripMenuItem(item.strName, null, OnMenu_ChangeArg) { Tag = i });
			}
			menu.Items.Add(menuArgs);


			menu.Opening += OnMenu_ContextMenuOpening;
			listViewDownload.ContextMenuStrip = menu;
		}




		bool RemoveItem(int nIndex)
		{
			if (nIndex >= _listItem.Count)
				return false;

			var item = _listItem[nIndex];
			_listItem.RemoveAt(nIndex);

			item?._process?.Abort();

			if (item == null)
				return true;
			if (item._process == null)
				return true;
			if (item._process.IsRunning == false)
			{
				item._process.Dispose();
				return true;
			}

			_listRemoveItem.Add(new RemoveInfo(item));
			return true;
		}








		void OnMenu_ContextMenuOpening(object sender, CancelEventArgs e)
		{
			Point pt = listViewDownload.PointToClient(Cursor.Position);
			ListViewHitTestInfo info = listViewDownload.HitTest(pt);

			if (info.Item == null)
			{
				e.Cancel = true;
				return;
			}

			var menu = (ContextMenuStrip)sender;
			if (menu == null)
				return;

			OnMenuOpening(menu.Items);
		}


		void OnMenuOpening(ToolStripItemCollection Items)
		{
			bool[] pbAll = new bool[(int)DownloadItemStatus._COUNT_];
			bool[] pbSelected = new bool[(int)DownloadItemStatus._COUNT_];

			foreach (var item in _listItem)
			{
				if (item == null)
					continue;

				pbAll[(int)item.enumStatus] = true;

				////all flags is true, break
				//if (bExistStart && bExistPause && bExistStop && bExistCompleted && bExistEtc)
				//	break;
			}

			var selected = listViewDownload.SelectedIndices;
			foreach (int index in selected)
			{
				var item = _listItem[index];

				pbSelected[(int)item.enumStatus] = true;

				////all flags is true, break
				//if (bExistStart && bExistPause && bExistStop && bExistCompleted && bExistEtc)
				//	break;
			}



			foreach (ToolStripItem item in Items)
			{
				foreach (var menuItem in _listMenu)
				{
					if (menuItem.IsThisMenu(item) == false)
						continue;

					menuItem.SetEnable(pbAll, pbSelected);
					item.Enabled = menuItem.Enable;
					break;
				}
			}
		}



		void OnMenu_SelectAll(object sender, EventArgs e)
		{
			for (int i = 0; i < listViewDownload.Items.Count; i++)
			{
				listViewDownload.SelectedIndices.Add(i);
			}
		}



		void OnMenu_StartSelected(object sender, EventArgs e)
		{
			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];
				StartItem(dlItem);
			}
			StartNext();
		}


		bool StartItem(DownloadItem item)
		{
			if (item._process != null && item._process.IsRunning)
				return false;
			if (item.enumStatus == DownloadItemStatus.Completed)
				return false;

			item.enumStatus = DownloadItemStatus.Pause;
			RefreshItem(item);

			return true;
		}





		void OnMenu_ChangeArg(object sender, EventArgs e)
		{
			var menu = (ToolStripMenuItem)sender;
			if (menu == null)
				return;

			int? nArgIndex = (int?)menu.Tag;
			if (nArgIndex == null)
				return;

			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (dlItem._process != null && dlItem._process.IsRunning)
					continue;
				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;
				if (dlItem.nYtDlpArgs == nArgIndex)
					continue;

				dlItem.nYtDlpArgs = (int)nArgIndex;

				RefreshItem(dlItem);
				DBUty.SetItemDB(dlItem);
			}
		}



		void OnMenu_EditFolderName(object sender, EventArgs e)
		{
			List<DownloadItem> list = new List<DownloadItem>();

			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (dlItem._process != null && dlItem._process.IsRunning)
					continue;
				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;

				list.Add(dlItem);
			}

			if (list.Count == 0)
				return;

			using (InputTextForm dlg = new InputTextForm())
			{
				dlg.strTitle = ResourceSet.EditFolderNameForm_Title;
				dlg.strName = ResourceSet.EditFolderNameForm_Name;
				dlg.strDescription = ResourceSet.EditFolderNameForm_Description;
				dlg.strText = list[0].GetFolderName();

				dlg.ShowDialog();
				if (dlg.IsChange == false)
					return;

				foreach (var item in list)
				{
					if (item.strFolderName == dlg.strText)
						continue;

					item.strFolderName = dlg.strText;
					DBUty.SetItemDB(item);
					RefreshItem(item);
				}
			}

		}



		void OnMenu_CopyUrl(object sender, EventArgs e)
		{
			string strText = "";

			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (strText != "")
					strText += "\n";
				strText += dlItem.strUrl;
			}

			if (strText == "")
				return;

			//disable clipboard watching
			_bClipboardWatchEnable = false;

			ClipboardUty.SetText(strText);

			//enable clipboard watching
			_bClipboardWatchEnable = true;
		}



		void OnMenu_StartAll(object sender, EventArgs e)
		{
			for (int i = 0; i < _listItem.Count; i++)
			{
				var dlItem = _listItem[i];

				if (dlItem._process != null && dlItem._process.IsRunning)
					continue;
				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;
				if (dlItem.enumStatus == DownloadItemStatus.Error)
					continue;

				dlItem.enumStatus = DownloadItemStatus.Pause;
				RefreshItem(dlItem);
			}
			StartNext();
		}


		void OnMenu_StopSelected(object sender, EventArgs e)
		{
			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;

				if (dlItem._process == null || dlItem._process.IsRunning == false)
				{
					if (dlItem.enumStatus != DownloadItemStatus.Error)
						dlItem.enumStatus = DownloadItemStatus.Stop;
					RefreshItem(dlItem);
				}
				dlItem?._process?.Abort();
			}
		}



		void OnMenu_StopAll(object sender, EventArgs e)
		{
			for (int i = 0; i < _listItem.Count; i++)
			{
				var dlItem = _listItem[i];

				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;

				if (dlItem._process == null || dlItem._process.IsRunning == false)
				{
					if (dlItem.enumStatus != DownloadItemStatus.Error)
						dlItem.enumStatus = DownloadItemStatus.Stop;
					RefreshItem(dlItem);
					continue;
				}

				dlItem?._process?.Abort();
			}
		}



		void OnMenu_SetCompletedFlag(object sender, EventArgs e)
		{
			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;
				if (dlItem.enumStatus == DownloadItemStatus.Start)
					continue;

				dlItem.enumStatus = DownloadItemStatus.Completed;
				RefreshItem(dlItem);
				DBUty.SetItemDB(dlItem);
			}
		}


		void OnMenu_ResetCompletedFlag(object sender, EventArgs e)
		{
			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				if (dlItem.enumStatus != DownloadItemStatus.Completed)
					continue;

				dlItem.enumStatus = DownloadItemStatus.Stop;
				RefreshItem(dlItem);
				DBUty.SetItemDB(dlItem);
			}
		}




		void OnMenu_ClearCompleted(object sender, EventArgs e)
		{
			bool bChange = false;

			for (int i = _listItem.Count - 1; i >= 0; i--)
			{
				var dlItem = _listItem[i];

				if (dlItem.enumStatus != DownloadItemStatus.Completed)
					continue;

				RemoveItem(i);
				bChange = true;
			}

			if (bChange)
				listViewDownload.VirtualListSize = _listItem.Count;
		}


		/// <summary>
		/// press delete key, remove selected items
		/// 
		/// downloading item	: ignore
		/// completed item		: remove from only view
		/// other item			: remove from db
		/// </summary>
		void OnDeleteKey()
		{
			bool bChange = false;

			var pnIndex = listViewDownload.SelectedIndices;
			var nCount = pnIndex.Count;

			for (int i = nCount - 1; i >= 0; i--)
			{
				var dlItem = _listItem[pnIndex[i]];
				if (dlItem.enumStatus == DownloadItemStatus.Start)
					continue;

				RemoveItem(pnIndex[i]);
				bChange = true;

				//completed remove from only view
				if (dlItem.enumStatus == DownloadItemStatus.Completed)
					continue;

				//other item remove from db
				DBUty.RemoveItemDB(dlItem);
			}

			if (bChange)
				listViewDownload.VirtualListSize = _listItem.Count;
		}





		void OnMenu_DeleteFromDB(object sender, EventArgs e)
		{
			using (CancellationTokenSource cs = new CancellationTokenSource())
			using (var dlg = new ProgressForm())
			{
				//TODO: change resouce string
				dlg.strName = ResourceSet.ContextMenuStripMain_DeleteSelectedFromDB;
				dlg.strTitle = Application.ProductName;
				dlg.EnableCancelButton = true;

				var tmp = listViewDownload.SelectedIndices;
				var nCount = tmp.Count;

				var items = new int[nCount];
				for (int i = 0; i < nCount; i++)
				{
					items[i] = tmp[i];
				}
				listViewDownload.SelectedIndices.Clear();

				dlg.nMin = 0;
				dlg.nMax = nCount;
				dlg.SetProgress(0);

				var token = cs.Token;
				Task.Run(() =>
				{
					try
					{
						for (int i = nCount - 1; i >= 0; i--)
						{
							if (token.IsCancellationRequested)
								break;
							dlg.SetProgress(nCount - 1 - i);

							var dlItem = _listItem[items[i]];

							if (dlItem.enumStatus == DownloadItemStatus.Start)
								continue;

							Invoke(delegate
							{
								RemoveItem(items[i]);
								listViewDownload.VirtualListSize = _listItem.Count;
							});

							DBUty.RemoveItemDB(dlItem);
						}
						dlg.Close();
					}
					catch (Exception)
					{
					}
				});

				dlg.ShowDialog();
				cs.Cancel();
			}
		}










		void OnMenu_DeleteFromView(object sender, EventArgs e)
		{
			bool bChange = false;

			var items = listViewDownload.SelectedIndices;
			for (int i = items.Count - 1; i >= 0; i--)
			{
				var dlItem = _listItem[items[i]];
				if (dlItem.enumStatus == DownloadItemStatus.Start)
					continue;

				RemoveItem(items[i]);
				bChange = true;
			}
			if (bChange)
				listViewDownload.VirtualListSize = _listItem.Count;
		}





		void OnMenu_Kill(object sender, EventArgs e)
		{
			var items = listViewDownload.SelectedIndices;
			for (int i = 0; i < items.Count; i++)
			{
				var dlItem = _listItem[items[i]];

				KillItem(dlItem);
			}
		}










		void FormClosing_FormPartialDownloading()
		{
			KillAllItem();
		}






		void KillAllItem()
		{
			bool kill = false;

			for (int i = 0; i < _listRemoveItem.Count; i++)
			{
				_listRemoveItem[i].Item?._process?.Abort();
				kill = true;
			}
			for (int i = _listItem.Count - 1; i >= 0; i--)
			{
				_listItem[i]?._process?.Abort();
				kill = true;
			}
			if (kill == false)
				return;

			Thread.Sleep(100);

			for (int i = 0; i < _listRemoveItem.Count; i++)
			{
				KillItem(_listRemoveItem[i].Item);
			}
			for (int i = _listItem.Count - 1; i >= 0; i--)
			{
				KillItem(_listItem[i]);
			}
		}




		void KillItem(DownloadItem item)
		{
			if (item._process == null || item._process.IsRunning == false)
			{
				if (item.enumStatus != DownloadItemStatus.Completed
					&& item.enumStatus != DownloadItemStatus.Error)
				{
					item.enumStatus = DownloadItemStatus.Stop;
					RefreshItem(item);
				}
				return;
			}

			try
			{
				item?._process?.Kill();

				Thread.Sleep(0);

				if (item.enumStatus != DownloadItemStatus.Completed
					&& item.enumStatus != DownloadItemStatus.Error)
				{
					item.enumStatus = DownloadItemStatus.Stop;
				}
			}
			catch (Exception)
			{
			}
		}








		void StartThread(DownloadItem item)
		{
			try
			{
				item?._process?.Dispose();
			}
			catch (Exception)
			{
			}


			ProcessThread th = new ProcessThread();

			item._process = th;


			th.OnStart += (sender, e) =>
			{
				_nThreadCount++;

				th = (ProcessThread)sender;


				Invoke((MethodInvoker)delegate
				{
					SetLogText(item, "---", false);
					SetLogText(item, $"date> {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}", false);
					//SetLogText(item, $"cmd> {th._exe}", false);
					SetLogText(item, $"args> {th._arg}", false);
				});


				Invoke((MethodInvoker)delegate
				{
					splitContainerStatusbar.Items[0].Text = $"thread:{_nThreadCount}";
				});

				if (item.enumStatus == DownloadItemStatus.Completed)
				{
					th?.Abort();
					return;
				}

				item.enumStatus = DownloadItemStatus.Start;
				Invoke((MethodInvoker)delegate
				{
					RefreshItem(item);
				});
			};

			th.OnStandardOutput += (sender, e) =>
			{
				Invoke((MethodInvoker)delegate
				{
					SetLogText(item, e.Line);
				});
			};

			th.OnErrorOutput += (sender, e) =>
			{
				Invoke((MethodInvoker)delegate
				{
					SetLogText(item, e.Line);
				});
			};




			th.OnEnd += delegate
			{
				_nThreadCount--;
				try
				{
					if (item.enumStatus != DownloadItemStatus.Error
						&& item.enumStatus != DownloadItemStatus.Completed)
					{
						if (item.dProgress >= 100m)
							item.enumStatus = DownloadItemStatus.Completed;
						else
							item.enumStatus = DownloadItemStatus.Stop;
						DBUty.SetItemDB(item);
					}

					Invoke((MethodInvoker)delegate
					{
						RefreshItem(item);
						StartNext();

						//TODO: resource string
						if (_nThreadCount == 0)
							splitContainerStatusbar.Items[0].Text = $"ready";
						else
							splitContainerStatusbar.Items[0].Text = $"thread:{_nThreadCount}";
					});
				}
				catch (Exception)
				{
				}
			};



			string arg;

			if (item.nYtDlpArgs < Setting.Current.listYtDlpArg.Count)
				arg = Setting.Current.listYtDlpArg[item.nYtDlpArgs].strArg;
			else
				arg = Setting.Current.listYtDlpArg[0].strArg;


			arg = arg.Replace("%URL%", item.strUrl);

			var FileName = Setting.Current.GetYtDlpExePath();

			var WorkingDirectory = item.GetSafeFolderFullPath();



			th.Start(FileName, arg, WorkingDirectory);
		}











		public void StartNext()
		{
			int nCount = _nThreadCount;

			if (nCount >= Setting.Current.nMaxDownloadCount)
				return;

			for (int i = 0; i < _listItem.Count; i++)
			{
				var item = _listItem[i];
				if (item == null)
					continue;

				if (item.enumStatus != DownloadItemStatus.Pause)
					continue;
				if (item._process != null && item._process.IsRunning)
					continue;

				StartThread(item);

				nCount++;
				if (nCount >= Setting.Current.nMaxDownloadCount)
					return;
			}
		}









		void AddItem(DownloadItem item, bool bAddToDB, bool bStart=false)
		{
			try
			{
				//dupe check
				foreach (var check in _listItem)
				{
					if (check.strUrl != item.strUrl)
						continue;

					//already in view, skip to add
					RefreshItem(item);
					if (bAddToDB)
						DBUty.SetItemDB(item);
					return;
				}

				_listItem.Add(item);

				if (bAddToDB)
					DBUty.SetItemDB(item);

				listViewDownload.VirtualListSize = _listItem.Count;
			}
			finally
			{
				if (bStart)
				{
					var ret = StartItem(item);
					if (ret)
						StartNext();
				}
			}
		}



		void RefreshItem(DownloadItem item)
		{
			try
			{
				for (int i = 0; i < _listItem.Count; i++)
				{
					if (_listItem[i].strUrl != item.strUrl)
						continue;

					listViewDownload.RedrawItems(i, i, true);

					return;
				}
			}
			catch (Exception)
			{
			}
		}








		private void listViewDownload_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (e.ItemIndex >= _listItem.Count)
				return;
			if (e.Item == null)
				e.Item = new ListViewItem();

			var lvItem = e.Item;
			var item = _listItem[e.ItemIndex];

			while (lvItem.SubItems.Count < _listColumn.Count)
			{
				lvItem.SubItems.Add(new ListViewItem.ListViewSubItem());
			}

			//change bg color
			foreach (ListViewItem.ListViewSubItem subitem in lvItem.SubItems)
			{
				if (item.listSubscribeUrl.Count > 0)
					subitem.BackColor = Setting.Current.colorSubscribeBackGround;
				else
					subitem.BackColor = Setting.Current.colorDefaultBackGround;
			}

			lvItem.ImageIndex = (int)item.enumStatus;

			for (int i = 0; i < _listColumn.Count; i++)
			{
				lvItem.SubItems[i].Text = _listColumn[i].GetValue(item);
			}
		}





		void RefreshTextBox(DownloadItem item = null)
		{
			try
			{
				var items = listViewDownload.SelectedIndices;
				if (items.Count == 0)
				{
					textBoxLog.Text = "";
					return;
				}

				if (item == null)
				{
					item = _listItem[items[0]];
				}
				else
				{
					bool bSelected = false;
					for (int i = 0; i < items.Count; i++)
					{
						var refitem = _listItem[items[i]];
						if (refitem == null)
							continue;
						if (refitem.strUrl != item.strUrl)
							continue;

						bSelected = true;
						break;
					}
					if (bSelected == false)
						item = null;
				}
				if (item == null || item.strLogMessage == null)
				{
					//textBoxLog.Text = "";
					return;
				}

				textBoxLog.Text = item.strLogMessage;

				//scroll to end
				textBoxLog.SelectionStart = textBoxLog.Text.Length;
				textBoxLog.ScrollToCaret();
			}
			catch (Exception)
			{
			}
		}








		int _nThreadCount = 0;











		void SetLogText(DownloadItem item, string strLine, bool bAnalyze = true)
		{
			//[download] Destination: ReLive [eW1kkI3zc7o].mp4
			//[download]   2.9% of 697.87MiB at  1.61MiB/s ETA 07:00

			if (strLine == null)
				return;

			bool bRefreshLog = false;
			bool bRefreshListView = false;

			try
			{
				if (bAnalyze == false)
				{
					bRefreshLog = true;
					return;
				}

				if (strLine.IndexOf("ERROR:") == 0)
				{
					item.enumStatus = DownloadItemStatus.Error;
					bRefreshLog = true;
					bRefreshListView = true;
					return;
				}

				if (strLine.IndexOf("[download]") != 0 || strLine.Length < "[download]".Length)
				{
					bRefreshLog = true;
					return;
				}

				//[download] 100% of 216.69KiB
				if (strLine.IndexOf("[download] 100% of") == 0)
				{
					item.dProgress = 100m;

					bRefreshLog = true;
					bRefreshListView = true;
					return;
				}

				////[download] Downloading video 3 of 48
				//if (strLine.IndexOf("[download] Downloading video ") == 0)
				//{
				//	var matchs = Regex.Matches(strLine, "\\[download\\] Downloading video ([0-9]+?) of ([0-9]+?)$");

				//	foreach (Match match in matchs)
				//	{
				//		if (match.Groups.Count < 3)
				//			continue;

				//		item.nPlayListLastProcess = int.Parse(match.Groups[1].Value);
				//		int totalCount = int.Parse(match.Groups[2].Value);
				//		bRefreshLog = true;
				//		return;
				//	}
				//}

				if (strLine.IndexOf("[download] Destination:") == 0)
				{
					int n = "[download] Destination: ".Length;
					item.strFile = strLine.Substring(n, strLine.Length - n);

					bRefreshLog = true;
					return;
				}

				{
					var tmp = strLine + " ";
					var matchs = Regex.Matches(tmp, "\\[download\\] (.+?)\\% of (.+?) at (.+?) ETA ([0-9:Unkow]+?) ");

					foreach (Match match in matchs)
					{
						if (match.Groups.Count < 5)
							continue;

						try
						{
							item.dProgress = decimal.Parse(match.Groups[1].Value);
							item.strSize = match.Groups[2].Value;
							item.strSpeed = match.Groups[3].Value;
							item.strETA = match.Groups[4].Value;

							if (item.enumStatus != DownloadItemStatus.Start)
							{
								item.enumStatus = DownloadItemStatus.Start;
								DBUty.SetItemDB(item);
							}

							bRefreshListView = true;
							return;
						}
						catch (Exception)
						{
						}
					}
				}

				bRefreshLog = true;
			}
			finally
			{
				if (bRefreshListView)
					RefreshItem(item);

				if (bRefreshLog)
				{
					item.strLogMessage += strLine;
					item.strLogMessage += "\r\n";
					RefreshTextBox(item);

					DBUty.SetItemDB(item);
				}
			}
		}







	}
}
