using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class Statistics
    {
        const string ENTITY = "STATISTICS";
        public string StatisticsName { get; set; }
        public string Owner { get; set; }
        public string Definition { get; set; }
        public string NDistinct { get; set; }
        public string Dependencies { get; set; }
        public string MCV { get; set; }
        public int? StatisticsTarget { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="schemaMapping"></param>
        /// <param name="list"></param>
        public void Compare(Statistics target, SchemaMapping schemaMapping, List<DeltaReport> list)
        {
            if (this.Owner != target.Owner && schemaMapping.Schema1 == schemaMapping.Schema2)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "OWNER", this.Owner, target.Owner
                    ));
            }
            if (this.Definition != target.Definition)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "DEFINITION", this.Definition, target.Definition
                    ));
            }
            if (this.NDistinct != target.NDistinct)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "NDISTINCT", this.NDistinct, target.NDistinct
                    ));
            }
            if (this.Dependencies != target.Dependencies)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "DEPENDENCIES", this.Dependencies, target.Dependencies
                    ));
            }
            if (this.MCV != target.MCV)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "MCV", this.MCV, target.MCV
                    ));
            }
            if (this.StatisticsTarget != target.StatisticsTarget)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "STATISTICS_TARGET", this.StatisticsTarget.ToString(), target.StatisticsTarget.ToString()
                    ));
            }
            if (this.Description != target.Description)
            {
                list.Add(new DeltaReport(
                    schemaMapping.Uid, ENTITY, this.StatisticsName, null, LabelId.PropertyDifference, "DESCRIPTION", this.Description, target.Description
                    ));
            }
        }

    }
}
