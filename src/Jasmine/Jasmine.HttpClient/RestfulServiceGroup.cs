using System.Collections.Concurrent;

namespace Jasmine.HttpClient
{
    public  class RestfulServiceGroup:AbstractServiceGroup<RestFulService>
    {
        private ConcurrentDictionary<string, RestFulService> _map = new ConcurrentDictionary<string, RestFulService>();
        public string Domain { get; set; }
        public int Timeout { get; set; } = 10000;
        public int MaxRetryTime { get; set; } = 1;
        public string Method { get; set; } = "GET";

    }
}
