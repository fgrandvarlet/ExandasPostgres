using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaFunction(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "FUNCTION";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.function_name, s.identity_arguments FROM src_function s" +
                " LEFT JOIN tgt_function t USING(function_name, identity_arguments_hash)" +
                " WHERE t.function_name IS NULL" +
                " ORDER BY function_name, identity_arguments";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}({1})", (string)dr["function_name"], (string)dr["identity_arguments"]);
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, objectValue, LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.function_name, t.identity_arguments FROM tgt_function t" +
                " LEFT JOIN src_function s USING(function_name, identity_arguments_hash)" +
                " WHERE s.function_name IS NULL" +
                " ORDER BY function_name, identity_arguments";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}({1})", (string)dr["function_name"], (string)dr["identity_arguments"]);
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, objectValue, LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_function";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceFunction = new Function
                    {
                        FunctionName = (string)dr["function_name"],
                        IdentityArgumentsHash = (string)dr["identity_arguments_hash"],
                        IdentityArguments = (string)dr["src_identity_arguments"],
                        ArgumentDataTypes = (string)dr["src_argument_data_types"],
                        ResultDataType = dr["src_result_data_type"] is DBNull ? null : (string)dr["src_result_data_type"],
                        Owner = (string)dr["src_owner"],
                        FunctionType = (string)dr["src_function_type"],
                        Volatility = (string)dr["src_volatility"],
                        Parallel = (string)dr["src_parallel"],
                        Security = (string)dr["src_security"],
                        Language = (string)dr["src_language"],
                        SourceCode = (string)dr["src_source_code"],
                        Definition = dr["src_definition"] is DBNull ? null : (string)dr["src_definition"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                    };
                    var targetFunction = new Function
                    {
                        FunctionName = (string)dr["function_name"],
                        IdentityArgumentsHash = (string)dr["identity_arguments_hash"],
                        IdentityArguments = (string)dr["tgt_identity_arguments"],
                        ArgumentDataTypes = (string)dr["tgt_argument_data_types"],
                        ResultDataType = dr["tgt_result_data_type"] is DBNull ? null : (string)dr["tgt_result_data_type"],
                        Owner = (string)dr["tgt_owner"],
                        FunctionType = (string)dr["tgt_function_type"],
                        Volatility = (string)dr["tgt_volatility"],
                        Parallel = (string)dr["tgt_parallel"],
                        Security = (string)dr["tgt_security"],
                        Language = (string)dr["tgt_language"],
                        SourceCode = (string)dr["tgt_source_code"],
                        Definition = dr["tgt_definition"] is DBNull ? null : (string)dr["tgt_definition"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                    };
                    sourceFunction.Compare(targetFunction, this._schemaMapping, list);
                }
            }
        }

    }
}
