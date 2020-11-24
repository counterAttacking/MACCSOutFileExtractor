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
    public partial class ErrorMessageForm : Form
    {
        private string msg;

        public ErrorMessageForm(string msg)
        {
            InitializeComponent();

            this.msg = msg;
        }

        private void ErrorMessageForm_Load(object sender, EventArgs e)
        {
            this.ShowMsg();
        }

        private void ShowMsg()
        {
            this.txtErrorMsg.Text = this.msg;
        }
    }
}
