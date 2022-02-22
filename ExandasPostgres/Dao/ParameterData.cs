using System.Collections.Generic;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public class ParameterData
    {
        public List<ConnectionParams> ConnectionParamsList { get; set; }
        public List<ComparisonSet> ComparisonSetList { get; set; }

    }
}
