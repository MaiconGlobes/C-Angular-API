using BaseCodeAPI.Src.Enums;

namespace Src.Settings.Endpoints
{
   internal static class AddressEndpoint
   {
      internal const string Address       = "/api/v1/address/all";
      internal const string AddressById   = "/api/v1/address/id";
      internal const string AddressAdd    = "/api/v1/address/add";
      internal const string AddressEdit   = "/api/v1/address/edit";
      internal const string AddressRemove = "/api/v1/address/remove";

      public static readonly Dictionary<eEndpoint, string> Endpoints = new()
      {
         { eEndpoint.Address, Address },
         { eEndpoint.AddressById, AddressById },
         { eEndpoint.AddressAdd, AddressAdd },
         { eEndpoint.AddressEdit, AddressEdit },
         { eEndpoint.AddressRemove, AddressRemove },
      };
   }
}
