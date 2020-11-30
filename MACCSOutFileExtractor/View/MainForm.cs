using MACCSOutFileExtractor.Service;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        private void MsiOpenFolder_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Cancel)
            {
                return;
            }

            var fileOpenService = OutFileOpenService.GetOutFileOpenService;
            fileOpenService.OpenFile(openFolderDialog.FileName);

            this.frmFileExplorer.AddOutFiles(fileOpenService.GetFiles());
        }

        private void MsiOpenFiles_Click(object sender, EventArgs e)
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

        private void MsiRun_Click(object sender, EventArgs e)
        {
            var service = new OutFileReadService(OutFileOpenService.GetOutFileOpenService.GetFiles().ToArray());
            service.ReadOutFile();
        }
    }
}
