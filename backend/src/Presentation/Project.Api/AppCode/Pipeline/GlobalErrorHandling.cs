using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Infrastructure.Exceptions;
using System.Net.Mime;

namespace Project.Api.AppCode.Pipeline
{
    public class GlobalErrorHandlingMiddleware
    {
        static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                }
            }
        };

        private readonly RequestDelegate next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
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
                object response = null;
                int statusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = MediaTypeNames.Application.Json;
                switch (ex)
                {
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new
                        {
                            error = true,
                            message = ex.Message
                        };
                        break;

                    
                    case CircleReferenceException:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = new
                        {
                            error = true,
                            message = ex.Message
                        };
                        break;
                    case BadRequestException brEx:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = new
                        {
                            error = true,
                            message = ex.Message,
                            errors = brEx.Errors
                        };
                        break;
                    case AutoMapperMappingException:
                        statusCode = StatusCodes.Status500InternalServerError;
                        response = new
                        {
                            error = true,
                            message = ex.Message
                        };
                        break;
                    case SecurityTokenExpiredException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        response = new
                        {
                            error = true,
                            message = "token_expired"
                        };
                        break;
                    case UnauthorizedAccessException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        response = new
                        {
                            error = true,
                            message = "refresh_token_expired"
                        };
                        break;
                    case EntityAlreadyExistsException eaEx:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = new
                        {
                            error = true,
                            message = eaEx.Message
                        };
                        break;
                    case EntityCreationFailedException ecfEx:
                        statusCode = StatusCodes.Status500InternalServerError;
                        response = new
                        {
                            error = true,
                            message = ecfEx.Message
                        };
                        break;
                    case InvalidOperationException invalidOpEx:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = new
                        {
                            error = true,
                            message = invalidOpEx.Message
                        };
                        break;
                    case OwnerAccessException ownerEx:
                        statusCode = StatusCodes.Status403Forbidden;
                        response = new
                        {
                            error = true,
                            message = ownerEx.Message
                        };
                        break;
                    case OperationFailedException ofEx:
                        statusCode = StatusCodes.Status500InternalServerError;
                        response = new
                        {
                            error = true,
                            message = ofEx.Message
                        };
                        break;
                    case NotConfirmedException notConfirmedEx:
                        statusCode = StatusCodes.Status400BadRequest; 
                        response = new
                        {
                            error = true,
                            message = notConfirmedEx.Message
                        };
                        break;
                    default:
                        response = new
                        {
                            error = true,
                            message = "An unexpected error occurred."
                        };
                        break;
                }

                context.Response.StatusCode = statusCode;
                var jsonBody = JsonConvert.SerializeObject(response, settings: jsonSettings);
                await context.Response.WriteAsync(jsonBody);
            }
        }
    }

    public static class GlobalErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
