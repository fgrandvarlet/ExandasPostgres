
namespace ExandasPostgres.Components
{
    partial class DeltaReportListPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.topPanel = new System.Windows.Forms.Panel();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.actionToolStrip = new System.Windows.Forms.ToolStrip();
            this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.modifyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lookupToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lookupTimer = new System.Windows.Forms.Timer(this.components);
            this.fillPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.actionToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(660, 8);
            this.topPanel.TabIndex = 2;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.mainDataGridView);
            this.fillPanel.Controls.Add(this.actionToolStrip);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 8);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(660, 254);
            this.fillPanel.TabIndex = 3;
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.AllowUserToAddRows = false;
            this.mainDataGridView.AllowUserToDeleteRows = false;
            this.mainDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.mainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.GridColor = System.Drawing.Color.LightGray;
            this.mainDataGridView.Location = new System.Drawing.Point(0, 25);
            this.mainDataGridView.MultiSelect = false;
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.ReadOnly = true;
            this.mainDataGridView.RowTemplate.Height = 25;
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(660, 229);
            this.mainDataGridView.TabIndex = 1;
            // 
            // actionToolStrip
            // 
            this.actionToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripButton,
            this.modifyToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator1,
            this.lookupToolStripComboBox,
            this.refreshToolStripButton});
            this.actionToolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionToolStrip.Name = "actionToolStrip";
            this.actionToolStrip.Size = new System.Drawing.Size(660, 25);
            this.actionToolStrip.TabIndex = 0;
            this.actionToolStrip.Text = "toolStrip1";
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToolStripButton.Image = global::ExandasPostgres.Properties.Resources.B16x16_Folder_Inbox;
            this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addToolStripButton.Text = "addToolStripButton";
            this.addToolStripButton.ToolTipText = "Ajouter";
            // 
            // modifyToolStripButton
            // 
            this.modifyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modifyToolStripButton.Image = global::ExandasPostgres.Properties.Resources.B16x16_Edit;
            this.modifyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifyToolStripButton.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.modifyToolStripButton.Name = "modifyToolStripButton";
            this.modifyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.modifyToolStripButton.Text = "modifyToolStripButton";
            this.modifyToolStripButton.ToolTipText = "Modifier";
            this.modifyToolStripButton.Click += new System.EventHandler(this.ModifyToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::ExandasPostgres.Properties.Resources.B16x16_Cut;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "deleteToolStripButton";
            this.deleteToolStripButton.ToolTipText = "Supprimer";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lookupToolStripComboBox
            // 
            this.lookupToolStripComboBox.Name = "lookupToolStripComboBox";
            this.lookupToolStripComboBox.Size = new System.Drawing.Size(180, 25);
            this.lookupToolStripComboBox.Leave += new System.EventHandler(this.LookupToolStripComboBox_Leave);
            this.lookupToolStripComboBox.TextChanged += new System.EventHandler(this.LookupToolStripComboBox_TextChanged);
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::ExandasPostgres.Properties.Resources.refresh;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "refreshToolStripButton";
            this.refreshToolStripButton.ToolTipText = "Rafraîchir";
            this.refreshToolStripButton.Click += new System.EventHandler(this.RefreshToolStripButton_Click);
            // 
            // lookupTimer
            // 
            this.lookupTimer.Interval = 200;
            this.lookupTimer.Tick += new System.EventHandler(this.LookupTimer_Tick);
            // 
            // DeltaReportListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "DeltaReportListPanel";
            this.Size = new System.Drawing.Size(660, 262);
            this.Load += new System.EventHandler(this.DeltaReportListPanel_Load);
            this.fillPanel.ResumeLayout(false);
            this.fillPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.actionToolStrip.ResumeLayout(false);
            this.actionToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.ToolStrip actionToolStrip;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
        private System.Windows.Forms.ToolStripButton modifyToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox lookupToolStripComboBox;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.Timer lookupTimer;
    }
}
