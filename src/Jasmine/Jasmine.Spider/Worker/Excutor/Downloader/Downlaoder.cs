﻿
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Jasmine.Spider.Worker.Excutor.Downloader
{

    public  class Downloader
    {
     
        public DownloadResult DownLoad(DownloadParamers paramers)
        {
            try
            {
                DownloadResult result=new DownloadResult();
               
                var response =(HttpWebResponse) createRequest(paramers).GetResponse();
                result.Stream = response.GetResponseStream();
                result.Cookie = response.Cookies;
                result.ContentType = response.ContentType;

                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public string DownLoadHtml(DownloadParamers parameters)
        {
            var result = DownLoad(parameters);
            return result == null ? null : HttpPageDecoder.GetPage( result.Stream);
        }
        public string DownLoadHtml(string url)
        {
            var result = DownLoad(new DownloadParamers(url));
            return result == null ? null : HttpPageDecoder.GetPage(result.Stream);
        }
       

        public string DownloadHtmlWithEncoding(string url,Encoding encoding)
        {
            var result = DownLoad(new DownloadParamers(url));
            return result == null ? null : HttpPageDecoder.GetPage(null,result.Stream,encoding);
        }
        public bool DownLoad(Stream destinationstream,string url,Func<long,long,bool>downloadPartHandler)
        {
            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var bt = new byte[1];
                var sourceStream = response.GetResponseStream();
                long total = response.ContentLength;
                long finished = 0;
                var t = 0;
                while (sourceStream.Read(bt,0,1)!=0)
                {
                    destinationstream.Write(bt, 0, 1);
                    if(t++>1024*1024)
                    {
                        finished += 1024 * 1024;
                        if (downloadPartHandler!=null&&!downloadPartHandler(total, finished))
                            return false;
                        t = 0;
                    }
                }
                downloadPartHandler?.Invoke(total, finished);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private HttpWebRequest createRequest(DownloadParamers paramers)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(paramers.Url);
            req.Method = paramers.Method;
            req.KeepAlive = paramers.KeepAlive;
            req.Timeout = paramers.Timeout;
            req.CookieContainer = paramers.Cookie;
            req.Proxy = paramers.Proxy;
            req.UserAgent = paramers.UserAgent;
            req.Accept = paramers.Accept;
            req.Referer = paramers.Referer;
         
            return req;
        }
    }
}
