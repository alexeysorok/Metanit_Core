using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloApp
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        string _pattern;

        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this._next = next;
            this._pattern = pattern;
        }

        // Изменим класс TokenMiddleware, чтобы он извне получал образец токена для сравнения
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != _pattern)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await _next.Invoke(context);
            }
        }


        //public async Task InvokeAsync(HttpContext context)
        //{
        //    var token = context.Request.Query["token"];
        //    if (token != "12345678")
        //    {
        //        context.Response.StatusCode = 403;
        //        await context.Response.WriteAsync("Token is invalid");
        //    }
        //    else
        //    {
        //        await _next.Invoke(context);
        //    }
        //}


    }
}
