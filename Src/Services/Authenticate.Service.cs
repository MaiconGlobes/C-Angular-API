using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class AuthenticateService : IAuthenticate
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }

      public AuthenticateService(IMapper AIMapper, IRepository<UserModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
      }

      public async Task<(byte Status, object Json)> ProcessLoginAsync<T>(T AModel)
      {
         try
         {
            var userModelDto = AModel as UserModelDto;
            var user         = this.FMapper.Map<UserModel>(userModelDto);

            user.Password = SecurityService.New().EncryptPassword(userModelDto.Password);

            var usersObject  = await FIRepository.GetOneRegisterAsync(user);

            if (usersObject != null)
            {
               var newObject = new
               {
                  email    = userModelDto.Email,
                  password = user.Password,
               };

               var newToken = SecurityService.New().GenerateToken(newObject);

               var objReturn = new
               {
                  token = newToken
               };
               
               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
            }

            return UtilsClass.New().ProcessExceptionMessage(new Exception("Email e/ou Password inválidos!"));
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
            var userModelDto = AModel as UserModelDto;
            var user         = this.FMapper.Map<UserModel>(userModelDto);
            var person       = this.FMapper.Map<PersonModel>(userModelDto.Person);
            var days         = 30;
            var hours        = 24;
            var minutes      = 60;

            if (user != null || person != null)
            {
               user.Password = SecurityService.New().EncryptPassword(userModelDto.Password);
               user.RefreshToken = SecurityService.New().GenerateToken(userModelDto, (hours * minutes * days));
               user.Person        = person!;

               int rowsAffect = await FIRepository.CreateRegisterAsync(user);

               if (rowsAffect > 0)
               {
                  userModelDto.PessoaId = person.Id;
                  userModelDto.Password = user.Password;
                  userModelDto.Password = null;

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(userModelDto));
               }
            }

            throw new Exception("Houve um erro ao salvar os dados");
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      
   }
}
