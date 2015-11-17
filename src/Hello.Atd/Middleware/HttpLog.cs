using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;

namespace Hello.Atd.Middleware
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class HttpLog
    {
        private readonly RequestDelegate _next;
        private ILogger _log;

        public HttpLog(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _log = logger.CreateLogger<HttpLog>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // log request
            _log.LogInformation("Request " + httpContext.Request.Path);

            await _next(httpContext);

            // log response
            _log.LogInformation("Response " + httpContext.Response.StatusCode.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpLogExtensions
    {
        public static IApplicationBuilder UseHttpLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpLog>();
        }
    }
}
