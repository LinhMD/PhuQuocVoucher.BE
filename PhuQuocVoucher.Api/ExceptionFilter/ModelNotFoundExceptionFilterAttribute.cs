using CrudApiTemplate.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhuQuocVoucher.Api.Ultility;

namespace PhuQuocVoucher.Api.ExceptionFilter;

public class ModelNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not ModelNotFoundException exception) return;

        exception.StackTrace.Dump();

        IActionResult result = new BadRequestObjectResult(context)
        {
            Value = new {message = exception.Message}
        };
        context.Result = result;
    }
}