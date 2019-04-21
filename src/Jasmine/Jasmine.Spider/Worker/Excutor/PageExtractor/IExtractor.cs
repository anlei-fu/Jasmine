using Jasmine.Parsers.Html;
using Jasmine.Parsers.Json;
using Jasmine.Spider.PageExtractor.Models;

namespace Jasmine.Spider.Interface
{
    public  interface IExtractor
    {
        JsonObJect Extract(Node node, Element page);
    }
}
