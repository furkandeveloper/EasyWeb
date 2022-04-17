using EasyWeb.AspNetCore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyWeb.AspNetCore.Filters.Attributes;

/// <summary>
/// An abstract filter that asynchronously surrounds execution of the action and the action result. Subclasses
/// should override <see cref="OnActionExecuting"/>, <see cref="ActionFilterAttribute.OnActionExecuted"/> or
/// <see cref="ActionFilterAttribute.OnActionExecutionAsync"/> but not <see cref="ActionFilterAttribute.OnActionExecutionAsync"/> and either of the other two.
/// Similarly subclasses should override <see cref="ActionFilterAttribute.OnResultExecuting"/>, <see cref="ActionFilterAttribute.OnResultExecuted"/> or
/// <see cref="ActionFilterAttribute.OnResultExecutionAsync"/> but not <see cref="ActionFilterAttribute.OnResultExecutionAsync"/> and either of the other two.
/// </summary>
public class ModelStateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var baseController = context.Controller as BaseApiController;
            context.Result = baseController?.BadRequest(context.ModelState) ??
                             new BadRequestObjectResult(context.ModelState);
        }
        base.OnActionExecuting(context);
    }
}