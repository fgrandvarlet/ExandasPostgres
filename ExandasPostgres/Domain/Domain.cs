using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Domain
    {
        const string ENTITY = "DOMAIN";
        public string DomainName { get; set; }
        public string Owner { get; set; }
        public string DataType { get; set; }
        public string Collation { get; set; }
        public string Nullable { get; set; }
        public string DataDefault { get; set; }
        public string CheckConstraint { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Domain).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Domain).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Domain target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.DataType != target.DataType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "DATA_TYPE", this.DataType, target.DataType
                    ));
            }
            if (this.Collation != target.Collation)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "COLLATION", this.Collation, target.Collation
                    ));
            }
            if (this.Nullable != target.Nullable)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "NULLABLE", this.Nullable, target.Nullable
                    ));
            }
            if (this.DataDefault != target.DataDefault)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "DATA_DEFAULT", this.DataDefault, target.DataDefault
                    ));
            }
            if (this.CheckConstraint != target.CheckConstraint)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "CHECK_CONSTRAINT", this.CheckConstraint, target.CheckConstraint
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.DomainName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
        }

    }
}
