using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManagerAPI.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CubanAdministratorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var country = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country")?.Value;
            //var isAdmin = claims.Any(c => c.Type == "Role" && c.Value == "Administrator");
            var isAdmin = context.HttpContext.User.IsInRole("Administrator");
            if (country != "CU" && isAdmin)
                context.Result = new ForbidResult();
        }
    }
}
