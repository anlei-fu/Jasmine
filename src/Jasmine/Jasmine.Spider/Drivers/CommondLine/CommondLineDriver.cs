using Jasmine.Spider.Drivers;
using Jasmine.Spider.Worker;
using System;

namespace Jasmine.Spider.Drivers
{
    public class CommondLineDriver : IDriver
    {
        public void CreateTask(ISpiderTaskConfig config)
        {
            throw new NotImplementedException();
        }

        public void DestroyTask(long taskId)
        {
            throw new NotImplementedException();
        }

        public void GetConfig(long taskId)
        {
            throw new NotImplementedException();
        }

        public void Reconfig(long taskId, ISpiderTaskConfig config)
        {
            throw new NotImplementedException();
        }

        public void StartTask(long taskId)
        {
            throw new NotImplementedException();
        }

        public void StopTask(long id)
        {
            throw new NotImplementedException();
        }
    }
}
