namespace DriverSyntax
{
    partial class MainPage
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
            this.btnChSav = new System.Windows.Forms.Button();
            this.ofdSav = new System.Windows.Forms.OpenFileDialog();
            this.bntWriteSyntax = new System.Windows.Forms.Button();
            this.dgVariables = new ADGV.AdvancedDataGridView();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.rbQuestion = new System.Windows.Forms.RadioButton();
            this.rbLoop = new System.Windows.Forms.RadioButton();
            this.grpGridType = new System.Windows.Forms.GroupBox();
            this.rbColumn = new System.Windows.Forms.RadioButton();
            this.rbRow = new System.Windows.Forms.RadioButton();
            this.btnWhiteList = new System.Windows.Forms.Button();
            this.lblFailText = new System.Windows.Forms.Label();
            this.lblFailCount = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).BeginInit();
            this.grpType.SuspendLayout();
            this.grpGridType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChSav
            // 
            this.btnChSav.Location = new System.Drawing.Point(640, 12);
            this.btnChSav.Name = "btnChSav";
            this.btnChSav.Size = new System.Drawing.Size(79, 101);
            this.btnChSav.TabIndex = 0;
            this.btnChSav.Text = ".sav Dosyası Seç";
            this.btnChSav.UseVisualStyleBackColor = true;
            this.btnChSav.Click += new System.EventHandler(this.btnChSav_Click);
            // 
            // ofdSav
            // 
            this.ofdSav.FileName = "openFileDialog1";
            // 
            // bntWriteSyntax
            // 
            this.bntWriteSyntax.Location = new System.Drawing.Point(640, 377);
            this.bntWriteSyntax.Name = "bntWriteSyntax";
            this.bntWriteSyntax.Size = new System.Drawing.Size(79, 103);
            this.bntWriteSyntax.TabIndex = 2;
            this.bntWriteSyntax.Text = "Driver Syntax Yaz";
            this.bntWriteSyntax.UseVisualStyleBackColor = true;
            this.bntWriteSyntax.Click += new System.EventHandler(this.bntWriteSyntax_Click);
            // 
            // dgVariables
            // 
            this.dgVariables.AllowUserToAddRows = false;
            this.dgVariables.AutoGenerateContextFilters = true;
            this.dgVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVariables.DateWithTime = false;
            this.dgVariables.Location = new System.Drawing.Point(12, 6);
            this.dgVariables.Name = "dgVariables";
            this.dgVariables.Size = new System.Drawing.Size(622, 474);
            this.dgVariables.TabIndex = 3;
            this.dgVariables.TimeFilter = false;
            this.dgVariables.SortStringChanged += new System.EventHandler(this.dgVariables_SortStringChanged);
            this.dgVariables.FilterStringChanged += new System.EventHandler(this.dgVariables_FilterStringChanged);
            this.dgVariables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVariables_CellContentClick);
            this.dgVariables.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgVariables_CellFormatting);
            this.dgVariables.Click += new System.EventHandler(this.dgVariables_Click);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rbQuestion);
            this.grpType.Controls.Add(this.rbLoop);
            this.grpType.Location = new System.Drawing.Point(640, 135);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(90, 93);
            this.grpType.TabIndex = 4;
            this.grpType.TabStop = false;
            this.grpType.Text = "MAKE TYPE";
            // 
            // rbQuestion
            // 
            this.rbQuestion.AutoSize = true;
            this.rbQuestion.Location = new System.Drawing.Point(7, 54);
            this.rbQuestion.Name = "rbQuestion";
            this.rbQuestion.Size = new System.Drawing.Size(81, 17);
            this.rbQuestion.TabIndex = 1;
            this.rbQuestion.TabStop = true;
            this.rbQuestion.Text = "QUESTION";
            this.rbQuestion.UseVisualStyleBackColor = true;
            // 
            // rbLoop
            // 
            this.rbLoop.AutoSize = true;
            this.rbLoop.Location = new System.Drawing.Point(7, 31);
            this.rbLoop.Name = "rbLoop";
            this.rbLoop.Size = new System.Drawing.Size(54, 17);
            this.rbLoop.TabIndex = 0;
            this.rbLoop.TabStop = true;
            this.rbLoop.Text = "LOOP";
            this.rbLoop.UseVisualStyleBackColor = true;
            this.rbLoop.CheckedChanged += new System.EventHandler(this.rbLoop_CheckedChanged);
            // 
            // grpGridType
            // 
            this.grpGridType.Controls.Add(this.rbColumn);
            this.grpGridType.Controls.Add(this.rbRow);
            this.grpGridType.Location = new System.Drawing.Point(640, 234);
            this.grpGridType.Name = "grpGridType";
            this.grpGridType.Size = new System.Drawing.Size(90, 93);
            this.grpGridType.TabIndex = 5;
            this.grpGridType.TabStop = false;
            this.grpGridType.Text = "MULTIGRID TYPE";
            // 
            // rbColumn
            // 
            this.rbColumn.AutoSize = true;
            this.rbColumn.Location = new System.Drawing.Point(7, 54);
            this.rbColumn.Name = "rbColumn";
            this.rbColumn.Size = new System.Drawing.Size(71, 17);
            this.rbColumn.TabIndex = 1;
            this.rbColumn.TabStop = true;
            this.rbColumn.Text = "COLUMN";
            this.rbColumn.UseVisualStyleBackColor = true;
            // 
            // rbRow
            // 
            this.rbRow.AutoSize = true;
            this.rbRow.Location = new System.Drawing.Point(7, 31);
            this.rbRow.Name = "rbRow";
            this.rbRow.Size = new System.Drawing.Size(52, 17);
            this.rbRow.TabIndex = 0;
            this.rbRow.TabStop = true;
            this.rbRow.Text = "ROW";
            this.rbRow.UseVisualStyleBackColor = true;
            // 
            // btnWhiteList
            // 
            this.btnWhiteList.Location = new System.Drawing.Point(640, 333);
            this.btnWhiteList.Name = "btnWhiteList";
            this.btnWhiteList.Size = new System.Drawing.Size(79, 38);
            this.btnWhiteList.TabIndex = 6;
            this.btnWhiteList.Text = "White/Black List";
            this.btnWhiteList.UseVisualStyleBackColor = true;
            this.btnWhiteList.Click += new System.EventHandler(this.btnWhiteList_Click);
            // 
            // lblFailText
            // 
            this.lblFailText.AutoSize = true;
            this.lblFailText.Location = new System.Drawing.Point(9, 483);
            this.lblFailText.Name = "lblFailText";
            this.lblFailText.Size = new System.Drawing.Size(39, 13);
            this.lblFailText.TabIndex = 7;
            this.lblFailText.Text = "FAILS:";
            this.lblFailText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblFailText_MouseClick);
            // 
            // lblFailCount
            // 
            this.lblFailCount.AutoSize = true;
            this.lblFailCount.Location = new System.Drawing.Point(50, 483);
            this.lblFailCount.Name = "lblFailCount";
            this.lblFailCount.Size = new System.Drawing.Size(0, 13);
            this.lblFailCount.TabIndex = 8;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(599, 483);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(31, 13);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "0.0.2";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 500);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblFailCount);
            this.Controls.Add(this.lblFailText);
            this.Controls.Add(this.btnWhiteList);
            this.Controls.Add(this.grpGridType);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.dgVariables);
            this.Controls.Add(this.bntWriteSyntax);
            this.Controls.Add(this.btnChSav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPage";
            this.Text = "DriverSyntax";
            this.Load += new System.EventHandler(this.MainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).EndInit();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.grpGridType.ResumeLayout(false);
            this.grpGridType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChSav;
        private System.Windows.Forms.OpenFileDialog ofdSav;
        private System.Windows.Forms.Button bntWriteSyntax;
        private ADGV.AdvancedDataGridView dgVariables;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton rbQuestion;
        private System.Windows.Forms.RadioButton rbLoop;
        private System.Windows.Forms.GroupBox grpGridType;
        private System.Windows.Forms.RadioButton rbColumn;
        private System.Windows.Forms.RadioButton rbRow;
        private System.Windows.Forms.Button btnWhiteList;
        private System.Windows.Forms.Label lblFailText;
        private System.Windows.Forms.Label lblFailCount;
        private System.Windows.Forms.Label lblVersion;
    }
}