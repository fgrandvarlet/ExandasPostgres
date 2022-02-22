using System;
using System.Windows.Forms;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Components
{
    public partial class DeltaReportListPanel : UserControl
    {
        readonly SchemaMapping _schemaMapping;

        DataGridViewTextBoxColumn idColumn;
        DataGridViewTextBoxColumn entityColumn;
        DataGridViewTextBoxColumn objectColumn;
        DataGridViewTextBoxColumn parentObjectColumn;
        DataGridViewTextBoxColumn labelColumn;
        DataGridViewTextBoxColumn propertyColumn;
        DataGridViewTextBoxColumn sourceColumn;
        DataGridViewTextBoxColumn targetColumn;

        public DeltaReportListPanel(SchemaMapping schemaMapping)
        {
            InitializeComponent();

            this._schemaMapping = schemaMapping;

            this.addToolStripButton.Visible = false;
            this.deleteToolStripButton.Visible = false;
        }

        private void InitMainDataGridView()
        {
            idColumn = new DataGridViewTextBoxColumn
            {
                Name = "id",
                DataPropertyName = "id",
                HeaderText = "ID delta report",
                Visible = false
            };

            entityColumn = new DataGridViewTextBoxColumn
            {
                Name = "entity",
                DataPropertyName = "entity",
                HeaderText = Strings.Entity,
                Width = 200
            };

            objectColumn = new DataGridViewTextBoxColumn
            {
                Name = "object",
                DataPropertyName = "object",
                HeaderText = Strings.ObjectName,
                Width = 250
            };

            parentObjectColumn = new DataGridViewTextBoxColumn
            {
                Name = "parent_object",
                DataPropertyName = "parent_object",
                HeaderText = Strings.ParentObject,
                Width = 180
            };

            labelColumn = new DataGridViewTextBoxColumn
            {
                Name = "label",
                DataPropertyName = "label",
                HeaderText = Strings.Label,
                Width = 200
            };

            propertyColumn = new DataGridViewTextBoxColumn
            {
                Name = "property",
                DataPropertyName = "property",
                HeaderText = Strings.Property,
                Width = 200
            };

            sourceColumn = new DataGridViewTextBoxColumn
            {
                Name = "source",
                DataPropertyName = "source",
                HeaderText = Strings.SourceValue,
                Width = 200
            };

            targetColumn = new DataGridViewTextBoxColumn
            {
                Name = "target",
                DataPropertyName = "target",
                HeaderText = Strings.TargetValue,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewColumn[] cols = { idColumn, entityColumn, objectColumn, parentObjectColumn, labelColumn, propertyColumn, sourceColumn, targetColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        private void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetDeltaReportDao().GetDataTable(criteria);
        }

        private void LoadData()
        {
            var criteria = new Criteria
            {
                Entity = this._schemaMapping
            };
            LoadData(criteria);
        }

        private void DeltaReportListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();
        }

        public void RunLookup()
        {
            var criteria = new Criteria
            {
                Entity = this._schemaMapping
            };
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                criteria.Text = current;
            }
            LoadData(criteria);
        }

        private void LookupToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            lookupTimer.Enabled = true;
        }

        private void LookupToolStripComboBox_Leave(object sender, EventArgs e)
        {
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                if (!lookupToolStripComboBox.Items.Contains(current))
                {
                    lookupToolStripComboBox.Items.Insert(0, current);
                }
            }
        }

        private void LookupTimer_Tick(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            RunLookup();
        }

        private void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                if (!lookupToolStripComboBox.Items.Contains(current))
                {
                    lookupToolStripComboBox.Items.Insert(0, current);
                }
            }
            lookupToolStripComboBox.Text = null;
        }
    }
}
