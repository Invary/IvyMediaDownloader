using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyMediaDownloader
{
	abstract class MainMenuBase
	{
		public string Name { get; set; } = "";
		public string Tooltips { get; set; } = "";
		public string Description { get; set; } = "";

		public Image Icon { get; set; } = null;

		public bool Enable { get; set; } = true;


		public EventHandler OnClick { get; set; } = null;


		abstract public bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected);



		public bool IsThisMenu(ToolStripItem item)
		{
			if (item == null)
				return false;
			if (string.IsNullOrEmpty(Name))
				return false;

			return (item.Name == Name);
		}



		public void Selected()
		{
			if (Enable)
				OnClick?.Invoke(this, new EventArgs());
		}


		virtual public ToolStripItem CreateForContextMenu()
		{
			ToolStripMenuItem item;

			if (string.IsNullOrEmpty(Name))
				item = new ToolStripMenuItem(Name, Icon, OnClick);
			else
				item = new ToolStripMenuItem(Name, Icon, OnClick, Name);

			if (string.IsNullOrEmpty(Tooltips) == false)
			{
				item.AutoToolTip = true;
				item.ToolTipText = Tooltips;
			}

			return item;
		}


		virtual public ToolStripItem CreateForToolbar()
		{
			ToolStripItem item;

			if (Icon == null)
			{
				if (string.IsNullOrEmpty(Name) == false)
					item = new ToolStripMenuItem(Name, null, OnClick);
				else
					item = new ToolStripMenuItem("", null, OnClick);
			}
			else
			{
				if (string.IsNullOrEmpty(Name))
					item = new ToolStripMenuItem("", Icon, OnClick);
				else
					item = new ToolStripMenuItem("", Icon, OnClick, Name);
			}

			item.AutoToolTip = true;
			item.ToolTipText = (string.IsNullOrEmpty(Tooltips) ? Name : Tooltips);

			return item;
		}

		virtual public void SetEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			Enable = IsEnable(pbStatusExistsInAll, pbStatusExistsInSelected);
		}



		protected static bool IsExists(bool[] pbStatusExists, DownloadItemStatus status)
		{
			return pbStatusExists[(int)status];
		}
	}



	internal class MainMenu_Separator : MainMenuBase
	{
		public override ToolStripItem CreateForContextMenu()
		{
			return new ToolStripSeparator();
		}

		public override ToolStripItem CreateForToolbar()
		{
			return new ToolStripSeparator();
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			return false;
		}
	}




	internal class MainMenu_StartSelected : MainMenuBase
	{
		public MainMenu_StartSelected(EventHandler OnClick)
		{
			Icon = ResourceImage.StartSelected;
			Name = ResourceSet.ContextMenuStripMain_StartSelected;
			Tooltips = ResourceSet.MainMenu_Tooltips_StartSelected;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Stop))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Unknown))
				return true;

			return false;
		}
	}



	internal class MainMenu_StartAll : MainMenuBase
	{
		public MainMenu_StartAll(EventHandler OnClick)
		{
			Icon = ResourceImage.StartAll;
			Name = ResourceSet.ContextMenuStripMain_StartAll;
			Tooltips = ResourceSet.MainMenu_Tooltips_StartAll;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Stop))
				return true;
			//if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
			//	return true;
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Unknown))
				return true;

			return false;
		}
	}




	internal class MainMenu_StopSelected : MainMenuBase
	{
		public MainMenu_StopSelected(EventHandler OnClick)
		{
			Icon = ResourceImage.StopSelected;
			Name = ResourceSet.ContextMenuStripMain_StopSelected;
			Tooltips = ResourceSet.MainMenu_Tooltips_StopSelected;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Start))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;

			return false;
		}
	}



	internal class MainMenu_StopAll : MainMenuBase
	{
		public MainMenu_StopAll(EventHandler OnClick)
		{
			Icon = ResourceImage.StopAll;
			Name = ResourceSet.ContextMenuStripMain_StopAll;
			Tooltips = ResourceSet.MainMenu_Tooltips_StopAll;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Start))
				return true;
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Pause))
				return true;

			return false;
		}
	}





	internal class MainMenu_SetCompletedFlag : MainMenuBase
	{
		public MainMenu_SetCompletedFlag(EventHandler OnClick)
		{
			Icon = ResourceImage.SetCompletedFlag;
			Name = ResourceSet.ContextMenuStripMain_SetCompletedFlag;
			Tooltips = ResourceSet.MainMenu_Tooltips_SetCompletedFlag;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Stop))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Unknown))
				return true;

			return false;
		}
	}




	internal class MainMenu_ResetCompletedFlag : MainMenuBase
	{
		public MainMenu_ResetCompletedFlag(EventHandler OnClick)
		{
			Icon =  ResourceImage.Recycle;
			Name = ResourceSet.ContextMenuStripMain_ResetCompletedFlag;
			Tooltips = ResourceSet.MainMenu_Tooltips_ResetCompletedFlag;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Completed))
				return true;

			return false;
		}
	}







	internal class MainMenu_ClearCompleted : MainMenuBase
	{
		public MainMenu_ClearCompleted(EventHandler OnClick)
		{
			Icon = ResourceImage.ClearCompleted;
			Name = ResourceSet.ContextMenuStripMain_ClearCompleted;
			Tooltips = ResourceSet.MainMenu_Tooltips_ClearCompleted;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInAll, DownloadItemStatus.Completed))
				return true;

			return false;
		}
	}




	internal class MainMenu_DeleteDB : MainMenuBase
	{
		public MainMenu_DeleteDB(EventHandler OnClick)
		{
			Icon = ResourceImage.DeleteDB;
			Name = ResourceSet.ContextMenuStripMain_DeleteSelectedFromDB;
			Tooltips = ResourceSet.MainMenu_Tooltips_DeleteSelectedFromDB;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Stop))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Unknown))
				return true;

			return false;
		}
	}




	internal class MainMenu_DeleteView : MainMenuBase
	{
		public MainMenu_DeleteView(EventHandler OnClick)
		{
			Icon = ResourceImage.DeleteView;
			Name = ResourceSet.ContextMenuStripMain_DeleteSelectedFromView;
			Tooltips = ResourceSet.MainMenu_Tooltips_DeleteSelectedFromView;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Stop))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Unknown))
				return true;

			return false;
		}
	}






	internal class MainMenu_KillSelected : MainMenuBase
	{
		public MainMenu_KillSelected(EventHandler OnClick)
		{
			Icon = ResourceImage.KillSelected;
			Name = ResourceSet.ContextMenuStripMain_KillSelected;
			Tooltips = ResourceSet.MainMenu_Tooltips_KillSelected;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Start))
				return true;

			return false;
		}
	}




	internal class MainMenu_SelectAll : MainMenuBase
	{
		public MainMenu_SelectAll(EventHandler OnClick)
		{
			Icon = ResourceImage.SelectAllItems;
			Name = ResourceSet.ContextMenuStripMain_SelectAll;
			Tooltips = ResourceSet.MainMenu_Tooltips_SelectAll;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			return true;
		}
	}



	internal class MainMenu_EditFolderName : MainMenuBase
	{
		public MainMenu_EditFolderName(EventHandler OnClick)
		{
			Icon = ResourceImage.FolderSetting;
			Name = ResourceSet.ContextMenuStripMain_EditFolderName;
			Tooltips = ResourceSet.MainMenu_Tooltips_EditFolderName;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Pause))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Stop))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Error))
				return true;
			if (IsExists(pbStatusExistsInSelected, DownloadItemStatus.Unknown))
				return true;
			return true;
		}
	}


	internal class MainMenu_CopyUrl : MainMenuBase
	{
		public MainMenu_CopyUrl(EventHandler OnClick)
		{
			Icon = ResourceImage.CopyUrlLink;
			Name = ResourceSet.ContextMenuStripMain_CopyUrl;
			Tooltips = ResourceSet.MainMenu_Tooltips_CopyUrl;
			Description = "";
			this.OnClick = OnClick;
		}


		public override bool IsEnable(bool[] pbStatusExistsInAll, bool[] pbStatusExistsInSelected)
		{
			foreach(var item in pbStatusExistsInSelected)
			{
				if (item)
					return true;
			}

			return true;
		}
	}




}
