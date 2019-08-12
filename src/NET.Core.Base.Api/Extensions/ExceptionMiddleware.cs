using System;
using System.Net;
using Elmah.Io.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NET.Core.Base.Api.Extensions
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private readonly RequestDelegate _next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            exception.Ship(context);
            // await exception.ShipAsync(context);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Task.CompletedTask;
        }
    }
}
