using Serilog;
using System.Net;
using System.Reflection;

namespace InforseTestTask.Api.Middlewares
{
    public abstract class AbstractExceptionHandlerMiddleware
    {
        private static readonly Serilog.ILogger Logger = Log.ForContext(MethodBase.GetCurrentMethod()?.DeclaringType);

        private readonly RequestDelegate _next;

        public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

        public AbstractExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "error during executing {Context}", context.Request.Path.Value);
                var response = context.Response;
                response.ContentType = "application/json";

                var (status, message) = GetResponse(exception);
                response.StatusCode = (int)status;
                await response.WriteAsync(message);
            }
        }
    }
}
