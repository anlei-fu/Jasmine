using System.Net;

namespace Jasmine.Spider.Common
{
    public abstract class SpiderComponentBase : INameFearture,IEndPointFearture
    {
        public abstract string Name { get; set; }

        public abstract EndPoint EndPoint { get; set; }
    }
}
