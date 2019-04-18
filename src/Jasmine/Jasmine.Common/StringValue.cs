namespace Jasmine.Common
{
    public  class StringValue
    {
        public StringValue(StringValueType type)
        {
            ValueType = type;
        }
        public string Value { get; set; }

        public StringValueType ValueType { get; set; }
    }
    public enum StringValueType
    {
        Globle,
        Property,
        Local,
        Nomal,

    }
}
