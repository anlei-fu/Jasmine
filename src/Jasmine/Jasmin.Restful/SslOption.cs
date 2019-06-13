using System;
using System.Security.Authentication;

namespace Jasmine.Restful
{
    public  class SslOption
    {
        public SslProtocols SslProtocol { get; set; }
        public string X509Certificate2Path { get; set; }
        public TimeSpan HandshakeTimeout { get; set; }
    }
}
