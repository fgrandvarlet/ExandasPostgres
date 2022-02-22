using System;
using System.Collections.Generic;
using System.ComponentModel;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Core
{
    public partial class MetaDataLoader
    {
        private readonly ComparisonSet _comparisonSet;
        private readonly SchemaMapping _schemaMapping;
        private readonly int _schemaMappingCounter;
        private readonly int _schemaMappingCount;
        private readonly ILocalDao _localDao;
        private readonly Dictionary<string, LoaderDelegate> _loaderDictionary;
        private readonly int _totalOperationCount;
        private int _operationCounter;

        public MetaDataLoader(ComparisonSet comparisonSet, SchemaMapping schemaMapping, int schemaMappingCounter, int schemaMappingCount)
        {
            this._comparisonSet = comparisonSet ?? throw new ArgumentNullException(nameof(comparisonSet));
            this._schemaMapping = schemaMapping ?? throw new ArgumentNullException(nameof(schemaMapping));
            this._schemaMappingCounter = schemaMappingCounter;
            this._schemaMappingCount = schemaMappingCount;
            this._localDao = DaoFactory.Instance.GetLocalDao();
            this._loaderDictionary = BuildLoaderDictionary();
            this._totalOperationCount = this._loaderDictionary.Count * 2;
        }

        private Dictionary<string, LoaderDelegate> BuildLoaderDictionary()
        {
            var dict = new Dictionary<string, LoaderDelegate>
            {
                { Strings.Tables, LoadTables },
                { Strings.TableColumns, LoadTableColumns },
                { Strings.PrimaryKeys, LoadPrimaryKeys },
                { Strings.Uniques, LoadUniques },
                { Strings.ForeignKeys, LoadForeignKeys },
                { Strings.Checks, LoadChecks },
                { Strings.Exclusions, LoadExclusions },
                { Strings.Views, LoadViews },
                { Strings.ViewColumns, LoadViewColumns },
                { Strings.MaterializedViews, LoadMaterializedViews },
                { Strings.MaterializedViewColumns, LoadMaterializedViewColumns },
                { Strings.ForeignTables, LoadForeignTables },
                { Strings.ForeignTableColumns, LoadForeignTableColumns },
                { Strings.Sequences, LoadSequences },
                { Strings.Indexes, LoadIndexes },
                { Strings.IndexColumns, LoadIndexColumns },
                { Strings.Triggers, LoadTriggers },
                { Strings.Functions, LoadFunctions },
                { Strings.Domains, LoadDomains },
                { Strings.Types, LoadTypes },
                { Strings.Extensions, LoadExtensions },
                { Strings.Statistics, LoadStatistics },
            };

            return dict;
        }

        private Version GetMinimumVersion(Version version1, Version version2)
        {
            if (version1.CompareTo(version2) < 0)
            {
                return version1;
            }
            else
            {
                return version2;
            }
        }

        public void Execute(BackgroundWorker worker, DoWorkEventArgs e)
        {
            this._operationCounter = 0;

            var version1 = DaoFactory.GetRemoteDao(this._comparisonSet.Connection1.ConnectionString).GetVersion();
            var version2 = DaoFactory.GetRemoteDao(this._comparisonSet.Connection2.ConnectionString).GetVersion();
            var pgVersion = GetMinimumVersion(version1, version2);

            using (FbConnection conn = this._localDao.GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    LoadMetaData(tran, SchemaType.Source, pgVersion, worker, e);
                    LoadMetaData(tran, SchemaType.Target, pgVersion, worker, e);

                    // local transaction validation
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void LoadMetaData(FbTransaction tran, SchemaType schemaType, Version pgVersion, BackgroundWorker worker, DoWorkEventArgs e)
        {
            IRemoteDao dao = null;
            string schema = null;
            string step = null;

            switch (schemaType)
            {
                case SchemaType.Source:
                    dao = DaoFactory.GetRemoteDao(this._comparisonSet.Connection1.ConnectionString);
                    schema = this._schemaMapping.Schema1;
                    break;
                case SchemaType.Target:
                    dao = DaoFactory.GetRemoteDao(this._comparisonSet.Connection2.ConnectionString);
                    schema = this._schemaMapping.Schema2;
                    break;
            }

            var conn = dao.GetNpgsqlConnection();

            try
            {
                conn.Open();

                foreach (KeyValuePair<string, LoaderDelegate> item in this._loaderDictionary)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    switch (schemaType)
                    {
                        case SchemaType.Source:
                            step = string.Format(Strings.LoadingObjects, item.Key, Strings.Source);
                            break;
                        case SchemaType.Target:
                            step = string.Format(Strings.LoadingObjects, item.Key, Strings.Target);
                            break;
                    }
                    IncrementStep(worker, step);
                    item.Value(tran, schemaType, dao, conn, schema, pgVersion);
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
            int percentage = (int)Math.Round((double)this._operationCounter / this._totalOperationCount * 50);

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
