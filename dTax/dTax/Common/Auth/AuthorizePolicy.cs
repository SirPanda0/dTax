using System;

namespace dTax.Auth
{
    [Flags]
    public enum AuthorizePolicy
    {
        Operator = 1,
        User = 2,
        Driver = 3,
        FullAccess = 0
    }

    public static class AuthorizePolicyValues
    {
        public static string Operator => AuthorizePolicy.Operator.ToString();
        public static string User = AuthorizePolicy.User.ToString();
        public static string Driver = AuthorizePolicy.Driver.ToString();
        public static string FullAccess = AuthorizePolicy.FullAccess.ToString();
    }

}
