namespace Jasmine.ConfigCenter.Common
{
    public class ConfigCenterServiceResponse
    {
        public static readonly ConfigCenterServiceResponse ServiceNotFoundResponse = new ConfigCenterServiceResponse()
        {

        };
        public long ResponseId { get; set; }

        public string Result { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
