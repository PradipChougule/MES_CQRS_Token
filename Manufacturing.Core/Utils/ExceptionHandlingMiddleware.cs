namespace Manufacturing.Core.Utils
{

    using Manufacturing.Core.Utils;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;
    //using System.Text.Json;
    using ValidationExceptionData = Manufacturing.Core.Utils.ApplicationException;
    //using ValidationExceptionData = Manufacturing.Core.Utils.ValidationException;

    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        //private readonly ILogger<ExceptionHandlingMiddleware> _logger;

       // public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (Exception e)
            {
               // _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }

        private static string GetTitle(Exception exception) =>
          exception switch
          {
              ValidationExceptionData applicationException => applicationException.Title,
              _ => "Server Error"
          };

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                return validationException.ErrorsDictionary;
            }
            return null;
        }
    }
}
