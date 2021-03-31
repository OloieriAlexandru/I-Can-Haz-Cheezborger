using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Filters
{
    // https://berilkavakli.medium.com/fluent-validation-on-net-core-api-3-1-f01ff4a4c6f5
    // https://code-maze.com/fluentvalidation-in-aspnet/
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Not useful for this validation filter
        }
    }
}
