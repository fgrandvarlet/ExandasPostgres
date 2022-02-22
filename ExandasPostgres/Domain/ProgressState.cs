namespace ExandasPostgres.Domain
{
    class ProgressState
    {
        public string SchemaMappingStep { get; set; }
        public int SchemaMappingPercentage { get; set; }
        public string MetaDataStep { get; set; }
    }
}
