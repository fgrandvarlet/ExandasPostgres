using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using Npgsql;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="schemaType"></param>
    /// <param name="dao"></param>
    /// <param name="conn"></param>
    /// <param name="schema"></param>
    /// <param name="pgVersion"></param>
    public delegate void LoaderDelegate(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="list"></param>
    public delegate void DeltaDelegate(FbConnection conn, List<DeltaReport> list);

}
