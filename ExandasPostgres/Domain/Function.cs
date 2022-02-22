using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Function
    {
        const string ENTITY = "FUNCTION";
        public string FunctionName { get; set; }
        public string IdentityArgumentsHash { get; set; }
        public string IdentityArguments { get; set; }
        public string ArgumentDataTypes { get; set; }
        public string ResultDataType { get; set; }
        public string Owner { get; set; }
        public string FunctionType { get; set; }
        public string Volatility { get; set; }
        public string Parallel { get; set; }
        public string Security { get; set; }
        public string Language { get; set; }
        public string SourceCode { get; set; }
        public string Definition { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }

        public string Signature
        {
            get
            {
                return string.Format("{0}({1})", FunctionName, IdentityArguments);
            }
        }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Function).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.Function).ResultWithoutGrantor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Function target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.ArgumentDataTypes != target.ArgumentDataTypes)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "ARGUMENT_DATA_TYPES", this.ArgumentDataTypes, target.ArgumentDataTypes
                    ));
            }
            if (this.ResultDataType != target.ResultDataType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "RESULT_DATA_TYPE", this.ResultDataType, target.ResultDataType
                    ));
            }
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.FunctionType != target.FunctionType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "FUNCTION_TYPE", this.FunctionType, target.FunctionType
                    ));
            }
            if (this.Volatility != target.Volatility)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "VOLATILITY", this.Volatility, target.Volatility
                    ));
            }
            if (this.Parallel != target.Parallel)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "PARALLEL", this.Parallel, target.Parallel
                    ));
            }
            if (this.Security != target.Security)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "SECURITY", this.Security, target.Security
                    ));
            }
            if (this.Language != target.Language)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "LANGUAGE", this.Language, target.Language
                    ));
            }
            if (this.SourceCode != target.SourceCode)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "SOURCE_CODE", this.SourceCode, target.SourceCode
                    ));
            }
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.AccessPrivileges, target.AccessPrivileges
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.Signature, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.AccessPrivileges, target.AccessPrivileges
                        ));
                }
            }
        }

    }
}
