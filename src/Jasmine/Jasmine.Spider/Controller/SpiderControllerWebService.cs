using Jasmine.Spider.Models;
using Jasmine.Spider.Worker;

namespace Jasmine.Spider.Controller
{
    public class SpiderControllerWebService : ISpiderController
    {
        public void CreateTask(SpiderTaskConfig config)
        {
            throw new System.NotImplementedException();
        }

        public void DestroyTask(long taskId)
        {
            throw new System.NotImplementedException();
        }

        public void GetConfig(long taskId)
        {
            throw new System.NotImplementedException();
        }

        public ISpiderTaskStatFeature GetStat(long taskId)
        {
            throw new System.NotImplementedException();
        }

        public void Reconfig(long taskId, ISpiderTaskConfig config)
        {
            throw new System.NotImplementedException();
        }

        public void StartTask(long taskId)
        {
            throw new System.NotImplementedException();
        }

        public void StopTask(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
