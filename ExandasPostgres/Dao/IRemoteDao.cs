using System;
using System.Collections.Generic;
using Npgsql;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public interface IRemoteDao
    {
        bool CheckConnection();

        NpgsqlConnection GetNpgsqlConnection();

        Version GetVersion();

        bool CheckExistsSchema(string schemaName);

        List<Table> GetTableList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<TableColumn> GetTableColumnList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<PrimaryKey> GetPrimaryKeyList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Unique> GetUniqueList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<ForeignKey> GetForeignKeyList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Check> GetCheckList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Exclusion> GetExclusionList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<View> GetViewList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<ViewColumn> GetViewColumnList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<MaterializedView> GetMaterializedViewList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<MaterializedViewColumn> GetMaterializedViewColumnList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<ForeignTable> GetForeignTableList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<ForeignTableColumn> GetForeignTableColumnList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Sequence> GetSequenceList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Index> GetIndexList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<IndexColumn> GetIndexColumnList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Trigger> GetTriggerList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Function> GetFunctionList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Domain.Domain> GetDomainList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<PostgresType> GetTypeList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Extension> GetExtensionList(NpgsqlConnection conn, string schema, Version pgVersion);

        List<Statistics> GetStatisticsList(NpgsqlConnection conn, string schema, Version pgVersion);

    }
}
