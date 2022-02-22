using System;
using FirebirdSql.Data.FirebirdClient;
using Npgsql;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Core
{
    public partial class MetaDataLoader
    {
        private void LoadTables(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadTableList(tran, schemaType, dao.GetTableList(conn, schema, pgVersion));
        }

        private void LoadTableColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadTableColumnList(tran, schemaType, dao.GetTableColumnList(conn, schema, pgVersion));
        }

        private void LoadPrimaryKeys(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadPrimaryKeyList(tran, schemaType, dao.GetPrimaryKeyList(conn, schema, pgVersion));
        }

        private void LoadUniques(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadUniqueList(tran, schemaType, dao.GetUniqueList(conn, schema, pgVersion));
        }

        private void LoadForeignKeys(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadForeignKeyList(tran, schemaType, dao.GetForeignKeyList(conn, schema, pgVersion));
        }

        private void LoadChecks(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadCheckList(tran, schemaType, dao.GetCheckList(conn, schema, pgVersion));
        }

        private void LoadExclusions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadExclusionList(tran, schemaType, dao.GetExclusionList(conn, schema, pgVersion));
        }

        private void LoadViews(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadViewList(tran, schemaType, dao.GetViewList(conn, schema, pgVersion));
        }

        private void LoadViewColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadViewColumnList(tran, schemaType, dao.GetViewColumnList(conn, schema, pgVersion));
        }

        private void LoadMaterializedViews(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadMaterializedViewList(tran, schemaType, dao.GetMaterializedViewList(conn, schema, pgVersion));
        }

        private void LoadMaterializedViewColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadMaterializedViewColumnList(tran, schemaType, dao.GetMaterializedViewColumnList(conn, schema, pgVersion));
        }

        private void LoadForeignTables(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadForeignTableList(tran, schemaType, dao.GetForeignTableList(conn, schema, pgVersion));
        }

        private void LoadForeignTableColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadForeignTableColumnList(tran, schemaType, dao.GetForeignTableColumnList(conn, schema, pgVersion));
        }

        private void LoadSequences(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadSequenceList(tran, schemaType, dao.GetSequenceList(conn, schema, pgVersion));
        }

        private void LoadIndexes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadIndexList(tran, schemaType, dao.GetIndexList(conn, schema, pgVersion));
        }

        private void LoadIndexColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadIndexColumnList(tran, schemaType, dao.GetIndexColumnList(conn, schema, pgVersion));
        }

        private void LoadTriggers(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadTriggerList(tran, schemaType, dao.GetTriggerList(conn, schema, pgVersion));
        }

        private void LoadFunctions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadFunctionList(tran, schemaType, dao.GetFunctionList(conn, schema, pgVersion));
        }

        private void LoadDomains(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadDomainList(tran, schemaType, dao.GetDomainList(conn, schema, pgVersion));
        }

        private void LoadTypes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadTypeList(tran, schemaType, dao.GetTypeList(conn, schema, pgVersion));
        }

        private void LoadExtensions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadExtensionList(tran, schemaType, dao.GetExtensionList(conn, schema, pgVersion));
        }

        private void LoadStatistics(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, NpgsqlConnection conn, string schema, Version pgVersion)
        {
            this._localDao.LoadStatisticsList(tran, schemaType, dao.GetStatisticsList(conn, schema, pgVersion));
        }

    }
}
