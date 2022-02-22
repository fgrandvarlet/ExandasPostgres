using System;
using System.Diagnostics;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using OfficeOpenXml;
using OfficeOpenXml.Table;

using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Reporting
{
    public static class ReportUtils
    {
        public static void ExportToExcel(ComparisonSet comparisonSet)
        {
            var connectionString = DaoFactory.Instance.LocalConnectionString;
            var schemaMappingList = DaoFactory.Instance.GetSchemaMappingDao().GetListByComparisonSetUid(comparisonSet.Uid);
            string fileName;

            using (var package = new ExcelPackage())
            {
                using (var conn = new FbConnection(connectionString))
                {
                    conn.Open();
                    int i = 0;

                    foreach (var schemaMapping in schemaMappingList)
                    {
                        i++;
                        string sheetName = schemaMapping.ToString();
                        if (sheetName.Length > 31)
                        {
                            sheetName = sheetName.Substring(0, 31);
                        }
                        var sheet = package.Workbook.Worksheets.Add(sheetName);

                        var filterStatements = DaoFactory.Instance.GetFilterSettingDao().GetFilteringWhereClause(comparisonSet.Uid);

                        const string ROOT_SELECT = "SELECT id, entity, object, parent_object, label, property, source, target" +
                            " FROM delta_report WHERE schema_mapping_uid = @schema_mapping_uid {0} ORDER BY id";
                        var sql = String.Format(ROOT_SELECT, filterStatements);
                        var cmd = new FbCommand(sql, conn);
                        cmd.Parameters.AddWithValue("schema_mapping_uid", schemaMapping.Uid);

                        using (var dr = cmd.ExecuteReader())
                        {
                            // The second argument specifies if we should print headers on the first row or not
                            sheet.Cells["A1"].LoadFromDataReader(dr, true, "DeltaReport" + i, TableStyles.Medium2);

                            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                        }
                    }
                }

                fileName = Path.Combine(Defs.REPORTS_DIRECTORY, comparisonSet.ToFileName + ".xlsx");
                package.SaveAs(new FileInfo(fileName));
            }
            
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,

                // indispensable pour que cela fonctionne
                // cf. https://github.com/dotnet/runtime/issues/28005
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }

    }
}
