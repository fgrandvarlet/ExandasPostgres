using System;
using System.Windows.Forms;

using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Forms;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Components
{
    public partial class ComparisonSetListPanel : ListPanel
    {
        DataGridViewTextBoxColumn uidColumn;
        DataGridViewLinkColumn nameColumn;
        DataGridViewTextBoxColumn connection1Column;
        DataGridViewTextBoxColumn connection2Column;
        DataGridViewTextBoxColumn database1Column;
        DataGridViewTextBoxColumn database2Column;
        DataGridViewTextBoxColumn lastReportTimeColumn;

        public ComparisonSetListPanel()
        {
            InitializeComponent();

            this.titleLabel.Text = Strings.ComparisonSetList;
        }

        protected override void InitMainDataGridView()
        {
            uidColumn = new DataGridViewTextBoxColumn
            {
                Name = "uid",
                DataPropertyName = "uid",
                HeaderText = Strings.ComparisonSetID,
                Visible = false
            };

            nameColumn = new DataGridViewLinkColumn
            {
                Name = "name",
                DataPropertyName = "name",
                HeaderText = Strings.Name,
                SortMode = DataGridViewColumnSortMode.Automatic,
                TrackVisitedState = false,
                LinkBehavior = LinkBehavior.HoverUnderline,
                Width = 240
            };

            connection1Column = new DataGridViewTextBoxColumn
            {
                Name = "connection1",
                DataPropertyName = "connection1",
                HeaderText = Strings.SourceConnection,
                Width = 150
            };

            connection2Column = new DataGridViewTextBoxColumn
            {
                Name = "connection2",
                DataPropertyName = "connection2",
                HeaderText = Strings.TargetConnection,
                Width = 150
            };

            database1Column = new DataGridViewTextBoxColumn
            {
                Name = "database1",
                DataPropertyName = "database1",
                HeaderText = Strings.SourceDatabase,
                Width = 160
            };

            database2Column = new DataGridViewTextBoxColumn
            {
                Name = "database2",
                DataPropertyName = "database2",
                HeaderText = Strings.TargetDatabase,
                Width = 160
            };

            lastReportTimeColumn = new DataGridViewTextBoxColumn
            {
                Name = "last_report_time",
                DataPropertyName = "last_report_time",
                HeaderText = Strings.LastReportTime,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewColumn[] cols = { uidColumn, nameColumn, connection1Column, connection2Column, database1Column, database2Column, lastReportTimeColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        protected override void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetComparisonSetDao().GetDataTable(criteria);
        }

        protected override void AddToolStripButton_Click(object sender, EventArgs e)
        {
            using (var frmInserting = new ComparisonSetForm(null))
            {
                DialogResult drInserting = frmInserting.ShowDialog(this);
                if (drInserting == DialogResult.OK)
                {
                    using (var frmUpdating = new ComparisonSetForm(frmInserting.ComparisonSet))
                    {
                        DialogResult drUpdating = frmUpdating.ShowDialog(this);
                    }
                    RunLookup();
                }
            }
        }

        protected override void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count <= 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                var uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                ComparisonSet cs = DaoFactory.Instance.GetComparisonSetDao().Get(uid);
                using (var frm = new ComparisonSetForm(cs))
                {
                    DialogResult dr = frm.ShowDialog(this);
                    RunLookup();
                }
            }
        }

        protected override void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count == 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                try
                {
                    var uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                    ComparisonSet cs = DaoFactory.Instance.GetComparisonSetDao().Get(uid);

                    string record = string.Format("[ {0} ]", cs.Name);
                    if (Defs.ConfirmDeleteDialog(record))
                    {
                        DaoFactory.Instance.GetComparisonSetDao().Delete(cs);
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
