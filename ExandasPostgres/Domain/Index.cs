using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Index
    {
        const string ENTITY = "INDEX";
        public string IndexName { get; set; }
        public string TableName { get; set; }
        public string Owner { get; set; }
        public string TablespaceName { get; set; }
        public string Persistence { get; set; }
        public string AccessMethod { get; set; }
        public string IsPartitioned { get; set; }
        public string IsPartition { get; set; }
        public string HasSubclass { get; set; }
        public string IsUnique { get; set; }
        public string IsPrimary { get; set; }
        public string IsExclusion { get; set; }
        public string Immediate { get; set; }
        public string IsClustered { get; set; }
        public string IsValid { get; set; }
        public string Definition { get; set; }
        public string Description { get; set; }
        public string Options { get; set; }

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
        public void Compare(Index target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.Persistence != target.Persistence)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "PERSISTENCE", this.Persistence, target.Persistence
                    ));
            }
            if (this.AccessMethod != target.AccessMethod)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "ACCESS_METHOD", this.AccessMethod, target.AccessMethod
                    ));
            }
            if (this.IsPartitioned != target.IsPartitioned)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_PARTITIONED", this.IsPartitioned, target.IsPartitioned
                    ));
            }
            if (this.IsPartition != target.IsPartition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_PARTITION", this.IsPartition, target.IsPartition
                    ));
            }
            if (this.HasSubclass != target.HasSubclass)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "HAS_SUBCLASS", this.HasSubclass, target.HasSubclass
                    ));
            }
            if (this.IsUnique != target.IsUnique)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_UNIQUE", this.IsUnique, target.IsUnique
                    ));
            }
            if (this.IsPrimary != target.IsPrimary)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_PRIMARY", this.IsPrimary, target.IsPrimary
                    ));
            }
            if (this.IsExclusion != target.IsExclusion)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_EXCLUSION", this.IsExclusion, target.IsExclusion
                    ));
            }
            if (this.Immediate != target.Immediate)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IMMEDIATE", this.Immediate, target.Immediate
                    ));
            }
            if (this.IsClustered != target.IsClustered)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_CLUSTERED", this.IsClustered, target.IsClustered
                    ));
            }
            if (this.IsValid != target.IsValid)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "IS_VALID", this.IsValid, target.IsValid
                    ));
            }
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.NormalizedOptions != target.NormalizedOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "OPTIONS", this.NormalizedOptions, target.NormalizedOptions
                    ));
            }
        }

    }
}
