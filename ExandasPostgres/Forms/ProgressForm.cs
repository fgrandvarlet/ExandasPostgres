using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Forms
{
    public partial class ProgressForm : Form
    {
        private readonly ComparisonSet _comparisonSet;
        private bool _cancellationDone = false;

        public ProgressForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(620, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.CancelButton = noButton;
            this.AcceptButton = yesButton;

            this.topPanel.Height = 60;
            this.bottomFlowLayoutPanel.Height = 52;
            this.schemaMappingProgressBar.Visible = false;
            this.schemaMappingProgressLabel.Visible = false;
            this.schemaMappingProgressLabel.Text = null;
            this.metaDataProgressBar.Visible = false;
            this.metaDataProgressLabel.Visible = false;
            this.metaDataProgressLabel.Text = null;
            this.cancelButton.Visible = false;

            this._comparisonSet = comparisonSet;

            // localization
            this.cancelButton.Text = Strings.CancelButton;
            this.yesButton.Text = Strings.YesButton;
            this.noButton.Text = Strings.NoButton;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            Text = Strings.LaunchComparisonReport;
            messageLabel.Text = Strings.DoYouConfirmGeneration;
        }

        private void MainBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var comparisonSet = e.Argument as ComparisonSet;

            // enrichment of the ComparisonSet object with instances of ConnectionParams
            comparisonSet.Connection1 = DaoFactory.Instance.GetConnectionParamsDao().Get(comparisonSet.Connection1Uid);
            comparisonSet.Connection2 = DaoFactory.Instance.GetConnectionParamsDao().Get(comparisonSet.Connection2Uid);

            var schemaMappingList = DaoFactory.Instance.GetSchemaMappingDao().GetListByComparisonSetUid(comparisonSet.Uid);
            int counter = 0;
            foreach (var schemaMapping in schemaMappingList)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                counter++;
                new MetaDataLoader(comparisonSet, schemaMapping, counter, schemaMappingList.Count).Execute(worker, e);
                new Delta(schemaMapping, counter, schemaMappingList.Count).Execute(worker, e);
            }

            if (worker.CancellationPending == false)
            {
                // mise à jour du ComparisonSet colonne last_report_time
                comparisonSet.LastReportTime = DateTime.Now;
                DaoFactory.Instance.GetComparisonSetDao().Save(comparisonSet);
            }
        }

        private void MainBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressState progressState = e.UserState as ProgressState;

            schemaMappingProgressBar.Value = progressState.SchemaMappingPercentage;
            schemaMappingProgressLabel.Text = progressState.SchemaMappingStep;
            metaDataProgressBar.Value = e.ProgressPercentage;
            metaDataProgressLabel.Text = string.Format("{0} : {1} %", progressState.MetaDataStep, e.ProgressPercentage);
        }

        private void MainBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                messageLabel.Text = Strings.OperationCanceled;
                schemaMappingProgressBar.Visible = false;
                schemaMappingProgressLabel.Visible = false;
                metaDataProgressBar.Visible = false;
                metaDataProgressLabel.Visible = false;
                this._cancellationDone = true;
                cancelButton.Text = Strings.Close;
            }
            else if (e.Error != null)
            {
                schemaMappingProgressBar.Visible = false;
                MessageBox.Show(e.Error.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            schemaMappingProgressBar.Visible = true;
            schemaMappingProgressBar.Style = ProgressBarStyle.Continuous;
            schemaMappingProgressLabel.Visible = true;

            metaDataProgressBar.Visible = true;
            metaDataProgressBar.Style = ProgressBarStyle.Continuous;
            metaDataProgressLabel.Visible = true;

            cancelButton.Visible = true;
            yesButton.Visible = false;
            noButton.Visible = false;

            messageLabel.Text = Strings.GenerationInProgress;
            mainBackgroundWorker.RunWorkerAsync(_comparisonSet);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this._cancellationDone)
            {
                this.Close();
            }
            else
            {
                if (mainBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    mainBackgroundWorker.CancelAsync();
                }
            }
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainBackgroundWorker.IsBusy)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
