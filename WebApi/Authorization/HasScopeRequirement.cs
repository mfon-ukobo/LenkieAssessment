using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public HasScopeRequirement(string scope)
        {
            Scope = scope;
        }
    }
}
