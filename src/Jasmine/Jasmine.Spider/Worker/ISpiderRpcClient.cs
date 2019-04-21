using System.Collections.Generic;

namespace Jasmine.Spider.Worker
{
    public interface ISpiderRpcClient
    {
        void ReportUrlResult();
        void ReportResult();
        List<string> FetchNewUrl();
        ISpiderTaskConfig GetConfig(long id);
        

    }
}
