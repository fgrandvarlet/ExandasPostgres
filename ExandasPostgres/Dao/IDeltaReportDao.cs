using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public interface IDeltaReportDao
    {
        FbConnection GetFirebirdConnection();

        DataTable GetDataTable(Criteria criteria);

        void LoadDeltaReportList(FbConnection conn, Guid schemaMappingUid, List<DeltaReport> list);

    }
}
