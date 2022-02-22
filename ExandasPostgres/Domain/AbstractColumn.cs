using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public abstract class AbstractColumn
    {
        public string RelationName { get; set; }
        public string ColumnName { get; set; }
        public string Owner { get; set; }
        public short ColumnNum { get; set; }
        public string DataType { get; set; }
        public string Collation { get; set; }
        public string Nullable { get; set; }
        public string DataDefault { get; set; }
        public string Identity { get; set; }
        public string Generated { get; set; }
        public string Storage { get; set; }
        public string Compression { get; set; }
        public int? StatisticsTarget { get; set; }
        public string IsLocal { get; set; }
        public int InheritanceCount { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }
        public string Options { get; set; }

        public string NormalizedOptions
        {
            get
            {
                return NormalizeHelpers.GetNormalizedOptions(Options);
            }
        }

        protected virtual string NormalizedAccessPrivilegesWithGrantor
        {
            get;
        }

        protected virtual string NormalizedAccessPrivilegesWithoutGrantor
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        public void Compare(AbstractColumn target, SchemaMapping schemaMapping, List<DeltaReport> list, string entity)
        {
            if (this.ColumnNum != target.ColumnNum)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "COLUMN_NUM", this.ColumnNum.ToString(), target.ColumnNum.ToString()
                    ));
            }
            if (this.DataType != target.DataType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "DATA_TYPE", this.DataType, target.DataType
                    ));
            }
            if (this.Collation != target.Collation)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "COLLATION", this.Collation, target.Collation
                    ));
            }
            if (this.Nullable != target.Nullable)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "NULLABLE", this.Nullable, target.Nullable
                    ));
            }
            if (this.DataDefault != target.DataDefault)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "DATA_DEFAULT", this.DataDefault, target.DataDefault
                    ));
            }
            if (this.Identity != target.Identity)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "IDENTITY", this.Identity, target.Identity
                    ));
            }
            if (this.Generated != target.Generated)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "GENERATED", this.Generated, target.Generated
                    ));
            }
            if (this.Storage != target.Storage)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "STORAGE", this.Storage, target.Storage
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
            if (this.StatisticsTarget != target.StatisticsTarget)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "STATISTICS_TARGET", this.StatisticsTarget.ToString(), target.StatisticsTarget.ToString()
                    ));
            }
            if (this.IsLocal != target.IsLocal)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "IS_LOCAL", this.IsLocal, target.IsLocal
                    ));
            }
            if (this.InheritanceCount != target.InheritanceCount)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "INHERITANCE_COUNT", this.InheritanceCount.ToString(), target.InheritanceCount.ToString()
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
            if (this.NormalizedOptions != target.NormalizedOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ColumnName, this.RelationName, LabelId.PropertyDifference, "OPTIONS", this.NormalizedOptions, target.NormalizedOptions
                    ));
            }
        }

    }
}
