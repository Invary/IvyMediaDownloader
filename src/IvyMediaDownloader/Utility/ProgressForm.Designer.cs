namespace Invary.Utility
{
	partial class ProgressForm
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.labelText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(317, 103);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 51);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(380, 23);
			this.progressBar.TabIndex = 1;
			// 
			// labelText
			// 
			this.labelText.AutoSize = true;
			this.labelText.Location = new System.Drawing.Point(12, 23);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(38, 15);
			this.labelText.TabIndex = 2;
			this.labelText.Text = "label1";
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(396, 130);
			this.ControlBox = false;
			this.Controls.Add(this.labelText);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.buttonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ProgressForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label labelText;
	}
}