using System;

namespace Jasmine.Tools.Csv
{
    public  class CsvAttribute:Attribute
    {
        public CsvAttribute(int column)
        {
            Column = column;
        }
        public int Column { get; }

    }

    public class CsvIgnoreAttribute:Attribute
    {

    }
}
