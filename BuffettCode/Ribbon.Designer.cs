namespace BuffettCode
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group = this.Factory.CreateRibbonGroup();
            this.leftBox = this.Factory.CreateRibbonBox();
            this.refreshButton = this.Factory.CreateRibbonButton();
            this.csvButton = this.Factory.CreateRibbonButton();
            this.rightBox = this.Factory.CreateRibbonBox();
            this.settingButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group.SuspendLayout();
            this.leftBox.SuspendLayout();
            this.rightBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group
            // 
            this.group.Items.Add(this.leftBox);
            this.group.Items.Add(this.rightBox);
            this.group.Label = "バフェットコード";
            this.group.Name = "group";
            // 
            // leftBox
            // 
            this.leftBox.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.leftBox.Items.Add(this.refreshButton);
            this.leftBox.Items.Add(this.csvButton);
            this.leftBox.Name = "leftBox";
            // 
            // refreshButton
            // 
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.Label = "更新";
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.ShowImage = true;
            this.refreshButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RefreshButton_Click);
            // 
            // csvButton
            // 
            this.csvButton.Image = ((System.Drawing.Image)(resources.GetObject("csvButton.Image")));
            this.csvButton.Label = "CSV出力";
            this.csvButton.Name = "csvButton";
            this.csvButton.ShowImage = true;
            this.csvButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CSVButton_Click);
            // 
            // rightBox
            // 
            this.rightBox.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.rightBox.Items.Add(this.settingButton);
            this.rightBox.Name = "rightBox";
            // 
            // settingButton
            // 
            this.settingButton.Image = ((System.Drawing.Image)(resources.GetObject("settingButton.Image")));
            this.settingButton.Label = "設定";
            this.settingButton.Name = "settingButton";
            this.settingButton.ScreenTip = "APIキーを設定します。";
            this.settingButton.ShowImage = true;
            this.settingButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SettingButton_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.leftBox.ResumeLayout(false);
            this.leftBox.PerformLayout();
            this.rightBox.ResumeLayout(false);
            this.rightBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton settingButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox leftBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton refreshButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton csvButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox rightBox;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon1
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
