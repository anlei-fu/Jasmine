using Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal;
using System;

namespace Jasmine.Restful
{
    public class ListenOption
    {
        public int Port { get; set; }
        public int Backlog { get; set; }
        public bool NoDelay { get; set; }
        public int MaxCurrency { get; set; }
        public TimeSpan RequestTimeout { get; set; }
        public TimeSpan ResponseTimeout { get; set; }
        public TimeSpan KeepAliveTimeout { get; set; }
        public ListenType Type { get; }
        public FileHandleType HandleType { get; set; }
        public string SocketPath { get; }
        public ulong? FileHandle { get; }
        public int? MaxRequestLineSize { get; set; }
        public int? MaxRequestHeadersTotalSize { get; set; }
        public int? MaxRequestHeaderCount { get; set; }
        public long? MaxRequestBodySize { get; set; }
        public TimeSpan? RequestHeadersTimeout { get; set; }

    }
}
