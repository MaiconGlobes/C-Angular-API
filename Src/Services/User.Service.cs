using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Src.Models.Instances;

namespace BaseCodeAPI.Src.Services
{
   public class UserService : IServices
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }

      private string FToken { get; set; }

      public UserService(IMapper AIMapper, IRepository<UserModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper      = AIMapper;
         this.FIRepository = AiRepository;
         this.FToken       = (string)InstanceToken.LoadInstance();
      }

      public async Task<(byte Status, object Json)> GetAllRegistersAsync()
      {
         try
         {
            var usersObject = await FIRepository.GetAllRegisterAsync();

            foreach (var user in usersObject)
               user.Password = null;

            var objReturn = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
               usuario = usersObject
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

            var usersObject = await FIRepository.GetOneRegisterByIdAsync(AId);

            if (usersObject != null)
            {
               var objReturn = new
               {
                  objReturnToken.token,
                  usuario = usersObject
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

      public Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         throw new NotImplementedException();
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
