namespace Spider.Downloader
{
    public  interface IDownloader
    {
        DownloadResult DownLoad(DownloadParamers paramer);
        string DownLoad(string url,int timeout);
        
    }
}
