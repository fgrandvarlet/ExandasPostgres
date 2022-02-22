using System;

namespace ExandasPostgres.Domain
{
    public class SchemaMapping
    {
        public Guid Uid { get; set; }
        public Guid ComparisonSetUid { get; set; }
        public string Schema1 { get; set; }
        public string Schema2 { get; set; }
        public int NoOrder { get; set; }

        public override string ToString()
        {
            if (Schema1 != null && Schema2 != null)
            {
                return string.Format("{0} - {1}", Schema1, Schema2);
            }
            return string.Empty;
        }
    }
}
