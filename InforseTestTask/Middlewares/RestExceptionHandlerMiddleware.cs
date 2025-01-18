using InforseTestTask.Core.DTO.Response;
using InforseTestTask.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace InforseTestTask.Api.Middlewares
{

    public class RestExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
    {
        public RestExceptionHandlerMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;

            switch (exception)
            {
                case EntityNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case UnAuthenticatedException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case UrlAlreadyExistException:
                    code = HttpStatusCode.Conflict;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            return (code, JsonSerializer.Serialize(new ErrorResponse(message: exception.Message)));
        }
    }
}
