namespace Invary.Utility
{
	partial class DonationControl
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonationControl));
			this.groupBoxDonationControl = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelQR2_Address = new System.Windows.Forms.Label();
			this.labelQR1_Address = new System.Windows.Forms.Label();
			this.labelQR2_Title = new System.Windows.Forms.Label();
			this.textBoxQR2_Description = new System.Windows.Forms.TextBox();
			this.textBoxQR1_Description = new System.Windows.Forms.TextBox();
			this.labelQR1_Title = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.pictureBoxQR2 = new System.Windows.Forms.PictureBox();
			this.pictureBoxQR1 = new System.Windows.Forms.PictureBox();
			this.pictureBoxKofi = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxDonationControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxKofi)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxDonationControl
			// 
			this.groupBoxDonationControl.Controls.Add(this.label1);
			this.groupBoxDonationControl.Controls.Add(this.labelQR2_Address);
			this.groupBoxDonationControl.Controls.Add(this.labelQR1_Address);
			this.groupBoxDonationControl.Controls.Add(this.labelQR2_Title);
			this.groupBoxDonationControl.Controls.Add(this.textBoxQR2_Description);
			this.groupBoxDonationControl.Controls.Add(this.textBoxQR1_Description);
			this.groupBoxDonationControl.Controls.Add(this.labelQR1_Title);
			this.groupBoxDonationControl.Controls.Add(this.textBoxDescription);
			this.groupBoxDonationControl.Controls.Add(this.pictureBoxQR2);
			this.groupBoxDonationControl.Controls.Add(this.pictureBoxQR1);
			this.groupBoxDonationControl.Controls.Add(this.pictureBoxKofi);
			resources.ApplyResources(this.groupBoxDonationControl, "groupBoxDonationControl");
			this.groupBoxDonationControl.Name = "groupBoxDonationControl";
			this.groupBoxDonationControl.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// labelQR2_Address
			// 
			resources.ApplyResources(this.labelQR2_Address, "labelQR2_Address");
			this.labelQR2_Address.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelQR2_Address.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.labelQR2_Address.Name = "labelQR2_Address";
			this.toolTip1.SetToolTip(this.labelQR2_Address, resources.GetString("labelQR2_Address.ToolTip"));
			// 
			// labelQR1_Address
			// 
			resources.ApplyResources(this.labelQR1_Address, "labelQR1_Address");
			this.labelQR1_Address.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelQR1_Address.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.labelQR1_Address.Name = "labelQR1_Address";
			this.toolTip1.SetToolTip(this.labelQR1_Address, resources.GetString("labelQR1_Address.ToolTip"));
			// 
			// labelQR2_Title
			// 
			resources.ApplyResources(this.labelQR2_Title, "labelQR2_Title");
			this.labelQR2_Title.Name = "labelQR2_Title";
			// 
			// textBoxQR2_Description
			// 
			this.textBoxQR2_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxQR2_Description.Cursor = System.Windows.Forms.Cursors.Arrow;
			resources.ApplyResources(this.textBoxQR2_Description, "textBoxQR2_Description");
			this.textBoxQR2_Description.Name = "textBoxQR2_Description";
			this.textBoxQR2_Description.ReadOnly = true;
			this.textBoxQR2_Description.TabStop = false;
			// 
			// textBoxQR1_Description
			// 
			this.textBoxQR1_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxQR1_Description.Cursor = System.Windows.Forms.Cursors.Arrow;
			resources.ApplyResources(this.textBoxQR1_Description, "textBoxQR1_Description");
			this.textBoxQR1_Description.Name = "textBoxQR1_Description";
			this.textBoxQR1_Description.ReadOnly = true;
			this.textBoxQR1_Description.TabStop = false;
			// 
			// labelQR1_Title
			// 
			resources.ApplyResources(this.labelQR1_Title, "labelQR1_Title");
			this.labelQR1_Title.Name = "labelQR1_Title";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
			resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.TabStop = false;
			// 
			// pictureBoxQR2
			// 
			this.pictureBoxQR2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxQR2.Image = global::Invary.IvyMediaDownloader.Utility.ResourceUty.donation_btc;
			resources.ApplyResources(this.pictureBoxQR2, "pictureBoxQR2");
			this.pictureBoxQR2.Name = "pictureBoxQR2";
			this.pictureBoxQR2.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBoxQR2, resources.GetString("pictureBoxQR2.ToolTip"));
			// 
			// pictureBoxQR1
			// 
			this.pictureBoxQR1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxQR1.Image = global::Invary.IvyMediaDownloader.Utility.ResourceUty.donation_bsc;
			resources.ApplyResources(this.pictureBoxQR1, "pictureBoxQR1");
			this.pictureBoxQR1.Name = "pictureBoxQR1";
			this.pictureBoxQR1.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBoxQR1, resources.GetString("pictureBoxQR1.ToolTip"));
			// 
			// pictureBoxKofi
			// 
			this.pictureBoxKofi.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxKofi.Image = global::Invary.IvyMediaDownloader.Utility.ResourceUty.donation_kofi;
			resources.ApplyResources(this.pictureBoxKofi, "pictureBoxKofi");
			this.pictureBoxKofi.Name = "pictureBoxKofi";
			this.pictureBoxKofi.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBoxKofi, resources.GetString("pictureBoxKofi.ToolTip"));
			// 
			// DonationControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBoxDonationControl);
			this.Name = "DonationControl";
			this.groupBoxDonationControl.ResumeLayout(false);
			this.groupBoxDonationControl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxKofi)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxDonationControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelQR2_Address;
		private System.Windows.Forms.Label labelQR1_Address;
		private System.Windows.Forms.Label labelQR2_Title;
		private System.Windows.Forms.TextBox textBoxQR2_Description;
		private System.Windows.Forms.TextBox textBoxQR1_Description;
		private System.Windows.Forms.Label labelQR1_Title;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.PictureBox pictureBoxQR2;
		private System.Windows.Forms.PictureBox pictureBoxQR1;
		private System.Windows.Forms.PictureBox pictureBoxKofi;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
