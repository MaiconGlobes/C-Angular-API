using BaseCodeAPI.Src.Enums;

namespace Src.Settings.Endpoints
{
   internal static class AuthenticateEndpoint
    {
        internal const string AuthenticateLogin    = "/api/v1/authenticate/login";
        internal const string AuthenticateRegister = "/api/v1/authenticate/register";

        public static readonly Dictionary<eEndpoint, string> Endpoints = new()
      {
         { eEndpoint.AuthenticateLogin, AuthenticateLogin },
         { eEndpoint.AuthenticateRegister, AuthenticateRegister },
      };
    }
}
