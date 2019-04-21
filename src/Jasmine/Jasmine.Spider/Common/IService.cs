using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Spider.Common
{
  public  interface IService
    {
        Task StartAsync();
        Task StopAsync();
    }
}
