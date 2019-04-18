using System.Collections.Generic;

namespace Jasmine.Orm.Model
{
    public  class Template
    {
        public string Name { get; set; }
        public IEnumerable<TemplateSegment> Segments { get; set; }
    }
}
