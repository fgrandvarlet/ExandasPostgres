using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class ForeignTable
    {
        const string ENTITY = "FOREIGN TABLE";
        public string ForeignTableName { get; set; }
        public string Owner { get; set; }
        public string Server { get; set; }
        public string FdwOptions { get; set; }
        public string Persistence { get; set; }
        public string IsPartition { get; set; }
        public string HasSubclass { get; set; }
        public string RowSecurity { get; set; }
        public string ForceRowSecurity { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.ForeignTable).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.ForeignTable).ResultWithoutGrantor;
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
        public void Compare(ForeignTable target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.Server != target.Server)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "SERVER", this.Server, target.Server
                    ));
            }
            if (this.NormalizedFdwOptions != target.NormalizedFdwOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "FDW_OPTIONS", this.NormalizedFdwOptions, target.NormalizedFdwOptions
                    ));
            }
            if (this.Persistence != target.Persistence)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "PERSISTENCE", this.Persistence, target.Persistence
                    ));
            }
            if (this.IsPartition != target.IsPartition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "IS_PARTITION", this.IsPartition, target.IsPartition
                    ));
            }
            if (this.HasSubclass != target.HasSubclass)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "HAS_SUBCLASS", this.HasSubclass, target.HasSubclass
                    ));
            }
            if (this.RowSecurity != target.RowSecurity)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "ROW_SECURITY", this.RowSecurity, target.RowSecurity
                    ));
            }
            if (this.ForceRowSecurity != target.ForceRowSecurity)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "FORCE_ROW_SECURITY", this.ForceRowSecurity, target.ForceRowSecurity
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.ForeignTableName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
        }

    }
}
