using Jasmine.Common;
using Jasmine.Orm.Attributes;
using Jasmine.Reflection;
using System;

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

            }

            if(attrs.Contains<DataSourceAttribute>())
            {

            }


            foreach (var item in JasmineReflectionCache.Instance.GetItem(type).Properties)
            {
                var pattrs = item.Attributes;

                if (pattrs.Contains<SqlIgnoreAttribute>())
                    continue;

                if(pattrs.Contains<JoinTableAttribute>())
                {

                    continue;
                }

                if(pattrs.Contains<JoinColumnsAttribute>())
                {

                }


                var column = new ColumnMetaData();

                if(pattrs.Contains<ColumnNameAttribute>())
                {

                }

                if(pattrs.Contains<SqlColumnTypeAttributeAttribute>())
                {

                }

                if(pattrs.Contains<NotNullAttribute>())
                {

                }

                if(attrs.Contains<PrimaryKeyAttribute>())
                {

                }

                if(attrs.Contains<UniqueAttribute>())
                {

                }

                if(attrs.Contains<ForeignKeyAttribute>())
                {

                }

                if(attrs.Contains<DefaultAttribute>())
                {

                }

                if(attrs.Contains<CheckAttribute>())
                {

                }




            }

        }
    }
}
