using BaseCodeAPI.Src.Enums;

namespace Src.Settings.Endpoints
{
   internal static class MigrationsEndpoint
    {
        internal const string MigrationApply  = "/api/v1/apply-migration";
        internal const string MigrationRevert = "/api/v1/revert-migration";

        public static readonly Dictionary<eEndpoint, string> Endpoints = new()
      {
         { eEndpoint.MigrationApply, MigrationApply },
         { eEndpoint.MigrationRevert, MigrationRevert },
      };
    }
}
