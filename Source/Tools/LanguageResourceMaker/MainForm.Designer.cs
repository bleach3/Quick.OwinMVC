﻿namespace LanguageResourceMaker
{
    partial class MainForm
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
            this.btnExtractLanguageResource = new System.Windows.Forms.Button();
            this.btnExtractLanguageDict = new System.Windows.Forms.Button();
            this.btnAutoTranslateLanguageDict = new System.Windows.Forms.Button();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectInput = new System.Windows.Forms.Button();
            this.cbInputMode = new System.Windows.Forms.ComboBox();
            this.pnlInputFolder = new System.Windows.Forms.Panel();
            this.lblShowInputFolder = new System.Windows.Forms.Label();
            this.lblShowLanguageFolder = new System.Windows.Forms.Label();
            this.lblShowLanguageDict = new System.Windows.Forms.Label();
            this.btnDeleteLanguageFolder = new System.Windows.Forms.Button();
            this.btnImportLanguageDict = new System.Windows.Forms.Button();
            this.btnGenerateLanguageResource = new System.Windows.Forms.Button();
            this.lblShowLanguageFolder2 = new System.Windows.Forms.Label();
            this.btnDeleteLanguageDict = new System.Windows.Forms.Button();
            this.pnlInputFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExtractLanguageResource
            // 
            this.btnExtractLanguageResource.Location = new System.Drawing.Point(336, 55);
            this.btnExtractLanguageResource.Name = "btnExtractLanguageResource";
            this.btnExtractLanguageResource.Size = new System.Drawing.Size(130, 23);
            this.btnExtractLanguageResource.TabIndex = 100;
            this.btnExtractLanguageResource.Text = "提取语言资源";
            this.btnExtractLanguageResource.UseVisualStyleBackColor = true;
            this.btnExtractLanguageResource.Click += new System.EventHandler(this.btnExtractLanguageResource_Click);
            // 
            // btnExtractLanguageDict
            // 
            this.btnExtractLanguageDict.Location = new System.Drawing.Point(336, 130);
            this.btnExtractLanguageDict.Name = "btnExtractLanguageDict";
            this.btnExtractLanguageDict.Size = new System.Drawing.Size(130, 23);
            this.btnExtractLanguageDict.TabIndex = 101;
            this.btnExtractLanguageDict.Text = "提取语言字典";
            this.btnExtractLanguageDict.UseVisualStyleBackColor = true;
            this.btnExtractLanguageDict.Click += new System.EventHandler(this.btnExtractLanguageDict_Click);
            // 
            // btnAutoTranslateLanguageDict
            // 
            this.btnAutoTranslateLanguageDict.Location = new System.Drawing.Point(336, 250);
            this.btnAutoTranslateLanguageDict.Name = "btnAutoTranslateLanguageDict";
            this.btnAutoTranslateLanguageDict.Size = new System.Drawing.Size(130, 23);
            this.btnAutoTranslateLanguageDict.TabIndex = 102;
            this.btnAutoTranslateLanguageDict.Text = "机翻语言字典..";
            this.btnAutoTranslateLanguageDict.UseVisualStyleBackColor = true;
            this.btnAutoTranslateLanguageDict.Click += new System.EventHandler(this.btnAutoTranslateLanguageDict_Click);
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputFolder.Location = new System.Drawing.Point(1, 2);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.Size = new System.Drawing.Size(266, 21);
            this.txtInputFolder.TabIndex = 1;
            this.txtInputFolder.TextChanged += new System.EventHandler(this.txtInputFolder_TextChanged);
            // 
            // btnSelectInput
            // 
            this.btnSelectInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectInput.Location = new System.Drawing.Point(273, 0);
            this.btnSelectInput.Name = "btnSelectInput";
            this.btnSelectInput.Size = new System.Drawing.Size(41, 23);
            this.btnSelectInput.TabIndex = 2;
            this.btnSelectInput.Text = "...";
            this.btnSelectInput.UseVisualStyleBackColor = true;
            this.btnSelectInput.Click += new System.EventHandler(this.btnSelectInput_Click);
            // 
            // cbInputMode
            // 
            this.cbInputMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputMode.FormattingEnabled = true;
            this.cbInputMode.Items.AddRange(new object[] {
            "程序启动目录",
            "自定义"});
            this.cbInputMode.Location = new System.Drawing.Point(336, 10);
            this.cbInputMode.Name = "cbInputMode";
            this.cbInputMode.Size = new System.Drawing.Size(130, 20);
            this.cbInputMode.TabIndex = 0;
            this.cbInputMode.SelectedIndexChanged += new System.EventHandler(this.cbInputMode_SelectedIndexChanged);
            // 
            // pnlInputFolder
            // 
            this.pnlInputFolder.Controls.Add(this.txtInputFolder);
            this.pnlInputFolder.Controls.Add(this.btnSelectInput);
            this.pnlInputFolder.Location = new System.Drawing.Point(152, 31);
            this.pnlInputFolder.Name = "pnlInputFolder";
            this.pnlInputFolder.Size = new System.Drawing.Size(314, 24);
            this.pnlInputFolder.TabIndex = 1;
            // 
            // lblShowInputFolder
            // 
            this.lblShowInputFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblShowInputFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowInputFolder.Location = new System.Drawing.Point(45, 2);
            this.lblShowInputFolder.Name = "lblShowInputFolder";
            this.lblShowInputFolder.Size = new System.Drawing.Size(92, 43);
            this.lblShowInputFolder.TabIndex = 1004;
            this.lblShowInputFolder.Click += new System.EventHandler(this.lblShowInputFolder_Click);
            // 
            // lblShowLanguageFolder
            // 
            this.lblShowLanguageFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblShowLanguageFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowLanguageFolder.Location = new System.Drawing.Point(2, 85);
            this.lblShowLanguageFolder.Name = "lblShowLanguageFolder";
            this.lblShowLanguageFolder.Size = new System.Drawing.Size(177, 35);
            this.lblShowLanguageFolder.TabIndex = 1005;
            this.lblShowLanguageFolder.Click += new System.EventHandler(this.lblShowLanguageFolder_Click);
            // 
            // lblShowLanguageDict
            // 
            this.lblShowLanguageDict.BackColor = System.Drawing.Color.Transparent;
            this.lblShowLanguageDict.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowLanguageDict.Location = new System.Drawing.Point(2, 159);
            this.lblShowLanguageDict.Name = "lblShowLanguageDict";
            this.lblShowLanguageDict.Size = new System.Drawing.Size(177, 37);
            this.lblShowLanguageDict.TabIndex = 1006;
            this.lblShowLanguageDict.Click += new System.EventHandler(this.lblShowLanguageDict_Click);
            // 
            // btnDeleteLanguageFolder
            // 
            this.btnDeleteLanguageFolder.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteLanguageFolder.Location = new System.Drawing.Point(336, 419);
            this.btnDeleteLanguageFolder.Name = "btnDeleteLanguageFolder";
            this.btnDeleteLanguageFolder.Size = new System.Drawing.Size(130, 23);
            this.btnDeleteLanguageFolder.TabIndex = 1007;
            this.btnDeleteLanguageFolder.Text = "删除语言目录";
            this.btnDeleteLanguageFolder.UseVisualStyleBackColor = true;
            this.btnDeleteLanguageFolder.Click += new System.EventHandler(this.btnDeleteLanguageFolder_Click);
            // 
            // btnImportLanguageDict
            // 
            this.btnImportLanguageDict.Location = new System.Drawing.Point(336, 279);
            this.btnImportLanguageDict.Name = "btnImportLanguageDict";
            this.btnImportLanguageDict.Size = new System.Drawing.Size(130, 23);
            this.btnImportLanguageDict.TabIndex = 103;
            this.btnImportLanguageDict.Text = "导入翻译结果..";
            this.btnImportLanguageDict.UseVisualStyleBackColor = true;
            this.btnImportLanguageDict.Click += new System.EventHandler(this.btnImportLanguageDict_Click);
            // 
            // btnGenerateLanguageResource
            // 
            this.btnGenerateLanguageResource.Location = new System.Drawing.Point(336, 356);
            this.btnGenerateLanguageResource.Name = "btnGenerateLanguageResource";
            this.btnGenerateLanguageResource.Size = new System.Drawing.Size(130, 23);
            this.btnGenerateLanguageResource.TabIndex = 104;
            this.btnGenerateLanguageResource.Text = "生成语言资源";
            this.btnGenerateLanguageResource.UseVisualStyleBackColor = true;
            this.btnGenerateLanguageResource.Click += new System.EventHandler(this.btnGenerateLanguageResource_Click);
            // 
            // lblShowLanguageFolder2
            // 
            this.lblShowLanguageFolder2.BackColor = System.Drawing.Color.Transparent;
            this.lblShowLanguageFolder2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowLanguageFolder2.Location = new System.Drawing.Point(2, 387);
            this.lblShowLanguageFolder2.Name = "lblShowLanguageFolder2";
            this.lblShowLanguageFolder2.Size = new System.Drawing.Size(177, 35);
            this.lblShowLanguageFolder2.TabIndex = 1008;
            this.lblShowLanguageFolder2.Click += new System.EventHandler(this.lblShowLanguageFolder_Click);
            // 
            // btnDeleteLanguageDict
            // 
            this.btnDeleteLanguageDict.Location = new System.Drawing.Point(336, 390);
            this.btnDeleteLanguageDict.Name = "btnDeleteLanguageDict";
            this.btnDeleteLanguageDict.Size = new System.Drawing.Size(130, 23);
            this.btnDeleteLanguageDict.TabIndex = 105;
            this.btnDeleteLanguageDict.Text = "删除语言字典";
            this.btnDeleteLanguageDict.UseVisualStyleBackColor = true;
            this.btnDeleteLanguageDict.Click += new System.EventHandler(this.btnDeleteLanguageDict_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LanguageResourceMaker.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(484, 454);
            this.Controls.Add(this.btnDeleteLanguageDict);
            this.Controls.Add(this.lblShowLanguageFolder2);
            this.Controls.Add(this.btnGenerateLanguageResource);
            this.Controls.Add(this.btnImportLanguageDict);
            this.Controls.Add(this.btnDeleteLanguageFolder);
            this.Controls.Add(this.lblShowLanguageDict);
            this.Controls.Add(this.lblShowLanguageFolder);
            this.Controls.Add(this.lblShowInputFolder);
            this.Controls.Add(this.pnlInputFolder);
            this.Controls.Add(this.cbInputMode);
            this.Controls.Add(this.btnAutoTranslateLanguageDict);
            this.Controls.Add(this.btnExtractLanguageDict);
            this.Controls.Add(this.btnExtractLanguageResource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "语言工作室";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlInputFolder.ResumeLayout(false);
            this.pnlInputFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExtractLanguageResource;
        private System.Windows.Forms.Button btnExtractLanguageDict;
        private System.Windows.Forms.Button btnAutoTranslateLanguageDict;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Button btnSelectInput;
        private System.Windows.Forms.ComboBox cbInputMode;
        private System.Windows.Forms.Panel pnlInputFolder;
        private System.Windows.Forms.Label lblShowInputFolder;
        private System.Windows.Forms.Label lblShowLanguageFolder;
        private System.Windows.Forms.Label lblShowLanguageDict;
        private System.Windows.Forms.Button btnDeleteLanguageFolder;
        private System.Windows.Forms.Button btnImportLanguageDict;
        private System.Windows.Forms.Button btnGenerateLanguageResource;
        private System.Windows.Forms.Label lblShowLanguageFolder2;
        private System.Windows.Forms.Button btnDeleteLanguageDict;
    }
}