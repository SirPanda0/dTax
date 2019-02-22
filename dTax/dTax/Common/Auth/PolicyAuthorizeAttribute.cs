using Microsoft.AspNetCore.Authorization;
using System;

namespace dTax.Auth
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class PolicyAuthorizeAttribute : AuthorizeAttribute
    {
        public PolicyAuthorizeAttribute(AuthorizePolicy policy)
        {
            Policy = policy.ToString();
        }
    }
}
