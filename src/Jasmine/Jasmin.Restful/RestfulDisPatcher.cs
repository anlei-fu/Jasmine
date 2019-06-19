using System;
using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Serialization;
using log4net;

namespace Jasmine.Restful
{
    public class RestfulDispatcher : AbstractDispatcher<HttpFilterContext>
    {
        public RestfulDispatcher(string name, IRequestProcessorManager<HttpFilterContext> processorManager) : base(name, processorManager)
        {
        }
        private ILog _logger=LogManager.GetLogger(typeof(RestfulDispatcher));
      
    
        public override async Task DispatchAsync(string path, HttpFilterContext context)
        {
            /*
             *  is restful api
             */ 
            if (_processorManager.ContainsProcessor(path))
            {
                var processor = _processorManager.GetProcessor(path);

                /*
                 * api available
                 */
                if (processor.Available)
                {
                    try
                    {
                        await processor.FiltsAsysnc(context);

                        //handle processor output

                        if (RestfulApplicationGlobalConfig.EnableCrossDomain)
                            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        if (context.ReturnValue != null)
                        {
                            var buffer = JsonSerializer.Instance.SerializeToBytes(context.ReturnValue);

                            await context.HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);

                        if (!context.HttpContext.Response.HasStarted)
                            context.HttpContext.Response.StatusCode = HttpStatusCodes.SERVER_ERROR;
                    }
                }
                /*
                 *  has alternative service
                 */ 
                else if(processor.HasAlternativeService)
                {
                    await DispatchAsync(processor.AlternativeServicePath, context);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCodes.SERVER_NOT_AVAILABLE;
                }
            }
            /*
             *  static file enabled 
             */ 
            else if(RestfulApplicationGlobalConfig.StaticFileEnabled)
            {
                var stream = await RestfulApplicationBaseComponent.StaticFileProvider.GetStreamAsync(RestfulApplicationGlobalConfig.VirtueRootPath + path);

                /*
                 *  file not exists
                 */ 
                if(stream==null)
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCodes.NOT_FOUND;
                }
                else
                {
                    var index = path.LastIndexOf(".");

                    var ext =  index!=-1? path.Substring(index, path.Length - index):
                                           ".html";

                    context.HttpContext.Response.StatusCode = HttpStatusCodes.SUCESSED;

                    context.HttpContext.Response.Headers.Add("Content-Type", MediaTypeHelper.GetMediaTypeByExtension(ext));

                    await stream.CopyToAsync(context.HttpContext.Response.Body);

                    stream.Close();
                    
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = HttpStatusCodes.NOT_FOUND;
            }

            /*
             * flush 
             */

            await context.HttpContext.Response.Body.FlushAsync();

        }
    }
}
