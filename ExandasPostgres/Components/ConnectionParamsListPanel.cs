using System;
using System.Windows.Forms;

using ExandasPostgres.Core;
using ExandasPostgres.Domain;
using ExandasPostgres.Dao;
using ExandasPostgres.Forms;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Components
{
    public partial class ConnectionParamsListPanel : ListPanel
    {
        DataGridViewTextBoxColumn uidColumn;
        DataGridViewLinkColumn nameColumn;
        DataGridViewTextBoxColumn usernameColumn;
        DataGridViewTextBoxColumn hostColumn;
        DataGridViewTextBoxColumn portColumn;
        DataGridViewTextBoxColumn databaseColumn;

        public ConnectionParamsListPanel()
        {
            InitializeComponent();

            this.titleLabel.Text = Strings.ServerConnectionsList;
        }

        protected override void InitMainDataGridView()
        {
            uidColumn = new DataGridViewTextBoxColumn
            {
                Name = "uid",
                DataPropertyName = "uid",
                HeaderText = Strings.ConnectionID,
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
                Width = 200
            };

            usernameColumn = new DataGridViewTextBoxColumn
            {
                Name = "username",
                DataPropertyName = "username",
                HeaderText = Strings.User,
                Width = 150
            };

            hostColumn = new DataGridViewTextBoxColumn
            {
                Name = "host",
                DataPropertyName = "host",
                HeaderText = Strings.Host,
                Width = 250
            };

            portColumn = new DataGridViewTextBoxColumn
            {
                Name = "port",
                DataPropertyName = "port",
                HeaderText = "Port",
                Width = 60
            };

            databaseColumn = new DataGridViewTextBoxColumn
            {
                Name = "database",
                DataPropertyName = "database",
                HeaderText = Strings.Database,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewColumn[] cols = { uidColumn, nameColumn, usernameColumn, hostColumn, portColumn, databaseColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        protected override void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetConnectionParamsDao().GetDataTable(criteria);
        }

        protected override void AddToolStripButton_Click(object sender, EventArgs e)
        {
            using (var frm = new ConnectionParamsForm(null))
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
                ConnectionParams cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);
                using (var frm = new ConnectionParamsForm(cp))
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK) RunLookup();
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
                    Guid uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                    ConnectionParams cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);

                    int dependencyCount = DaoFactory.Instance.GetConnectionParamsDao().GetDependencyCount(cp);
                    if (dependencyCount > 0)
                    {
                        MessageBox.Show(Strings.CannotDelete, Strings.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string record = string.Format("[ {0} ]", cp.Name);
                    if (Defs.ConfirmDeleteDialog(record))
                    {
                        DaoFactory.Instance.GetConnectionParamsDao().Delete(cp);
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
