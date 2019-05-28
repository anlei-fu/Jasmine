using System.Net;

namespace Spider.Downloader
{
    public class DownloadParamers
    {
        public DownloadParamers()
        { }
        public DownloadParamers(string url)
        {
            Url = url;
        }
        /// <summary>
        /// 等待时长
        /// </summary>
        public int Timeout  = 20000;
        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string UserAgent  = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36";
        /// <summary>
        /// 方法
        /// </summary>
        public string Method = "GET";
        /// <summary>
        /// 地址
        /// </summary>
        public string Url  = "";
        /// <summary>
        /// 接受类型
        /// </summary>
        public string Accept  = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
        /// <summary>
        /// 
        /// </summary>
        public bool KeepAlive  = true;
        /// <summary>
        /// 代理服务器
        /// </summary>
        public WebProxy Proxy;
        /// <summary>
        /// 相联系的网页地址
        /// </summary>
        public string Referer;
        /// <summary>
        /// cookie
        /// </summary>
        public CookieContainer Cookie;
       
       
      
    }
}
