using Jasmine.Spider.Worker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Spider.Controller
{
   public interface ISpiderWorkerManager:IDictionary<string,ISpiderWorker>
    {
    }
}
