using Jasmine.Common;
using Jasmine.Restful.Implement;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public  class RestfulApplication
    {
        public RestfulApplication(int port,IDispatcher<HttpFilterContext> dispatcher)
        {
            _dispatcher = dispatcher;
            _port = port;
        }
        private IDispatcher<HttpFilterContext> _dispatcher;
        private int _port;
        private IWebHost _host;
        public Task StartAsyn()
        {

          _host=  WebHost.CreateDefaultBuilder()
                         .UseKestrel()
                         .ConfigureKestrel(
                                           option =>
                                           {
                                               option.ConfigureEndpointDefaults(
                                                              option1=> 
                                                              {
                                                                  option1.IPEndPoint = new IPEndPoint(IPAddress.Any, _port);
                                                              });
                                           })
                        .ConfigureServices(
                                           services=>
                                          {
                                           services.AddSingleton<ResfulMiddleware>();
                                          })
                        .Configure(
                                          app=>
                                          {
                                           ResfulMiddleware.Dispatcher = _dispatcher;
                               
                                          app.UseMiddleware<ResfulMiddleware>();
                                          })
                        .Build();

            return _host.RunAsync();

        }
        public Task StopAsync()
        {
           return  _host.StopAsync();
        }

        
    }
}
