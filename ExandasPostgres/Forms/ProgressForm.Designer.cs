
namespace ExandasPostgres.Forms
{
    partial class ProgressForm
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.bottomFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.noButton = new System.Windows.Forms.Button();
            this.yesButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.metaDataProgressLabel = new System.Windows.Forms.Label();
            this.metaDataProgressBar = new System.Windows.Forms.ProgressBar();
            this.schemaMappingProgressLabel = new System.Windows.Forms.Label();
            this.schemaMappingProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.topPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.fillPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Window;
            this.topPanel.Controls.Add(this.messageLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(19, 18, 19, 18);
            this.topPanel.Size = new System.Drawing.Size(668, 76);
            this.topPanel.TabIndex = 1;
            // 
            // messageLabel
            // 
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLabel.Location = new System.Drawing.Point(19, 18);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(630, 40);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "messageLabel";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // bottomFlowLayoutPanel
            // 
            this.bottomFlowLayoutPanel.Controls.Add(this.noButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.yesButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.cancelButton);
            this.bottomFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomFlowLayoutPanel.Location = new System.Drawing.Point(0, 299);
            this.bottomFlowLayoutPanel.Name = "bottomFlowLayoutPanel";
            this.bottomFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(668, 55);
            this.bottomFlowLayoutPanel.TabIndex = 2;
            // 
            // noButton
            // 
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.noButton.Location = new System.Drawing.Point(577, 11);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(80, 23);
            this.noButton.TabIndex = 0;
            this.noButton.Text = "Non";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(491, 11);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(80, 23);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "Oui";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(405, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // fillPanel
            // 
            this.fillPanel.BackColor = System.Drawing.SystemColors.Window;
            this.fillPanel.Controls.Add(this.metaDataProgressLabel);
            this.fillPanel.Controls.Add(this.metaDataProgressBar);
            this.fillPanel.Controls.Add(this.schemaMappingProgressLabel);
            this.fillPanel.Controls.Add(this.schemaMappingProgressBar);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 76);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(16);
            this.fillPanel.Size = new System.Drawing.Size(668, 223);
            this.fillPanel.TabIndex = 3;
            // 
            // metaDataProgressLabel
            // 
            this.metaDataProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metaDataProgressLabel.Location = new System.Drawing.Point(16, 104);
            this.metaDataProgressLabel.Name = "metaDataProgressLabel";
            this.metaDataProgressLabel.Size = new System.Drawing.Size(636, 42);
            this.metaDataProgressLabel.TabIndex = 3;
            this.metaDataProgressLabel.Text = "metaDataProgressLabel";
            this.metaDataProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metaDataProgressBar
            // 
            this.metaDataProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metaDataProgressBar.Location = new System.Drawing.Point(16, 81);
            this.metaDataProgressBar.Name = "metaDataProgressBar";
            this.metaDataProgressBar.Size = new System.Drawing.Size(636, 23);
            this.metaDataProgressBar.TabIndex = 2;
            // 
            // schemaMappingProgressLabel
            // 
            this.schemaMappingProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schemaMappingProgressLabel.Location = new System.Drawing.Point(16, 39);
            this.schemaMappingProgressLabel.Name = "schemaMappingProgressLabel";
            this.schemaMappingProgressLabel.Size = new System.Drawing.Size(636, 42);
            this.schemaMappingProgressLabel.TabIndex = 1;
            this.schemaMappingProgressLabel.Text = "schemaMappingProgressLabel";
            this.schemaMappingProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // schemaMappingProgressBar
            // 
            this.schemaMappingProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schemaMappingProgressBar.Location = new System.Drawing.Point(16, 16);
            this.schemaMappingProgressBar.Name = "schemaMappingProgressBar";
            this.schemaMappingProgressBar.Size = new System.Drawing.Size(636, 23);
            this.schemaMappingProgressBar.TabIndex = 0;
            // 
            // mainBackgroundWorker
            // 
            this.mainBackgroundWorker.WorkerReportsProgress = true;
            this.mainBackgroundWorker.WorkerSupportsCancellation = true;
            this.mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MainBackgroundWorker_DoWork);
            this.mainBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MainBackgroundWorker_ProgressChanged);
            this.mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MainBackgroundWorker_RunWorkerCompleted);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 354);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomFlowLayoutPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.topPanel.ResumeLayout(false);
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.fillPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.Label schemaMappingProgressLabel;
        private System.Windows.Forms.ProgressBar schemaMappingProgressBar;
        private System.ComponentModel.BackgroundWorker mainBackgroundWorker;
        private System.Windows.Forms.ProgressBar metaDataProgressBar;
        private System.Windows.Forms.Label metaDataProgressLabel;
    }
}