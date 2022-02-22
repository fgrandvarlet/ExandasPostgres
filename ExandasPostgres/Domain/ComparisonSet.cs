using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExandasPostgres.Domain
{
    public class ComparisonSet
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public Guid Connection1Uid { get; set; }

        [JsonIgnore]
        public ConnectionParams Connection1 { get; set; }
        public Guid Connection2Uid { get; set; }

        [JsonIgnore]
        public ConnectionParams Connection2 { get; set; }

        [JsonIgnore]
        public DateTime? LastReportTime { get; set; }

        [JsonIgnore]
        public string ToFileName
        {
            get
            {
                if (LastReportTime.HasValue)
                {
                    return Name.Replace(" ", "_") + "_" + LastReportTime.Value.ToString(@"yyyyMMdd-HH\hmm");
                }
                else
                {
                    return Name.Replace(" ", "_");
                }
            }
        }

        public List<SchemaMapping> SchemaMappings { get; set; }

        public List<FilterSetting> FilterSettings { get; set; }

    }
}
