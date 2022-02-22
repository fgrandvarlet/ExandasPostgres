using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class PostgresType
    {
        const string ENTITY = "TYPE";
        public string TypeName { get; set; }
        public string FormatTypeName { get; set; }
        public string Owner { get; set; }
        public string TypeType { get; set; }
        public string TypeSize { get; set; }
        public string Elements { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Type).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Type).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(PostgresType target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.FormatTypeName != target.FormatTypeName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "FORMAT_TYPE_NAME", this.FormatTypeName, target.FormatTypeName
                    ));
            }
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.TypeType != target.TypeType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "TYPE_TYPE", this.TypeType, target.TypeType
                    ));
            }
            if (this.TypeSize != target.TypeSize)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "TYPE_SIZE", this.TypeSize, target.TypeSize
                    ));
            }
            if (this.Elements != target.Elements)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "ELEMENTS", this.Elements, target.Elements
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
        }

    }
}
