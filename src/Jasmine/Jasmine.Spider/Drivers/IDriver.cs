using Jasmine.Spider.Worker;

namespace Jasmine.Spider.Drivers
{
    public  interface IDriver
    {
        void CreateTask(ISpiderTaskConfig config);
        void DestroyTask(long taskId);
        void StartTask(long taskId);
        void StopTask(long id);
        void Reconfig(long taskId, ISpiderTaskConfig config);
        void GetConfig(long taskId);
    }
}
