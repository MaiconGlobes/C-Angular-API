namespace BaseCodeAPI.Src.Utils
{
   public class RequestUtils
   {
      private static RequestUtils _instance { get; set; }
      private object _objJSON { get; set; }

      public static RequestUtils Instance()
      {
         _instance ??= new RequestUtils();
         return _instance;
      }

      internal bool ValidateRote(HttpContext AContext, string ASlug)
      {
         var endpoint = AContext.GetEndpoint();

         if (endpoint != null)
         {
            var path = AContext.Request.Path;

            if (!path.HasValue || !path.Value.Contains(ASlug))
            {
               return true;
            }

            return false;
         }

         return true;
      }
   }
}
