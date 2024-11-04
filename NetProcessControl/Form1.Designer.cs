namespace NetProcessControlWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox listBoxProcesses;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Button buttonToggle;
        private System.Windows.Forms.Label labelSelectedProcess;
        private System.Windows.Forms.Label labelStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.listBoxProcesses = new System.Windows.Forms.ListBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.buttonToggle = new System.Windows.Forms.Button();
            this.labelSelectedProcess = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxProcesses
            // 
            this.listBoxProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxProcesses.FormattingEnabled = true;
            this.listBoxProcesses.Location = new System.Drawing.Point(12, 41);
            this.listBoxProcesses.Name = "listBoxProcesses";
            this.listBoxProcesses.Size = new System.Drawing.Size(300, 368);
            this.listBoxProcesses.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(76, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(236, 20);
            this.textBoxSearch.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(12, 15);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(58, 13);
            this.labelSearch.TabIndex = 2;
            this.labelSearch.Text = "Recherche :";
            // 
            // buttonToggle
            // 
            this.buttonToggle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonToggle.Location = new System.Drawing.Point(318, 98);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(150, 50);
            this.buttonToggle.TabIndex = 3;
            this.buttonToggle.Text = "OFF";
            this.buttonToggle.UseVisualStyleBackColor = true;
            // 
            // labelSelectedProcess
            // 
            this.labelSelectedProcess.AutoSize = true;
            this.labelSelectedProcess.Location = new System.Drawing.Point(318, 41);
            this.labelSelectedProcess.Name = "labelSelectedProcess";
            this.labelSelectedProcess.Size = new System.Drawing.Size(150, 13);
            this.labelSelectedProcess.TabIndex = 4;
            this.labelSelectedProcess.Text = "Processus sélectionné : Aucun";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 422);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 5;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(480, 450);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelSelectedProcess);
            this.Controls.Add(this.buttonToggle);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.listBoxProcesses);
            this.MinimumSize = new System.Drawing.Size(496, 489);
            this.Name = "Form1";
            this.Text = "NetProcessControl";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
