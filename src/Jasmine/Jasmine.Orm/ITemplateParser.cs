using Jasmine.Orm.Model;
using System.Collections.Generic;

namespace Jasmine.Components
{
    public   interface ISegmentParser
    {
        IEnumerable<TemplateSegment> Parse(string template);
    }
}
