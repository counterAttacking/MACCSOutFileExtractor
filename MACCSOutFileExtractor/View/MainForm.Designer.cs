namespace MACCSOutFileExtractor.View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.msiOpenFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.editEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiDeleteAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.buildBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiRun = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPnlMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.viewVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiShowInputFileList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.editEToolStripMenuItem,
            this.viewVToolStripMenuItem,
            this.buildBToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // openOToolStripMenuItem
            // 
            this.openOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiOpenFolder,
            this.msiOpenFiles});
            this.openOToolStripMenuItem.Name = "openOToolStripMenuItem";
            this.openOToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openOToolStripMenuItem.Text = "Open(&O)";
            // 
            // msiOpenFolder
            // 
            this.msiOpenFolder.Name = "msiOpenFolder";
            this.msiOpenFolder.Size = new System.Drawing.Size(180, 22);
            this.msiOpenFolder.Text = "Folder(&D)";
            this.msiOpenFolder.Click += new System.EventHandler(this.MsiOpenFolder_Click);
            // 
            // msiOpenFiles
            // 
            this.msiOpenFiles.Name = "msiOpenFiles";
            this.msiOpenFiles.Size = new System.Drawing.Size(180, 22);
            this.msiOpenFiles.Text = "Files(&F)";
            this.msiOpenFiles.Click += new System.EventHandler(this.MsiOpenFiles_Click);
            // 
            // editEToolStripMenuItem
            // 
            this.editEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiDeleteAllFiles});
            this.editEToolStripMenuItem.Name = "editEToolStripMenuItem";
            this.editEToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.editEToolStripMenuItem.Text = "Edit(&E)";
            // 
            // msiDeleteAllFiles
            // 
            this.msiDeleteAllFiles.Name = "msiDeleteAllFiles";
            this.msiDeleteAllFiles.Size = new System.Drawing.Size(180, 22);
            this.msiDeleteAllFiles.Text = "Delete All Files(&D)";
            this.msiDeleteAllFiles.Click += new System.EventHandler(this.MsiDeleteAllFiles_Click);
            // 
            // buildBToolStripMenuItem
            // 
            this.buildBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiRun});
            this.buildBToolStripMenuItem.Name = "buildBToolStripMenuItem";
            this.buildBToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.buildBToolStripMenuItem.Text = "Build(&B)";
            // 
            // msiRun
            // 
            this.msiRun.Name = "msiRun";
            this.msiRun.Size = new System.Drawing.Size(180, 22);
            this.msiRun.Text = "Run(&R)";
            this.msiRun.Click += new System.EventHandler(this.MsiRun_Click);
            // 
            // dockPnlMain
            // 
            this.dockPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPnlMain.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dockPnlMain.Location = new System.Drawing.Point(0, 24);
            this.dockPnlMain.Name = "dockPnlMain";
            this.dockPnlMain.Padding = new System.Windows.Forms.Padding(6);
            this.dockPnlMain.ShowAutoHideContentOnHover = false;
            this.dockPnlMain.Size = new System.Drawing.Size(1008, 515);
            this.dockPnlMain.TabIndex = 2;
            this.dockPnlMain.Theme = this.vS2015DarkTheme1;
            // 
            // viewVToolStripMenuItem
            // 
            this.viewVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiShowInputFileList});
            this.viewVToolStripMenuItem.Name = "viewVToolStripMenuItem";
            this.viewVToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.viewVToolStripMenuItem.Text = "View(&V)";
            // 
            // msiShowInputFileList
            // 
            this.msiShowInputFileList.Name = "msiShowInputFileList";
            this.msiShowInputFileList.Size = new System.Drawing.Size(180, 22);
            this.msiShowInputFileList.Text = "Input File List";
            this.msiShowInputFileList.Click += new System.EventHandler(this.MsiShowInputFileList_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.dockPnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MACCS Out File Extractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPnlMain;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.ToolStripMenuItem editEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem msiOpenFiles;
        private System.Windows.Forms.ToolStripMenuItem msiDeleteAllFiles;
        private System.Windows.Forms.ToolStripMenuItem buildBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiRun;
        private System.Windows.Forms.ToolStripMenuItem viewVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiShowInputFileList;
    }
}