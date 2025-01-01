using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Src.Models.Instances;

namespace BaseCodeAPI.Src.Services
{
   public class AddressService : IServices
   {
      private IMapper FMapper { get; set; }
      private IRepository<AddressModel> FIRepository { get; set; }

      private string FToken { get; set; }

      public AddressService(IMapper AIMapper, IRepository<AddressModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
         this.FToken = (string)InstanceToken.LoadInstance();
      }

      public async Task<(byte Status, object Json)> GetAllRegistersAsync()
      {
         try
         {
            var AddressObject = await FIRepository.GetAllRegisterAsync();

            var objReturn = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
               address = AddressObject
            };

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
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

            var addressObject = await FIRepository.GetOneRegisterByIdAsync(AId);

            if (addressObject != null)
            {
               var objReturn = new
               {
                  objReturnToken.token,
                  usuario = addressObject
               };

               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
            }

            return ((byte)GlobalEnum.eStatusProc.NaoLocalizado, ResponseUtils.Instancia().ReturnNotFound(objReturnToken));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }

      }

      public async Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         try
         {
            var address = this.FMapper.Map<AddressModel>(AModel);

            if (address != null)
            {            
               int rowsAffect = await FIRepository.CreateRegisterAsync(address);

               if (rowsAffect > 0)
               {
                  var objReturn = new
                  {
                     token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
                     address = address
                  };

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
               }
            }

            throw new Exception("Houve um erro ao salvar os dados");
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      public Task<(byte Status, object Json)> DeleteRegisterAsync(int AId)
      {
         throw new NotImplementedException();
      }

      public Task<(byte Status, object Json)> UpdateRegisterAsync<T>(T AModel)
      {
         throw new NotImplementedException();
      }
   }
}
