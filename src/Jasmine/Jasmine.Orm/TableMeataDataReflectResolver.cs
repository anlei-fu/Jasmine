using Jasmine.Common;
using Jasmine.Orm.Attributes;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class TableMetaDataReflectResolver : IMetaDataReflectResolver<TableMetaData>
    {
        public TableMetaData Resolve(Type type)
        {
            var table = new TableMetaData();

            var attrs = JasmineReflectionCache.Instance.GetItem(type).Attributes;

            if(attrs.Contains<TableNameAttribute>())
            {
                table.Name = attrs.GetAttribute<TableNameAttribute>()[0].Name;
            }

            if(attrs.Contains<DataSourceAttribute>())
            {
                table.DataSource = attrs.GetAttribute<DataSourceAttribute>()[0].DataSource;
            }


            foreach (var item in JasmineReflectionCache.Instance.GetItem(type).Properties)
            {
                var pattrs = item.Attributes;

                if (pattrs.Contains<SqlIgnoreAttribute>())
                    continue;

                

                if(pattrs.Contains<JoinColumnsAttribute>())
                {
                    foreach (var joinColumn in getJooinColumns(item))
                    {
                        table.Columns.Add(joinColumn.ColumnName, joinColumn);
                    }

                }


                var column = new ColumnMetaData();

                if(pattrs.Contains<ColumnNameAttribute>())
                {
                    column.ColumnName = pattrs.GetAttribute<ColumnNameAttribute>()[0].ColumnName;
                }

                if(pattrs.Contains<SqlColumnTypeAttribute>())
                {
                    column.SqlType = pattrs.GetAttribute<SqlDataTypeAttribute>()[0].SqlType;
                }

                if(pattrs.Contains<NullableAttribute>())
                {
                    column.Nullable = pattrs.GetAttribute<NullableAttribute>()[0].Nullable;
                }

                if(attrs.Contains<PrimaryKeyAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<PrimaryKeyAttribute>()[0]);
                }

                if(attrs.Contains<UniqueAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<UniqueAttribute>()[0]);
                }

                if(attrs.Contains<ForeignKeyAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<ForeignKeyAttribute>()[0]);
                }

                if(attrs.Contains<DefaultAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<DefaultAttribute>()[0]);
                }

                if(attrs.Contains<CheckAttribute>())
                {
                    column.Constraints.Add(pattrs.GetAttribute<CheckAttribute>()[0]);
                }

                column.RelatedType = item.RelatedInfo.PropertyType;
                column.PorpertyName = item.Name;
                column.Getter = item.Getter;
                column.Setter = item.Setter;

            }

            return table;

        }

        public List<ColumnMetaData> getJooinColumns(Property property)
        {
            return null;
        }

        public TableMetaData getJoinTable(Property property)
        {
            return null;
        }
    }
}
