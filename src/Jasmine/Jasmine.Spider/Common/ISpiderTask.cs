using Jasmine.Spider.Worker;

namespace Jasmine.Spider.Common
{
    public  interface ISpiderTask
    {
        ISpiderTaskConfig Config { get; }
        ISpiderTaskStat Stat { get; }

    }
}
