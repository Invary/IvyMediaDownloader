namespace Invary.Utility
{
	partial class InputTextForm
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
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.labelDescription = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(366, 173);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(467, 173);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxText
			// 
			this.textBoxText.Location = new System.Drawing.Point(12, 75);
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.Size = new System.Drawing.Size(530, 23);
			this.textBoxText.TabIndex = 2;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(12, 47);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(38, 15);
			this.labelName.TabIndex = 3;
			this.labelName.Text = "label1";
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.labelDescription.Location = new System.Drawing.Point(22, 116);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(38, 15);
			this.labelDescription.TabIndex = 4;
			this.labelDescription.Text = "label1";
			// 
			// InputTextForm
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(554, 208);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.textBoxText);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "InputTextForm";
			this.ShowInTaskbar = false;
			this.Text = "InputTextForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxText;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Label labelDescription;
	}
}