﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Core
{
    public static class Defs
    {
        internal const string APPLICATION_TITLE = "Exandas - Postgres";

        internal static readonly Guid EMPTY_ITEM_GUID = Guid.Empty;
        internal static readonly string EMPTY_ITEM_STRING = string.Empty;
        internal const short EMPTY_ITEM_SHORT = 0;
        internal static readonly string EMPTY_ITEM_LABEL = Strings.NotSpecified;

        internal const string REPORTS_DIRECTORY = "reports";

        internal static readonly int MAX_PROPERTY_SIZE = (int)Math.Pow(2, 15) - 20; // 32768 - 20 = 32748

        static Defs()
        {
        }

        public static string TruncateTooLong(string str)
        {
            if (str != null)
            {
                if (str.Length > MAX_PROPERTY_SIZE)
                {
                    return string.Format("[{0}] {1}", Strings.Truncated, str.Substring(0, MAX_PROPERTY_SIZE));
                }
                else
                {
                    return str;
                }
            }
            else
            {
                return str;
            }
        }

        public static string QuoteValue(string str)
        {
            string result = str.Replace("'", "''");
            result = "'" + result + "'";
            return result;
        }

        public static string GetLabel(LabelId labelId)
        {
            switch (labelId)
            {
                case LabelId.ObjectInSourceNotInTarget:
                    return Strings.ObjectInSource;
                case LabelId.ObjectInTargetNotInSource:
                    return Strings.ObjectInTarget;
                case LabelId.PropertyDifference:
                    return Strings.PropertyDifference;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// cf. https://stackoverflow.com/questions/1547476/easiest-way-to-split-a-string-on-newlines-in-net
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (input == null)
            {
                yield break;
            }

            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// cf. https://github.com/npgsql/npgsql/blob/main/src/Npgsql/Util/VersionExtensions.cs
        /// </summary>
        /// <param name="version"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>
        public static bool IsGreaterOrEqual(this Version version, int major, int minor)
            => version.Major != major ? version.Major > major : version.Minor >= minor;

        public static void ErrorDialog(string message)
        {
            MessageBox.Show(
                message,
                Strings.ExandasPostgresError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static void ValidatingErrorDialog(string message)
        {
            MessageBox.Show(
                string.Format(
                    "{0}" + System.Environment.NewLine + System.Environment.NewLine + "{1}",
                    Strings.FormError,
                    message
                ),
                Strings.Warning,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static bool ConfirmDeleteDialog(string record)
        {
            DialogResult dr = MessageBox.Show(
                string.Format(
                    Strings.AreYouSureDelete +
                    Environment.NewLine +
                    Environment.NewLine +
                    "{0} ?",
                    record
                ),
                Strings.DeleteRecord,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );
            return (dr == DialogResult.Yes);
        }

        #region dropdown lists sources

        public static List<KeyValuePair<Guid, string>> GetConnectionReferenceList()
        {
            var list = new List<KeyValuePair<Guid, string>>
            {
                new KeyValuePair<Guid, string>(EMPTY_ITEM_GUID, EMPTY_ITEM_LABEL)
            };

            foreach (var cp in DaoFactory.Instance.GetConnectionParamsDao().GetList())
            {
                list.Add(new KeyValuePair<Guid, string>(cp.Uid, cp.FormattedString));
            }
            return list;
        }

        public static List<KeyValuePair<string, string>> GetEntityReferenceList()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(EMPTY_ITEM_STRING, EMPTY_ITEM_LABEL)
            };

            foreach (var er in DaoFactory.Instance.GetReferenceDao().GetEntityReferenceList())
            {
                list.Add(new KeyValuePair<string, string>(er.Entity, er.Entity));
            }
            return list;
        }

        public static List<KeyValuePair<short, string>> GetLabelReferenceList()
        {
            var list = new List<KeyValuePair<short, string>>
            {
                new KeyValuePair<short, string>(EMPTY_ITEM_SHORT, EMPTY_ITEM_LABEL),
                new KeyValuePair<short, string>((short)LabelId.ObjectInSourceNotInTarget, Strings.ObjectInSource),
                new KeyValuePair<short, string>((short)LabelId.ObjectInTargetNotInSource, Strings.ObjectInTarget),
                new KeyValuePair<short, string>((short)LabelId.PropertyDifference, Strings.PropertyDifference)
            };
            return list;
        }

        public static List<KeyValuePair<string, string>> GetPropertyReferenceListByEntity(EntityReference er)
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(EMPTY_ITEM_STRING, EMPTY_ITEM_LABEL)
            };

            foreach (var pr in DaoFactory.Instance.GetReferenceDao().GetPropertyReferenceListByEntity(er))
            {
                list.Add(new KeyValuePair<string, string>(pr.Property, pr.Property));
            }
            return list;
        }

        #endregion

    }
}
