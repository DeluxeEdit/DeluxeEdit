using System.Text;
using System.Linq;
using System;
namespace DeluxeEdit.CustomFileApiFile.Controls
{
    public partial class OpenFileDialogEncoding : OpenFileDialogEx
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
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();


            cmbEncoding = new System.Windows.Forms.ComboBox();
            // 
            // cmbEncoding
            // 
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Location = new System.Drawing.Point(-17, -9);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new System.Drawing.Size(182, 33);
            cmbEncoding.TabIndex = 0;
           
            // 
            // OpenFileDialogEncoding
            // 
            Controls.Add(cmbEncoding);
            Name = "OpenFileDialogEncoding";
            ResumeLayout(false);
        }

        #endregion

        protected System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ComboBox cmbEncoding;
    }
}