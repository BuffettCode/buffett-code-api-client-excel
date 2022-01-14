namespace BuffettCodeAddinRibbon
{
    partial class CsvDownloadForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textTicker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textTo = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupOutput = new System.Windows.Forms.GroupBox();
            this.radioSheet = new System.Windows.Forms.RadioButton();
            this.radioCSV = new System.Windows.Forms.RadioButton();
            this.groupEncoding = new System.Windows.Forms.GroupBox();
            this.radioShiftJIS = new System.Windows.Forms.RadioButton();
            this.radioUTF8 = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupOutput.SuspendLayout();
            this.groupEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "銘柄コード";
            // 
            // textTicker
            // 
            this.textTicker.Location = new System.Drawing.Point(22, 32);
            this.textTicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textTicker.Name = "textTicker";
            this.textTicker.Size = new System.Drawing.Size(72, 19);
            this.textTicker.TabIndex = 1;
            this.textTicker.Text = "6501";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "期間";
            // 
            // textFrom
            // 
            this.textFrom.Location = new System.Drawing.Point(22, 78);
            this.textFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textFrom.Name = "textFrom";
            this.textFrom.Size = new System.Drawing.Size(72, 19);
            this.textFrom.TabIndex = 2;
            this.textFrom.Text = "2016Q1";
            this.textFrom.Validating += new System.ComponentModel.CancelEventHandler(this.TextFrom_Validating);
            this.textFrom.Validated += new System.EventHandler(this.TextFrom_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "～";
            // 
            // textTo
            // 
            this.textTo.Location = new System.Drawing.Point(137, 78);
            this.textTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textTo.Name = "textTo";
            this.textTo.Size = new System.Drawing.Size(72, 19);
            this.textTo.TabIndex = 3;
            this.textTo.Text = "2018Q4";
            this.textTo.Validating += new System.ComponentModel.CancelEventHandler(this.TextTo_Validating);
            this.textTo.Validated += new System.EventHandler(this.TextTo_Validated);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(178, 235);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(55, 20);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "出力";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // groupOutput
            // 
            this.groupOutput.Controls.Add(this.radioSheet);
            this.groupOutput.Controls.Add(this.radioCSV);
            this.groupOutput.Location = new System.Drawing.Point(16, 115);
            this.groupOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupOutput.Name = "groupOutput";
            this.groupOutput.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupOutput.Size = new System.Drawing.Size(217, 41);
            this.groupOutput.TabIndex = 11;
            this.groupOutput.TabStop = false;
            this.groupOutput.Text = "出力先";
            // 
            // radioSheet
            // 
            this.radioSheet.AutoSize = true;
            this.radioSheet.Location = new System.Drawing.Point(121, 16);
            this.radioSheet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioSheet.Name = "radioSheet";
            this.radioSheet.Size = new System.Drawing.Size(82, 16);
            this.radioSheet.TabIndex = 5;
            this.radioSheet.TabStop = true;
            this.radioSheet.Text = "新しいシート";
            this.radioSheet.UseVisualStyleBackColor = true;
            this.radioSheet.CheckedChanged += new System.EventHandler(this.RadioSheet_CheckedChanged);
            // 
            // radioCSV
            // 
            this.radioCSV.AutoSize = true;
            this.radioCSV.Checked = true;
            this.radioCSV.Location = new System.Drawing.Point(5, 16);
            this.radioCSV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioCSV.Name = "radioCSV";
            this.radioCSV.Size = new System.Drawing.Size(80, 16);
            this.radioCSV.TabIndex = 4;
            this.radioCSV.TabStop = true;
            this.radioCSV.Text = "CSVファイル";
            this.radioCSV.UseVisualStyleBackColor = true;
            this.radioCSV.CheckedChanged += new System.EventHandler(this.RadioCSV_CheckedChanged);
            // 
            // groupEncoding
            // 
            this.groupEncoding.Controls.Add(this.radioShiftJIS);
            this.groupEncoding.Controls.Add(this.radioUTF8);
            this.groupEncoding.Location = new System.Drawing.Point(16, 169);
            this.groupEncoding.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupEncoding.Name = "groupEncoding";
            this.groupEncoding.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupEncoding.Size = new System.Drawing.Size(217, 43);
            this.groupEncoding.TabIndex = 12;
            this.groupEncoding.TabStop = false;
            this.groupEncoding.Text = "文字コード";
            // 
            // radioShiftJIS
            // 
            this.radioShiftJIS.AutoSize = true;
            this.radioShiftJIS.Location = new System.Drawing.Point(121, 16);
            this.radioShiftJIS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioShiftJIS.Name = "radioShiftJIS";
            this.radioShiftJIS.Size = new System.Drawing.Size(64, 16);
            this.radioShiftJIS.TabIndex = 7;
            this.radioShiftJIS.Text = "ShiftJIS";
            this.radioShiftJIS.UseVisualStyleBackColor = true;
            // 
            // radioUTF8
            // 
            this.radioUTF8.AutoSize = true;
            this.radioUTF8.Checked = true;
            this.radioUTF8.Location = new System.Drawing.Point(5, 17);
            this.radioUTF8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioUTF8.Name = "radioUTF8";
            this.radioUTF8.Size = new System.Drawing.Size(51, 16);
            this.radioUTF8.TabIndex = 6;
            this.radioUTF8.TabStop = true;
            this.radioUTF8.Text = "UTF8";
            this.radioUTF8.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CsvDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 273);
            this.Controls.Add(this.groupEncoding);
            this.Controls.Add(this.groupOutput);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textTicker);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CsvDownloadForm";
            this.Text = "CSV出力";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CSVForm_FormClosing);
            this.groupOutput.ResumeLayout(false);
            this.groupOutput.PerformLayout();
            this.groupEncoding.ResumeLayout(false);
            this.groupEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textTo;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupOutput;
        private System.Windows.Forms.RadioButton radioSheet;
        private System.Windows.Forms.RadioButton radioCSV;
        private System.Windows.Forms.GroupBox groupEncoding;
        private System.Windows.Forms.RadioButton radioShiftJIS;
        private System.Windows.Forms.RadioButton radioUTF8;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}