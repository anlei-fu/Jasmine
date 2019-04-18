using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IStringValueParser
    {
        IList<StringValue> Parse(string str);
    }
}
