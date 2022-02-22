using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Table
    {
        const string ENTITY = "TABLE";
        public string TableName { get; set; }
        public string Owner { get; set; }
        public string TablespaceName { get; set; }
        public string Persistence { get; set; }
        public string AccessMethod { get; set; }
        public string IsPartitioned { get; set; }
        public string IsPartition { get; set; }
        public string HasSubclass { get; set; }
        public string RowSecurity { get; set; }
        public string ForceRowSecurity { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }
        public string Options { get; set; }
        public string PartitionKey { get; set; }
        public string PartitionBound { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Table).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Table).ResultWithoutGrantor;
            }
        }

        public string NormalizedOptions
        {
            get
            {
                return NormalizeHelpers.GetNormalizedOptions(Options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Table target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.Persistence != target.Persistence)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "PERSISTENCE", this.Persistence, target.Persistence
                    ));
            }
            if (this.AccessMethod != target.AccessMethod)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "ACCESS_METHOD", this.AccessMethod, target.AccessMethod
                    ));
            }
            if (this.IsPartitioned != target.IsPartitioned)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "IS_PARTITIONED", this.IsPartitioned, target.IsPartitioned
                    ));
            }
            if (this.IsPartition != target.IsPartition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "IS_PARTITION", this.IsPartition, target.IsPartition
                    ));
            }
            if (this.HasSubclass != target.HasSubclass)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "HAS_SUBCLASS", this.HasSubclass, target.HasSubclass
                    ));
            }
            if (this.RowSecurity != target.RowSecurity)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "ROW_SECURITY", this.RowSecurity, target.RowSecurity
                    ));
            }
            if (this.ForceRowSecurity != target.ForceRowSecurity)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "FORCE_ROW_SECURITY", this.ForceRowSecurity, target.ForceRowSecurity
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
            if (this.NormalizedOptions != target.NormalizedOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "OPTIONS", this.NormalizedOptions, target.NormalizedOptions
                    ));
            }
            if (this.PartitionKey != target.PartitionKey)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "PARTITION_KEY", this.PartitionKey, target.PartitionKey
                    ));
            }
            if (this.PartitionBound != target.PartitionBound)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "PARTITION_BOUND", this.PartitionBound, target.PartitionBound
                    ));
            }
        }

    }
}
