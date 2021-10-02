using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = "Internal Server Error :" + e.Message;
            if (e.GetType() != typeof(ValidationException))
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = message
                }.ToString());
            message = e.Message;
            var validationErrors = ((ValidationException)e).Errors;
            httpContext.Response.StatusCode = 400;
            return httpContext.Response.WriteAsync(new ValidationErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message,
                ValidationErrors = validationErrors
            }.ToString());

        }
    }
}