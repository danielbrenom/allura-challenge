using Alura.Challenge.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alura.Challenge.BackEnd.Api.ExceptionFilter
{
    public class CustomExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = context.Exception switch
            {
                GenericException genericException => new JsonResult(genericException.Message) { StatusCode = genericException.StatusCode },
                InvalidDataException invalidDataException => new JsonResult(invalidDataException.ValidationErrors) { StatusCode = 422 },
                _ => new JsonResult(context.Exception?.Message) { StatusCode = 500 }
            };

            context.ExceptionHandled = true;
        }

        public int Order => int.MaxValue - 10;
    }
}