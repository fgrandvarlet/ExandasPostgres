using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class ForeignTableColumn : AbstractColumn
    {
        const string ENTITY = "FOREIGN TABLE COLUMN";
        public string FdwOptions { get; set; }

        protected override string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.ForeignTableColumn).ResultWithGrantor;
            }
        }

        protected override string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.ForeignTableColumn).ResultWithoutGrantor;
            }
        }

        public string NormalizedFdwOptions
        {
            get
            {
                return NormalizeHelpers.GetNormalizedOptions(FdwOptions);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(ForeignTableColumn target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            base.Compare(target, schemaMapping, list, ENTITY);

            if (this.NormalizedFdwOptions != target.NormalizedFdwOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "FDW_OPTIONS", this.NormalizedFdwOptions, target.NormalizedFdwOptions
                    ));
            }
        }

    }
}
