using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Exceptions;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
        //обработка исключений
        //exception handler
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //code = код ответа
            //result = результат ответа
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch(exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            //тип возвращаемого контента
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            //если результат всё ещё пуст
            if(result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { errpr = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
