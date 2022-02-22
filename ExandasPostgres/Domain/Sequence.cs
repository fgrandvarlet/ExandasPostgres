using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Sequence
    {
        const string ENTITY = "SEQUENCE";
        public string SequenceName { get; set; }
        public string Owner { get; set; }
        public string DataType { get; set; }
        public long StartValue { get; set; }
        public long MinValue { get; set; }
        public long MaxValue { get; set; }
        public long IncrementBy { get; set; }
        public string Cycle { get; set; }
        public long CacheSize { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }
        public string OwnedBy { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Sequence).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Sequence).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Sequence target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.DataType != target.DataType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "DATA_TYPE", this.DataType, target.DataType
                    ));
            }
            if (this.StartValue != target.StartValue)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "START_VALUE", this.StartValue.ToString(), target.StartValue.ToString()
                    ));
            }
            if (this.MinValue != target.MinValue)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "MIN_VALUE", this.MinValue.ToString(), target.MinValue.ToString()
                    ));
            }
            if (this.MaxValue != target.MaxValue)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "MAX_VALUE", this.MaxValue.ToString(), target.MaxValue.ToString()
                    ));
            }
            if (this.IncrementBy != target.IncrementBy)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "INCREMENT_BY", this.IncrementBy.ToString(), target.IncrementBy.ToString()
                    ));
            }
            if (this.Cycle != target.Cycle)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "CYCLE", this.Cycle, target.Cycle
                    ));
            }
            if (this.CacheSize != target.CacheSize)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "CACHE_SIZE", this.CacheSize.ToString(), target.CacheSize.ToString()
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
            if (this.OwnedBy != target.OwnedBy)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "OWNED_BY", this.OwnedBy, target.OwnedBy
                    ));
            }
        }

    }
}
