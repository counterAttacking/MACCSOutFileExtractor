namespace MACCSOutFileExtractor.View
{
    partial class BuildCheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildCheckForm));
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.chkFrequency = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnOK);
            this.pnlMain.Controls.Add(this.chkFrequency);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.HorizontalScrollbarBarColor = true;
            this.pnlMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMain.HorizontalScrollbarSize = 10;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(424, 146);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.VerticalScrollbarBarColor = true;
            this.pnlMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMain.VerticalScrollbarSize = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnCancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnCancel.Location = new System.Drawing.Point(254, 79);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 45);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnOK.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnOK.Location = new System.Drawing.Point(88, 79);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 45);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // chkFrequency
            // 
            this.chkFrequency.AutoSize = true;
            this.chkFrequency.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFrequency.Location = new System.Drawing.Point(21, 30);
            this.chkFrequency.Name = "chkFrequency";
            this.chkFrequency.Size = new System.Drawing.Size(138, 24);
            this.chkFrequency.TabIndex = 8;
            this.chkFrequency.Text = "Apply Frequency";
            this.chkFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFrequency.UseVisualStyleBackColor = true;
            // 
            // BuildCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 146);
            this.Controls.Add(this.pnlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildCheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Check";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlMain;
        private MetroFramework.Controls.MetroButton btnOK;
        private MetroFramework.Controls.MetroButton btnCancel;
        private System.Windows.Forms.CheckBox chkFrequency;
    }
}