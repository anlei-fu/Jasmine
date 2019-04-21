using System.Collections.Generic;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public  interface ISpiderUrlFilter
    {
        List<string> GetNewUrl(long taskId);
        bool Filt(long taskId, List<string> newUrls);

        bool CreateNewTask(long taskId);
        bool StopTask(long taskId);

        bool RestartTask(long taskId);
        List<UrlFilterStatics> GetAllTaskStatics();

        UrlFilterStatics GetStatics(long taskId);

    }
}
