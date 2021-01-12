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
    public partial class FrequencyInputForm : DockContent
    {
        private string[] colNames;

        public FrequencyInputForm()
        {
            InitializeComponent();

            this.SetColNames();
            this.ShowColNames();
        }

        private void FrequencyInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void SetColNames()
        {
            var names = new List<string>();
            names.Add("No");
            names.Add("File Name");
            names.Add("Frequency Value");

            this.colNames = names.ToArray();
        }

        private void ShowColNames()
        {
            this.dgvFrequency.ColumnCount = this.colNames.Length;
            for (var i = 0; i < this.colNames.Length; i++)
            {
                this.dgvFrequency.Columns[i].Name = this.colNames[i];
                if (this.colNames[i].Equals("No") || this.colNames[i].Equals("File Name"))
                {
                    this.dgvFrequency.Columns[i].ReadOnly = true;
                }
            }
        }

        public void AddOutFiles(List<OutFile> files)
        {
            this.dgvFrequency.Rows.Clear();
            for (var i = 0; i < files.Count; i++)
            {
                this.dgvFrequency.Rows.Add(i + 1, files[i].name);
            }
        }

        public void DeleteAllFiles()
        {
            this.dgvFrequency.Rows.Clear();
        }
    }
}
