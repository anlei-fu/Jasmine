using System;
using System.Diagnostics;
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
        private ILog _logger = LogManager.GetLogger(typeof(RestfulDispatcher));


        public override async Task DispatchAsync(string path, HttpFilterContext context)
        {
            /*
             *  is restful api
             */
            if (_processorManager.ContainsProcessor(path))
            {
                /***begin stat*****/
                var stat = new StatItem();
                var watch = Stopwatch.StartNew();
                watch.Start();

                if (RestfulApplicationGlobalConfig.DebugMode)
                    RestfulApplicationBaseComponents.Tracer.BeginTrace(context);

                var processor = (RestfulRequestProcessor)_processorManager.GetProcessor(path);
                /*
                 * is method  match
                 */
                if (processor.HttpMethod == context.HttpContext.Request.Method.ToLower())
                {
                    /*
                     * is api available
                     */
                    if (processor.Available)
                    {
                        try
                        {
                            await processor.FiltsAsysnc(context);

                            //handle processor output

                            if (RestfulApplicationGlobalConfig.EnableCrossDomain)
                                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            context.HttpContext.Response.Headers.Add("Content-Type", "text/html");

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
                    else if (processor.HasAlternativeService)
                    {
                        await DispatchAsync(processor.AlternativeServicePath, context);

                        stat.Sucessed = false;
                        stat.Elapsed = watch.ElapsedMilliseconds;
                        processor.Metric.Add(stat);

                        if (RestfulApplicationGlobalConfig.DebugMode)
                            RestfulApplicationBaseComponents.Tracer.EndTrace(context);

                        // should return  cause alternative service already flushed
                        return;
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = HttpStatusCodes.SERVER_NOT_AVAILABLE;
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCodes.INCORRECT_HTTP_METHOD;
                }

                if (context.HttpContext.Response.StatusCode != HttpStatusCodes.SUCESSED)
                    stat.Sucessed = false;

                stat.Elapsed = watch.ElapsedMilliseconds;

                processor.Metric.Add(stat);

                if (RestfulApplicationGlobalConfig.DebugMode)
                    RestfulApplicationBaseComponents.Tracer.EndTrace(context);
            }
            /*
             *  static file enabled 
             */
            else if (RestfulApplicationGlobalConfig.StaticFileEnabled)
            {
                var stream = await RestfulApplicationBaseComponents.StaticFileProvider.GetStreamAsync(RestfulApplicationGlobalConfig.VirtueRootPath + path);

                /*
                 *  file not exists
                 */
                if (stream == null)
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCodes.NOT_FOUND;
                }
                else
                {
                    var index = path.LastIndexOf(".");

                    var ext = index != -1 ? path.Substring(index, path.Length - index) :
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
