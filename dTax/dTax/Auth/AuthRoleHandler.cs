using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dTax.Auth
{
    public class AuthRoleHandler : AuthorizationHandler<RoleRequirement>
    {

        private void Validate(AuthenticationRole role, AuthorizationHandlerContext context, RoleRequirement roleRequirement)
        {
            if (context.User.Claims.Any(c => c.Type == ClaimsIdentity.DefaultRoleClaimType && c.Value == ((int)role).ToString()))
            {
                context.Succeed(roleRequirement);
            }
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement roleRequirement)
        {
            roleRequirement.roles.ForEach(r => Validate(r, context, roleRequirement));
            return Task.FromResult(0);
        }
    }
}
