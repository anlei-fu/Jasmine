

namespace Jasmine.Spider.Worker.Excutor.Downloader
{
    public  interface IDownloader
    {
        DownloadResult DownLoad(DownloadParamers paramer);
        string DownLoad(string url,int timeout);
        
    }
}
