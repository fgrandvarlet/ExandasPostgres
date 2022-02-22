using System;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.Firebird
{
    public class ParameterDataDaoFirebird : AbstractDaoFirebird, IParameterDataDao
    {
        public ParameterDataDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        public void Load(ParameterData pd)
        {
            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    // traitement des ConnectionParams
                    var connectionParamsDao = DaoFactory.Instance.GetConnectionParamsDao();
                    foreach (ConnectionParams connectionParams in pd.ConnectionParamsList)
                    {
                        var cp = connectionParamsDao.Get(connectionParams.Uid);
                        if (cp == null)
                        {
                            connectionParamsDao.Add(tran, connectionParams);
                        }
                    }

                    // traitement des ComparisonSet
                    var comparisonSetDao = DaoFactory.Instance.GetComparisonSetDao();
                    var schemaMappingDao = DaoFactory.Instance.GetSchemaMappingDao();
                    var filterSettingDao = DaoFactory.Instance.GetFilterSettingDao();
                    foreach (ComparisonSet comparisonSet in pd.ComparisonSetList)
                    {
                        var cs = comparisonSetDao.Get(comparisonSet.Uid);
                        if (cs == null)
                        {
                            comparisonSetDao.Add(tran, comparisonSet);
                            // traitement des SchemaMapping
                            foreach (SchemaMapping schemaMapping in comparisonSet.SchemaMappings)
                            {
                                schemaMappingDao.Add(tran, schemaMapping);
                            }
                            // traitement des FilterSetting
                            foreach (FilterSetting filterSetting in comparisonSet.FilterSettings)
                            {
                                filterSettingDao.Add(tran, filterSetting);
                            }
                        }
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

    }
}
