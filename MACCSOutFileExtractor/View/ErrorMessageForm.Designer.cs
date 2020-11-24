namespace MACCSOutFileExtractor.View
{
    partial class ErrorMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageForm));
            this.txtErrorMsg = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // txtErrorMsg
            // 
            // 
            // 
            // 
            this.txtErrorMsg.CustomButton.Image = null;
            this.txtErrorMsg.CustomButton.Location = new System.Drawing.Point(352, 2);
            this.txtErrorMsg.CustomButton.Name = "";
            this.txtErrorMsg.CustomButton.Size = new System.Drawing.Size(445, 445);
            this.txtErrorMsg.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtErrorMsg.CustomButton.TabIndex = 1;
            this.txtErrorMsg.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtErrorMsg.CustomButton.UseSelectable = true;
            this.txtErrorMsg.CustomButton.Visible = false;
            this.txtErrorMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorMsg.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtErrorMsg.Lines = new string[0];
            this.txtErrorMsg.Location = new System.Drawing.Point(0, 0);
            this.txtErrorMsg.MaxLength = 32767;
            this.txtErrorMsg.Multiline = true;
            this.txtErrorMsg.Name = "txtErrorMsg";
            this.txtErrorMsg.PasswordChar = '\0';
            this.txtErrorMsg.ReadOnly = true;
            this.txtErrorMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorMsg.SelectedText = "";
            this.txtErrorMsg.SelectionLength = 0;
            this.txtErrorMsg.SelectionStart = 0;
            this.txtErrorMsg.ShortcutsEnabled = true;
            this.txtErrorMsg.Size = new System.Drawing.Size(800, 450);
            this.txtErrorMsg.TabIndex = 3;
            this.txtErrorMsg.UseSelectable = true;
            this.txtErrorMsg.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtErrorMsg.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // ErrorMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtErrorMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorMessageForm";
            this.Text = "Error Message";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtErrorMsg;
    }
}