using MACCSOutFileExtractor.Manager;
using MACCSOutFileExtractor.Service;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MACCSOutFileExtractor.View
{
    public partial class MainForm : Form
    {
        private FileExplorerForm frmFileExplorer;

        public MainForm()
        {
            InitializeComponent();

            this.frmFileExplorer = new FileExplorerForm();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.frmFileExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void MsiOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Out File|*.out",
                Multiselect = true,
            };
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var fileOpenService = OutFileOpenService.GetOutFileOpenService;
            fileOpenService.OpenFile(openFileDialog.FileNames);

            this.frmFileExplorer.AddOutFiles(fileOpenService.GetFiles());
        }

        private void MsiDeleteAllFiles_Click(object sender, EventArgs e)
        {
            var fileOpenService = OutFileOpenService.GetOutFileOpenService;
            fileOpenService.ClearList();
            this.frmFileExplorer.DeleteAllFiles();
        }

        private void MsiShowInputFileList_Click(object sender, EventArgs e)
        {
            this.frmFileExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void MsiRun_Click(object sender, EventArgs e)
        {
            var extractManager = new ExtractManager(OutFileOpenService.GetOutFileOpenService.GetFiles().ToArray());
            extractManager.Run();
        }
    }
}
