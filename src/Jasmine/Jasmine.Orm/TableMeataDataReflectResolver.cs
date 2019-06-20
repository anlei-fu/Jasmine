using Jasmine.Common;
using Jasmine.Ioc;
using Jasmine.Orm.Attributes;
using Jasmine.Orm.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class TableMetaDataReflectResolver : IMetaDataReflectResolver<TableMetaData>
    {
        private TableMetaDataReflectResolver()
        {

        }
        private ITableMetaDataProvider _provider =>DefaultTableMetaDataProvider.Instance;
        public static readonly IMetaDataReflectResolver<TableMetaData> Instace = new TableMetaDataReflectResolver();
        public TableMetaData Resolve(Type type)
        {
            var table = new TableMetaData();
            table.RelatedType = type;

            var attrs = JasmineReflectionCache.Instance.GetItem(type).Attributes;

            var joinColumns = new Dictionary<string, JoinColumns>();
            var joinTables = new Dictionary<string, JoinTable>();
            var associateTables = new Dictionary<string, AssociateTable>();

            //table name
            if (attrs.Contains<TableNameAttribute>())
            {
                table.Name = attrs.GetAttribute<TableNameAttribute>()[0].Name;
            }

            //data source ,default is sql server
            if (attrs.Contains<DataSourceAttribute>())
            {
                table.DataSource = attrs.GetAttribute<DataSourceAttribute>()[0].DataSource;
            }

            // query result resolver
            if (attrs.Contains<QueryResultResolverAttribute>())
            {
                table.Resolver = (IQueryResultResolver)IocServiceProvider.Instance.GetService(attrs.GetAttribute<QueryResultResolverAttribute>()[0].ResolverType);
            }

           
            //default is type name
            if (table.Name == null)
            {
                table.Name = type.Name;
            }

            // non parameter constructor is required
            table.Constructor = JasmineReflectionCache.Instance.GetItem(type).Constructors.GetDefaultConstructor()?.DefaultInvoker;

            if (table.Constructor == null)
            {
                throw new RequiredDefaultConstructorNotFoundException(type);
            }

            // set default query result resolver
            if (table.Resolver == null)
                table.Resolver = DefaultQueryResultResolver.Instance;


            foreach (var item in JasmineReflectionCache.Instance.GetItem(type).Properties)
            {
                var pattrs = item.Attributes;

                //ignore property
                if (pattrs.Contains<SqlIgnoreAttribute>())
                    continue;

                // resolve join column
                if (pattrs.Contains<JoinColumnsAttribute>())
                {
                    var joinColumn = getJoinColumns(item);

                    joinColumns.Add(joinColumn.Table.Name, joinColumn);

                    continue;
                }

                //resolve join table
                if (pattrs.Contains<JoinTableAttribute>())
                {
                    var joinTable = getJoinTable(item, pattrs.GetAttribute<JoinTableAttribute>()[0].Outter, pattrs.GetAttribute<JoinTableAttribute>()[0].Inner);

                    joinTables.Add(joinTable.Table.Name, joinTable);

                    continue;

                }

                // resolve  associate query
                if(pattrs.Contains<AssociateAttribute>())
                {
                    var associateTable = getAssociateTable(item,pattrs.GetAttribute<AssociateAttribute>()[0].Condition);

                    associateTables.Add(associateTable.Table.Name, associateTable);

                    continue;
                }

                //resolve column
                var column = new ColumnMetaData();

                //column name
                if (pattrs.Contains<ColumnNameAttribute>())
                {
                    column.ColumnName = pattrs.GetAttribute<ColumnNameAttribute>()[0].ColumnName;
                }

                // sql type
                if (pattrs.Contains<SqlColumnTypeAttribute>())
                {
                    column.SqlType = pattrs.GetAttribute<SqlColumnTypeAttribute>()[0].SqlColumnType;
                }

                // nullable
                if (pattrs.Contains<NotNullAttribute>())
                {
                    column.Nullable = true;
                }

                //primary key
                if (pattrs.Contains<PrimaryKeyAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<PrimaryKeyAttribute>()[0]);
                }

                //unique key
                if (pattrs.Contains<UniqueAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<UniqueAttribute>()[0]);
                }

                //foreign key
                if (pattrs.Contains<ForeignKeyAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<ForeignKeyAttribute>()[0]);
                }

                // default value
                if (pattrs.Contains<DefaultAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<DefaultAttribute>()[0]);
                }

                //check 
                if (pattrs.Contains<CheckAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<CheckAttribute>()[0]);
                }

                // config default sqltype ,if sqltype not be set
                if (column.SqlType == null)
                {
                    column.SqlType = SqlServerDataTypeMapper.Instace.GetSqlType(item.PropertyType, table.DataSource);
                }

                // default column name is property name
                if (column.ColumnName == null)
                {
                    column.ColumnName = item.Name;
                }

                column.RelatedType = item.PropertyInfo.PropertyType;
                column.PorpertyName = item.Name;
                column.Getter = item.Getter;
                column.Setter = item.Setter;
                column.OwnerType = item.OwnerType;

                table.Columns.Add(column.ColumnName, column);

            }

            table.JoinColumns = joinColumns;
            table.AssociateTables = associateTables;
            table.JoinTables = joinTables;

            finalCheck(table);

            return table;

        }

        private void finalCheck(TableMetaData table)
        {
            // check join key is ok
            foreach (var item in table.JoinTables.Values)
            {
                bool joinKeyOk = false;

                foreach (var column in table.Columns)
                {
                    if(column.Key.ToLower()==item.InnerKey.ToLower())
                    {
                        joinKeyOk = true;
                        break;
                    }
                }


                if(!joinKeyOk)
                {
                    throw new Orm.Exceptions.JoinKeyNotFountException(table,item.Table,item.InnerKey);
                }

                joinKeyOk = false;

                foreach (var outterColumn in item.Table.Columns)
                {
                    if (outterColumn.Key.ToLower() == item.OutterKey.ToLower())
                    {
                        joinKeyOk = true;
                        break;
                    }
                }
                if (!joinKeyOk)
                {
                    throw new Orm.Exceptions.JoinKeyNotFountException(table, item.Table, item.InnerKey);
                }


            }

            // check assciate query varible is ok
            foreach (var item in table.AssociateTables.Values)
            {
                foreach (var seg in item.ConditionTemplate.Segments)
                {
                    if(seg.IsVarible)
                    {
                        bool conditionParameterIsOk = false;

                        foreach (var column in table.Columns)
                        {
                            if(seg.Value==column.Key.ToLower())
                            {
                                conditionParameterIsOk = true;
                                break;
                            }
                        }

                        if(!conditionParameterIsOk)
                        {
                            throw new Orm.Exceptions.ConditionParameterCanNotBeFoundException(item.ConditionTemplate.RawString, seg.Value, table);
                        }
                    }
                }
            }
        }

        private JoinColumns getJoinColumns(Property property)
        {
            if (BaseTypes.Base.Contains(property.PropertyType))
            {
                throw new BadJoinColumnException(property.PropertyType);
            }

            var joinColumn = new JoinColumns();

            var table = _provider.GetTable(property.PropertyType);

            joinColumn.Table = table;
            joinColumn.PropertyName = property.Name;

            joinColumn.Setter = property.Setter;
            joinColumn.Getter = property.Getter;

            joinColumn.OwnerType = property.OwnerType;
            joinColumn.RelatedType = property.PropertyType;

            return joinColumn;
        }




        public JoinTable getJoinTable(Property property, string outter,string inner)
        {
            if (BaseTypes.Base.Contains(property.PropertyType))
            {
                throw new BadJoinColumnException(property.PropertyType);
            }

            var joinTable = new JoinTable();

            var table = _provider.GetTable(property.PropertyType);

            joinTable.Table = table;

            joinTable.InnerKey = inner;
            joinTable.OutterKey = outter;

            joinTable.Setter = property.Setter;
            joinTable.Getter = property.Getter;
            joinTable.OwnerType = property.OwnerType;
            joinTable.RelatedType = property.PropertyType;
            joinTable.PropertyName = property.Name;

            return joinTable;
        }

        public AssociateTable getAssociateTable(Property property, string condition)
        {
            if (condition == null)
                throw new Orm.Exceptions.AssociateConditionCanNotBeNullException();

            if (!property.PropertyType.IsArray || !property.PropertyType.IsList())
            {
                throw new Orm.Exceptions.BadJoinTableTypeException(property.PropertyType);
            }

            if (BaseTypes.Base.Contains(property.PropertyType.GetElementType()))
                throw new Orm.Exceptions.BadJoinTableTypeException(property.PropertyType);

            var associateTable = new AssociateTable();

            var table = _provider.GetTable(property.PropertyType);

            associateTable.Table = table;

            associateTable.PropertyName = property.Name;

            associateTable.ConditionTemplate = SqlTemplate.Parse(condition);

            associateTable.Setter = property.Setter;
            associateTable .Getter = property.Getter;

            associateTable.OwnerType = property.OwnerType;
            associateTable.RelatedType = property.PropertyType;
            return associateTable;
        }
    }
}
