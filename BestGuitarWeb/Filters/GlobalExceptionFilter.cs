using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestGuitarWeb.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //todo:处理异常
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            //todo:异步处理异常
            return null;
        }
    }
}
