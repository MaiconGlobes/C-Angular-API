using static BaseCodeAPI.Src.Enums.GlobalEnum;
using System.Text.RegularExpressions;

namespace BaseCodeAPI.Src.Utils
{
   public class ErrorHandler : ResponseUtils
   {
      public static ErrorHandler _instance { get; set; }

      public static ErrorHandler New()
      {
         _instance ??= new ErrorHandler();
         return _instance;
      }

      private Func<Exception, object> UniqueKeyConstraintError(string customMessage)
      {
         return ex =>
         {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;

            var regex = new Regex(@"Duplicate entry : ([a-zA-Z0-9_]+)\.([a-zA-Z0-9_]+)");
            var match = regex.Match(errorMessage);

            string field = null;

            if (match.Success)
               field = match.Groups[2].Value;

            if (!string.IsNullOrEmpty(field))
               errorMessage = $"{customMessage} para o campo '{field}'";
            else
               errorMessage = customMessage;

            var exceptionReturn = new Exception(errorMessage);

            return base.ReturnDuplicated(exceptionReturn);
         };
      }

      private Func<Exception, object> ForeignKeyConstraintError(string customMessage)
      {
         return ex =>
         {
            var errorMessage = ex.InnerException?.Message[34..] ?? ex.Message;
            errorMessage = errorMessage.Replace("a foreign key constraint fails", customMessage);
            return base.ReturnDuplicated(new Exception(errorMessage));
         };
      }

      internal (byte Status, object Json) ProcessExceptionMessage(Exception ex)
      {
         Dictionary<string, (byte, Func<Exception, object>)> errorMappings = new()
         {
            { "Duplicate entry ",     ((byte)eStatusProc.RegistroDuplicado, UniqueKeyConstraintError("Violação de UNIQUE KEY: Valor já cadastrado no banco de dados.")) },
            { "a foreign key constraint fails", ((byte)eStatusProc.ErroProcessamento, ForeignKeyConstraintError("Falha de FOREIGN KEY: A referência ao registro não foi encontrada.")) },
         };

         if (ex?.InnerException != null)
         {
            string message = ex.InnerException?.Message ?? ex.Message;

            foreach (var kvp in errorMappings)
            {
               if (message.Contains(kvp.Key))
               {
                  var (status, func) = kvp.Value;
                  return (status, func(ex));
               }
            }
         }

         return ((byte)eStatusProc.ErroProcessamento, base.ReturnErrorProcess(ex));
      }
   }

}
