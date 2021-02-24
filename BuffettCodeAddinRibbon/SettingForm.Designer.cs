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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabConrtoll = new System.Windows.Forms.TabControl();
            this.tabAPI = new System.Windows.Forms.TabPage();
            this.tabDeveloper = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textAPIKey = new System.Windows.Forms.TextBox();
            this.checkDebugMode = new System.Windows.Forms.CheckBox();
            this.checkParallelism = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textParallelism = new System.Windows.Forms.TextBox();
            this.tabConrtoll.SuspendLayout();
            this.tabAPI.SuspendLayout();
            this.tabDeveloper.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(425, 489);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 30);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(532, 489);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 30);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // tabConrtoll
            // 
            this.tabConrtoll.Controls.Add(this.tabAPI);
            this.tabConrtoll.Controls.Add(this.tabDeveloper);
            this.tabConrtoll.Location = new System.Drawing.Point(30, 29);
            this.tabConrtoll.Name = "tabConrtoll";
            this.tabConrtoll.SelectedIndex = 0;
            this.tabConrtoll.Size = new System.Drawing.Size(592, 437);
            this.tabConrtoll.TabIndex = 6;
            // 
            // tabAPI
            // 
            this.tabAPI.Controls.Add(this.label1);
            this.tabAPI.Controls.Add(this.textAPIKey);
            this.tabAPI.Location = new System.Drawing.Point(4, 28);
            this.tabAPI.Name = "tabAPI";
            this.tabAPI.Padding = new System.Windows.Forms.Padding(3);
            this.tabAPI.Size = new System.Drawing.Size(584, 405);
            this.tabAPI.TabIndex = 0;
            this.tabAPI.Text = "API";
            this.tabAPI.UseVisualStyleBackColor = true;
            // 
            // tabDeveloper
            // 
            this.tabDeveloper.Controls.Add(this.textParallelism);
            this.tabDeveloper.Controls.Add(this.label2);
            this.tabDeveloper.Controls.Add(this.checkParallelism);
            this.tabDeveloper.Controls.Add(this.checkDebugMode);
            this.tabDeveloper.Location = new System.Drawing.Point(4, 28);
            this.tabDeveloper.Name = "tabDeveloper";
            this.tabDeveloper.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeveloper.Size = new System.Drawing.Size(584, 405);
            this.tabDeveloper.TabIndex = 1;
            this.tabDeveloper.Text = "開発者向けオプション";
            this.tabDeveloper.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "APIキー";
            // 
            // textAPIKey
            // 
            this.textAPIKey.Location = new System.Drawing.Point(31, 60);
            this.textAPIKey.Name = "textAPIKey";
            this.textAPIKey.Size = new System.Drawing.Size(518, 25);
            this.textAPIKey.TabIndex = 2;
            // 
            // checkDebugMode
            // 
            this.checkDebugMode.AutoSize = true;
            this.checkDebugMode.Location = new System.Drawing.Point(30, 111);
            this.checkDebugMode.Name = "checkDebugMode";
            this.checkDebugMode.Size = new System.Drawing.Size(223, 22);
            this.checkDebugMode.TabIndex = 6;
            this.checkDebugMode.Text = "デバッグモードを有効にする";
            this.checkDebugMode.UseVisualStyleBackColor = true;
            // 
            // checkParallelism
            // 
            this.checkParallelism.AutoSize = true;
            this.checkParallelism.Location = new System.Drawing.Point(30, 39);
            this.checkParallelism.Name = "checkParallelism";
            this.checkParallelism.Size = new System.Drawing.Size(507, 22);
            this.checkParallelism.TabIndex = 7;
            this.checkParallelism.Text = "APIの実行ペースを制限する (変更後にExcelの再起動が必要です)";
            this.checkParallelism.UseVisualStyleBackColor = true;
            this.checkParallelism.CheckedChanged += new System.EventHandler(this.CheckParallelism_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "最大同時実行数";
            // 
            // textParallelism
            // 
            this.textParallelism.Location = new System.Drawing.Point(276, 68);
            this.textParallelism.Name = "textParallelism";
            this.textParallelism.Size = new System.Drawing.Size(100, 25);
            this.textParallelism.TabIndex = 9;
            this.textParallelism.Text = "1";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 542);
            this.Controls.Add(this.tabConrtoll);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Location = new System.Drawing.Point(100, 100);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabConrtoll.ResumeLayout(false);
            this.tabAPI.ResumeLayout(false);
            this.tabAPI.PerformLayout();
            this.tabDeveloper.ResumeLayout(false);
            this.tabDeveloper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabConrtoll;
        private System.Windows.Forms.TabPage tabAPI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAPIKey;
        private System.Windows.Forms.TabPage tabDeveloper;
        private System.Windows.Forms.TextBox textParallelism;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkParallelism;
        private System.Windows.Forms.CheckBox checkDebugMode;
    }
}