namespace Invary.IvyMediaDownloader
{
	partial class EditArgForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditArgForm));
			this.textBoxArgName = new System.Windows.Forms.TextBox();
			this.textBoxArg = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxArgName
			// 
			resources.ApplyResources(this.textBoxArgName, "textBoxArgName");
			this.textBoxArgName.Name = "textBoxArgName";
			// 
			// textBoxArg
			// 
			resources.ApplyResources(this.textBoxArg, "textBoxArg");
			this.textBoxArg.Name = "textBoxArg";
			// 
			// buttonOk
			// 
			resources.ApplyResources(this.buttonOk, "buttonOk");
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			resources.ApplyResources(this.buttonCancel, "buttonCancel");
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// EditArgForm
			// 
			this.AcceptButton = this.buttonOk;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.textBoxArgName);
			this.Controls.Add(this.textBoxArg);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EditArgForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxArgName;
		private System.Windows.Forms.TextBox textBoxArg;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}