using Jasmine.Parsers.Html;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class SpiderExtractor
    {
        public JObject  Extract(Scope scope,Element root)
        {
            scope.Declare("output",new JObject());

            scope.Excute();

            return scope.GetVariable("output");

        }
    }
}
