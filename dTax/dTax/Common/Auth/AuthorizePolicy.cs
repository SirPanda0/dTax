using System;

namespace dTax.Auth
{
    [Flags]
    public enum AuthorizePolicy
    {
        SystemAdmin = 0,
        Operator = 1,
        User = 2,
        Driver = 3
    }

    public static class AuthorizePolicyValues
    {
        public static string Operator => AuthorizePolicy.Operator.ToString();
        public static string SystemAdmin => AuthorizePolicy.SystemAdmin.ToString();
        public static string User = AuthorizePolicy.User.ToString();
        public static string Driver = AuthorizePolicy.Driver.ToString();
    }

}
