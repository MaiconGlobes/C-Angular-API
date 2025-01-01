using BaseCodeAPI.Src.Enums;

namespace Src.Settings.Endpoints
{
   internal static class ClientEndpoint
   {
      internal const string Clients       = "/api/v1/client/all";
      internal const string ClientsById   = "/api/v1/client/id";
      internal const string ClientNew    = "/api/v1/client/new";
      internal const string ClientUpd = "/api/v1/client/update";
      internal const string ClientRmv = "/api/v1/client/remove";

      public static readonly Dictionary<eEndpoint, string> Endpoints = new()
      {
         { eEndpoint.Clients, Clients },
         { eEndpoint.ClientById, ClientsById },
         { eEndpoint.ClientAdd, ClientNew},
         { eEndpoint.ClientEdit, ClientUpd },
         { eEndpoint.ClientRemove, ClientRmv },
      };
   }
}
