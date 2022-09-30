using CVBuilder.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CVBuilder.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValidationErrors);
                    break;

                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;

                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    
                    break;
                case Exception ex:
                    httpStatusCode=HttpStatusCode.BadRequest; 
                    break;
            }


            context.Response.StatusCode = (int)httpStatusCode;

            if(result == String.Empty)
            {
                result = JsonConvert.SerializeObject(new {error = exception.Message});
            }    

            return context.Response.WriteAsync(result);
        }
    }
}
