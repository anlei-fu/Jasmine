using System.Collections.Generic;

namespace Jasmine.Spider.Common
{
    public  class UrlResult
    {
        public int TaskId { get; set; }
        public List<string> Successed { get; set; }
        public List<string> Failed { get; set; }
        public string WorkerId { get; set; }
    }
}
