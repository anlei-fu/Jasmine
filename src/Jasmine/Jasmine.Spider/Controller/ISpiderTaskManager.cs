using Jasmine.Spider.Common;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Spider.Controller
{
    public interface ISpiderTaskManager:IDictionary<long,ISpiderTask>
    {
        ISpiderTask   GetTask(long taskiId);
        bool RemoveTask(long taskId);
        bool ContainsTask(long taskId);
        int GetTaskCount();
    }

    public class SpiderTaskManager
    { 

       
    }
}
