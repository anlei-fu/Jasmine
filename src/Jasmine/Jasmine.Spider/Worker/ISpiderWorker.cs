using Jasmine.Spider.Common;
using System.Collections.Generic;

namespace Jasmine.Spider.Worker
{
    public interface ISpiderWorker:IEnumerable<ISpiderTask>,IService,IIdFearture
    {
        void StartTask(long taskId);
        void StopTask(long taskId);
        void ReportDomainBlocked(long taskId,string domain);
        void ReportTaskFinished(long taskId,string workerId);


    }
}
