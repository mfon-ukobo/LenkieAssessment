using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            context.Succeed(requirement);
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "permissions"))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.Claims.Where(x => x.Type == "permissions").ToList();

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s.Value == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
