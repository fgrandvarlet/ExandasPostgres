﻿
namespace ExandasPostgres.Forms
{
    partial class DeltaReportListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeltaReportListForm));
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.bottomFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.doCancelButton = new System.Windows.Forms.Button();
            this.doOkButton = new System.Windows.Forms.Button();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.deltaReportTabControl = new System.Windows.Forms.TabControl();
            this.innerTopPanel = new System.Windows.Forms.Panel();
            this.filteringButton = new System.Windows.Forms.Button();
            this.exportExcelButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.fillPanel.SuspendLayout();
            this.innerTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(799, 52);
            this.topPanel.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.bottomFlowLayoutPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 379);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(799, 73);
            this.bottomPanel.TabIndex = 2;
            // 
            // bottomFlowLayoutPanel
            // 
            this.bottomFlowLayoutPanel.Controls.Add(this.doCancelButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.doOkButton);
            this.bottomFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomFlowLayoutPanel.Name = "bottomFlowLayoutPanel";
            this.bottomFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(799, 73);
            this.bottomFlowLayoutPanel.TabIndex = 0;
            // 
            // doCancelButton
            // 
            this.doCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doCancelButton.Location = new System.Drawing.Point(708, 3);
            this.doCancelButton.Name = "doCancelButton";
            this.doCancelButton.Size = new System.Drawing.Size(80, 23);
            this.doCancelButton.TabIndex = 1;
            this.doCancelButton.Text = "Annuler";
            this.doCancelButton.UseVisualStyleBackColor = true;
            // 
            // doOkButton
            // 
            this.doOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doOkButton.Location = new System.Drawing.Point(622, 3);
            this.doOkButton.Name = "doOkButton";
            this.doOkButton.Size = new System.Drawing.Size(80, 23);
            this.doOkButton.TabIndex = 0;
            this.doOkButton.Text = "OK";
            this.doOkButton.UseVisualStyleBackColor = true;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.deltaReportTabControl);
            this.fillPanel.Controls.Add(this.innerTopPanel);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 52);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(8);
            this.fillPanel.Size = new System.Drawing.Size(799, 327);
            this.fillPanel.TabIndex = 3;
            // 
            // deltaReportTabControl
            // 
            this.deltaReportTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deltaReportTabControl.Location = new System.Drawing.Point(8, 65);
            this.deltaReportTabControl.Name = "deltaReportTabControl";
            this.deltaReportTabControl.SelectedIndex = 0;
            this.deltaReportTabControl.Size = new System.Drawing.Size(783, 254);
            this.deltaReportTabControl.TabIndex = 1;
            // 
            // innerTopPanel
            // 
            this.innerTopPanel.Controls.Add(this.filteringButton);
            this.innerTopPanel.Controls.Add(this.exportExcelButton);
            this.innerTopPanel.Controls.Add(this.titleLabel);
            this.innerTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.innerTopPanel.Location = new System.Drawing.Point(8, 8);
            this.innerTopPanel.Name = "innerTopPanel";
            this.innerTopPanel.Size = new System.Drawing.Size(783, 57);
            this.innerTopPanel.TabIndex = 0;
            // 
            // filteringButton
            // 
            this.filteringButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filteringButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteringButton.Location = new System.Drawing.Point(413, 26);
            this.filteringButton.Name = "filteringButton";
            this.filteringButton.Size = new System.Drawing.Size(180, 23);
            this.filteringButton.TabIndex = 3;
            this.filteringButton.Text = "Paramètres de filtrage";
            this.filteringButton.UseVisualStyleBackColor = true;
            this.filteringButton.Click += new System.EventHandler(this.FilteringButton_Click);
            // 
            // exportExcelButton
            // 
            this.exportExcelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportExcelButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportExcelButton.Location = new System.Drawing.Point(599, 26);
            this.exportExcelButton.Name = "exportExcelButton";
            this.exportExcelButton.Size = new System.Drawing.Size(180, 23);
            this.exportExcelButton.TabIndex = 4;
            this.exportExcelButton.Text = "Exporter au format Excel";
            this.exportExcelButton.UseVisualStyleBackColor = true;
            this.exportExcelButton.Click += new System.EventHandler(this.ExportExcelButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(783, 23);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "titleLabel";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DeltaReportListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 452);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeltaReportListForm";
            this.Text = "DeltaReportListForm";
            this.Load += new System.EventHandler(this.DeltaReportListForm_Load);
            this.bottomPanel.ResumeLayout(false);
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.fillPanel.ResumeLayout(false);
            this.innerTopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
        private System.Windows.Forms.Button doCancelButton;
        private System.Windows.Forms.Button doOkButton;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.TabControl deltaReportTabControl;
        private System.Windows.Forms.Panel innerTopPanel;
        private System.Windows.Forms.Button filteringButton;
        private System.Windows.Forms.Button exportExcelButton;
        public System.Windows.Forms.Label titleLabel;
    }
}