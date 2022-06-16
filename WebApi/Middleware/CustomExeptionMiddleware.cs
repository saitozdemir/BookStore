using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            var watch = Stopwatch.StartNew();
            try
            { 
                string message = "[Request] HTTP " + context.Request.Method + "-" + context.Request.Path;
                Console.WriteLine(message);
                await _next(context);
                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " +watch.Elapsed.TotalMilliseconds+ " ms";
                System.Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await  HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " +watch.Elapsed.TotalMilliseconds+ " ms";
            System.Console.WriteLine(message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
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