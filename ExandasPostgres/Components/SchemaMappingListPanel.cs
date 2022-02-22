using System;
using System.Windows.Forms;

using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Forms;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Components
{
    public partial class SchemaMappingListPanel : ListPanel
    {
        DataGridViewTextBoxColumn uidColumn;
        DataGridViewLinkColumn schema1Column;
        DataGridViewTextBoxColumn schema2Column;
        ComparisonSet _comparisonSet;

        public SchemaMappingListPanel(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.titleLabel.BorderStyle = BorderStyle.None;
            this.topPanel.Height = 32;

            this._comparisonSet = comparisonSet;

            // localization
            this.titleLabel.Text = Strings.ListSchemaMappings;
        }

        public ComparisonSet ComparisonSet
        {
            set { _comparisonSet = value; }
        }

        protected override void InitMainDataGridView()
        {
            uidColumn = new DataGridViewTextBoxColumn
            {
                Name = "uid",
                DataPropertyName = "uid",
                HeaderText = Strings.SchemaMappingID,
                Visible = false
            };

            schema1Column = new DataGridViewLinkColumn
            {
                Name = "schema1",
                DataPropertyName = "schema1",
                HeaderText = Strings.SourceSchema,
                SortMode = DataGridViewColumnSortMode.Automatic,
                TrackVisitedState = false,
                LinkBehavior = LinkBehavior.HoverUnderline,
                Width = 400
            };

            schema2Column = new DataGridViewTextBoxColumn
            {
                Name = "schema2",
                DataPropertyName = "schema2",
                HeaderText = Strings.TargetSchema,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewColumn[] cols = { uidColumn, schema1Column, schema2Column};
            mainDataGridView.Columns.AddRange(cols);
        }

        protected override void LoadData()
        {
            var criteria = new Criteria
            {
                Entity = this._comparisonSet
            };
            LoadData(criteria);
        }

        protected override void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetSchemaMappingDao().GetDataTable(criteria);
        }

        protected override void RunLookup()
        {
            var criteria = new Criteria
            {
                Entity = this._comparisonSet
            };
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                criteria.Text = current;
            }
            LoadData(criteria);
        }

        protected override void AddToolStripButton_Click(object sender, EventArgs e)
        {
            using (var frm = new SchemaMappingForm(null, this._comparisonSet))
            {
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) RunLookup();
            }
        }

        protected override void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count == 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                Guid uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                SchemaMapping sm = DaoFactory.Instance.GetSchemaMappingDao().Get(uid);
                using (var frm = new SchemaMappingForm(sm, this._comparisonSet))
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK) RunLookup();
                }
            }
        }

        protected override void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count == 0) return;
            if (mainDataGridView.Rows.Count == 1)
            {
                MessageBox.Show(
                    Strings.UnableToDeleteLastSchemaMapping,
                    Strings.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                return;
            }
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                try
                {
                    Guid uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                    SchemaMapping sm = DaoFactory.Instance.GetSchemaMappingDao().Get(uid);

                    string record = string.Format("[ {0} ]", sm.ToString());
                    if (Defs.ConfirmDeleteDialog(record))
                    {
                        DaoFactory.Instance.GetSchemaMappingDao().Delete(sm);
                        RunLookup();
                    }
                }
                catch (Exception ex)
                {
                    Defs.ErrorDialog(ex.Message);
                }
            }
        }

    }
}
