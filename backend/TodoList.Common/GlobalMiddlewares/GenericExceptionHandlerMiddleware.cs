﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TodoList.Common.Exceptions;

namespace TodoList.Common.GlobalMiddlewares
{
    public class GenericExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GenericExceptionHandlerMiddleware> _logger;

        public GenericExceptionHandlerMiddleware(RequestDelegate next, ILogger<GenericExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
#pragma warning disable CA2254
                _logger.LogError($"Something went wrong: {ex}");
#pragma warning restore CA2254

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                InvalidUsernameOrPasswordException => 401,
                NotFoundException => 404,
                BadOperationException => 400,
                _ => 500
            };

            return context.Response.WriteAsync(exception.Message);
        }
    }
}
