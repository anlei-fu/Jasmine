using System;
using System.Collections.Generic;

namespace Jasmine.Orm.Attributes
{
    public   class AttributeUtls
    {
        public static readonly HashSet<Type> Constraints = new HashSet<Type>()
        {
            typeof(CheckAttribute),
            typeof(DefaultAttribute),
            typeof(PrimaryKeyAttribute),
            typeof(ForeignKeyAttribute),
            typeof(NotNullAttribute),
            typeof(UniqueAttribute),
            typeof(TextAttribute),
        };

        public const string PRIMARYKEY = " PRIMARY KEY ";
        public const string FOREIGNKEY = " FPREIGN KEY ";
        public const string CHECK = " CHECK ";
        public const string DEFAULT = " DEFAULT ";
        public const string NOTNULL = " NOT NULL ";
        public const string UNIQUE = " UNIQUE ";

    }
}
