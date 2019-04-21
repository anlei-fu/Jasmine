using Jasmine.Spider.Common;

namespace Jasmine.Spider.Worker
{
    public interface IWorkerMessager
    {

        void SendMessage(SpiderMessage msg);
   
      
    }
}
