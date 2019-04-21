using System;

namespace Jasmine.Spider.Worker
{
    public interface ISpiderTaskStat
    {
        int PageDownloade { get; }
        int PageExtracted { get; }
        int ExcutroCount { get; }
        int UrlFound { get; }
        TimeSpan TimeRunning { get; }

    }
}
