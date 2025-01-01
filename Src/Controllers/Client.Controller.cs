using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using Src.Settings.Endpoints;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class ClientController : ControllerBase
   {
      private readonly IServices FIServices;

      public ClientController(IServicesFactory servicesFactory)
      {
         this.FIServices = servicesFactory.CreateService(eServiceType.Client);
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientModelDto))]
      [Authenticated()]
      [Route(ClientEndpoint.Clients)]
      public async Task<IActionResult> GetAllRegisterAsync()
      {
         try
         {
            var (Status, Json) = await this.FIServices.GetAllRegistersAsync();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoAutorizado => new UnauthorizedObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpGet("id")]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientModelDto))]
      [Authenticated()]
      [Route(ClientEndpoint.ClientsById)]
      public async Task<IActionResult> GetOneRegisterByIdAsync([FromQuery] int id)
      {
         try
         {
            var (Status, Json) = await this.FIServices.GetOneRegisterByIdAsync(id);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoLocalizado => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoAutorizado => new UnauthorizedObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientModelDto))]
      [Authenticated()]
      [Route(ClientEndpoint.ClientNew)]
      public async Task<IActionResult> CreateRegisterAsync([FromBody] ClientModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIServices.CreateRegisterAsync(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new CreatedResult(string.Empty, Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.RegistroDuplicado => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpPut]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientModelDto))]
      [Authenticated()]
      [Route(ClientEndpoint.ClientUpd)]
      public async Task<IActionResult> UpdateRegisterAsync([FromBody] ClientModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIServices.UpdateRegisterAsync(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.RegistroDuplicado => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpDelete("id")]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientModelDto))]
      [Authenticated()]
      [Route(ClientEndpoint.ClientRmv)]
      public async Task<IActionResult> DeleteRegisterAsync([FromQuery] int id)
      {
         try
         {
            var (Status, Json) = await this.FIServices.DeleteRegisterAsync(id);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoAutorizado => new UnauthorizedObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
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
