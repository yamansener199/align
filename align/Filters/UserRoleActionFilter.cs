using align.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace align.Filters
{
    public class UserRoleActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                string? userRole = null;

                var claims = context.HttpContext?.User?.Identities?.FirstOrDefault()?.Claims;

                if (claims != null && claims.Count() > 4)
                {
                    userRole = claims?.ToList()[4].Value;
                }
                if (userRole != null)
                {
                    controller.ViewBag.UserRole = userRole;
                }
                else
                {
                    //if (!context.HttpContext.Request.Path.Value.StartsWith("/Auth/Login"))
                    //{
                    //    context.Result = new RedirectResult("~/Auth/Login");
                    //}

                    return;
                }
            }

            return;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}
