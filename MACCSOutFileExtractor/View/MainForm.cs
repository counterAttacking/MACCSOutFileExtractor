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
        private FrequencyInputForm frmFrequencyInput;
        private BuildCheckForm frmBuildCheck;

        public MainForm()
        {
            InitializeComponent();

            this.frmFileExplorer = new FileExplorerForm();
            this.frmFrequencyInput = new FrequencyInputForm();
            this.frmBuildCheck = new BuildCheckForm();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.frmFileExplorer.Show(this.dockPnlMain, DockState.DockLeft);
            this.frmFrequencyInput.Show(this.dockPnlMain, DockState.DockLeftAutoHide);
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
            this.frmFrequencyInput.AddOutFiles(fileOpenService.GetFiles());
        }

        private void MsiDeleteAllFiles_Click(object sender, EventArgs e)
        {
            var fileOpenService = OutFileOpenService.GetOutFileOpenService;
            fileOpenService.ClearList();
            this.frmFileExplorer.DeleteAllFiles();
            this.frmFrequencyInput.DeleteAllFiles();
        }

        private void MsiShowInputFileList_Click(object sender, EventArgs e)
        {
            this.frmFileExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void MsiShowFrequencyInput_Click(object sender, EventArgs e)
        {
            this.frmFrequencyInput.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void MsiRun_Click(object sender, EventArgs e)
        {
            var outFiles = OutFileOpenService.GetOutFileOpenService.GetFiles().ToArray();
            if (outFiles.Length <= 0)
            {
                MessageBox.Show("There is no out file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.frmBuildCheck.ShowDialog();
            if (this.frmBuildCheck.GetIsClicked == false)
            {
                return;
            }
            else if (this.frmBuildCheck.GetIsClicked == true)
            {
                var extractManager = new ExtractManager(outFiles);
                extractManager.Run();
            }
        }
    }
}
