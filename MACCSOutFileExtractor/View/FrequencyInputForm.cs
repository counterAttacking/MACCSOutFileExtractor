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

        private void DgvFrequency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.V)
            {
                // DataGridView의 Frequency Value 열에 값을 복사 붙여넣기 해야하기에
                var targetColIdx = 0;
                for (var i = 0; i < this.colNames.Length; i++)
                {
                    if (this.colNames[i].Equals("Frequency Value"))
                    {
                        targetColIdx = i;
                    }
                }

                var copiedData = Clipboard.GetText().Split(new string[] { "\r\n" }, StringSplitOptions.None).ToArray();
                // copiedData.Length - 1 을 하는 이유는 Split()하면서 배열의 제일 마지막에 값이 하나 더 생성이 되기 때문에
                for (var i = 0; i < copiedData.Length - 1; i++)
                {
                    this.dgvFrequency[targetColIdx, i].Value = copiedData[i];
                }
            }
        }

        private void SetColNames()
        {
            var names = new List<string>
            {
                "No",
                "File Name",
                "Frequency Value"
            };

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

        public DataGridView GetDgvFrequency() => this.dgvFrequency;
    }
}
