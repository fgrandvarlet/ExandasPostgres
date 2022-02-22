using System;
using System.Collections.Generic;

using ExandasPostgres.Dao;

namespace ExandasPostgres.Domain
{
    public class AccessPrivilegesNormalizer
    {
        private readonly string _accessPrivileges;
        private readonly string _owner;
        private readonly PrivilegeObjectType _privilegeObjectType;
        private static readonly Dictionary<PrivilegeObjectType, string> privilegesDict = BuildPrivilegesDict();
        private readonly string[] _items;

        public string ResultWithGrantor
        {
            get
            {
                if (this._items == null) return null;
                return string.Join(",", this._items);
            }
        }

        public string ResultWithoutGrantor
        {
            get
            {
                if (this._items == null) return null;

                var list = new List<string>();
                foreach (string item in this._items)
                {
                    if (item.StartsWith(this._owner + "="))
                    {
                        continue;
                    }
                    string[] elements = item.Split('/');
                    if (elements.Length == 2)
                    {
                        list.Add(elements[0]);
                    }
                }
                return string.Join(",", list);
            }
        }

        public AccessPrivilegesNormalizer(string accessPrivileges, string owner, PrivilegeObjectType privilegeObjectType)
        {
            this._owner = owner ?? throw new ArgumentNullException(nameof(owner));
            this._privilegeObjectType = privilegeObjectType;

            if (privilegeObjectType != PrivilegeObjectType.TableColumn &&
                privilegeObjectType != PrivilegeObjectType.ViewColumn &&
                privilegeObjectType != PrivilegeObjectType.MaterializedViewColumn &&
                privilegeObjectType != PrivilegeObjectType.ForeignTableColumn &&
                privilegeObjectType != PrivilegeObjectType.IndexColumn)
            {
                this._accessPrivileges = accessPrivileges ?? string.Format("{0}={1}/{0}", this._owner, privilegesDict[this._privilegeObjectType]);
            }
            else
            {
                this._accessPrivileges = accessPrivileges;
            }
            
            if (this._accessPrivileges != null)
            {
                string[] items = this._accessPrivileges.Split(',');
                Array.Sort(items);
                this._items = items;
            }
        }

        private static Dictionary<PrivilegeObjectType, string> BuildPrivilegesDict()
        {
            var dict = new Dictionary<PrivilegeObjectType, string>
            {
                [PrivilegeObjectType.Table] = "arwdDxt",
                [PrivilegeObjectType.View] = "arwdDxt",
                [PrivilegeObjectType.MaterializedView] = "arwdDxt",
                [PrivilegeObjectType.ForeignTable] = "arwdDxt",
                [PrivilegeObjectType.TableColumn] = "arwx",
                [PrivilegeObjectType.ViewColumn] = "arwx",
                [PrivilegeObjectType.MaterializedViewColumn] = "arwx",
                [PrivilegeObjectType.ForeignTableColumn] = "arwx",
                [PrivilegeObjectType.IndexColumn] = "",
                [PrivilegeObjectType.Sequence] = "rwU",
                [PrivilegeObjectType.Function] = "X",
                [PrivilegeObjectType.Domain] = "U",
                [PrivilegeObjectType.Type] = "U",
            };
            return dict;
        }

    }
}
