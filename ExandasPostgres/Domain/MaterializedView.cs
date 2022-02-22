﻿using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class MaterializedView
    {
        const string ENTITY = "MATERIALIZED VIEW";
        public string MaterializedViewName { get; set; }
        public string Owner { get; set; }
        public string TablespaceName { get; set; }
        public string Persistence { get; set; }
        public string AccessMethod { get; set; }
        public string Definition { get; set; }
        public string Description { get; set; }
        public string AccessPrivileges { get; set; }
        public string Options { get; set; }

        public string NormalizedAccessPrivilegesWithGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.MaterializedView).ResultWithGrantor;
            }
        }

        public string NormalizedAccessPrivilegesWithoutGrantor
        {
            get
            {
                return new AccessPrivilegesNormalizer(AccessPrivileges, Owner, PrivilegeObjectType.MaterializedView).ResultWithoutGrantor;
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
        public void Compare(MaterializedView target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.Persistence != target.Persistence)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "PERSISTENCE", this.Persistence, target.Persistence
                    ));
            }
            if (this.AccessMethod != target.AccessMethod)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "ACCESS_METHOD", this.AccessMethod, target.AccessMethod
                    ));
            }
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
            if (this.Owner == target.Owner)
            {
                if (this.NormalizedAccessPrivilegesWithGrantor != target.NormalizedAccessPrivilegesWithGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithGrantor, target.NormalizedAccessPrivilegesWithGrantor
                        ));
                }
            }
            else
            {
                if (this.NormalizedAccessPrivilegesWithoutGrantor != target.NormalizedAccessPrivilegesWithoutGrantor)
                {
                    list.Add(new DeltaReport(
                        schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "ACCESS_PRIVILEGES", this.NormalizedAccessPrivilegesWithoutGrantor, target.NormalizedAccessPrivilegesWithoutGrantor
                        ));
                }
            }
            if (this.NormalizedOptions != target.NormalizedOptions)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.MaterializedViewName, null, LabelId.PropertyDifference, "OPTIONS", this.NormalizedOptions, target.NormalizedOptions
                    ));
            }
        }

    }
}
