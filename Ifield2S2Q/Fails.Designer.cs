namespace DriverSyntax
{
    partial class Fails
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
            this.lstbxFails = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstbxFails
            // 
            this.lstbxFails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbxFails.FormattingEnabled = true;
            this.lstbxFails.Location = new System.Drawing.Point(0, 0);
            this.lstbxFails.Name = "lstbxFails";
            this.lstbxFails.Size = new System.Drawing.Size(204, 364);
            this.lstbxFails.TabIndex = 0;
            // 
            // Fails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 364);
            this.Controls.Add(this.lstbxFails);
            this.Name = "Fails";
            this.Text = "Fails";
            this.Load += new System.EventHandler(this.Fails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstbxFails;
    }
}