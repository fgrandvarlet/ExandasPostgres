using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public interface ISchemaMappingDao
    {
        DataTable GetDataTable(Criteria criteria);

        SchemaMapping Get(Guid uid);

        void Add(FbTransaction tran, SchemaMapping sm);

        void Add(SchemaMapping sm);

        void Save(SchemaMapping sm);

        void Delete(SchemaMapping sm);

        List<SchemaMapping> GetListByComparisonSetUid(Guid comparisonSetUid);

    }
}
