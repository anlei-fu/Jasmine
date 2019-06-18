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
        public TimeSpan? RequestTimeout { get; set; }
        public TimeSpan? ResponseTimeout { get; set; }
        public TimeSpan? KeepAliveTimeout { get; set; }
        public ListenType? Type { get; }
        public FileHandleType? HandleType { get; set; }
        public string SocketPath { get; }
        public ulong? FileHandle { get; }
        public int? MaxRequestLineSize { get; set; }
        public int? MaxRequestHeadersTotalSize { get; set; }
        public int? MaxRequestHeaderCount { get; set; }
        public long? MaxRequestBodySize { get; set; }
        public TimeSpan? RequestHeadersTimeout { get; set; }
        internal SslConfig SslOption { get; }

        public bool UseSsl { get; set; }

        public void ConfigSsl(Action<SslConfig> config)
        {
            UseSsl = true;

            config(SslOption);
        }

       

    }
    public class SslConfig
    {
        public SslProtocols? SslProtocol { get; set; }
        public string X509Certificate2Path { get; set; }
        public TimeSpan? HandshakeTimeout { get; set; }
    }
}
