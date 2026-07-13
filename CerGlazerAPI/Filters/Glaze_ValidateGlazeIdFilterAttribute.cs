using Microsoft.AspNetCore.Mvc.Filters;

namespace CerGlazerAPI.Filters
{
    public class Glaze_ValidateGlazeIdFilterAttribute : ActionFilterAttribute
    {
        override public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = context.ActionArguments["id"] as int?;

                if (id.HasValue && id.Value <= 0)
                {
                    context.Result = 
                        new Microsoft.AspNetCore.Mvc.BadRequestObjectResult("Invalid ID. ID must be a positive integer.");
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
