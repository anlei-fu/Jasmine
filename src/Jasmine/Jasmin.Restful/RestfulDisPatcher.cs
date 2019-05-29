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
        private ILog _logger;
        public IStaticFileProvider FileProvider { get; set; } = new JasmineStaticFileProvider();
        public bool UseStaticFile { get; set; }
        public string VirtuePathRoot { get; set; }
    
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

                        var buffer = JsonSerializer.Instance.SerializeToBytes(context.ReturnValue);

                        await context.HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                    }
                    catch (Exception ex)
                    {
                        _logger?.Error(ex);

                        context.HttpContext.Response.StatusCode = HttpStatuCodes.SERVER_ERROR;
                    }
                }
                /*
                 *  has alternative service
                 */ 
                else if(processor.AlternativeService!=null)
                {
                    await DispatchAsync(processor.AlternativeService, context);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = HttpStatuCodes.SERVER_NOT_AVAILABLE;
                }
            }
            /*
             *  static file enabled 
             */ 
            else if(UseStaticFile)
            {
                var stream = FileProvider.GetAsync(VirtuePathRoot + path);

                /*
                 *  file not exists
                 */ 
                if(stream==null)
                {
                    context.HttpContext.Response.StatusCode = HttpStatuCodes.NOT_FPOUND;
                }
                else
                {
                    var index = path.LastIndexOf(".");

                    var ext =  index!=-1? path.Substring(index, path.Length - index):
                                           ".html";

                    context.HttpContext.Response.Headers.Add("Content-Type", MediaTypeHelper.GetContentTypeByExtension(ext));

                    await stream.CopyToAsync(context.HttpContext.Response.Body);

                    stream.Close();
                    
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = HttpStatuCodes.NOT_FPOUND;
            }

            /*
             * flush 
             */ 

            await context.HttpContext.Response.Body.FlushAsync();


        }
    }
}
