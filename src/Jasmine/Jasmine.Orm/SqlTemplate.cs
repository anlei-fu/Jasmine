using Jasmine.Common;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class SqlTemplate:INameFearture
    {
        public bool AlreadyParsed { get; internal set; }
        public string Source { get; set; }
        public string Name { get; }
        public IEnumerable<SqlTemplateSegment> Segments { get; internal set; }

    }
}
