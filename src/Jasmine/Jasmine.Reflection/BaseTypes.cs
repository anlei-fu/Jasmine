using System;
using System.Collections.Generic;

namespace Jasmine.Reflection
{
    public class BaseTypes
    {
        public static readonly Type TByte = typeof(byte);
        public static readonly Type TSByte = typeof(sbyte);
        public static readonly Type TChar = typeof(char);
        public static readonly Type TBoolean = typeof(bool);
        public static readonly Type TShort = typeof(short);
        public static readonly Type TUShort = typeof(ushort);
        public static readonly Type TInt = typeof(int);
        public static readonly Type TUInt = typeof(uint);
        public static readonly Type TFloat = typeof(float);
        public static readonly Type TDouble = typeof(double);
        public static readonly Type TDecimal = typeof(decimal);
        public static readonly Type TLong = typeof(long);
        public static readonly Type TULong = typeof(ulong);
        public static readonly Type TDateTime = typeof(DateTime);
        public static readonly Type TTimeSpan = typeof(TimeSpan);
        public static readonly Type TDateTimeOffset = typeof(DateTimeOffset);
        public static readonly Type TString = typeof(string);
        public static readonly Type TGuid = typeof(Guid);


        public static readonly HashSet<Type> Numbers = new HashSet<Type>()
        {
            TByte,
            TSByte,
            TShort,
            TNShort,
            TUShort,
            TNUShort,
            TInt,
            TNInt,
            TUInt,
            TNUInt,
            TLong,
            TNULong,
            TULong,
            TDouble,
            TDecimal,
            TFloat,

        };

        public static readonly Type TNByte = typeof(byte?);
        public static readonly Type TNSByte = typeof(sbyte?);
        public static readonly Type TNChar = typeof(char?);
        public static readonly Type TNBoolean = typeof(bool?);
        public static readonly Type TNShort = typeof(short?);
        public static readonly Type TNUShort = typeof(ushort?);
        public static readonly Type TNInt = typeof(int?);
        public static readonly Type TNUInt = typeof(uint?);
        public static readonly Type TNFloat = typeof(float?);
        public static readonly Type TNDouble = typeof(double?);
        public static readonly Type TNDecimal = typeof(decimal?);
        public static readonly Type TNLong = typeof(long?);
        public static readonly Type TNULong = typeof(ulong?);
        public static readonly Type TNDateTime = typeof(DateTime?);
        public static readonly Type TNTimeSpan = typeof(TimeSpan?);
        public static readonly Type TNDateTimeOffset = typeof(DateTimeOffset?);
        public static readonly Type TNGuid = typeof(Guid?);



        public const string Byte = "System.Byte";
        public const string SByte = "System.SByte";
        public const string Char = "System.Char";
        public const string Boolean = "System.Boolean";
        public const string Short = "System.Int16";
        public const string UShort = "System.UInt16";
        public const string Int = "System.Int32";
        public const string UInt = "System.UInt32";
        public const string Float = "System.Single";
        public const string Double = "System.Double";
        public const string Decimal = "System.Decimal";
        public const string Long = "System.Int64";
        public const string ULong = "System.UInt64";
        public const string DateTime = "System.DateTime";
        public const string TimeSpan = "System.TimeSpan";
        public const string DateTimeOffset = "System.DateTimeOffset";
        public const string String = "System.String";
        public const string Guid = "System.Guid";


        public const string NByte = "System.Nullable`1[[System.Byte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NSByte = "System.Nullable`1[[System.SByte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NChar = "System.Nullable`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NBoolean = "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NShort = "System.Nullable`1[[System.Int16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NUShort = "System.Nullable`1[[System.UInt16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NInt = "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NUInt = "System.Nullable`1[[System.UInt32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NFloat = "System.Nullable`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NDouble = "System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NDecimal = "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NLong = "System.Nullable`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NULong = "System.Nullable`1[[System.UInt64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NDateTime = "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NTimeSpan = "System.Nullable`1[[System.TimeSpan, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NDateTimeOffset = "System.Nullable`1[[System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        public const string NGuid = "System.Nullable`1[[System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";


        public static readonly Type EnptyType=null;
        public static readonly Type[] EmptyTypes = { };

        public static readonly HashSet<Type> Base =new HashSet<Type>
        {
            TSByte,
            TBoolean,
            TByte,
            TChar,
            TShort,
            TUShort,
            TInt,
            TUInt,
            TFloat,
            TDouble,
            TLong,
            TULong,
            TDecimal,
            TDateTime,
            TDateTimeOffset,
            TTimeSpan,
            TString,
            TGuid,

            TNSByte,
            TNByte,
            TNChar,
            TNBoolean,
            TNShort,
            TNUShort,
            TNInt,
            TNUInt,
            TNLong,
            TNULong,
            TNFloat,
            TNDouble,
            TNDecimal,
            TNDateTime,
            TNTimeSpan,
            TNDateTimeOffset,
            TNGuid
        };

        public static readonly HashSet<Type> Times = new HashSet<Type>()
        {
            TDateTime,
            TTimeSpan,
            TDateTimeOffset
        };





    }
}
