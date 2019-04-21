using Jasmine.Parsers.Html;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public   class Selector:NameFearture
    {
        public Selector(string name,string pattern) : base(name)
        {
            SelectPattern = pattern;
        }

        public string SelectPattern { get;}

        public IList<NewItem> NewItems { get; set; }
        public IList<Matcher> Matchers { get; set; }
        public IList<Selector> Children { get; set; }
        public IEnumerable<Element> Select(Element element)
        {
            return null;
        }
    }
}
