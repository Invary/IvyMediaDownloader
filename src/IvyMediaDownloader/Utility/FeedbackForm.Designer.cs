namespace Invary.Utility
{
	partial class FeedbackForm
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
			this.buttonSend = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabelUrl = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(224, 330);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 0;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(337, 330);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cencel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.AcceptsReturn = true;
			this.textBoxMessage.AcceptsTab = true;
			this.textBoxMessage.Location = new System.Drawing.Point(12, 68);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(400, 229);
			this.textBoxMessage.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Feedback message";
			// 
			// linkLabelUrl
			// 
			this.linkLabelUrl.AutoSize = true;
			this.linkLabelUrl.Location = new System.Drawing.Point(12, 338);
			this.linkLabelUrl.Name = "linkLabelUrl";
			this.linkLabelUrl.Size = new System.Drawing.Size(94, 15);
			this.linkLabelUrl.TabIndex = 4;
			this.linkLabelUrl.TabStop = true;
			this.linkLabelUrl.Text = "Send by browser";
			this.linkLabelUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrl_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.LightCoral;
			this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(12, 300);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(395, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Please do NOT fill in any personal information (name, email address, etc.)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(56, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(310, 15);
			this.label3.TabIndex = 6;
			this.label3.Text = "If you have any bugs or requests, please send them to me.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(56, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(192, 15);
			this.label4.TabIndex = 7;
			this.label4.Text = "Please let me know what you think.";
			// 
			// FeedbackForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(435, 373);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.linkLabelUrl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSend);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "FeedbackForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FeedbackForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabelUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}