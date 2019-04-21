using Jasmine.Spider.Common;
using Jasmine.Spider.Controller;

namespace Jasmine.Spider.Worker
{
    public interface ISpiderTaskExcutor:IIdFearture,ISpiderTaskStatFeature
    {
        void Excute();
        void ReConfig(ISpiderTaskConfig config);
        void Stop();
    }
}
