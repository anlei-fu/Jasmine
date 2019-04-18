using Jasmine.Surpport;
using System.Xml.Serialization;

namespace Jasmine.Rpc
{
    public  class RpcService
    {
        public string Name { get; set; }
        public int Timeout { get; set; }
        public bool Throwlable { get; set; }
        [XmlIgnore]
        public ServiceStat Stat { get; set; } = new ServiceStat();
    }
    
}
