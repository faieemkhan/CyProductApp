using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCCoreApplication.UserException;

namespace MVCCoreApplication.Aspect_Oriented_Programming
{ 
    // Aspect  orinted programming create own Decorator
   
    public sealed  class ExceptionHandlerAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(UserAlreadyExixtsException))
            {
               context.Result = new ConflictObjectResult(context.Exception.Message);
            }
            else if (context.Exception.GetType() == typeof(UserNotExistsException))
            {
                context.Result = new ConflictObjectResult(context.Exception.Message);
             }
            else if (context.Exception.GetType() == typeof(UserCredentialInvalidException))
            {
                context.Result = new ConflictObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
        }
    }
}
