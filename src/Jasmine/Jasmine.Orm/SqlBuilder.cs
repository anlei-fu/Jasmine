using Jasmine.Components;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Implements;
using Jasmine.Orm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Orm
{
    public class SqlBuilder
    {
        public SqlBuilder(DataSourceType enviroment)
        {
            Environment =enviroment;
        }

        public const string SELECT = " SELECT ";
        public const string WHERE = "  WHERE ";
        public const string VALUES = " VALUES ";
        public const string FROM = " FROM ";
        public const string SET = " SET ";
        public const string CREATETABLE = " CREATE TABLE ";
        public const string COMA = ",";
        public const string LEFT_PARREN = "(";
        public const string RIGHHT_PARREN = ")";
        public const string BLANK = " ";
        public const string AND = " AND ";
        public const string UPDATE = " UPDATE ";
        public const string DELETE_FROM = " DELETE FROM ";
        public const string INSERT_INTO = "INSERT INTO ";
        public const string EQUEL = "=";
        public const string TOP = " TOP";
        public const string AS = " AS ";
        public const string OR = " OR ";
        public const string BETWEEN = " BETWEEN ";
        public const string PRIMARY_KEY = " PRIMARY KEY";
        public const string DEFAULT = " DEFAULT ";
        public const string DROP = " DROP ";
        public const string ALTER = " ALTER ";
        public const string LIKE = " LIKE ";
        public const string LIMITE = " LIMITE ";
        public const string ORDER_BY = " ORDER BY";
        public const string GROUP_BY = " GROUP BY";
        public const string All = " * ";
        public const string BIGGER = " > ";
        public const string LESS = " < ";
        public const string BIGGEREQUEL = " >= ";
        public const string LESSEQUEL = " <= ";
        public const string LEFT_JOIN = "LEFT JOIN ";
        public const string RIGHT_JOIN = " RIGHT JOIN ";
        public const string INNER_JOIN = " INNER JOIN ";
        public const string FULL_JOIN = "FULL JOIN ";
        public const string UNION = " UNION ";
        public const string UNION_ALL = " UNION ALL ";
        public const string ON = " ON ";
        public const string DOT = ".";
        public const string IN = " IN ";
        public const string NOT_IN = " NOT IN";
        public const string EXISTS = " EXISTS ";
        public const string NOT_EXISTS = "NOT EXISTS ";
        public const string NOT = "NOT";
        public const string DISTINCT = " DISTINCT ";
        public const string USE = " USE ";
        public const string HAVING = " HAVING ";






      
        private StringBuilder _buffer=new StringBuilder();
        private ITableInfoCache _tableInfos => DefaultTableInfoCache.Instance;
        private ISqlBaseTypeConvertor _baseConvertor => DefaultBaseTypeConvertor.Instance;

      
        public DataSourceType Environment { get; }

        public SqlBuilder Select(string sql)
        {
            return Append(SELECT+sql);
        }
        public SqlBuilder Select<T>(string table)
        {
            return Select(typeof(T), table);
        }
        public SqlBuilder SelectDistinct<T>(string table)
        {
            return SelectDistinct(typeof(T),table);
        }
        public SqlBuilder SelectTop<T>(string table, int count)
        {
            return SelectTop(typeof(T),table,count);
        }
        public SqlBuilder SelectTopDistinct<T>(string table, int count)
        {
            return SelectTopDistinct(typeof(T),table,count);
        }
        public SqlBuilder Select(Type type, string table)
        {
            return Select(_tableInfos.GetTable(type).Columns.Keys, table);
        }
        public SqlBuilder SelectDistinct(Type type, string table)
        {
            return SelectDistinct(_tableInfos.GetTable(type).Columns.Keys,table);
        }
        public SqlBuilder SelectTop(Type type, string table, int count)
        {
            return SelectTop(_tableInfos.GetTable(type).Columns.Keys, table,count);
        }
        public SqlBuilder SelectTopDistinct(Type type, string table, int count)
        {
            return SelectTopDistinct(_tableInfos.GetTable(type).Columns.Keys,table,count);
        }
        public SqlBuilder Select(IEnumerable<string> columns, string table)
        {
             Append(SELECT);

            foreach (var item in columns)
                _buffer.Append(item)
                       .Append(",");

            return removeLastComma()
                   .Append(FROM)
                   .Append(table);
        }
        public SqlBuilder Select(string table, params string[] columns)
        {
            return Select(columns, table);
        }

        public SqlBuilder SelectTop(string table,int count,params string[] columns)
        {
            return SelectTop(columns, table, count);
        }

        public SqlBuilder SelectDistinct(string table,params string[] columns)
        {
            return SelectDistinct(columns, table);
        }

        public SqlBuilder SelectTopDistinct(string table,int count,params string[] columns)
        {
            return SelectTopDistinct(columns, table, count);
        }

        public SqlBuilder SelectDistinct(IEnumerable<string> columns, string table)
        {
            Append(SELECT)
           .Append(DISTINCT);

            foreach (var item in columns)
                Append(item)
               .Append(",");

            return removeLastComma()
                  .Append(FROM)
                  .Append(table);
        }
        public SqlBuilder SelectTopDistinct(IEnumerable<string> columns, string table, int count)
        {
            if (Environment != DataSourceType.SqlServer)
                throw new SqlGrammerNotSurportedException(Environment, " Top StateMent ");

            Append(SELECT)
                .Append(DISTINCT)
                .Append(TOP)
                .Append($" {count} ");

            foreach (var item in columns)
            {
                Append(item)
                .Append(",");

            }

            return removeLastComma()
                 .Append(FROM)
                 .Append(table);
        }
        public SqlBuilder SelectTop(IEnumerable<string> columns, string table, int count)
        {
            if (Environment != DataSourceType.SqlServer)
                throw new SqlGrammerNotSurportedException(Environment, " Top StateMent ");

            Append(SELECT)
              .Append(TOP)
              .Append($" {count} ");

            foreach (var item in columns)
            {
                Append(item)
                .Append(",");

            }

            return removeLastComma()
                 .Append(FROM)
                 .Append(table);
        }
        public SqlBuilder Where(string  column)
        {
            return Append(WHERE)
                   .Append(column)
                   .Append(BLANK);
        }
        public SqlBuilder Where(Func<string> condition)
        {
            return Where().Append(condition.Invoke());
        }
        public SqlBuilder Where(Expression expression)
        {
            return Where().Append(expression.ToString());
        }
        public SqlBuilder Where(IEnumerable<Expression> expressions, IEnumerable<string> logics)
        {
            var ls = logics.ToList();
            var t = 0;

            Where();

            foreach (var item in expressions)
            {
                Append(item.ToString());
                Append(ls[t++]);
            }

            return this;
        }
        public SqlBuilder Update(string table)
        {
            _buffer.Append(UPDATE)
                   .Append(table)
                   .Append(BLANK);

            return this;
        }
        public SqlBuilder DeleteFrom(string table)
        {
            _buffer.Append(DELETE_FROM)
                   .Append(table)
                   .Append(BLANK);

            return this;
        }
        public SqlBuilder Set()
        {
            return Append(SET);
        }
        public SqlBuilder Set(string sql)
        {
            _buffer.Append(sql);

            return this;
        }
        public SqlBuilder Set(Expression item)
        {
            return Set().Append(item.ToString());
        }
        public SqlBuilder Set(IEnumerable<Expression> items)
        {
            Append(SET);

            foreach (var item in items)
            {
                Append(item.ToString())
                    .Append(COMA);
            }

            return removeLastComma();

        }
        public SqlBuilder Set(object obj)
        {
            var dic = new Dictionary<string, object>();

            foreach (var item in _tableInfos.GetTable(obj.GetType()).Columns)
                dic.Add(item.Value.SqlName, item.Value.Getter.Invoke(obj));

            return Set(dic);
        }
        public SqlBuilder Set(IDictionary<string,object> parameters)
        {
            Set();

            foreach (var item in parameters)
            {
                Append(item.Key)
                    .Append(EQUEL)
                    .Append(_baseConvertor.ToSQL(item.Value.GetType(), item.Value))
                    .Append(COMA);
            }

            return removeLastComma();
        }
        public SqlBuilder Having()
        {
            return Append(GROUP_BY);
        }
        public SqlBuilder Insert(string sql)
        {

            return Append(sql);
        }
        public SqlBuilder Insert<T>(T data, string table)
        {
            return Insert(typeof(T), data, table);
        }
        public SqlBuilder Insert(Type type,IEnumerable<object>data,string table)
        {
            return InsertAnonymouse(type,data,table);
        }
        public SqlBuilder Insert<T>(IEnumerable<T> datas, string table)
        {
            switch (Environment)
            {
                case DataSourceType.Oracle:
                    break;
                case DataSourceType.SqlServer:
                    return InsertSqlServer(datas, table);
                case DataSourceType.MySql:
                    break;
                case DataSourceType.Db2:
                    break;
                case DataSourceType.Sqlite:
                    break;
                default:
                    break;
            }

            return this;
        }
        public SqlBuilder InsertAnonymouse(object data, string table)
        {
            return Insert(data.GetType(), data, table);
        }
        public SqlBuilder InsertAnonymouse(Type type, IEnumerable<object> datas, string table)
        {
            switch (Environment)
            {
                case DataSourceType.SqlServer:
                    return insertSqlServer(type, datas, table);
                case DataSourceType.Oracle:
                    break;
                case DataSourceType.MySql:
                    break;
                case DataSourceType.Db2:
                    break;
                case DataSourceType.Sqlite:
                    break;
                default:
                    break;
            }

            return this;
        }
        private SqlBuilder InsertSqlServer<T>(IEnumerable<T> datas, string table)
        {
            return insertSqlServer(typeof(T), (IEnumerable<object>)datas, table);
        }
        private SqlBuilder insertSqlServer(Type type,IEnumerable<object> datas,string table)
        {
            var prefix = _tableInfos.GetTable(type).CreateInsertPrefix(table);// prefix :   insert into {table}(column,column)values

            var t = 0;
            var columns = _tableInfos.GetTable(type).Columns;

            foreach (var item in datas)
            {
                if ((t %= 2000) == 0)//sql server max batch sql insert is 2056,It should add a new prefix, when the mod is zero;
                {
                    Append(prefix);
                    removeLastComma();
                }

                Append(LEFT_PARREN)
                .Append(DefaultSqlConvertor.Instance.ToSql(item))
                .Append(RIGHHT_PARREN)
                .Append(COMA);

                t++;
            }


            return removeLastComma();
        }
        public SqlBuilder Insert(Type type, object data, string table)
        {
            return Append(_tableInfos.GetTable(type).CreateInsertPrefix(table))
                 .Append(LEFT_PARREN)
                 .Append(DefaultSqlConvertor.Instance.ToSql(data))
                 .Append(RIGHHT_PARREN);
        }
        public SqlBuilder Between(object min, object max)
        {

            return Append(BETWEEN)
                  .Append(_baseConvertor.ToSQL(min.GetType(), min))
                  .Append(AND)
                  .Append(_baseConvertor.ToSQL(max.GetType(), max))
                  .Append(BLANK);
        }
        public SqlBuilder Like(string expression)
        {
            return Append(LIKE)
                  .Append(_baseConvertor.ToSQL(typeof(string), expression));
        }
        public SqlBuilder LeftJoin(string table1, string table2, IEnumerable<Expression> conditions, IEnumerable<string> logics)
        {
            return join(LEFT_JOIN, table1, table2, conditions, logics);
        }
        public SqlBuilder RightJoin(string table1, string table2, IEnumerable<Expression> conditions, IEnumerable<string> logics)
        {
            return join(RIGHT_JOIN, table1, table2, conditions, logics);
        }
        public SqlBuilder InnerJoin(string table1, string table2, IEnumerable<Expression> conditions, IEnumerable<string> logics)
        {
            return join(LEFT_JOIN, table1, table2, conditions, logics);
        }
        public SqlBuilder FullJoin(string table1, string table2, IEnumerable<Expression> conditions, IEnumerable<string> logics)
        {
            return join(FULL_JOIN, table1, table2, conditions, logics);
        }
        private SqlBuilder join(string joinKind, string table1, string table2, IEnumerable<Expression> conditions, IEnumerable<string> logics)
        {

            Append(joinKind)
             .Append(table2)
             .Append(BLANK)
             .Append(ON);

            var ls = logics.ToList();
            var t = 0;


            foreach (var item in conditions)
            {
                Append(item.ToString())
                    .Append(ls[t++]);
            }

            return this;
        }
        public SqlBuilder Union()
        {
           return Append(UNION);
        }
        public SqlBuilder UnionAll()
        {
            return Append(UNION_ALL);
        }
        public SqlBuilder OrderByAsc(string column)
        {
            Append($" ORDER BY {column} ASC ");
            return this;
        }
        public SqlBuilder OrderByDesc(string column)
        {
            Append($" ORDER BY {column} DESC ");
            return this;
        }
        public SqlBuilder GroupBy(params string[] columns)
        {
          return   GroupBy((IEnumerable<string>)columns);
        }
        public SqlBuilder GroupBy(IEnumerable<string> columns)
        {
            Append(GROUP_BY);

            foreach (var item in columns)
            {
                Append(item)
                    .Append(COMA);
            }

            return removeLastComma();
        }
        public SqlBuilder Limite(int count)
        {
            return Append(LIMITE)
                   .Append(count.ToString())
                   .Append(BLANK);

        }
        public SqlBuilder In()
        {
            return Append(IN);
        }
        public SqlBuilder NotIn()
        {
            return Append(NOT_IN);
        }
        public SqlBuilder Exists()
        {
            return Append(EXISTS);
        }
        public SqlBuilder NotExists()
        {
            return Append(NOT_EXISTS);
        }

        public SqlBuilder And(string column)
        {
            return And().Append(column).Append(BLANK);
        }
        public SqlBuilder And()
        {
            return Append(AND);
        }
        public SqlBuilder Or()
        {
            return Append(OR);
        }
        public SqlBuilder Like()
        {
            return Append(LIKE);
        }
        public SqlBuilder Between()
        {
            return Append(BETWEEN);
        }
        public SqlBuilder Not()
        {
            return Append(NOT);
        }
        public SqlBuilder Select()
        {
            return Append(SELECT);
        }
        public SqlBuilder Top()
        {
            return Append(TOP);
        }
        public SqlBuilder Distinct()
        {
            return Append(DISTINCT);
        }
        public SqlBuilder As()
        {
            return Append(AS);
        }
        public SqlBuilder From()
        {
            return Append(FROM);
        }
        public SqlBuilder Where()
        {
            return Append(WHERE);
        }
        public SqlBuilder InnerJoin()
        {
            return Append(INNER_JOIN);
        }
        public SqlBuilder FullJoin()
        {
            return Append(FULL_JOIN);
        }
        public SqlBuilder LeftJoin()
        {
            return Append(LEFT_JOIN);
        }
        public SqlBuilder RightJoin()
        {
            return Append(RIGHT_JOIN);
        }
        public SqlBuilder On()
        {
            return Append(ON);
        }
        public SqlBuilder Update()
        {
            return Append(UPDATE);
        }
        public SqlBuilder Use()
        {
            return Append(USE);
        }
        public SqlBuilder Limite()
        {
            return Append(LIMITE);
        }
        public SqlBuilder GroupBy()
        {
            return Append(GROUP_BY);
        }
        public SqlBuilder OrderBy()
        {
            return Append(ORDER_BY);
        }

        public SqlBuilder Bigger()
        {
            return Append(BIGGER);
        }
        public SqlBuilder BiggerEquel()
        {
            return Append(BIGGEREQUEL);
        }

        public SqlBuilder Less()
        {
            return Append(LESS);
        }
        public SqlBuilder LessEquel()
        {
            return Append(LESSEQUEL);
        }
        public SqlBuilder Equel()
        {
            return Append(EQUEL);
        }

        public SqlBuilder AppendBlank()
        {
            return Append(BLANK);
        }
        public SqlBuilder Bigger(object obj)
        {
            return Bigger()
                .Append(_baseConvertor.ToSQL(obj.GetType(), obj))
                .AppendBlank(); ;
        }
        public SqlBuilder BiggerEquel(object obj)
        {
            return BiggerEquel()
               .Append(_baseConvertor.ToSQL(obj.GetType(), obj))
               .AppendBlank(); 
        }
        public SqlBuilder Less(object obj)
        {
            return Less()
               .Append(_baseConvertor.ToSQL(obj.GetType(), obj))
               .AppendBlank();
        }
        public SqlBuilder LessEquel(object obj)
        {
            return LessEquel()
             .Append(_baseConvertor.ToSQL(obj.GetType(), obj))
             .AppendBlank();
        }
        public SqlBuilder Equel(object obj)
        {
            return Equel()
             .Append(_baseConvertor.ToSQL(obj.GetType(), obj))
             .AppendBlank();
        }


        public SqlBuilder Append(string str)
        {
            _buffer.Append(str);

            return this;
        }
        public string Build()
        {
            var result = _buffer.ToString();
            _buffer.Clear();
            return result;
        }
        public SqlBuilder CreateTable<T>(string table)
        {
            return CreateTable(typeof(T), table);
        }
        public SqlBuilder CreateTable(Type type, string table)
        {
            return Append(_tableInfos.GetTable(type).CreateTable(table));
        }

        public SqlBuilder DropTable(string table)
        {

            return Append($" DROP TABLE {table}");
          
        }
        public SqlBuilder DropColumn(string table,IEnumerable<string> columns)
        {
            return this;
        }
        public SqlBuilder AddColumn(string table, Column column)
        {
            return this;
        }
        public SqlBuilder AddColumn(string table,IEnumerable<Column>column)
        {
            return this;
        }
        public SqlBuilder RemoveConstraint(string table,string column,IEnumerable<Attribute> constraints)
        {
            return this;
        }
        public SqlBuilder AddConstraint(string table,string column ,IEnumerable<Attribute> constraints)
        {
            return this;
        }
        public SqlBuilder RenameTable(string oldName ,string newName)
        {
            return this;
        }

        public SqlBuilder BeginTransanctiong(string name)
        {
            return this;
        }
        public SqlBuilder EndTransanction(string name)
        {
            return this;
        }
        public SqlBuilder Commit()
        {
            return this;
        }
        public SqlBuilder Rollback()
        {
            return this;
        }

        public SqlBuilder BuildTemplate(string template, object parameter)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(template, parameter));
        }

        public SqlBuilder BuildTemplate(string template, IDictionary<string, object> parameter)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(template, parameter));
        }

        public SqlBuilder BuildTemplate(string template, IEnumerable<IDictionary<string, object>> parameters)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(template, parameters));
        }

        public SqlBuilder BuildTemplate(string template, IEnumerable<object> parameters)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(template, parameters));
        }

        public SqlBuilder BuildTemplate(IEnumerable<TemplateSegment> segments, object parameter)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(segments, parameter));
        }

        public SqlBuilder BuildTemplate(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameter)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(segments, parameter));
        }

        public SqlBuilder BuildTemplate(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(segments, parameters));
        }

        public SqlBuilder BuildTemplate(IEnumerable<TemplateSegment> segments, IEnumerable<object> parameters)
        {
            return Append(DefaultTemplateConvertor.Instance.Convert(segments, parameters));
        }

        private SqlBuilder removeLastComma()
        {
            if (_buffer[_buffer.Length - 1] == ',')
                _buffer.Remove(_buffer.Length - 1, 1);

            return this;
        }

    }
}
