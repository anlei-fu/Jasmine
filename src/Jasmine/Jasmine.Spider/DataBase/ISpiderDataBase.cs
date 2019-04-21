using Jasmine.Spider.Common;
using Jasmine.Spider.Worker;
using System.Collections.Generic;

namespace Jasmine.Spider.DataBase
{
    public   interface ISpiderDataBase
    {
        bool SaveResult(IEnumerable<SpiderTaskResult> results);
        List<string> GetAllUrl(long taskId);

        List<string> GetUnFinishedUrl(long taskId,int count);
        ISpiderTaskConfig GetTaskConfig(long taskId);
        bool SaveTaskConfig(ISpiderTaskConfig config);
        bool ReStoreTaskConfig(ISpiderTaskConfig config);


    }
}
