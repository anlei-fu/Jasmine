using Jasmine.Spider.Common;
using Jasmine.Spider.Worker;

namespace Jasmine.Spider.Drivers
{
    public class Driver : IDriver
    {
        public void CreateTask(ISpiderTaskConfig config)
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
