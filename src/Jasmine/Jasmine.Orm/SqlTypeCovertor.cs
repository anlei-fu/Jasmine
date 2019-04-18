using Jasmine.Reflection;
using System;

namespace Jasmine.Orm.Model
{
    public class SqlTypeCovertor
    {
        public const string Boolean = "bit";
        public const string TinyInt = "tinyint";
        public const string SmallInt = "samllint";
        public const string Int = "int";
        public const string BigInt = "bigint";
        public const string Real = "real";
        public const string Float = "float";

        public const string Numeric = "numeric";
        public const string Decimal = "decimal";
        public const string SmallMoney = "smallmoney";
        public const string Money = "money";


        public const string SmallDateTime = "samlldatetime";
        public const string DateTime = "datetime";
        public const string TimeStamp = "timestamp";



        public const string Char = "char";
        public const string VarChar = "varchar";
        public const string Nchar = "nchar";
        public const string NvarChar = "nvarchar";
        public const string Text = "text";
        public const string NText = "ntext";

        public const string Image = "image";
        public const string Binary = "binary";
        public const string Guid = "uniqueidentifier";

        public static Type ConvertSqlTypeToCsharpType(string type)
        {
            switch (type)
            {
                case Boolean:
                    return BaseTypes.TBoolean;
                case TinyInt:
                    return BaseTypes.TByte;
                case SmallInt:
                    return BaseTypes.TShort;
                case Int:
                    return BaseTypes.TInt;
                case BigInt:
                    return BaseTypes.TLong;
                case Real:
                    return BaseTypes.TFloat;
                case Float:
                    return BaseTypes.TDouble;
                case Numeric:
                case Decimal:
                case SmallMoney:
                case Money:
                    return BaseTypes.TDecimal;
                case DateTime:
                case SmallDateTime:
                case TimeStamp:
                    return BaseTypes.TDateTime;
                case Char:
                    return BaseTypes.TChar;
                case VarChar:
                case Nchar:
                case NvarChar:
                case Text:
                case NText:
                    return BaseTypes.TString;
                case Guid:
                    return BaseTypes.TGuid;
                default:
                    throw new NotSupportedException($"this type {type} is not convertable in this convertor!");
            }
        }



        public static Type ConvertSqlDataTypeToCsharpType(Type type)
        {
            switch (type.Name)
            {
                case "SqlBoolean":
                    return BaseTypes.TBoolean;
                case "SqlInt32":
                    return BaseTypes.TInt;
                case "SqlInt16":
                    return BaseTypes.TShort;
                case "SqlInt64":
                    return BaseTypes.TLong;
                case "SqlByte":
                    return BaseTypes.TByte;
                case "SqlBytes":
                    return typeof(byte[]);
                case "SqlMoney":
                    return BaseTypes.TDecimal;
                case "SqlDateTime":
                    return BaseTypes.TDateTime;
                case "SqlDecimal":
                    return BaseTypes.TDecimal;
                case "SqlGuid":
                    return BaseTypes.TGuid;
                case "SqlSingle":
                    return BaseTypes.TFloat;
                case "SqlDouble":
                    return BaseTypes.TDouble;
                case "SqlString":
                case "SqlChars":
                    return BaseTypes.TString;
                case "SqlTimeSpan":
                    return BaseTypes.TTimeSpan;
                default:
                    throw new NotSupportedException($"this type {type} is not convertable in this convertor!");
            }
        }


        public static string ConvertCsharpTypeToSqlType(Type type)
        {
           

            switch (type.FullName)
            {
                case BaseTypes.NChar:
                case BaseTypes.Char:
                    return Char;
                case BaseTypes.String:
                    return Text;
                case BaseTypes.NBoolean:
                case BaseTypes.Boolean:
                    return Boolean;
                case BaseTypes.NByte:
                case BaseTypes.NSByte:
                case BaseTypes.Byte:
                case BaseTypes.SByte:
                    return TinyInt;
                case BaseTypes.NShort:
                case BaseTypes.NUShort:
                case BaseTypes.Short:
                case BaseTypes.UShort:
                    return SmallInt;
                case BaseTypes.NInt:
                case BaseTypes.NUInt:
                case BaseTypes.Int:
                case BaseTypes.UInt:
                    return Int;
                case BaseTypes.NLong:
                case BaseTypes.NULong:
                case BaseTypes.Long:
                case BaseTypes.ULong:
                    return BigInt;
                case BaseTypes.NGuid:
                case BaseTypes.Guid:
                    return Guid;
                case BaseTypes.NFloat:
                case BaseTypes.Float:
                    return Real;
                case BaseTypes.NDouble:
                case BaseTypes.Double:
                    return Float;
                case BaseTypes.NDecimal:
                case BaseTypes.Decimal:
                    return Decimal;
                case BaseTypes.NDateTime:
                case BaseTypes.DateTime:
                case BaseTypes.NTimeSpan:
                case BaseTypes.TimeSpan:
                case BaseTypes.NDateTimeOffset:
                case BaseTypes.DateTimeOffset:
                    return DateTime;
                default:
                    throw new NotSupportedException($"this type {type} is not convertable in this convertor!");
            }

        }







    }
}
