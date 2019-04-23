namespace Jasmine.Orm
{
    public  struct SqlTemplateSegment
    {
        public SqlTemplateSegment(string value,bool isParameter,bool isStringParameter)
        {
            IsParamer = isParameter;
            Value = value;
            IsStringParameter = isParameter;
        }
        public bool IsStringParameter { get; set; }
        public bool IsParamer { get; set; }
        public string Value { get; set; }
    }
}
