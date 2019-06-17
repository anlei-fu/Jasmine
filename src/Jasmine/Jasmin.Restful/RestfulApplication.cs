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
    public class RestfulApplication
    {
        public RestfulApplication(int port, IDispatcher<HttpFilterContext> dispatcher)
        {
           
        }

        private ListenOption _listenOption;
        private SslOption _sslOption;
        private StaticFileOption _fileOption;
    
        

        private IWebHost _webHost;

        public  Task StartAsync()
        {

            //_webHost = WebHost.CreateDefaultBuilder()
            //                  .UseKestrel()
            //                  .ConfigureKestrel(
            //                                 option =>
            //                                 {
            //                                     option.ConfigureEndpointDefaults(
            //                                                    kestrelOptions =>
            //                                                    {
            //                                                        if (!_portSeted)
            //                                                        {
            //                                                            kestrelOptions.IPEndPoint = new IPEndPoint(IPAddress.Any, _port);

            //                                                            _portSeted = true;
            //                                                        }
            //                                                    });
            //                                 })
            //                  .ConfigureServices(
            //                                 services =>
            //                                {
            //                                    services.AddSingleton<JasmineResfulMiddleware>();
            //                                })
            //                  .Configure(
            //                                app =>
            //                                {
            //                                    JasmineResfulMiddleware.Dispatcher = _dispatcher;

            //                                    app.UseMiddleware<JasmineResfulMiddleware>();
            //                                })
            //                  .Build();

            //await _webHost.RunAsync();

            return null;

        }
        public async Task StopAsync()
        {
            await _webHost.StopAsync();
        }


    }
}
