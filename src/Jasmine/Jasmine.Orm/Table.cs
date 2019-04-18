using Jasmine.Orm.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Orm.Model
{
    public class Table:IEnumerable<Column>
    {
        private string _insertPrefixPart2;
        private string _createTableSTring;
        private string _allColumns;
        private readonly object _lockObject = new object();
        public Type RelatedType { get; set; }
        public IDictionary<string, Column> Columns { get; set; } = new Dictionary<string, Column>();
        public Func<object> Constructor { get; set; }
        public string CreateTable(string tableName)
        {
            lock (_lockObject)
            {
                var prefix = $"CREATE TABLE {tableName} (";

                if (_createTableSTring == null)//generate create table string
                {
                    var sb = new StringBuilder();

                    foreach (var item in Columns.Values)
                    {
                        sb.Append(item.SqlName)
                          .Append(" ");

                        if(item.SqlType==null)
                        {
                            sb.Append(SqlTypeCovertor.ConvertCsharpTypeToSqlType(item.RelatedType))
                               .Append(" ");
                        }
                        else
                        {
                            sb.Append(item.SqlType)
                              .Append(" ");
                        }

                        foreach (var attr in item.Constraints.Values)
                        {
                            if(attr is PrimaryKeyAttribute)
                            {
                                sb.Append(AttributeUtls.PRIMARYKEY);
                            }
                            else if(attr is NotNullAttribute)
                            {
                                sb.Append(AttributeUtls.NOTNULL);
                            }
                            else if(attr is DefaultAttribute)
                            {
                                sb.Append(AttributeUtls.DEFAULT + $"({((DefaultAttribute)attr).DefaultValue}) ");
                            }
                            else if(attr is CheckAttribute)
                            {
                                sb.Append($"{AttributeUtls.CHECK}{((CheckAttribute)attr).Expression} ");
                            }
                            else if(attr is ForeignKeyAttribute)
                            {
                                sb.Append(AttributeUtls.FOREIGNKEY);
                            }
                            else if(attr is UniqueAttribute)
                            {
                                sb.Append(AttributeUtls.UNIQUE);
                            }
                        }

                        sb.Append(",");

                    }

                    if (sb.Length == 0)
                        throw new ArgumentException($"this no property in this type({RelatedType.Name})");


                     sb.Remove(sb.Length - 1, 1);

                    _createTableSTring = sb.ToString();
                }

                return prefix + _createTableSTring + ")";
            }
        }

        public string CreateInsertPrefix(string table)
        {
            lock(_lockObject)
            {
                var insertPrefixPart1 = $"INSERT INTO {table}";

                if(_insertPrefixPart2==null)
                {
                    var sb = new StringBuilder();

                    if (_allColumns == null)
                        getColumnsAll();

                     sb.Append("(")
                       .Append(_allColumns)
                       .Append(")")
                       .Append("VALUES");

                    _insertPrefixPart2 = sb.ToString();
                }

                return insertPrefixPart1+_insertPrefixPart2;
            }

        }

        private void getColumnsAll()
        {
            var sb = new StringBuilder();

            foreach (var item in Columns)
            {
                sb.Append(item.Value.SqlName)
                  .Append(",");
            }

            if (sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);

            _allColumns = sb.ToString();
        }

        public string CreateSelect(string table)
        {

            return $" SELECT {_allColumns} FROM {table} ";

        }
        public string CreateSelectTop(string table,int count)
        {
            return $" SELECT TOP {count} {_allColumns} FROM {table} ";
        }
        public string CreateSelectDistinct(string table)
        {
            return $" SELECT DISTINCT {_allColumns} FROM {table} ";
        }
        public string CreateSelectTopDistinc(string table,int count)
        {
            return $" SELECT DISTINCT TOP {count} {_allColumns} FROM {table} ";
        }


        public IEnumerator<Column> GetEnumerator()
        {
            foreach (var item in Columns.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Columns.Values)
            {
                yield return item;
            }
        }

       

    }
}
