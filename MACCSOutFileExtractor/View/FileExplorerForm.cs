using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MACCSOutFileExtractor.View
{
    public partial class FileExplorerForm : DockContent
    {
        public FileExplorerForm()
        {
            InitializeComponent();
        }

        public void AddOutFiles(List<OutFile> files)
        {
            this.tvwFiles.Nodes.Clear();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Out Files");
            stringBuilder.Append(" (");
            stringBuilder.Append(files.Count.ToString());
            stringBuilder.Append(")");
            var fileNode = new TreeNode(stringBuilder.ToString());
            foreach (var file in files)
            {
                fileNode.Nodes.Add(file.path, file.name);
            }
            this.tvwFiles.Nodes.Add(fileNode);
        }

        public void DeleteAllFiles()
        {
            this.tvwFiles.Nodes.Clear();
        }
    }
}
