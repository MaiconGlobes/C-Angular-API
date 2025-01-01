using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Settings.Endpoints;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [AllowAnonymous]
   //[ApiExplorerSettings(IgnoreApi = true)]
   public class MigrationController : ControllerBase
   {
      private MigrationService FMigrationService {  get; set; }

      public MigrationController()
      {
         FMigrationService = new MigrationService();   
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [AllowAnonymous]
      [Route(MigrationsEndpoint.MigrationApply)]
      public IActionResult GetApplyMigrate()
      {
         try
         {
            var (Status, Json) = this.FMigrationService.ApplyMigrate();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso           => new NoContentResult(),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor      => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpGet]
      [AllowAnonymous]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [Route(MigrationsEndpoint.MigrationRevert)]
      public IActionResult GetRevertMigrate()
      {
         try
         {
            var (Status, Json) = this.FMigrationService.RevertAllMigration();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso           => new NoContentResult(),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor      => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
