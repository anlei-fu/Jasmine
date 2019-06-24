using Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal;
using System;
using System.Security.Authentication;

namespace Jasmine.Restful
{
    public class ServerConfig
    {
        public int Port { get; set; } = 8000;
        public int Backlog { get; set; } = 100;
        public bool NoDelay { get; set; } = true;
        public int MaxCurrency { get; set; } = 1000;
        public TimeSpan RequestTimeout { get; set; } = new TimeSpan(10 * 60 * 1000);
        public TimeSpan ResponseTimeout { get; set; } = new TimeSpan(10 * 60 * 1000);
        public TimeSpan KeepAliveTimeout { get; set; } = new TimeSpan(10 * 60 * 1000);
        public ListenType? Type { get; } = ListenType.IPEndPoint;
        public FileHandleType? HandleType { get; set; } = FileHandleType.Auto;
        public string SocketPath { get; }
        public ulong FileHandle { get; }
        public int MaxRequestLineSize { get; set; } = 10000;
        public int MaxRequestHeadersTotalSize { get; set; } = 1024 * 1024 ;
        public int MaxRequestHeaderCount { get; set; } = 10000;
        public long MaxRequestBodySize { get; set; } = 1024 * 1024 * 15;
        public TimeSpan RequestHeadersTimeout { get; set; } = new TimeSpan(10 * 60 * 1000);
        public long MaxResponseBufferSize { get; set; } = 1024 * 1024 * 300;
        public SslConfig SslOption { get; } = new SslConfig();
        public bool UseSsl { get; set; } = false;

        public long MaxRequestBufferSize { get; set; } = 1024 * 1024 * 20;

    }
    public class SslConfig
    {
        public SslProtocols SslProtocol { get; set; } = SslProtocols.Tls;
        public string X509Certificate2Path { get; set; }
        public TimeSpan HandshakeTimeout { get; set; } =  new TimeSpan( 5 * 1000);
    }
}
