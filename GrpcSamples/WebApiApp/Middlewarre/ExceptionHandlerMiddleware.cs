using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;    
using System.Text.Json;
using System;    
using System.Collections.Generic;    
using System.Linq;    
using System.Net;    
using System.Threading.Tasks;
using System.Text;
using System.Text.Encodings.Web;

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
            //https://www.terry-u16.net/entry/system-text-json
            var options = new JsonSerializerOptions
            {
                // JavaScriptEncoder.Createでエンコードしない文字を指定するのも可
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // 読みやすいようインデントを付ける
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            }, options);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}