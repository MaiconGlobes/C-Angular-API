using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   internal class MigrationRepository
   {
      private Context _context { get; set; }

      internal MigrationRepository()
      {
         _context = new Context();
      }

      internal void ApplyMigration()
      {
         _context.Database.Migrate();
      }

      internal void RevertMigration()
      {

         var appliedMigrations = _context.Database.GetAppliedMigrations();

         foreach (var migration in appliedMigrations)
         {
            _context.Database.ExecuteSqlRaw($"DELETE FROM __EFMigrationsHistory WHERE MigrationId = '{migration}'");
         }

         _context.Database.EnsureDeleted();
      }
   }
}
