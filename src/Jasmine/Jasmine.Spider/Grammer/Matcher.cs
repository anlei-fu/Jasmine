using Jasmine.Parsers.Html;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class Matcher:NameFearture
    {
        public Matcher(string name) : base(name)
        {
        }

        public string MatchPattern { get; set; }
        public bool Match(Element element)
        {
            return true;
        }

        public bool HasChildren => Children != null&&Children.Count!=0;
        public bool HasSelector => Selectors != null && Selectors.Count != 0;
        public bool HasExtractor => Extractors != null && Extractors.Count != 0;
    
        public IList<Matcher> Children { get; set; }
        public IList<Selector> Selectors { get; set; }

        public IList<Extractor> Extractors { get; set; }

      
    }
}
