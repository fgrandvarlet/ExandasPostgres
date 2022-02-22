using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class MaterializedViewColumn : AbstractColumn
    {
        const string ENTITY = "MATERIALIZED VIEW COLUMN";

        protected override string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.MaterializedViewColumn).ResultWithGrantor;
            }
        }

        protected override string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.MaterializedViewColumn).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(MaterializedViewColumn target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            base.Compare(target, schemaMapping, list, ENTITY);
        }

    }
}
