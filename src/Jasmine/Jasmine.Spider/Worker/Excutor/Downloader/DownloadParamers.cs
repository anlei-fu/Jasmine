using System.Net;

namespace Jasmine.Spider.Worker.Excutor.Downloader
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
        public string UserAgent  = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:62.0) Gecko/20100101 Fire";
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
        public string Accept  = "*/*";
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
