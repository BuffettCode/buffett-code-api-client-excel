namespace BuffettCodeAddinRibbon
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabConrtoll = new System.Windows.Forms.TabControl();
            this.tabDeveloper = new System.Windows.Forms.TabPage();
            this.checkDebugMode = new System.Windows.Forms.CheckBox();
            this.tabAPI = new System.Windows.Forms.TabPage();
            this.DescOndemandEndpointEnabled = new System.Windows.Forms.Label();
            this.checkForceOndemandApiEnabled = new System.Windows.Forms.CheckBox();
            this.apiSpecialNotesLink = new System.Windows.Forms.LinkLabel();
            this.ondemandModeDesc = new System.Windows.Forms.TextBox();
            this.checkIsOndemandEndpointEnabled = new System.Windows.Forms.CheckBox();
            this.labelAPIKey = new System.Windows.Forms.Label();
            this.textAPIKey = new System.Windows.Forms.TextBox();
            this.ondemandUsageEntryLink = new System.Windows.Forms.LinkLabel();
            this.ForceOndemandEndpointDesc = new System.Windows.Forms.Label();
            this.tabConrtoll.SuspendLayout();
            this.tabDeveloper.SuspendLayout();
            this.tabAPI.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(255, 326);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(52, 20);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(311, 326);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 20);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // tabConrtoll
            // 
            this.tabConrtoll.Controls.Add(this.tabDeveloper);
            this.tabConrtoll.Controls.Add(this.tabAPI);
            this.tabConrtoll.Location = new System.Drawing.Point(18, 19);
            this.tabConrtoll.Margin = new System.Windows.Forms.Padding(2);
            this.tabConrtoll.Name = "tabConrtoll";
            this.tabConrtoll.SelectedIndex = 0;
            this.tabConrtoll.Size = new System.Drawing.Size(355, 291);
            this.tabConrtoll.TabIndex = 6;
            // 
            // tabDeveloper
            // 
            this.tabDeveloper.Controls.Add(this.checkDebugMode);
            this.tabDeveloper.Location = new System.Drawing.Point(4, 22);
            this.tabDeveloper.Margin = new System.Windows.Forms.Padding(2);
            this.tabDeveloper.Name = "tabDeveloper";
            this.tabDeveloper.Padding = new System.Windows.Forms.Padding(2);
            this.tabDeveloper.Size = new System.Drawing.Size(347, 265);
            this.tabDeveloper.TabIndex = 1;
            this.tabDeveloper.Text = "開発者向けオプション";
            this.tabDeveloper.UseVisualStyleBackColor = true;
            this.tabDeveloper.Click += new System.EventHandler(this.TabDeveloper_Click);
            // 
            // checkDebugMode
            // 
            this.checkDebugMode.AutoSize = true;
            this.checkDebugMode.Location = new System.Drawing.Point(9, 10);
            this.checkDebugMode.Margin = new System.Windows.Forms.Padding(2);
            this.checkDebugMode.Name = "checkDebugMode";
            this.checkDebugMode.Size = new System.Drawing.Size(149, 16);
            this.checkDebugMode.TabIndex = 6;
            this.checkDebugMode.Text = "デバッグモードを有効にする";
            this.checkDebugMode.UseVisualStyleBackColor = true;
            this.checkDebugMode.CheckedChanged += new System.EventHandler(this.CheckDebugMode_CheckedChanged);
            // 
            // tabAPI
            // 
            this.tabAPI.Controls.Add(this.ForceOndemandEndpointDesc);
            this.tabAPI.Controls.Add(this.DescOndemandEndpointEnabled);
            this.tabAPI.Controls.Add(this.checkForceOndemandApiEnabled);
            this.tabAPI.Controls.Add(this.apiSpecialNotesLink);
            this.tabAPI.Controls.Add(this.ondemandModeDesc);
            this.tabAPI.Controls.Add(this.checkIsOndemandEndpointEnabled);
            this.tabAPI.Controls.Add(this.labelAPIKey);
            this.tabAPI.Controls.Add(this.textAPIKey);
            this.tabAPI.Location = new System.Drawing.Point(4, 22);
            this.tabAPI.Margin = new System.Windows.Forms.Padding(2);
            this.tabAPI.Name = "tabAPI";
            this.tabAPI.Padding = new System.Windows.Forms.Padding(2);
            this.tabAPI.Size = new System.Drawing.Size(347, 265);
            this.tabAPI.TabIndex = 0;
            this.tabAPI.Text = "API";
            this.tabAPI.UseVisualStyleBackColor = true;
            this.tabAPI.Click += new System.EventHandler(this.TabAPI_Click);
            // 
            // DescOndemandEndpointEnabled
            // 
            this.DescOndemandEndpointEnabled.AutoSize = true;
            this.DescOndemandEndpointEnabled.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.DescOndemandEndpointEnabled.Location = new System.Drawing.Point(20, 67);
            this.DescOndemandEndpointEnabled.Name = "DescOndemandEndpointEnabled";
            this.DescOndemandEndpointEnabled.Size = new System.Drawing.Size(289, 11);
            this.DescOndemandEndpointEnabled.TabIndex = 9;
            this.DescOndemandEndpointEnabled.Text = "バフェットコードAPIの従量課金エンドポイントの利用を可能にします";
            // 
            // checkForceOndemandApiEnabled
            // 
            this.checkForceOndemandApiEnabled.AutoSize = true;
            this.checkForceOndemandApiEnabled.Location = new System.Drawing.Point(10, 95);
            this.checkForceOndemandApiEnabled.Name = "checkForceOndemandApiEnabled";
            this.checkForceOndemandApiEnabled.Size = new System.Drawing.Size(251, 16);
            this.checkForceOndemandApiEnabled.TabIndex = 8;
            this.checkForceOndemandApiEnabled.Text = "常に従量課金APIを利用する (制限回避モード)";
            this.checkForceOndemandApiEnabled.UseVisualStyleBackColor = true;
            this.checkForceOndemandApiEnabled.CheckedChanged += new System.EventHandler(this.CheckForceOndemandEndpointEnabled_CheckedChanged);
            // 
            // apiSpecialNotesLink
            // 
            this.apiSpecialNotesLink.AccessibleName = "apiSpecialNotesLink";
            this.apiSpecialNotesLink.AutoSize = true;
            this.apiSpecialNotesLink.BackColor = System.Drawing.SystemColors.Window;
            this.apiSpecialNotesLink.Location = new System.Drawing.Point(37, 222);
            this.apiSpecialNotesLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.apiSpecialNotesLink.Name = "apiSpecialNotesLink";
            this.apiSpecialNotesLink.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.apiSpecialNotesLink.Size = new System.Drawing.Size(234, 12);
            this.apiSpecialNotesLink.TabIndex = 7;
            this.apiSpecialNotesLink.TabStop = true;
            this.apiSpecialNotesLink.Text = "バフェットコード WEB API機能に関する特記事項";
            this.apiSpecialNotesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ApiSpecialNotes_LinkClicked);
            // 
            // ondemandModeDesc
            // 
            this.ondemandModeDesc.BackColor = System.Drawing.SystemColors.Window;
            this.ondemandModeDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ondemandModeDesc.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ondemandModeDesc.Location = new System.Drawing.Point(25, 141);
            this.ondemandModeDesc.Margin = new System.Windows.Forms.Padding(2);
            this.ondemandModeDesc.Multiline = true;
            this.ondemandModeDesc.Name = "ondemandModeDesc";
            this.ondemandModeDesc.ReadOnly = true;
            this.ondemandModeDesc.Size = new System.Drawing.Size(304, 115);
            this.ondemandModeDesc.TabIndex = 6;
            this.ondemandModeDesc.Text = resources.GetString("ondemandModeDesc.Text");
            this.ondemandModeDesc.TextChanged += new System.EventHandler(this.OndemandModeDescDesc_TextChanged);
            // 
            // checkIsOndemandEndpointEnabled
            // 
            this.checkIsOndemandEndpointEnabled.AutoSize = true;
            this.checkIsOndemandEndpointEnabled.Location = new System.Drawing.Point(10, 48);
            this.checkIsOndemandEndpointEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.checkIsOndemandEndpointEnabled.Name = "checkIsOndemandEndpointEnabled";
            this.checkIsOndemandEndpointEnabled.Size = new System.Drawing.Size(187, 16);
            this.checkIsOndemandEndpointEnabled.TabIndex = 5;
            this.checkIsOndemandEndpointEnabled.Text = "従量課金エンドポイントを利用する";
            this.checkIsOndemandEndpointEnabled.UseVisualStyleBackColor = true;
            this.checkIsOndemandEndpointEnabled.CheckedChanged += new System.EventHandler(this.CheckIsOndemandEndpointEnabled_CheckedChanged);
            // 
            // labelAPIKey
            // 
            this.labelAPIKey.AutoSize = true;
            this.labelAPIKey.Location = new System.Drawing.Point(4, 2);
            this.labelAPIKey.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAPIKey.Name = "labelAPIKey";
            this.labelAPIKey.Size = new System.Drawing.Size(43, 12);
            this.labelAPIKey.TabIndex = 3;
            this.labelAPIKey.Text = "APIキー";
            // 
            // textAPIKey
            // 
            this.textAPIKey.Location = new System.Drawing.Point(19, 19);
            this.textAPIKey.Margin = new System.Windows.Forms.Padding(2);
            this.textAPIKey.Name = "textAPIKey";
            this.textAPIKey.Size = new System.Drawing.Size(312, 19);
            this.textAPIKey.TabIndex = 2;
            this.textAPIKey.TextChanged += new System.EventHandler(this.TextAPIKey_TextChanged);
            // 
            // ondemandUsageEntryLink
            // 
            this.ondemandUsageEntryLink.AccessibleName = "ondemandUsageEntryLink";
            this.ondemandUsageEntryLink.AutoSize = true;
            this.ondemandUsageEntryLink.BackColor = System.Drawing.SystemColors.Window;
            this.ondemandUsageEntryLink.Location = new System.Drawing.Point(58, 275);
            this.ondemandUsageEntryLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ondemandUsageEntryLink.Name = "ondemandUsageEntryLink";
            this.ondemandUsageEntryLink.Size = new System.Drawing.Size(247, 12);
            this.ondemandUsageEntryLink.TabIndex = 7;
            this.ondemandUsageEntryLink.TabStop = true;
            this.ondemandUsageEntryLink.Text = "Web API従量課金エンドポイントのご利用にあたって";
            this.ondemandUsageEntryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OndemandUsageEntryLink_LinkClicked);
            // 
            // ForceOndemandEndpointDesc
            // 
            this.ForceOndemandEndpointDesc.AutoSize = true;
            this.ForceOndemandEndpointDesc.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.ForceOndemandEndpointDesc.Location = new System.Drawing.Point(20, 112);
            this.ForceOndemandEndpointDesc.Name = "ForceOndemandEndpointDesc";
            this.ForceOndemandEndpointDesc.Size = new System.Drawing.Size(269, 11);
            this.ForceOndemandEndpointDesc.TabIndex = 10;
            this.ForceOndemandEndpointDesc.Text = "常にバフェットコードAPIの従量課金エンドポイントを利用します";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 361);
            this.Controls.Add(this.ondemandUsageEntryLink);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabConrtoll);
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabConrtoll.ResumeLayout(false);
            this.tabDeveloper.ResumeLayout(false);
            this.tabDeveloper.PerformLayout();
            this.tabAPI.ResumeLayout(false);
            this.tabAPI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabConrtoll;
        private System.Windows.Forms.TabPage tabAPI;
        private System.Windows.Forms.Label labelAPIKey;
        private System.Windows.Forms.TextBox textAPIKey;
        private System.Windows.Forms.TabPage tabDeveloper;
        private System.Windows.Forms.CheckBox checkDebugMode;
        private System.Windows.Forms.CheckBox checkIsOndemandEndpointEnabled;
        private System.Windows.Forms.TextBox ondemandModeDesc;
        private System.Windows.Forms.LinkLabel apiSpecialNotesLink;
        private System.Windows.Forms.LinkLabel ondemandUsageEntryLink;
        private System.Windows.Forms.CheckBox checkForceOndemandApiEnabled;
        private System.Windows.Forms.Label DescOndemandEndpointEnabled;
        private System.Windows.Forms.Label ForceOndemandEndpointDesc;
    }

}