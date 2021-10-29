namespace DriverSyntax
{
    partial class WhiteList
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
            this.txtWhiteList = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.grpListType = new System.Windows.Forms.GroupBox();
            this.rbBlackList = new System.Windows.Forms.RadioButton();
            this.rbWhiteList = new System.Windows.Forms.RadioButton();
            this.grpListType.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtWhiteList
            // 
            this.txtWhiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWhiteList.Location = new System.Drawing.Point(0, 0);
            this.txtWhiteList.Name = "txtWhiteList";
            this.txtWhiteList.Size = new System.Drawing.Size(827, 20);
            this.txtWhiteList.TabIndex = 0;
            this.txtWhiteList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWhiteList_KeyPress);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(752, 26);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "OK";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // grpListType
            // 
            this.grpListType.Controls.Add(this.rbBlackList);
            this.grpListType.Controls.Add(this.rbWhiteList);
            this.grpListType.Location = new System.Drawing.Point(12, 26);
            this.grpListType.Name = "grpListType";
            this.grpListType.Size = new System.Drawing.Size(236, 39);
            this.grpListType.TabIndex = 2;
            this.grpListType.TabStop = false;
            this.grpListType.Text = "List Type";
            // 
            // rbBlackList
            // 
            this.rbBlackList.AutoSize = true;
            this.rbBlackList.Location = new System.Drawing.Point(132, 19);
            this.rbBlackList.Name = "rbBlackList";
            this.rbBlackList.Size = new System.Drawing.Size(52, 17);
            this.rbBlackList.TabIndex = 1;
            this.rbBlackList.TabStop = true;
            this.rbBlackList.Text = "Black";
            this.rbBlackList.UseVisualStyleBackColor = true;
            // 
            // rbWhiteList
            // 
            this.rbWhiteList.AutoSize = true;
            this.rbWhiteList.Location = new System.Drawing.Point(41, 19);
            this.rbWhiteList.Name = "rbWhiteList";
            this.rbWhiteList.Size = new System.Drawing.Size(53, 17);
            this.rbWhiteList.TabIndex = 0;
            this.rbWhiteList.TabStop = true;
            this.rbWhiteList.Text = "White";
            this.rbWhiteList.UseVisualStyleBackColor = true;
            this.rbWhiteList.CheckedChanged += new System.EventHandler(this.rbWhiteList_CheckedChanged);
            // 
            // WhiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 68);
            this.Controls.Add(this.grpListType);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtWhiteList);
            this.Name = "WhiteList";
            this.Text = "List";
            this.Load += new System.EventHandler(this.WhiteList_Load);
            this.grpListType.ResumeLayout(false);
            this.grpListType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWhiteList;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox grpListType;
        private System.Windows.Forms.RadioButton rbBlackList;
        private System.Windows.Forms.RadioButton rbWhiteList;
    }
}