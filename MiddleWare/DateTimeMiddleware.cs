using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace MVCCoreApplication.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DateTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public DateTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Debug.WriteLine("Date and Time Request Arrived " + DateTime.Now.ToString());
            await _next(httpContext); 
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DateTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseDateTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DateTimeMiddleware>();
        }
    }
}
