using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using TransportManagement.Application.Comman.Exceptions;
using TransportManagement.Application.Wrappers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransportManagement.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; 

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private  async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var result = new Result
            {
                Succeeded = false
                , 
            };
  

            switch (ex)
            {
                case FluentValidation.ValidationException  validationEx:
                    result.StatusCode =(int) HttpStatusCode.BadRequest;
                    result.Message = "Validation Failed";
                    result.Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList();
                    
                     
                    
                    break;

                case KeyNotFoundException keyEX:
                    result.StatusCode =(int) HttpStatusCode.NotFound;
                    result.Message = keyEX.Message;
                    
                    break;

                case NotFoundException notFoundEx:
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Message = notFoundEx.Message;
                    break;

                case BadRequestException badReqEx:
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    result.Message = badReqEx.Message;
                    break;

                default:
                    result.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result.Message = "An unexpected error occurred.";
                    
                    break;
            }

            var json = JsonSerializer.Serialize(result);


            await context.Response.WriteAsync(json);
        }
    }
}
