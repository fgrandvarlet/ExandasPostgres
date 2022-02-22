using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public abstract class AbstractConstraint
    {
        public string ConstraintName { get; set; }
        public string TableName { get; set; }
        public string Definition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        protected void Compare(AbstractConstraint target, SchemaMapping schemaMapping, List<DeltaReport> list, string entity)
        {
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, entity, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
        }

    }
}
