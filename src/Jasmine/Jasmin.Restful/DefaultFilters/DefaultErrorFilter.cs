using System;
using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful.DefaultFilters
{
    public class DefaultErrorFilter : AbstractFilter<HttpFilterContext>
    {
        private const string ErrorPage = @"
<html>
<head>
<meta http-equiv='Content-Type' content='text/html;charset=utf-8'>
<title>
error page
</title>
<style>
.footer
{
text-align:center;
border-bottom: solid 1px;
}
.header
{
border-bottom: solid 1px;
}
</style>
</head>
<body>
<h1 class='header'>
error happed!
</h1>
<div>
@msg
</div>
<div class='footer'>Jasmine Restful Application-@time</div>
</body>
</html>
";
        public DefaultErrorFilter() 
        {
        }

        public override Task<bool> FiltsAsync(HttpFilterContext context)
        {
            context.HttpContext.Response.StatusCode = HttpStatusCodes.SERVER_ERROR;

            context.ReturnValue = ErrorPage.Replace("@msg", context.Error.ToString())
                                           .Replace("@time", DateTime.Now.ToString());

            return  Task.FromResult(true);


        }
    }
}
