using System;

namespace ExandasPostgres.Domain
{
    public static class NormalizeHelpers
    {
        public static string GetNormalizedOptions(string options)
        {
            string result = null;

            if (options != null)
            {
                string[] items = options.Split(',');
                Array.Sort(items);
                result = string.Join(",", items);
            }
            return result;
        }

        public static string GetItemWithoutSchemaName(string item, string schema)
        {
            return item.Replace(schema + ".", "");
        }
    }
}
