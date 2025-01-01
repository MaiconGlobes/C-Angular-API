using BaseCodeAPI.Src.Enums;

namespace Src.Settings.Endpoints
{
   internal static class UserEndpoint
   {
      internal const string Users       = "/api/v1/user/all";
      internal const string UserById    = "/api/v1/user/id";
      internal const string UserAdd     = "/api/v1/user/add";
      internal const string UserEdit    = "/api/v1/user/edit";
      internal const string UsersRemove = "/api/v1/user/remove";


      public static readonly Dictionary<eEndpoint, string> Endpoints = new()
      {
         { eEndpoint.Users, Users },
         { eEndpoint.Users, UserById },
         { eEndpoint.UserAdd, UserAdd },
         { eEndpoint.UserEdit, UserEdit },
         { eEndpoint.UserRemove, UsersRemove },
      };
   }
}
