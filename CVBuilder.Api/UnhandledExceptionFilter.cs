using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CVBuilder.Api
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<UnhandledExceptionFilter> logger;

        public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                context.Exception.Message,
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().Name,
            })
            {
                StatusCode = (int)GetStatusCode(context.Exception.GetType().Name)
            };

            logger.LogError($"Exception Type: {context.Exception.GetType().Name} Message : {context.Exception.Message}");

            context.Result = result;
        }

        private static HttpStatusCode GetStatusCode(string exception)
        {
            if (exception == "NotFoundException")

                return HttpStatusCode.NotFound;

            else if (exception == "BadRequestException")

                return HttpStatusCode.BadRequest;

            else if (exception == "UnAuthorizedException")

                return HttpStatusCode.Unauthorized;
            
            else

            return HttpStatusCode.BadRequest;
        }
    }
}
