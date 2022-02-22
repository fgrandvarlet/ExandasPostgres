using System;
using System.Drawing;
using System.Windows.Forms;

using ExandasPostgres.Components;
using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Forms
{
    public partial class SchemaMappingForm : Form, IDataFormManager
    {
        readonly IDataFormManager _dataFormManager;
        SchemaMapping _schemaMapping;
        readonly ComparisonSet _comparisonSet;
        bool _inserting;
        bool _updating;
        bool _updated;
        TitlePanel titlePanel;
        BottomCommandPanel bottomCommandPanel;

        public SchemaMappingForm(SchemaMapping schemaMapping, ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(580, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this._dataFormManager = (IDataFormManager)this;
            this._schemaMapping = schemaMapping;
            this._comparisonSet = comparisonSet;
            this._inserting = (this._schemaMapping == null);

            // localization
            this.sourceSchemaLabel.Text = Strings.SourceSchema;
            this.targetSchemaLabel.Text = Strings.TargetSchema;
        }

        private void SchemaMappingForm_Load(object sender, EventArgs e)
        {
            Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel
            {
                Parent = topPanel,
                Dock = DockStyle.Fill
            };
            topPanel.Height = 62;
            titlePanel.titleLabel.Text = Strings.SchemaMappingDetail;

            bottomCommandPanel = new BottomCommandPanel(this)
            {
                Parent = bottomPanel,
                Dock = DockStyle.Fill
            };
            bottomPanel.Height = 38;

            if (!this._inserting)
            {
                this._dataFormManager.DataToForm();
            }

            // event handlers
            sourceSchemaTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            targetSchemaTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
        }

        Form IDataFormManager.Parent
        {
            get { return this; }
        }

        public bool Inserting
        {
            get { return _inserting; }
            set { _inserting = value; }
        }

        public bool Updating
        {
            get { return _updating; }
            set { _updating = value; }
        }

        public bool Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        public void DataToForm()
        {
            sourceSchemaTextBox.Text = _schemaMapping.Schema1;
            targetSchemaTextBox.Text = _schemaMapping.Schema2;
        }

        public void FormToData()
        {
            _schemaMapping.Schema1 = sourceSchemaTextBox.Text.Trim();
            _schemaMapping.Schema2 = targetSchemaTextBox.Text.Trim();
        }

        public bool ValidateDataForm()
        {
            bool result = true;
            string message = "";

            if (sourceSchemaTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.SourceSchemaRequired + Environment.NewLine;
            }
            else
            {
                try
                {
                    ConnectionParams connectionParams = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection1Uid);
                    if (!DaoFactory.GetRemoteDao(connectionParams.ConnectionString).CheckExistsSchema(sourceSchemaTextBox.Text.Trim()))
                    {
                        result = false;
                        string msg = string.Format(Strings.SchemaDoesNotExistSource, sourceSchemaTextBox.Text.Trim(), connectionParams.Database);
                        message += "- " + msg + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (targetSchemaTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.TargetSchemaRequired + Environment.NewLine;
            }
            else
            {
                try
                {
                    ConnectionParams connectionParams = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection2Uid);
                    if (!DaoFactory.GetRemoteDao(connectionParams.ConnectionString).CheckExistsSchema(targetSchemaTextBox.Text.Trim()))
                    {
                        result = false;
                        string msg = string.Format(Strings.SchemaDoesNotExistTarget, targetSchemaTextBox.Text.Trim(), connectionParams.Database);
                        message += "- " + msg + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!result)
            {
                Defs.ValidatingErrorDialog(message);
            }
            return result;
        }

        public bool SaveData()
        {
            bool result = false;
            try
            {
                if (_dataFormManager.Inserting)
                {
                    _schemaMapping = new SchemaMapping
                    {
                        Uid = Guid.NewGuid(),
                        ComparisonSetUid = _comparisonSet.Uid
                    };
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetSchemaMappingDao().Add(_schemaMapping);
                }
                else
                {
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetSchemaMappingDao().Save(_schemaMapping);
                }
                _dataFormManager.Inserting = false;
                _dataFormManager.Updating = false;
                bottomCommandPanel.doApplyButton.Enabled = false;
                _dataFormManager.Updated = true;
                result = true;
            }
            catch (Exception ex)
            {
                if (_dataFormManager.Inserting)
                {
                    _schemaMapping = null;
                }
                Defs.ErrorDialog(ex.Message);
            }
            return result;
        }

        public void DataChanged(object sender, EventArgs e)
        {
            _dataFormManager.Updating = true;
            bottomCommandPanel.doApplyButton.Enabled = true;
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            if (sourceSchemaTextBox.Text.Trim().Length > 0)
            {
                targetSchemaTextBox.Text = sourceSchemaTextBox.Text.Trim();
            }
        }
    }
}
