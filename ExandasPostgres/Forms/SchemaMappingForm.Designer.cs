
namespace ExandasPostgres.Forms
{
    partial class SchemaMappingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaMappingForm));
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sourceSchemaLabel = new System.Windows.Forms.Label();
            this.sourceSchemaTextBox = new System.Windows.Forms.TextBox();
            this.equalButton = new System.Windows.Forms.Button();
            this.targetSchemaLabel = new System.Windows.Forms.Label();
            this.targetSchemaTextBox = new System.Windows.Forms.TextBox();
            this.fillPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(683, 70);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 174);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(683, 83);
            this.bottomPanel.TabIndex = 11;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.tableLayoutPanel1);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 70);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(16, 8, 16, 16);
            this.fillPanel.Size = new System.Drawing.Size(683, 104);
            this.fillPanel.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanel1.Controls.Add(this.sourceSchemaLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sourceSchemaTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.equalButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.targetSchemaLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetSchemaTextBox, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(651, 66);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // sourceSchemaLabel
            // 
            this.sourceSchemaLabel.AutoSize = true;
            this.sourceSchemaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceSchemaLabel.Location = new System.Drawing.Point(3, 0);
            this.sourceSchemaLabel.Name = "sourceSchemaLabel";
            this.sourceSchemaLabel.Size = new System.Drawing.Size(267, 33);
            this.sourceSchemaLabel.TabIndex = 0;
            this.sourceSchemaLabel.Text = "Schéma source";
            this.sourceSchemaLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // sourceSchemaTextBox
            // 
            this.sourceSchemaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceSchemaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.sourceSchemaTextBox.Location = new System.Drawing.Point(3, 36);
            this.sourceSchemaTextBox.MaxLength = 63;
            this.sourceSchemaTextBox.Name = "sourceSchemaTextBox";
            this.sourceSchemaTextBox.Size = new System.Drawing.Size(267, 23);
            this.sourceSchemaTextBox.TabIndex = 1;
            // 
            // equalButton
            // 
            this.equalButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.equalButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equalButton.Location = new System.Drawing.Point(276, 36);
            this.equalButton.Name = "equalButton";
            this.equalButton.Size = new System.Drawing.Size(98, 23);
            this.equalButton.TabIndex = 2;
            this.equalButton.Text = "=";
            this.equalButton.UseVisualStyleBackColor = true;
            this.equalButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // targetSchemaLabel
            // 
            this.targetSchemaLabel.AutoSize = true;
            this.targetSchemaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetSchemaLabel.Location = new System.Drawing.Point(380, 0);
            this.targetSchemaLabel.Name = "targetSchemaLabel";
            this.targetSchemaLabel.Size = new System.Drawing.Size(268, 33);
            this.targetSchemaLabel.TabIndex = 3;
            this.targetSchemaLabel.Text = "Schéma cible";
            this.targetSchemaLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // targetSchemaTextBox
            // 
            this.targetSchemaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetSchemaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.targetSchemaTextBox.Location = new System.Drawing.Point(380, 36);
            this.targetSchemaTextBox.MaxLength = 63;
            this.targetSchemaTextBox.Name = "targetSchemaTextBox";
            this.targetSchemaTextBox.Size = new System.Drawing.Size(268, 23);
            this.targetSchemaTextBox.TabIndex = 4;
            // 
            // SchemaMappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 257);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SchemaMappingForm";
            this.Text = "SchemaMappingForm";
            this.Load += new System.EventHandler(this.SchemaMappingForm_Load);
            this.fillPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label sourceSchemaLabel;
        private System.Windows.Forms.TextBox sourceSchemaTextBox;
        private System.Windows.Forms.Button equalButton;
        private System.Windows.Forms.Label targetSchemaLabel;
        private System.Windows.Forms.TextBox targetSchemaTextBox;
    }
}