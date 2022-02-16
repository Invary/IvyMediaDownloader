
namespace Invary.IvyMediaDownloader
{
	partial class SettingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.numericUpDownMaxDownload = new System.Windows.Forms.NumericUpDown();
			this.listBoxArg = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxUseClipboarAutoImport = new System.Windows.Forms.CheckBox();
			this.textBoxUrlDetectRegExp = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSelectYtdlpExe = new System.Windows.Forms.Button();
			this.labelYtdlpPath = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxUpdateCheck = new System.Windows.Forms.CheckBox();
			this.checkBoxCheckUpdateYtdlp = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.labelWorkFolder = new System.Windows.Forms.Label();
			this.buttonEditWorkFolder = new System.Windows.Forms.Button();
			this.buttonCheckRegexp = new System.Windows.Forms.Button();
			this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDownload)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			// numericUpDownMaxDownload
			// 
			resources.ApplyResources(this.numericUpDownMaxDownload, "numericUpDownMaxDownload");
			this.numericUpDownMaxDownload.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownMaxDownload.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMaxDownload.Name = "numericUpDownMaxDownload";
			this.numericUpDownMaxDownload.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// listBoxArg
			// 
			this.listBoxArg.FormattingEnabled = true;
			resources.ApplyResources(this.listBoxArg, "listBoxArg");
			this.listBoxArg.Name = "listBoxArg";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listBoxArg);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// checkBoxUseClipboarAutoImport
			// 
			resources.ApplyResources(this.checkBoxUseClipboarAutoImport, "checkBoxUseClipboarAutoImport");
			this.checkBoxUseClipboarAutoImport.Name = "checkBoxUseClipboarAutoImport";
			this.checkBoxUseClipboarAutoImport.UseVisualStyleBackColor = true;
			// 
			// textBoxUrlDetectRegExp
			// 
			resources.ApplyResources(this.textBoxUrlDetectRegExp, "textBoxUrlDetectRegExp");
			this.textBoxUrlDetectRegExp.Name = "textBoxUrlDetectRegExp";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// buttonSelectYtdlpExe
			// 
			resources.ApplyResources(this.buttonSelectYtdlpExe, "buttonSelectYtdlpExe");
			this.buttonSelectYtdlpExe.Name = "buttonSelectYtdlpExe";
			this.buttonSelectYtdlpExe.UseVisualStyleBackColor = true;
			this.buttonSelectYtdlpExe.Click += new System.EventHandler(this.buttonSelectYtdlpExe_Click);
			// 
			// labelYtdlpPath
			// 
			resources.ApplyResources(this.labelYtdlpPath, "labelYtdlpPath");
			this.labelYtdlpPath.Name = "labelYtdlpPath";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// checkBoxUpdateCheck
			// 
			resources.ApplyResources(this.checkBoxUpdateCheck, "checkBoxUpdateCheck");
			this.checkBoxUpdateCheck.Name = "checkBoxUpdateCheck";
			this.checkBoxUpdateCheck.UseVisualStyleBackColor = true;
			// 
			// checkBoxCheckUpdateYtdlp
			// 
			resources.ApplyResources(this.checkBoxCheckUpdateYtdlp, "checkBoxCheckUpdateYtdlp");
			this.checkBoxCheckUpdateYtdlp.Name = "checkBoxCheckUpdateYtdlp";
			this.checkBoxCheckUpdateYtdlp.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// labelWorkFolder
			// 
			resources.ApplyResources(this.labelWorkFolder, "labelWorkFolder");
			this.labelWorkFolder.Name = "labelWorkFolder";
			// 
			// buttonEditWorkFolder
			// 
			resources.ApplyResources(this.buttonEditWorkFolder, "buttonEditWorkFolder");
			this.buttonEditWorkFolder.Name = "buttonEditWorkFolder";
			this.buttonEditWorkFolder.UseVisualStyleBackColor = true;
			this.buttonEditWorkFolder.Click += new System.EventHandler(this.buttonEditWorkFolder_Click);
			// 
			// buttonCheckRegexp
			// 
			resources.ApplyResources(this.buttonCheckRegexp, "buttonCheckRegexp");
			this.buttonCheckRegexp.Name = "buttonCheckRegexp";
			this.buttonCheckRegexp.UseVisualStyleBackColor = true;
			this.buttonCheckRegexp.Click += new System.EventHandler(this.buttonCheckRegexp_Click);
			// 
			// comboBoxLanguage
			// 
			this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLanguage.FormattingEnabled = true;
			resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
			this.comboBoxLanguage.Name = "comboBoxLanguage";
			// 
			// labelLanguage
			// 
			resources.ApplyResources(this.labelLanguage, "labelLanguage");
			this.labelLanguage.Name = "labelLanguage";
			// 
			// SettingForm
			// 
			this.AcceptButton = this.buttonOk;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.Controls.Add(this.labelLanguage);
			this.Controls.Add(this.comboBoxLanguage);
			this.Controls.Add(this.buttonCheckRegexp);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.labelWorkFolder);
			this.Controls.Add(this.buttonEditWorkFolder);
			this.Controls.Add(this.checkBoxCheckUpdateYtdlp);
			this.Controls.Add(this.checkBoxUpdateCheck);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelYtdlpPath);
			this.Controls.Add(this.buttonSelectYtdlpExe);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxUrlDetectRegExp);
			this.Controls.Add(this.checkBoxUseClipboarAutoImport);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownMaxDownload);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "SettingForm";
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.SettingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDownload)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.NumericUpDown numericUpDownMaxDownload;
		private System.Windows.Forms.ListBox listBoxArg;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBoxUseClipboarAutoImport;
		private System.Windows.Forms.TextBox textBoxUrlDetectRegExp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSelectYtdlpExe;
		private System.Windows.Forms.Label labelYtdlpPath;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxUpdateCheck;
		private System.Windows.Forms.CheckBox checkBoxCheckUpdateYtdlp;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelWorkFolder;
		private System.Windows.Forms.Button buttonEditWorkFolder;
		private System.Windows.Forms.Button buttonCheckRegexp;
		private System.Windows.Forms.ComboBox comboBoxLanguage;
		private System.Windows.Forms.Label labelLanguage;
	}
}