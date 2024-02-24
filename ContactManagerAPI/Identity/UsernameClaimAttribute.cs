using ContactManagerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerAPI.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UsernameClaimAttribute: Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext
            .RequestServices
            .GetService(typeof(ContactAPIDbContext)) as ContactAPIDbContext;
        var user = context.HttpContext.User;
        var username = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var userExists = await dbContext.Users.AnyAsync(u => u.Username == username);

        if (!userExists)
        {
            context.Result = new ForbidResult();
        }
    }
}
