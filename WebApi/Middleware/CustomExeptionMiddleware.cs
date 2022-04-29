using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middleware
{
    public class CustomExeptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExeptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string message = "[Request] HTTP " + context.Request.Method + "-" + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);
        }       
    }
    public static class CustomExeptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExeptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionMiddleware>();
        }
    }
}