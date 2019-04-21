using Jasmine.Parsers.Html;
using Jasmine.Parsers.Json;
using System.Collections.Generic;

namespace Jasmine.Spider.PageExtractor.Models
{
    public  class Node
    {
        public ChildrenModel ChildrenModel { get; set; }
        public string Selector { get; set; }
        public string Spilter { get; set; }
        public string Name { get; set; }
        public JonObjectType JsonObjectType { get; set; }
        public IList<Node> Children { get; set; }
   }
}
