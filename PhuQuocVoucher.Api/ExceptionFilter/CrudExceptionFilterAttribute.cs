using CrudApiTemplate.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhuQuocVoucher.Api.Ultility;

namespace PhuQuocVoucher.Api.ExceptionFilter;

public class CrudExceptionFilterAttribute : ExceptionFilterAttribute
{
    private ILogger<CrudExceptionFilterAttribute> Logger;

    public CrudExceptionFilterAttribute(ILogger<CrudExceptionFilterAttribute> logger)
    {
        Logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var guid = Guid.NewGuid();
        Logger.LogError(context.Exception, $"trace guid: {guid}");
        IActionResult result = context.Exception switch
        {
            ModelNotFoundException exception => new BadRequestObjectResult(context)
            {
                Value = new
                {
                    message = exception.Message,
                    action = "Get",
                    traceId = guid
                }
            },
            DbQueryException dbQueryException => new BadRequestObjectResult(context)
            {
                Value = new
                {
                    message = dbQueryException.Message,
                    action = dbQueryException.Error.ToString(),
                    traceId = guid
                }
            },
            ModelValueInvalidException valueInvalidException => new BadRequestObjectResult(context)
            {
                Value = new
                {
                    message = valueInvalidException.Message,
                    traceId = guid
                }
            },
            _ => new ObjectResult(new
            {
                traceId = guid,
                context.Exception.StackTrace,
                context.Exception.Message
            })
            {
                StatusCode = 500
            }};
        context.Result = result;
    }
}