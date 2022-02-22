﻿
namespace ExandasPostgres.Forms
{
    partial class ComparisonSetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComparisonSetForm));
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.schemaMappingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comparisonSetUserControl1 = new ExandasPostgres.Components.ComparisonSetUserControl();
            this.comparisonSetUserControl2 = new ExandasPostgres.Components.ComparisonSetUserControl();
            this.innerBottomPanel = new System.Windows.Forms.Panel();
            this.generateReportButton = new System.Windows.Forms.Button();
            this.innerTopPanel = new System.Windows.Forms.Panel();
            this.filteringButton = new System.Windows.Forms.Button();
            this.lastReportTimeTextBox = new System.Windows.Forms.TextBox();
            this.lastReportTimeLabel = new System.Windows.Forms.Label();
            this.deltaReportButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.fillPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.innerBottomPanel.SuspendLayout();
            this.innerTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(949, 52);
            this.topPanel.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 624);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(949, 48);
            this.bottomPanel.TabIndex = 2;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.schemaMappingPanel);
            this.fillPanel.Controls.Add(this.tableLayoutPanel1);
            this.fillPanel.Controls.Add(this.innerBottomPanel);
            this.fillPanel.Controls.Add(this.innerTopPanel);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 52);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(8, 24, 16, 0);
            this.fillPanel.Size = new System.Drawing.Size(949, 572);
            this.fillPanel.TabIndex = 3;
            this.fillPanel.TabStop = true;
            // 
            // schemaMappingPanel
            // 
            this.schemaMappingPanel.BackColor = System.Drawing.Color.LightCoral;
            this.schemaMappingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaMappingPanel.Location = new System.Drawing.Point(8, 336);
            this.schemaMappingPanel.Name = "schemaMappingPanel";
            this.schemaMappingPanel.Padding = new System.Windows.Forms.Padding(16, 8, 16, 0);
            this.schemaMappingPanel.Size = new System.Drawing.Size(925, 155);
            this.schemaMappingPanel.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.comparisonSetUserControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comparisonSetUserControl2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 104);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(925, 232);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // comparisonSetUserControl1
            // 
            this.comparisonSetUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.comparisonSetUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparisonSetUserControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comparisonSetUserControl1.Location = new System.Drawing.Point(3, 3);
            this.comparisonSetUserControl1.Name = "comparisonSetUserControl1";
            this.comparisonSetUserControl1.Padding = new System.Windows.Forms.Padding(8, 16, 8, 0);
            this.comparisonSetUserControl1.Size = new System.Drawing.Size(456, 226);
            this.comparisonSetUserControl1.TabIndex = 0;
            // 
            // comparisonSetUserControl2
            // 
            this.comparisonSetUserControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.comparisonSetUserControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparisonSetUserControl2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comparisonSetUserControl2.Location = new System.Drawing.Point(465, 3);
            this.comparisonSetUserControl2.Name = "comparisonSetUserControl2";
            this.comparisonSetUserControl2.Padding = new System.Windows.Forms.Padding(8, 16, 8, 0);
            this.comparisonSetUserControl2.Size = new System.Drawing.Size(457, 226);
            this.comparisonSetUserControl2.TabIndex = 1;
            // 
            // innerBottomPanel
            // 
            this.innerBottomPanel.Controls.Add(this.generateReportButton);
            this.innerBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.innerBottomPanel.Location = new System.Drawing.Point(8, 491);
            this.innerBottomPanel.Name = "innerBottomPanel";
            this.innerBottomPanel.Size = new System.Drawing.Size(925, 81);
            this.innerBottomPanel.TabIndex = 3;
            // 
            // generateReportButton
            // 
            this.generateReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generateReportButton.Image = global::ExandasPostgres.Properties.Resources.catchuprelease_rls;
            this.generateReportButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.generateReportButton.Location = new System.Drawing.Point(2, 20);
            this.generateReportButton.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(919, 42);
            this.generateReportButton.TabIndex = 10;
            this.generateReportButton.Text = "Générer le rapport de comparaison";
            this.generateReportButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.GenerateReportButton_Click);
            // 
            // innerTopPanel
            // 
            this.innerTopPanel.Controls.Add(this.filteringButton);
            this.innerTopPanel.Controls.Add(this.lastReportTimeTextBox);
            this.innerTopPanel.Controls.Add(this.lastReportTimeLabel);
            this.innerTopPanel.Controls.Add(this.deltaReportButton);
            this.innerTopPanel.Controls.Add(this.nameTextBox);
            this.innerTopPanel.Controls.Add(this.nameLabel);
            this.innerTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.innerTopPanel.Location = new System.Drawing.Point(8, 24);
            this.innerTopPanel.Name = "innerTopPanel";
            this.innerTopPanel.Size = new System.Drawing.Size(925, 80);
            this.innerTopPanel.TabIndex = 2;
            // 
            // filteringButton
            // 
            this.filteringButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filteringButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteringButton.Location = new System.Drawing.Point(475, 37);
            this.filteringButton.Name = "filteringButton";
            this.filteringButton.Size = new System.Drawing.Size(220, 23);
            this.filteringButton.TabIndex = 3;
            this.filteringButton.Text = "Paramètres de filtrage";
            this.filteringButton.UseVisualStyleBackColor = true;
            this.filteringButton.Click += new System.EventHandler(this.FilteringButton_Click);
            // 
            // lastReportTimeTextBox
            // 
            this.lastReportTimeTextBox.Location = new System.Drawing.Point(185, 38);
            this.lastReportTimeTextBox.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.lastReportTimeTextBox.MaxLength = 64;
            this.lastReportTimeTextBox.Name = "lastReportTimeTextBox";
            this.lastReportTimeTextBox.ReadOnly = true;
            this.lastReportTimeTextBox.Size = new System.Drawing.Size(240, 23);
            this.lastReportTimeTextBox.TabIndex = 2;
            this.lastReportTimeTextBox.TabStop = false;
            // 
            // lastReportTimeLabel
            // 
            this.lastReportTimeLabel.Location = new System.Drawing.Point(0, 41);
            this.lastReportTimeLabel.Name = "lastReportTimeLabel";
            this.lastReportTimeLabel.Size = new System.Drawing.Size(180, 23);
            this.lastReportTimeLabel.TabIndex = 3;
            this.lastReportTimeLabel.Text = "Dernier rapport de comparaison";
            // 
            // deltaReportButton
            // 
            this.deltaReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deltaReportButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deltaReportButton.Location = new System.Drawing.Point(701, 37);
            this.deltaReportButton.Name = "deltaReportButton";
            this.deltaReportButton.Size = new System.Drawing.Size(220, 23);
            this.deltaReportButton.TabIndex = 4;
            this.deltaReportButton.Text = "Consulter le rapport de comparaison";
            this.deltaReportButton.UseVisualStyleBackColor = true;
            this.deltaReportButton.Click += new System.EventHandler(this.DeltaReportButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(185, 0);
            this.nameTextBox.MaxLength = 64;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(736, 23);
            this.nameTextBox.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(0, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(180, 23);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Nom du jeu de comparaison";
            // 
            // ComparisonSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 672);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ComparisonSetForm";
            this.Text = "ComparisonSetForm";
            this.Load += new System.EventHandler(this.ComparisonSetForm_Load);
            this.fillPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.innerBottomPanel.ResumeLayout(false);
            this.innerTopPanel.ResumeLayout(false);
            this.innerTopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.Panel innerTopPanel;
        private System.Windows.Forms.Button filteringButton;
        private System.Windows.Forms.TextBox lastReportTimeTextBox;
        private System.Windows.Forms.Label lastReportTimeLabel;
        private System.Windows.Forms.Button deltaReportButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel innerBottomPanel;
        private System.Windows.Forms.Button generateReportButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Components.ComparisonSetUserControl comparisonSetUserControl1;
        private Components.ComparisonSetUserControl comparisonSetUserControl2;
        private System.Windows.Forms.Panel schemaMappingPanel;
    }
}