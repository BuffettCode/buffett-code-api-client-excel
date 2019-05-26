namespace BuffettCode
{
    partial class CSVForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textTicker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textTo = new System.Windows.Forms.TextBox();
            this.radioCSV = new System.Windows.Forms.RadioButton();
            this.radioSheet = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "銘柄コード";
            // 
            // textTicker
            // 
            this.textTicker.Location = new System.Drawing.Point(36, 48);
            this.textTicker.Name = "textTicker";
            this.textTicker.Size = new System.Drawing.Size(146, 25);
            this.textTicker.TabIndex = 1;
            this.textTicker.Text = "6501";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "対象四半期の始点";
            // 
            // textFrom
            // 
            this.textFrom.Location = new System.Drawing.Point(36, 117);
            this.textFrom.Name = "textFrom";
            this.textFrom.Size = new System.Drawing.Size(146, 25);
            this.textFrom.TabIndex = 3;
            this.textFrom.Text = "2017Q1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "対象四半期の終点";
            // 
            // textTo
            // 
            this.textTo.Location = new System.Drawing.Point(228, 117);
            this.textTo.Name = "textTo";
            this.textTo.Size = new System.Drawing.Size(147, 25);
            this.textTo.TabIndex = 5;
            this.textTo.Text = "2017Q4";
            // 
            // radioCSV
            // 
            this.radioCSV.AutoSize = true;
            this.radioCSV.Checked = true;
            this.radioCSV.Location = new System.Drawing.Point(36, 181);
            this.radioCSV.Name = "radioCSV";
            this.radioCSV.Size = new System.Drawing.Size(118, 22);
            this.radioCSV.TabIndex = 6;
            this.radioCSV.TabStop = true;
            this.radioCSV.Text = "CSVファイル";
            this.radioCSV.UseVisualStyleBackColor = true;
            this.radioCSV.Visible = false;
            // 
            // radioSheet
            // 
            this.radioSheet.AutoSize = true;
            this.radioSheet.Location = new System.Drawing.Point(228, 180);
            this.radioSheet.Name = "radioSheet";
            this.radioSheet.Size = new System.Drawing.Size(122, 22);
            this.radioSheet.TabIndex = 7;
            this.radioSheet.TabStop = true;
            this.radioSheet.Text = "新しいシート";
            this.radioSheet.UseVisualStyleBackColor = true;
            this.radioSheet.Visible = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(173, 244);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(92, 30);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(283, 244);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 30);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // CSVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 305);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioSheet);
            this.Controls.Add(this.radioCSV);
            this.Controls.Add(this.textTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textTicker);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSVForm";
            this.Text = "CSVForm";
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
        private System.Windows.Forms.RadioButton radioCSV;
        private System.Windows.Forms.RadioButton radioSheet;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}