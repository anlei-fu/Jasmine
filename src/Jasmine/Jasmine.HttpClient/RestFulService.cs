using System.Text;
using System.Xml.Serialization;

namespace Jasmine.HttpClient
{

    public class RestFulService:IService
    {
        public bool Https { get; set; }
        public Encoding Encoding { get; set; }
        public RestfulServiceGroup Parent { get; set; }
        public int Timeout { get; set; }
        public int MaxRetryTime { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public bool Throwlable { get; set; }
        [XmlIgnore]
        public ServiceStat Stat { get; set; } = new ServiceStat();
    }
}
