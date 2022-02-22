using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Unique : AbstractConstraint
    {
        const string ENTITY = "UNIQUE";
        public string Deferrable { get; set; }
        public string Deferred { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Unique target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            base.Compare(target, schemaMapping, list, ENTITY);

            if (this.Deferrable != target.Deferrable)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "DEFERRABLE", this.Deferrable, target.Deferrable
                    ));
            }
            if (this.Deferred != target.Deferred)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "DEFERRED", this.Deferred, target.Deferred
                    ));
            }
        }

    }
}
