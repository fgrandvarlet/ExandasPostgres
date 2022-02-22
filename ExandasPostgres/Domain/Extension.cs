using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Extension
    {
        const string ENTITY = "EXTENSION";
        public string ExtensionName { get; set; }
        public string Owner { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Extension target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ExtensionName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.Version != target.Version)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ExtensionName, null, LabelId.PropertyDifference, "VERSION", this.Version, target.Version
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ExtensionName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
        }

    }
}
