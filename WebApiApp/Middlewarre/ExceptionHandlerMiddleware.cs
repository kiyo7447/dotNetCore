using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;    
using System.Text.Json;
using System;    
using System.Collections.Generic;    
using System.Linq;    
using System.Net;    
using System.Threading.Tasks;
using System.Text;

namespace WebApiApp.Middlewarre
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            int statusCode = (exception is ApplicationException) ? 220 : (int)HttpStatusCode.InternalServerError;
            //context.Response.ContentType = "application/json";
            //var result = JsonConvert.SerializeObject(new
            //↓
            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result, Encoding.UTF8);
        }
    }
}