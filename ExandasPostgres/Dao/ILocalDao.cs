using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public interface ILocalDao
    {
        FbConnection GetFirebirdConnection();

        void LoadTableList(FbTransaction tran, SchemaType schemaType, List<Table> list);

        void LoadTableColumnList(FbTransaction tran, SchemaType schemaType, List<TableColumn> list);

        void LoadPrimaryKeyList(FbTransaction tran, SchemaType schemaType, List<PrimaryKey> list);

        void LoadUniqueList(FbTransaction tran, SchemaType schemaType, List<Unique> list);

        void LoadForeignKeyList(FbTransaction tran, SchemaType schemaType, List<ForeignKey> list);

        void LoadCheckList(FbTransaction tran, SchemaType schemaType, List<Check> list);

        void LoadExclusionList(FbTransaction tran, SchemaType schemaType, List<Exclusion> list);

        void LoadViewList(FbTransaction tran, SchemaType schemaType, List<View> list);

        void LoadViewColumnList(FbTransaction tran, SchemaType schemaType, List<ViewColumn> list);

        void LoadMaterializedViewList(FbTransaction tran, SchemaType schemaType, List<MaterializedView> list);

        void LoadMaterializedViewColumnList(FbTransaction tran, SchemaType schemaType, List<MaterializedViewColumn> list);

        void LoadForeignTableList(FbTransaction tran, SchemaType schemaType, List<ForeignTable> list);

        void LoadForeignTableColumnList(FbTransaction tran, SchemaType schemaType, List<ForeignTableColumn> list);

        void LoadSequenceList(FbTransaction tran, SchemaType schemaType, List<Sequence> list);

        void LoadIndexList(FbTransaction tran, SchemaType schemaType, List<Index> list);

        void LoadIndexColumnList(FbTransaction tran, SchemaType schemaType, List<IndexColumn> list);

        void LoadTriggerList(FbTransaction tran, SchemaType schemaType, List<Trigger> list);

        void LoadFunctionList(FbTransaction tran, SchemaType schemaType, List<Function> list);

        void LoadDomainList(FbTransaction tran, SchemaType schemaType, List<Domain.Domain> list);

        void LoadTypeList(FbTransaction tran, SchemaType schemaType, List<PostgresType> list);

        void LoadExtensionList(FbTransaction tran, SchemaType schemaType, List<Extension> list);

        void LoadStatisticsList(FbTransaction tran, SchemaType schemaType, List<Statistics> list);

        void PurgeMetaDataTables();

        void PurgeDeltaReport();

    }
}
