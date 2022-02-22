using System;
using System.Collections.Generic;
using System.ComponentModel;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Core
{
    public partial class Delta
    {
        private readonly SchemaMapping _schemaMapping;
        private readonly int _schemaMappingCounter;
        private readonly int _schemaMappingCount;
        private readonly Dictionary<string, DeltaDelegate> _deltaDictionary;
        private readonly int _totalOperationCount;
        private int _operationCounter;

        public Delta(SchemaMapping schemaMapping, int schemaMappingCounter, int schemaMappingCount)
        {
            this._schemaMapping = schemaMapping ?? throw new ArgumentNullException(nameof(schemaMapping));
            this._schemaMappingCounter = schemaMappingCounter;
            this._schemaMappingCount = schemaMappingCount;
            this._deltaDictionary = BuildDeltaDictionary();
            this._totalOperationCount = this._deltaDictionary.Count;
        }

        private Dictionary<string, DeltaDelegate> BuildDeltaDictionary()
        {
            var dict = new Dictionary<string, DeltaDelegate>
            {
                { Strings.Tables, DeltaTable },
                { Strings.TableColumns, DeltaTableColumn },
                { Strings.PrimaryKeys, DeltaPrimaryKey },
                { Strings.Uniques, DeltaUnique },
                { Strings.ForeignKeys, DeltaForeignKey },
                { Strings.Checks, DeltaCheck },
                { Strings.Exclusions, DeltaExclusion },
                { Strings.Views, DeltaView },
                { Strings.ViewColumns, DeltaViewColumn },
                { Strings.MaterializedViews, DeltaMaterializedView },
                { Strings.MaterializedViewColumns, DeltaMaterializedViewColumn },
                { Strings.ForeignTables, DeltaForeignTable },
                { Strings.ForeignTableColumns, DeltaForeignTableColumn },
                { Strings.Sequences, DeltaSequence },
                { Strings.Indexes, DeltaIndex },
                { Strings.IndexColumns, DeltaIndexColumn },
                { Strings.Triggers, DeltaTrigger },
                { Strings.Functions, DeltaFunction },
                { Strings.Domains, DeltaDomain },
                { Strings.Types, DeltaType },
                { Strings.Extensions, DeltaExtension },
                { Strings.Statistics, DeltaStatistics },
            };

            return dict;
        }

        public void Execute(BackgroundWorker worker, DoWorkEventArgs e)
        {
            var list = new List<DeltaReport>();
            var dao = DaoFactory.Instance.GetDeltaReportDao();
            var conn = dao.GetFirebirdConnection();
            try
            {
                conn.Open();

                foreach (KeyValuePair<string, DeltaDelegate> item in this._deltaDictionary)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    IncrementStep(worker, string.Format(Strings.DeltaOf, item.Key));
                    item.Value(conn, list);
                }

                if (worker.CancellationPending == false)
                {
                    dao.LoadDeltaReportList(conn, this._schemaMapping.Uid, list);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void IncrementStep(BackgroundWorker worker, string step)
        {
            this._operationCounter++;
            int percentage = (int)Math.Round((double)this._operationCounter / this._totalOperationCount * 50) + 50;

            ProgressState progressState = new ProgressState
            {
                SchemaMappingStep = string.Format(
                    "{0} [{1}] : {2} / {3}",
                    Strings.SchemaMapping,
                    this._schemaMapping.ToString(),
                    this._schemaMappingCounter,
                    this._schemaMappingCount
                )
            };
            int schemaMappingPercentage = (int)Math.Round((double)this._schemaMappingCounter * 100 / this._schemaMappingCount);
            progressState.SchemaMappingPercentage = schemaMappingPercentage;
            progressState.MetaDataStep = step;

            worker.ReportProgress(percentage, progressState);
        }

    }
}
