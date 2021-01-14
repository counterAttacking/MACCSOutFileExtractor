using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MACCSOutFileExtractor.View
{
    public partial class BuildCheckForm : Form
    {
        private bool isChecked;
        private bool isClicked;

        public BuildCheckForm()
        {
            InitializeComponent();

            this.isChecked = false;
            this.isClicked = false;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.CheckCheckBox();
            this.isClicked = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CheckCheckBox();
            this.isClicked = false;
            this.Close();
        }

        private void CheckCheckBox()
        {
            if (this.chkFrequency.Checked == true)
            {
                this.isChecked = true;
            }
            else
            {
                this.isChecked = false;
            }
        }

        public bool GetIsChecked => this.isChecked;

        public bool GetIsClicked => this.isClicked;
    }
}
