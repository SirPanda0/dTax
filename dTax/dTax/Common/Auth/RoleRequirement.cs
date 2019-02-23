using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public List<AuthenticationRole> roles = new List<AuthenticationRole>();
        public RoleRequirement(AuthenticationRole role)
        {
            switch (role)
            {
                case AuthenticationRole.Operator:
                    roles.Add(AuthenticationRole.Operator);
                    break;
                case AuthenticationRole.Driver:
                    roles.Add(AuthenticationRole.Driver);
                    break;
                case AuthenticationRole.User:
                    roles.Add(AuthenticationRole.User);
                    break;
                default:
                    break;
            }
        }
    }
}
