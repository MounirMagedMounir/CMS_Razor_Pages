using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CMS_UI.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {

            _logger = logger;
        }


        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var contextFealure = httpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.LogError(exception, exception.Message);
            var details = new ProblemDetails()
            {
                Detail = exception.Message,
                Status = (int)HttpStatusCode.InternalServerError,
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.WriteAsJsonAsync(details);



            string path = Path.Combine(Directory.GetCurrentDirectory(), $"ExceptionRecord{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.txt");



            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                await outputFile.WriteLineAsync(DateTime.Now.ToString());
                await outputFile.WriteLineAsync(contextFealure.Error.ToString());
                outputFile.WriteLineAsync();

            }

            return true;
        }
    }
}
