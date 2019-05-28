using System;
using System.Threading.Tasks;
using Jasmine.Common;
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
        public bool AllowUpload { get; set; }
        public bool UseStaticFile { get; set; }
        public string VirtuePathRoot { get; set; }
        public bool UseHotUpdate { get; set; }
        public bool UseApiManager { get; set; }
        public override async Task DispatchAsync(string path, HttpFilterContext context)
        {
            if (_processorManager.ContainsProcessor(path))
            {
                var processor = _processorManager.GetProcessor(path);

                if (processor.Available)
                {
                    try
                    {
                        await processor.Filter.First.FiltsAsync(context);
                    }
                    catch (Exception ex)
                    {
                        context.Error = ex;

                        _logger?.Error(ex);

                        await processor.ErrorFilter.First.FiltsAsync(context);
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
            else if(UseStaticFile)
            {
                var stream = FileProvider.GetAsync(VirtuePathRoot + path);

                if(stream==null)
                {
                    context.HttpContext.Response.StatusCode = HttpStatuCodes.NOT_FPOUND;

                    await context.HttpContext.Response.Body.FlushAsync();
                }
                else
                {
                    var index = path.LastIndexOf(".");

                    var ext =  index!=-1? path.Substring(index, path.Length - index):
                                           ".html";
                    context.HttpContext.Response.Headers.Add("Content-Type", MediaTypeHelper.GetContentTypeByExtension(ext));

                    await stream.CopyToAsync(context.HttpContext.Response.Body);

                    stream.Close();

                    await context.HttpContext.Response.Body.FlushAsync();

                    
                }


            }
        }
    }
}
