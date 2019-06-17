using Jasmine.Extensions;
using Jasmine.Orm.Attributes;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Orm
{
    public class DefaultTemplateCache : ITableTemplateCache
    {
        public DefaultTemplateCache(Type type)
        {
            _type = type;
        }
        private readonly object _locker = new object();
        private string[] _stringCache = new string[58];
        private SqlTemplate[] _templateCache = new SqlTemplate[11];

        private ITableMetaDataProvider _tableProvider => DefaultTableMetaDataProvider.Instance;
        private ITableTemplateCacheProvider _templateProvider => DefaultTableTemplateCacheProvider.Instance;
        private Type _type;
        private string _tableName => _table.Name;
        private TableMetaData _table => _tableProvider.GetTable(_type);

        /*
         * Keywords
         */
        private const string TABLE = " Table ";
        private const string CREATE_TABLE = "Create Table ";
        private const string VALUES = " Values ";
        private const string PRIMARY_KEY = " Primary key ";
        private const string UNIQUE = " Unique ";
        private const string FOREIGN_KEY = " Foreign key ";
        private const string CHECK = " Check ";
        private const string DEFAULT = " Default ";
        private const string NOT_NULL = " Not Null ";
        private const string SELECT = " Select ";
        private const string FROM = " From ";
        private const string WHERE = " Where ";
        private const string TOP = " Top ";
        private const string ORDERBY = "Order By ";
        private const string ASC = " Asc ";
        private const string DESC = " Desc ";
        private const string LIMIT = " Limit ";
        private const string INSERT_INTO = " Insert Into ";
        private const string DELETE = " Delete ";
        private const string DROP = " Drop ";


        private List<ColumnMetaData> getJoinColumn(TableMetaData table, string prefix)
        {
            var ls = new List<ColumnMetaData>();

            foreach (var joinColumnTable in table.JoinColumns.Values)
            {
                foreach (var column in joinColumnTable.Table.Columns.Values)
                {
                    var _new = column.Clone();

                    _new.ColumnName = prefix + joinColumnTable.Table.Name + "_" + _new.ColumnName;

                    ls.Add(_new);
                }

                if (joinColumnTable.Table.HasJoinColumns)
                    ls.AddRange(getJoinColumn(joinColumnTable.Table, prefix + joinColumnTable.Table.Name + "_"));
            }


            return ls;
        }
        /// <summary>
        /// index 1
        /// </summary>
        /// <returns></returns>
        public string GetCreate()
        {
            lock (_locker)
            {
                if (_stringCache[0] == null)
                {
                    var builder = new StringBuilder();

                    var table = _tableProvider.GetTable(_type);

                    var columns = new List<ColumnMetaData>();

                    columns.AddRange(table.Columns.Values);

                    if (table.HasJoinColumns)
                        columns.AddRange(getJoinColumn(_table, ""));

                    builder.Append(createSqlServerInternal(table.Name, columns));

                    if (table.HasJoinTable)
                    {
                        foreach (var item in table.JoinTables.Values)
                        {
                            var template = _templateProvider.GetCache(item.Table.RelatedType).GetCreate();

                            builder.Append("\r\n" + template);
                        }

                    }

                    _stringCache[0] = builder.ToString();
                }
            }

            return _stringCache[0];
        }



        private string createSqlServerInternal(string name, IEnumerable<ColumnMetaData> columns)
        {
            var builder = new StringBuilder();

            builder.Append($"{CREATE_TABLE} {name} (");

            foreach (var item in columns)
            {
                builder.Append($"{item.ColumnName} {item.SqlType} ");

                foreach (var constraint in item.Constraints)
                {
                    if (constraint is PrimaryKeyAttribute)
                    {
                        builder.Append(PRIMARY_KEY);
                    }
                    else if (constraint is ForeignKeyAttribute)
                    {
                        builder.Append(FOREIGN_KEY);
                    }
                    else if (constraint is UniqueAttribute)
                    {
                        builder.Append(UNIQUE);
                    }
                    else if (constraint is NotNullAttribute)
                    {
                        builder.Append(NOT_NULL);
                    }
                    else if (constraint is DefaultAttribute _default)
                    {
                        builder.Append($"{DEFAULT} ({_default.DefaultValue}) ");
                    }
                    else if (constraint is CheckAttribute check)
                    {
                        builder.Append($"{CHECK} {check.Expression} ");
                    }
                }

                builder.Append(", ");
            }

            builder.RemoveLastComa();

            builder.Append(")");

            return builder.ToString();

        }

        /// <summary>
        /// index 2
        /// </summary>
        /// <returns></returns>
        public string GetCreateWith(string table)
        {
            lock (_locker)
            {
                if (_stringCache[0] == null)
                {
                    var builder = new StringBuilder();

                    var tb = _tableProvider.GetTable(_type);

                    var columns = new List<ColumnMetaData>();

                    columns.AddRange(tb.Columns.Values);

                    if (tb.HasJoinColumns)
                    {
                        foreach (var joinColumns in tb.JoinColumns)
                        {
                            columns.AddRange(getJoinColumn(_table, ""));
                        }
                    }

                    builder.Append(createSqlServerInternal(table, columns));

                    if (tb.HasJoinTable)
                    {
                        foreach (var item in tb.JoinTables.Values)
                        {
                            var template = _templateProvider.GetCache(item.Table.RelatedType).GetCreate();

                            builder.Append(template);
                        }

                    }

                    _stringCache[0] = builder.ToString();
                }
            }

            return _stringCache[0];
        }
        /// <summary>
        /// index 3
        /// </summary>
        /// <returns></returns>
        public string GetDelete(string condition)
        {
            lock (_locker)
            {
                if (_stringCache[2] == null)
                    _stringCache[2] = $"{DELETE} {FROM} {_tableName} @condition";
            }

            return _stringCache[2].Replace("@condition", condition);
        }
        /// <summary>
        /// index 4
        /// </summary>
        /// <returns></returns>
        public string GetDeleteWith(string table, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[3] == null)
                {
                    _stringCache[3] = $"{DELETE} {FROM} @table {WHERE} @condition";
                }
            }

            return _stringCache[3].Replace("@table", table)
                                  .Replace("@condition", condition);
        }
        /// <summary>
        /// index 5
        /// </summary>
        /// <returns></returns>
        public string GetDrop()
        {
            lock (_locker)
            {
                if (_stringCache[4] == null)
                    _stringCache[4] = $"{DROP} {TABLE} {_tableName} ";
            }

            return _stringCache[4];
        }
        /// <summary>
        /// index 6
        /// </summary>
        /// <returns></returns>
        public string GetDropWith(string table)
        {
            lock (_locker)
            {
                if (_stringCache[5] == null)
                    _stringCache[5] = $"{DROP} {TABLE} @table ";
            }

            return _stringCache[4].Replace("@table", table);
        }
        /// <summary>
        /// sql index 1
        /// </summary>
        /// <returns></returns>
        public SqlTemplate GetInsert()
        {
            lock (_locker)
            {
                if (_templateCache[0] == null)
                {
                    var ls = new List<SqlTemplateSegment>();

                    var columns = getAllColumns();

                    var builder = new StringBuilder();

                    builder.Append($"{INSERT_INTO} {_tableName}(");

                    foreach (var item in columns)
                    {
                        builder.Append(item.ColumnName.Replace(".", "_") + ",");
                    }

                    builder.RemoveLastComa();

                    builder.Append($"){VALUES}( ");

                    ls.Add(new SqlTemplateSegment(builder.ToString(), false));

                    foreach (var item in columns)
                    {
                        ls.Add(new SqlTemplateSegment(item.ColumnName.Replace("_",".").ToLower(), true));
                        ls.Add(new SqlTemplateSegment(", ", false));
                    }

                    if (ls[ls.Count - 1].Value == ", ")
                        ls.RemoveAt(ls.Count - 1);

                    ls.Add(new SqlTemplateSegment(")", false));

                    ls.Add(new SqlTemplateSegment("\r\n", false));

                    if (_table.HasJoinTable)
                    {
                        foreach (var item in _table.JoinTables.Values)
                        {
                            var template = _templateProvider.GetCache(item.Table.RelatedType).GetInsert(item.PropertyName.ToLower());

                            ls.AddRange(template.Segments);
                        }

                    }

                    _templateCache[0] = new SqlTemplate()
                    {
                        Segments = ls.ToArray()
                    };

                }
            }

            return _templateCache[0];
        }

       public SqlTemplate GetInsert(string prefix)
        {

            var ls = new List<SqlTemplateSegment>();

            var columns = getAllColumns();

            var builder = new StringBuilder();

            builder.Append($"{INSERT_INTO} {_tableName}(");

            foreach (var item in columns)
            {
                builder.Append(item.ColumnName.Replace(".", "_") + ",");
            }

            builder.RemoveLastComa();

            builder.Append($"){VALUES}( ");

            ls.Add(new SqlTemplateSegment(builder.ToString(), false));

            foreach (var item in columns)
            {
                ls.Add(new SqlTemplateSegment($"{prefix}." + item.ColumnName.ToLower(), true));
                ls.Add(new SqlTemplateSegment(",", false));
            }

            if (ls[ls.Count - 1].Value == ",")
                ls.RemoveAt(ls.Count - 1);

            ls.Add(new SqlTemplateSegment(")", false));

            if (_table.HasJoinTable)
            {
                foreach (var item in _table.JoinTables.Values)
                {
                    var template = _templateProvider.GetCache(item.Table.RelatedType).GetInsert($"{prefix}.{item.PropertyName.ToLower()}");

                    ls.AddRange(template.Segments);
                }

            }


            return new SqlTemplate()
            {
                Segments = ls.ToArray()
            };

        }

        private List<ColumnMetaData> getAllColumns()
        {
            var table = _tableProvider.GetTable(_type);

            var columns = new List<ColumnMetaData>();

            columns.AddRange(table.Columns.Values);

            if (table.HasJoinColumns)
                columns.AddRange(getJoinColumn(_table, ""));

            return columns;
        }


        /// <summary>
        /// sql index 2
        /// </summary>
        /// <returns></returns>
        public SqlTemplate GetInsertPartial(params string[] columns)
        {
            var template = new SqlTemplate();

            var ls = new List<SqlTemplateSegment>();

            var builder = new StringBuilder();

            builder.Append($"{INSERT_INTO} {_tableName}(");

            foreach (var item in columns)
                builder.Append(item.Replace(".", "_") + ",");

            builder.RemoveLastComa();

            builder.Append($"){VALUES}( ");

            ls.Add(new SqlTemplateSegment(builder.ToString(), false));

            foreach (var item in columns)
            {
                ls.Add(new SqlTemplateSegment(item.Replace("_", "."), true));
                ls.Add(new SqlTemplateSegment(",", false));
            }

            if (ls[ls.Count - 1].Value == ",")
                ls.RemoveAt(ls.Count - 1);

            ls.Add(new SqlTemplateSegment(")", false));

            template.Segments = ls.ToArray();

            return template;
        }
        /// <summary>
        /// sql index 3
        /// </summary>
        /// <returns></returns>
        public SqlTemplate GetInsertPartialWith(string table, params string[] columns)
        {
            var template = new SqlTemplate();

            var ls = new List<SqlTemplateSegment>();

            var builder = new StringBuilder();

            builder.Append($"{INSERT_INTO} {table}(");

            foreach (var item in columns)
                builder.Append(item.Replace(".", "_") + ",");

            builder.RemoveLastComa();

            builder.Append($"){VALUES}( ");

            ls.Add(new SqlTemplateSegment(builder.ToString(), false));

            foreach (var item in columns)
            {
                ls.Add(new SqlTemplateSegment(item.Replace("_", "."), true));
                ls.Add(new SqlTemplateSegment(",", false));
            }

            if (ls[ls.Count - 1].Value == ",")
                ls.RemoveAt(ls.Count - 1);

            ls.Add(new SqlTemplateSegment(")", false));

            template.Segments = ls.ToArray();

            return template;
        }
        /// <summary>
        /// sql index 4
        /// </summary>
        /// <returns></returns>
        public SqlTemplate GetInsertWith(string table)
        {
            lock (_locker)
            {
                if (_templateCache[1] == null)
                {
                    var ls = new List<SqlTemplateSegment>();

                    var columns = getAllColumns();

                    var builder = new StringBuilder();

                    builder.Append($"{INSERT_INTO} {table}(");

                    foreach (var item in columns)
                    {
                        builder.Append(item.ColumnName.Replace(".", "_") + ",");
                    }

                    builder.RemoveLastComa();

                    builder.Append($"){VALUES}( ");

                    ls.Add(new SqlTemplateSegment(builder.ToString(), false));

                    foreach (var item in columns)
                    {
                        ls.Add(new SqlTemplateSegment(item.ColumnName, true));
                        ls.Add(new SqlTemplateSegment(",", false));
                    }

                    if (ls[ls.Count - 1].Value == ",")
                        ls.RemoveAt(ls.Count - 1);

                    ls.Add(new SqlTemplateSegment(")", false));

                    if (_table.HasJoinTable)
                    {
                        foreach (var item in _table.JoinTables.Values)
                        {
                            var template = _templateProvider.GetCache(item.Table.RelatedType).GetInsert();

                            ls.AddRange(template.Segments);
                        }
                    }

                    _templateCache[1] = new SqlTemplate()

                    {
                        Segments = ls.ToArray()
                    };
                }
            }

            return _templateCache[0];
        }
        /// <summary>
        /// index 11
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            lock (_locker)
            {
                if (_stringCache[10] == null)
                    _stringCache[10] = $"{SELECT} * {FROM} {_tableName} ";
            }

            return _stringCache[10];
        }
        /// <summary>
        /// index 12
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditional(string condition)
        {
            lock (_locker)
            {
                if (_stringCache[11] == null)
                    _stringCache[11] = $"{SELECT} * {FROM} {_tableProvider.GetTable(_type).Name} {getLeftJoin(_tableName)} {WHERE} @condition ";
            }

            return _stringCache[11].Replace("@condition", condition);
        }

        private string getLeftJoin(string name)
        {
            var builder = new StringBuilder();

            if (_table.HasJoinTable)
            {
                foreach (var item in _table.JoinTables.Values)
                    builder.Append($"left join {item.Table.Name} on {name}.{item.InnerKey}={item.Table.Name}.{item.OutterKey} ");
            }

            return builder.ToString();
        }
        /// <summary>
        /// index 13
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditionalOrderByAsc(string condition, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[12] == null)
                    _stringCache[12] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";
            }

            return _stringCache[12].Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 14
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditionalOrderByAscWith(string table, string condition, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[13] == null)
                    _stringCache[13] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";
            }

            return _stringCache[13].Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 15
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditionalOrderByDesc(string condition, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[14] == null)
                    _stringCache[14] = $"{SELECT} * {FROM} {_tableProvider.GetTable(_type).Name} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {DESC} ";
            }

            return _stringCache[14].Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 16
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditionalOrderByDescWith(string table, string condition, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[15] == null)
                    _stringCache[15] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")}  {WHERE} @condition {ORDERBY} @orderBy {DESC} ";
            }

            return _stringCache[15].Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 17
        /// </summary>
        /// <returns></returns>
        public string GetQueryConditionalWith(string table, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[16] == null)
                    _stringCache[16] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")} {WHERE} @condition  ";
            }

            return _stringCache[16].Replace("@condition", condition)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 18
        /// </summary>
        /// <returns></returns>
        public string GetQueryOrderByAsc(string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[17] == null)
                    _stringCache[17] = $"{SELECT} * {FROM} {_table} {getLeftJoin(_tableName)} {ORDERBY} @orderBy {ASC}  ";
            }

            return _stringCache[17].Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 19
        /// </summary>
        /// <returns></returns>
        public string GetQueryOrderByAscWith(string table, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[18] == null)
                    _stringCache[18] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {ASC}  ";
            }

            return _stringCache[18].Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 20
        /// </summary>
        /// <returns></returns>
        public string GetQueryOrderByDesc(string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[19] == null)
                    _stringCache[19] = $"{SELECT} * {FROM} {_tableName} {ORDERBY} {getLeftJoin(_tableName)} @orderBy {DESC}  ";
            }

            return _stringCache[19].Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 21
        /// </summary>
        /// <returns></returns>
        public string GetQueryOrderByDescWith(string table, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[20] == null)
                    _stringCache[20] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {DESC}  ";
            }

            return _stringCache[20].Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 22
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartial(params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[21] == null)
                    _stringCache[21] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} ";
            }

            return _stringCache[21].Replace("@body", generateColumnsString(columns));
        }


        private string requireColumnExistAndFormat(string column)
        {
            if (column == null)
                throw new ArgumentNullException("column can not be null!");

            var segs = column.Splite1(".");

            // search columns
            if (segs.Count == 1)
            {
                column = column.ToLower();

                foreach (var item in _table.Columns.Keys)
                {
                    if (column == item.ToLower())
                        return item;
                }

                throw new ColumnNotExistsException($"column({column}) not exists in table {_table.RelatedType}");
            }
            // search join columns
            else
            {
                var t = 0;
                var table = _table;
                var _newColumn = string.Empty;

                foreach (var item in segs)
                {
                    var temp = item.ToLower();

                    // last seg search from columns
                    if (t == segs.Count - 1)
                    {
                        foreach (var columnkey in table.Columns.Keys)
                        {
                            if (temp == columnkey.ToLower())
                                return _newColumn + columnkey;
                        }

                        throw new ColumnNotExistsException($"column({column}) not exists in table {_table.RelatedType}");
                    }

                    //find table 

                    bool tableFound = false;

                    foreach (var joinColumnTable in table.JoinColumns)
                    {
                        if (temp == joinColumnTable.Key.ToLower())
                        {
                            table = joinColumnTable.Value.Table;

                            _newColumn += $"{joinColumnTable.Key}_";

                            tableFound = true;
                        }
                    }

                    if (!tableFound)
                    {
                        throw new ColumnNotExistsException($"column({column}) not exists in table {_table.RelatedType}");
                    }

                    ++t;
                }
            }

            throw new ColumnNotExistsException($"column({column}) not exists in table {_table.RelatedType}");
        }
        private string generateColumnsString(string[] columns)
        {
            var builder = new StringBuilder();

            foreach (var item in columns)
                builder.Append($"{requireColumnExistAndFormat(item)},");

            return builder.RemoveLastComa().ToString();
        }

        /// <summary>
        /// index 23
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditional(string condition, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[22] == null)
                    _stringCache[22] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition  ";
            }

            return _stringCache[22].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition);
        }
        /// <summary>
        /// index 24
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditionalOrderByAsc(string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[23] == null)
                    _stringCache[23] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {ORDERBY} @orderBy {ASC}  {WHERE} @condition  ";
            }

            return _stringCache[23].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 25
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditionalOrderByAscWith(string table, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[24] == null)
                    _stringCache[24] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {ASC}  {WHERE} @condition  ";
            }

            return _stringCache[24].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 26
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditionalOrderByDesc(string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[25] == null)
                    _stringCache[25] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {ORDERBY} @orderBy {DESC}  {WHERE} @condition  ";
            }

            return _stringCache[25].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 27
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditionalOrderByDescWith(string table, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[26] == null)
                    _stringCache[26] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {DESC}  {WHERE} @condition  ";
            }

            return _stringCache[26].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition).Replace("@orderBy", orderBy)
                                   .Replace($"table", table);
        }
        /// <summary>
        /// index 28
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialConditionalWith(string table, string condition, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[27] == null)
                    _stringCache[27] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {ORDERBY}  {WHERE} @condition  ";
            }

            return _stringCache[27].Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 29
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialOrderByAsc(string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[28] == null)
                    _stringCache[28] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {ORDERBY} @orderBy {ASC}    ";
            }

            return _stringCache[28].Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 30
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialOrderByAscWith(string table, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[29] == null)
                    _stringCache[29] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {ASC}    ";
            }

            return _stringCache[29].Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 31
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialOrderByDesc(string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[30] == null)
                    _stringCache[30] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {ORDERBY} @orderBy {DESC}    ";
            }

            return _stringCache[30].Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 32
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialOrderByDescWith(string table, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[31] == null)
                    _stringCache[31] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {ORDERBY} @orderBy {DESC}    ";
            }

            return _stringCache[31].Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy).Replace("@table", table);
        }
        /// <summary>
        /// index 33
        /// </summary>
        /// <returns></returns>
        public string GetQueryPartialWith(string table, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[32] == null)
                    _stringCache[32] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")}  ";
            }

            return _stringCache[32].Replace("@body", generateColumnsString(columns))
                                   .Replace("@table", table);
        }

        private string markTop(string plain, DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.SqlServer:
                    return plain.Replace($"{SELECT}", $"{SELECT} {TOP} @count ");
                case DataSource.Oracle:
                    return $"{SELECT} * {FROM}({plain}) where rownum < @count";
                case DataSource.MySql:
                case DataSource.Sqlite:
                    return $"{plain} {LIMIT} 0,@count ";
                case DataSource.Db2:
                    return $"{plain} fetch first @count rows only ";
                case DataSource.Access:
                default:
                    throw new NotSurpportedDataSourceException(dataSource);
            }
        }
        /// <summary>
        /// index 34
        /// </summary>
        /// <returns></returns>
        public string GetQueryTop(int count)
        {
            lock (_locker)
            {
                if (_stringCache[33] == null)
                {
                    _stringCache[33] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)}  ";

                    _stringCache[33] = markTop(_stringCache[33], _table.DataSource);
                }
            }

            return _stringCache[33].Replace("@count", $"{count}");
        }
        /// <summary>
        /// index 35
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditional(int count, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[34] == null)
                {
                    _stringCache[34] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition  ";

                    _stringCache[34] = markTop(_stringCache[34], _table.DataSource);
                }
            }

            return _stringCache[34].Replace("@count", $"{count}")
                                   .Replace("@condition", condition);
        }
        /// <summary>
        /// index 36
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditionalOrderByAsc(int count, string orderBy, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[35] == null)
                {
                    _stringCache[35] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";

                    _stringCache[35] = markTop(_stringCache[35], _table.DataSource);
                }
            }

            return _stringCache[35].Replace("@count", $"{count}")
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 37
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditionalOrderByAscWith(string table, int count, string orderBy, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[36] == null)
                {
                    _stringCache[36] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";

                    _stringCache[36] = markTop(_stringCache[36], _table.DataSource);
                }
            }

            return _stringCache[35].Replace("@count", $"{count}")
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 38
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditionalOrderByDesc(int count, string orderBy, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[37] == null)
                {
                    _stringCache[37] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {DESC} ";

                    _stringCache[37] = markTop(_stringCache[37], _table.DataSource);
                }
            }

            return _stringCache[37].Replace("@count", $"{count}")
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 39
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditionalOrderByDescWith(string table, int count, string orderBy, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[38] == null)
                {
                    _stringCache[38] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy {DESC} ";

                    _stringCache[38] = markTop(_stringCache[38], _table.DataSource);
                }
            }

            return _stringCache[38].Replace("@count", $"{count}")
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 40
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopConditionalWith(string table, int count, string condition)
        {
            lock (_locker)
            {
                if (_stringCache[39] == null)
                {
                    _stringCache[39] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")} {WHERE} @condition  ";

                    _stringCache[39] = markTop(_stringCache[39], _table.DataSource);
                }
            }

            return _stringCache[39].Replace("@count", $"{count}")
                                   .Replace("@condition", condition)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 41
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopOrderByAsc(int count, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[40] == null)
                {
                    _stringCache[40] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)}   {ORDERBY} @orderBy {ASC} ";

                    _stringCache[40] = markTop(_stringCache[40], _table.DataSource);
                }
            }

            return _stringCache[40].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 42
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopOrderByAscWith(string table, int count, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[41] == null)
                {
                    _stringCache[41] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")}   {ORDERBY} @orderBy {ASC} ";

                    _stringCache[41] = markTop(_stringCache[41], _table.DataSource);
                }
            }

            return _stringCache[41].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 43
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopOrderByDesc(int count, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[42] == null)
                {
                    _stringCache[42] = $"{SELECT} * {FROM} {_tableName} {getLeftJoin(_tableName)}   {ORDERBY} @orderBy {DESC} ";

                    _stringCache[42] = markTop(_stringCache[42], _table.DataSource);
                }
            }

            return _stringCache[42].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 44
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopOrderByDescWith(string table, int count, string orderBy)
        {
            lock (_locker)
            {
                if (_stringCache[43] == null)
                {
                    _stringCache[43] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")}   {ORDERBY} @orderBy {DESC} ";

                    _stringCache[43] = markTop(_stringCache[43], _table.DataSource);
                }
            }

            return _stringCache[43].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 45
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopParitialOrderByAsc(int count, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[44] == null)
                {
                    _stringCache[44] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)}   {ORDERBY} @orderBy {ASC} ";

                    _stringCache[44] = markTop(_stringCache[44], _table.DataSource);
                }
            }

            return _stringCache[44].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@body", generateColumnsString(columns));
        }
        /// <summary>
        /// index 46
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopParitialOrderByAscWith(string table, int count, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[45] == null)
                {
                    _stringCache[45] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")}   {ORDERBY} @orderBy {ASC} ";

                    _stringCache[45] = markTop(_stringCache[45], _table.DataSource);
                }
            }

            return _stringCache[45].Replace("@count", $"{count}")
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 47
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartial(int count, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[46] == null)
                {
                    _stringCache[46] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)}  ";

                    _stringCache[46] = markTop(_stringCache[46], _table.DataSource);
                }
            }

            return _stringCache[46].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns));
        }
        /// <summary>
        /// index 48
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditional(int count, string condition, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[47] == null)
                {
                    _stringCache[47] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition  ";

                    _stringCache[47] = markTop(_stringCache[47], _table.DataSource);
                }
            }

            return _stringCache[47].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition);
        }
        /// <summary>
        /// index 49
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditionalOrderByAsc(int count, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[48] == null)
                {
                    _stringCache[48] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";

                    _stringCache[48] = markTop(_stringCache[47], _table.DataSource);
                }
            }

            return _stringCache[48].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 50
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditionalOrderByAscWith(string table, int count, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[49] == null)
                {
                    _stringCache[49] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy {ASC} ";

                    _stringCache[49] = markTop(_stringCache[49], _table.DataSource);
                }
            }

            return _stringCache[49].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@table", table).Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 51
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditionalOrderByDesc(int count, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[50] == null)
                {
                    _stringCache[50] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy {DESC} ";

                    _stringCache[50] = markTop(_stringCache[50], _table.DataSource);
                }
            }

            return _stringCache[50].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 52
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditionalOrderByDescWith(string table, int count, string condition, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[51] == null)
                {
                    _stringCache[51] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy {DESC} ";

                    _stringCache[51] = markTop(_stringCache[51], _table.DataSource);
                }
            }

            return _stringCache[51].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 53
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialConditionalWith(string table, int count, string condition, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[52] == null)
                {
                    _stringCache[52] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {WHERE} @condition  ";

                    _stringCache[52] = markTop(_stringCache[52], _table.DataSource);
                }
            }

            return _stringCache[50].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@condition", condition)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 54
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialOrderByDesc(int count, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[53] == null)
                {
                    _stringCache[53] = $"{SELECT} @body {FROM} {_tableName} {getLeftJoin(_tableName)} {WHERE} @condition {ORDERBY} @orderBy Desc  ";

                    _stringCache[53] = markTop(_stringCache[53], _table.DataSource);
                }
            }

            return _stringCache[53].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy);
        }
        /// <summary>
        /// index 55
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialOrderByDescWith(string table, int count, string orderBy, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[54] == null)
                {
                    _stringCache[54] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")} {WHERE} @condition {ORDERBY} @orderBy Desc  ";

                    _stringCache[54] = markTop(_stringCache[54], _table.DataSource);
                }
            }

            return _stringCache[54].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@orderBy", orderBy)
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 56
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopPartialWith(string table, int count, params string[] columns)
        {
            lock (_locker)
            {
                if (_stringCache[55] == null)
                {
                    _stringCache[55] = $"{SELECT} @body {FROM} @table {getLeftJoin("@table")}   ";

                    _stringCache[55] = markTop(_stringCache[55], _table.DataSource);
                }
            }

            return _stringCache[55].Replace("@count", $"{count}")
                                   .Replace("@body", generateColumnsString(columns))
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 57
        /// </summary>
        /// <returns></returns>
        public string GetQueryTopWith(string table, int count)
        {
            lock (_locker)
            {
                if (_stringCache[56] == null)
                {
                    _stringCache[56] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")}   ";

                    _stringCache[56] = markTop(_stringCache[56], _table.DataSource);
                }
            }

            return _stringCache[56].Replace("@count", $"{count}")
                                   .Replace("@table", table);
        }
        /// <summary>
        /// index 58
        /// </summary>
        /// <returns></returns>
        public string GetQueryWith(string table)
        {
            lock (_locker)
            {
                if (_stringCache[56] == null)
                    _stringCache[56] = $"{SELECT} * {FROM} @table {getLeftJoin("@table")}   ";
            }

            return _stringCache[56].Replace("@table", table);
        }

    }
}
