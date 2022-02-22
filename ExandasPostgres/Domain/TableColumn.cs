using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class TableColumn : AbstractColumn
    {
        const string ENTITY = "TABLE COLUMN";

        protected override string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.TableColumn).ResultWithGrantor;
            }
        }

        protected override string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.TableColumn).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(TableColumn target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            base.Compare(target, schemaMapping, list, ENTITY);
        }

    }
}
