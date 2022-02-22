using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Check : AbstractConstraint
    {
        const string ENTITY = "CHECK";
        public string Validated { get; set; }

        public void Compare(Check target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            base.Compare(target, schemaMapping, list, ENTITY);

            if (this.Validated != target.Validated)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "VALIDATED", this.Validated, target.Validated
                    ));
            }
        }

    }
}
