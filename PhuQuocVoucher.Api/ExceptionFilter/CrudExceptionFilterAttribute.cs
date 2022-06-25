using CrudApiTemplate.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhuQuocVoucher.Api.Ultility;

namespace PhuQuocVoucher.Api.ExceptionFilter;

public class CrudExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        $"error message: {context.Exception.Message}".Dump();
        context.Exception.StackTrace.Dump();
        IActionResult result = new BadRequestResult();

        switch (context.Exception)
        {
            case ModelNotFoundException exception:
                result = new BadRequestObjectResult(context)
                {
                    Value = new
                    {
                        message = exception.Message,
                        action = "Get"
                    }
                };
                break;
            case DbQueryException dbQueryException:
                result = new BadRequestObjectResult(context)
                {
                    Value = new
                    {
                        message = dbQueryException.Message,
                        action = dbQueryException.Error.ToString()
                    }
                };
                break;
        }

        context.Result = result;
    }
}