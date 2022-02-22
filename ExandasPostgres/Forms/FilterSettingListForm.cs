using System;
using System.Drawing;
using System.Windows.Forms;

using ExandasPostgres.Components;
using ExandasPostgres.Core;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Forms
{
    public partial class FilterSettingListForm : Form
    {
        readonly ComparisonSet _comparisonSet;
        FilterSettingListPanel filterSettingListPanel;
        TitlePanel titlePanel;

        public FilterSettingListForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(870, 782);

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            this._comparisonSet = comparisonSet;
        }

        private void FilterSettingListForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel
            {
                Parent = topPanel,
                Dock = DockStyle.Fill
            };
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = Strings.FilterSettings;

            filterSettingListPanel = new FilterSettingListPanel(this._comparisonSet);
            fillPanel.Controls.Add(filterSettingListPanel);
            filterSettingListPanel.Dock = DockStyle.Fill;

            filterSettingListPanel.titleLabel.Text = _comparisonSet.Name;
        }

    }
}
