using System.IO;
using System.Net;

namespace Jasmine.Spider.Downloader
{
    public  class DownloadResult
    {
        public long? ContentLength => Stream?.Length;
        public Stream Stream { get; internal set; }
        public string ContentType { get; internal set; }
        /// <summary>
        ///传过来的http 头
        /// </summary>
        public WebHeaderCollection ResposeHeaders { get; internal set; }
        /// <summary>
        /// 传过来的cookie
        /// </summary>
        public CookieCollection Cookie { get; internal set; }
    }
}
