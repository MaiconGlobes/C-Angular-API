
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaseCodeAPI.Src.Services
{
   public class SecurityService
   {
      internal static SecurityService FInstancia { get; set; }

      internal static SecurityService New()
      {
         FInstancia ??= new SecurityService();
         return FInstancia;
      }

      internal async Task<(byte Status, object Json)> CreateNewToken<T>(T AModel)
      {
         try
         {
            var userRepository    = new UserRepository();
            var tokenUserModelDto = AModel as TokenUserModelDto;
            var tokenHandler      = new JwtSecurityTokenHandler();
            var jwtSecurityToken  = tokenHandler.ReadJwtToken(tokenUserModelDto.Token);
            var email             = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value?.ToLower();
            var password          = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "secret")?.Value?.ToLower();
            var userModel         = new UserModel {Email = email, Password = password };
            var usersObject       = await userRepository.GetOneRegisterAsync(userModel);

            if (usersObject != null)
            {
               if (!this.IsTokenExpired(usersObject.RefreshToken))
               {
                  var newObject = new
                  {
                     token = tokenUserModelDto.Token
                  };

                  tokenUserModelDto.Token = this.GenerateToken(newObject);

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(tokenUserModelDto));
               }

               return UtilsClass.New().ProcessExceptionMessage(new Exception("Usuário não autorizado"));
            }

            return UtilsClass.New().ProcessExceptionMessage(new Exception("Token enviado é inválido"));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      internal string EncryptPassword(string password)
      {
         var secretKeyPassword = EnvironmentsSettings.New()._iConfigRoot.GetConnectionString("SecretKeyPassword");

         using (SHA256 sha256Hash = SHA256.Create())
         {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
               builder.Append(bytes[i].ToString(secretKeyPassword));

            return builder.ToString();
         }
      }

      internal virtual string GenerateToken(UserModelDto AModel, int AExpirationMinutes)
      {
         var secretKey = EnvironmentsSettings.New()._iConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
            {
               new ("email", AModel.Email.ToLower()),
               new ("secret", AModel.Password.ToLower()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(AExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

      internal virtual string GenerateToken(object AObject)
      {
         var secretKey    = EnvironmentsSettings.New()._iConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler = new JwtSecurityTokenHandler();
         var key          = Encoding.ASCII.GetBytes(secretKey);
         var email        = string.Empty;
         var password     = string.Empty;
         var token        = string.Empty;

         Type objectType = AObject.GetType();
         PropertyInfo emailProperty    = objectType.GetProperty("email");
         PropertyInfo passwordProperty = objectType.GetProperty("password");
         PropertyInfo tokenProperty    = objectType.GetProperty("token");

         if (emailProperty != null && passwordProperty != null)
         {
            email     = emailProperty.GetValue(AObject).ToString().ToLower();
            password  = passwordProperty.GetValue(AObject).ToString().ToLower();
         }

         if (tokenProperty != null)
            token = tokenProperty.GetValue(AObject).ToString();

         if (!string.IsNullOrEmpty(token))
         {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            email    = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
            password = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "secret")?.Value;
         }

         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
            {
               new ("email", email),
               new ("secret", password),
            }),
            Expires = DateTime.UtcNow.AddSeconds(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var newToken = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(newToken);
      }

      internal bool CompareHasch(string APassword, string AHashedPassword)
      {
         string hashedInput = this.EncryptPassword(APassword);
         return string.Equals(hashedInput, AHashedPassword, StringComparison.OrdinalIgnoreCase);
      }

      internal bool IsTokenExpired(string AToken)
      {
         try
         {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(AToken);

            if (DateTime.UtcNow < jwtSecurityToken.ValidTo)
               return false;

            return true;
         }
         catch
         {
            return true;
         }
      }
   }
}
