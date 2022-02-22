using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ExandasPostgres.Components;
using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;
using ExandasPostgres.Reporting;

namespace ExandasPostgres.Forms
{
    public partial class DeltaReportListForm : Form
    {
        readonly ComparisonSet _comparisonSet;
        TitlePanel titlePanel;
        List<DeltaReportListPanel> deltaReportListPanels;

        public DeltaReportListForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            this.Size = new Size(resolution.Width - 80, resolution.Height - 80);

            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            this.innerTopPanel.Height = 52;

            this._comparisonSet = comparisonSet;
        }

        private void DeltaReportListForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel
            {
                Parent = topPanel,
                Dock = DockStyle.Fill
            };
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = Strings.ComparisonReport;
            
            this.titleLabel.Text = this._comparisonSet.Name;

            InitializeTabControl();
        }

        private void InitializeTabControl()
        {
            var schemaMappingList = DaoFactory.Instance.GetSchemaMappingDao().GetListByComparisonSetUid(this._comparisonSet.Uid);
            this.deltaReportListPanels = new List<DeltaReportListPanel>();

            foreach (var schemaMapping in schemaMappingList)
            {
                TabPage tabPage = new TabPage(schemaMapping.ToString())
                {
                    UseVisualStyleBackColor = true
                };

                this.deltaReportTabControl.TabPages.Add(tabPage);

                DeltaReportListPanel deltaReportListPanel = new DeltaReportListPanel(schemaMapping);
                this.deltaReportListPanels.Add(deltaReportListPanel);
                tabPage.Controls.Add(deltaReportListPanel);
                deltaReportListPanel.Dock = DockStyle.Fill;
            }
        }

        private void FilteringButton_Click(object sender, EventArgs e)
        {
            using (FilterSettingListForm frm = new FilterSettingListForm(this._comparisonSet))
            {
                frm.ShowDialog(this);
                
                foreach(DeltaReportListPanel deltaReportListPanel in this.deltaReportListPanels)
                {
                    deltaReportListPanel.RunLookup();
                }
            }
        }

        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReportUtils.ExportToExcel(this._comparisonSet);
            }
            catch (InvalidOperationException ex)
            {
                var message = ex.Message + Environment.NewLine + Environment.NewLine + Strings.PleaseCheckExcel;
                MessageBox.Show(message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
