using System;

using ExandasPostgres.Core;
using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class DeltaReport
    {
        public long Id { get; set; }
        public Guid SchemaMappingUid { get; set; }
        public string Entity { get; set; }
        public string ObjectValue { get; set; }
        public string ParentObject { get; set; }
        public short LabelId { get; set; }
        public string Label { get; set; }
        public string Property { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }

        public DeltaReport(
            Guid schemaMappingUid,
            string entity,
            string objectValue,
            string parentObject,
            LabelId labelId,
            string property,
            string source,
            string target
            )
        {
            SchemaMappingUid = schemaMappingUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
            Property = property;
            Source = source;
            Target = target;
        }

        public DeltaReport(
            Guid schemaMappingUid,
            string entity,
            string objectValue,
            string parentObject,
            LabelId labelId
            )
        {
            SchemaMappingUid = schemaMappingUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
        }

        public DeltaReport(
            Guid schemaMappingUid,
            string entity,
            string objectValue,
            LabelId labelId
            )
        {
            SchemaMappingUid = schemaMappingUid;
            Entity = entity;
            ObjectValue = objectValue;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
        }

    }
}
