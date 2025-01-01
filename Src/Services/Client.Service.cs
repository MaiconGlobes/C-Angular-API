using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Src.Models.Instances;
using static BaseCodeAPI.Src.Enums.GlobalEnum;

namespace BaseCodeAPI.Src.Services
{
   public class ClientService : IServices
   {
      private IMapper FMapper { get; set; }
      private IRepository<ClientModel> FIRepository { get; set; }

      private string FToken { get; set; }

      public ClientService(IMapper AIMapper, IRepository<ClientModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
         this.FToken = (string)InstanceToken.LoadInstance();
      }

      public async Task<(byte Status, object Json)> GetAllRegistersAsync()
      {
         try
         {
            var clientObject = await FIRepository.GetAllRegisterAsync();
            var clientModelDto = clientObject.Select(item => FMapper.Map<ClientModelDto>(item)).ToList();

            var objReturn = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
               client = clientModelDto
            };

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
         }
         catch (Exception ex)
         {
            return ErrorHandler.New().ProcessExceptionMessage(ex);
         }
      }

      public async Task<(byte Status, object Json)> GetOneRegisterByIdAsync(int AId)
      {
         try
         {
            var objReturnToken = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken)
            };

            var clientObject = await FIRepository.GetOneRegisterByIdAsync(AId);

            if (clientObject != null)
            {
               var objReturn = new
               {
                  objReturnToken.token,
                  client = clientObject
               };

               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
            }

            return ((byte)GlobalEnum.eStatusProc.NaoLocalizado, ResponseUtils.Instancia().ReturnNotFound(objReturnToken));
         }
         catch (Exception ex)
         {
            return ErrorHandler.New().ProcessExceptionMessage(ex);
         }
      }

      public async Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         try
         {
            var clientModelDto = AModel as ClientModelDto;
            var client = FMapper.Map<ClientModel>(clientModelDto);

            int rowsAffect = await FIRepository.CreateSynchronizeRegisterAsync(client);

            if (rowsAffect > 0)
               return ((byte)eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(clientModelDto));

            throw new Exception("Houve um erro ao salvar os dados");

         }
         catch (Exception ex)
         {
            return ErrorHandler.New().ProcessExceptionMessage(ex);
         }
      }

      public async Task<(byte Status, object Json)> UpdateRegisterAsync<T>(T AModel)
      {
         try
         {
            var clientModelDto = AModel as ClientModelDto;
            var client = FMapper.Map<ClientModel>(clientModelDto);

            int rowsAffect = await FIRepository.UpdateRegisterAsync(client);

            if (rowsAffect > 0)
               return ((byte)eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(clientModelDto));

            throw new Exception("Houve um erro ao salvar os dados");

         }
         catch (Exception ex)
         {
            return ErrorHandler.New().ProcessExceptionMessage(ex);
         }
      }


      public async Task<(byte Status, object Json)> DeleteRegisterAsync(int AId)
      {
         try
         {
            var objReturn = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
            };

            var clientObject = await FIRepository.GetOneRegisterByIdUniqueEntityAsync(AId);

            if (clientObject != null)
            {
               int rowsAffect = await FIRepository.DeleteRegisterAsync(clientObject);

               if (rowsAffect > 0)
               {
                  

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
               }
            }

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnNotFound(objReturn));
         }
         catch (Exception ex)
         {
            return ErrorHandler.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
