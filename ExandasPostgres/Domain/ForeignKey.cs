using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class ForeignKey : AbstractConstraint
    {
        const string ENTITY = "FOREIGN KEY";
        public string Deferrable { get; set; }
        public string Deferred { get; set; }
        public string Validated { get; set; }
        public string IndexName { get; set; }
        public string ReferencedTableName { get; set; }
        public string UpdateAction { get; set; }
        public string DeleteAction { get; set; }
        public string MatchType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(ForeignKey target, SchemaMapping schemaMapping, List<DeltaReport> list)
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
            if (this.Validated != target.Validated)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "VALIDATED", this.Validated, target.Validated
                    ));
            }
            if (this.IndexName != target.IndexName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "INDEX_NAME", this.IndexName, target.IndexName
                    ));
            }
            if (this.ReferencedTableName != target.ReferencedTableName)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "REFERENCED_TABLE_NAME", this.ReferencedTableName, target.ReferencedTableName
                    ));
            }
            if (this.UpdateAction != target.UpdateAction)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "UPDATE_ACTION", this.UpdateAction, target.UpdateAction
                    ));
            }
            if (this.DeleteAction != target.DeleteAction)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "DELETE_ACTION", this.DeleteAction, target.DeleteAction
                    ));
            }
            if (this.MatchType != target.MatchType)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "MATCH_TYPE", this.MatchType, target.MatchType
                    ));
            }
        }

    }
}
