using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Trigger
    {
        const string ENTITY = "TRIGGER";
        public string TableName { get; set; }
        public string TriggerName { get; set; }
        public string Definition { get; set; }
        public string Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Trigger target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
            if (this.Enabled != target.Enabled)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.TriggerName, this.TableName, LabelId.PropertyDifference, "ENABLED", this.Enabled, target.Enabled
                    ));
            }
        }

    }
}
