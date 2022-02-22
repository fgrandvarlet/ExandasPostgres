
namespace ExandasPostgres.Components
{
    partial class ComparisonSetUserControl
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
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.connectionComboBox = new System.Windows.Forms.ComboBox();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // databaseLabel
            // 
            this.databaseLabel.Location = new System.Drawing.Point(11, 178);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(110, 23);
            this.databaseLabel.TabIndex = 9;
            this.databaseLabel.Text = "Base de données";
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseTextBox.Location = new System.Drawing.Point(127, 175);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.ReadOnly = true;
            this.databaseTextBox.Size = new System.Drawing.Size(169, 23);
            this.databaseTextBox.TabIndex = 10;
            this.databaseTextBox.TabStop = false;
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(127, 146);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.ReadOnly = true;
            this.portTextBox.Size = new System.Drawing.Size(169, 23);
            this.portTextBox.TabIndex = 8;
            this.portTextBox.TabStop = false;
            // 
            // portLabel
            // 
            this.portLabel.Location = new System.Drawing.Point(11, 149);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(110, 23);
            this.portLabel.TabIndex = 7;
            this.portLabel.Text = "Port";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostTextBox.Location = new System.Drawing.Point(127, 117);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.ReadOnly = true;
            this.hostTextBox.Size = new System.Drawing.Size(169, 23);
            this.hostTextBox.TabIndex = 6;
            this.hostTextBox.TabStop = false;
            // 
            // hostLabel
            // 
            this.hostLabel.Location = new System.Drawing.Point(11, 120);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(110, 23);
            this.hostLabel.TabIndex = 5;
            this.hostLabel.Text = "Nom d\'hôte";
            // 
            // userLabel
            // 
            this.userLabel.Location = new System.Drawing.Point(11, 91);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(110, 23);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "Nom utilisateur";
            // 
            // userTextBox
            // 
            this.userTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBox.Location = new System.Drawing.Point(127, 88);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ReadOnly = true;
            this.userTextBox.Size = new System.Drawing.Size(169, 23);
            this.userTextBox.TabIndex = 4;
            this.userTextBox.TabStop = false;
            // 
            // connectionComboBox
            // 
            this.connectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.connectionComboBox.FormattingEnabled = true;
            this.connectionComboBox.Location = new System.Drawing.Point(14, 42);
            this.connectionComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.connectionComboBox.Name = "connectionComboBox";
            this.connectionComboBox.Size = new System.Drawing.Size(282, 23);
            this.connectionComboBox.TabIndex = 2;
            // 
            // connectionLabel
            // 
            this.connectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.connectionLabel.Location = new System.Drawing.Point(11, 16);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(168, 23);
            this.connectionLabel.TabIndex = 1;
            this.connectionLabel.Text = "connectionLabel";
            // 
            // ComparisonSetUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.databaseTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.connectionComboBox);
            this.Controls.Add(this.connectionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ComparisonSetUserControl";
            this.Padding = new System.Windows.Forms.Padding(8, 16, 8, 0);
            this.Size = new System.Drawing.Size(307, 247);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label databaseLabel;
        public System.Windows.Forms.TextBox databaseTextBox;
        public System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label portLabel;
        public System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label userLabel;
        public System.Windows.Forms.TextBox userTextBox;
        public System.Windows.Forms.ComboBox connectionComboBox;
        public System.Windows.Forms.Label connectionLabel;
    }
}
