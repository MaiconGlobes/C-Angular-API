using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;

namespace BaseCodeAPI.Src.Utils
{
   internal class PopularDataUtils
   {
      private static PopularDataUtils FInstancia { get; set; }

      public static PopularDataUtils Instancia()
      {
         FInstancia ??= new PopularDataUtils();
         return FInstancia;
      }

      public PersonModel PopularPerson()
      {
         return new PersonModel
         {
            Id = 1,
            Name = "Person example",
            Document = "98585810652",
            Phone = "19999999999" ,
         };
      }

      public UserModel PopularUsers()
      {
         return new UserModel()
         {
            Id = 1,
            Email = "user@example.com",
            Password = $"{SecurityService.New().EncryptPassword("123456")}",
            RefreshToken = Guid.NewGuid().ToString(),
            PersonId = 1,
         };
      }

      public IList<AddressModel> PopularAddress()
      {
         return new List<AddressModel>
         {
            new()
            {
               Id = 1,
               Address = "address example" ,
               Number = "1001",
               District = "District example",
               City = "City example",
               Uf = "SP",
               ZipCode = "13545215",
               PersonId = 1
            }
         };
      }
   }
}
