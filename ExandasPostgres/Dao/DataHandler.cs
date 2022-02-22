using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public static class DataHandler
    {
        private static ParameterData GetParameterData()
        {
            var parameterData = new ParameterData();

            var connectionParamsDao = DaoFactory.Instance.GetConnectionParamsDao();
            parameterData.ConnectionParamsList = connectionParamsDao.GetList();

            var comparisonSetDao = DaoFactory.Instance.GetComparisonSetDao();
            var schemaMappingDao = DaoFactory.Instance.GetSchemaMappingDao();
            var filterSettingDao = DaoFactory.Instance.GetFilterSettingDao();
            List<ComparisonSet> comparisonSets = comparisonSetDao.GetList();
            foreach (ComparisonSet cs in comparisonSets)
            {
                cs.SchemaMappings = schemaMappingDao.GetListByComparisonSetUid(cs.Uid);
                cs.FilterSettings = filterSettingDao.GetListByComparisonSetUid(cs.Uid);
            }
            parameterData.ComparisonSetList = comparisonSets;

            return parameterData;
        }

        public static void SerializeConnectionsComparisonSets(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(GetParameterData(), options);
            File.WriteAllText(fileName, jsonString);
        }

        public static void DeserializeConnectionsComparisonSets(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            ParameterData parameterData = JsonSerializer.Deserialize<ParameterData>(jsonString);
            var parameterDataDao = DaoFactory.Instance.GetParameterDataDao();
            parameterDataDao.Load(parameterData);
        }

    }
}
