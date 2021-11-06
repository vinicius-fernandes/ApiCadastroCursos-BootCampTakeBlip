using curso.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
               context.Result = new BadRequestObjectResult(new ValidaCampoViewModelOutput(context.ModelState.SelectMany(s => s.Value.Errors).Select(s => s.ErrorMessage)));
            }
        }
    }
}
