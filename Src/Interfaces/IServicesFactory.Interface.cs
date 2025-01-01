using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Services;

namespace BaseCodeAPI.Src.Interfaces
{
   public interface IServicesFactory
   {
      IServices CreateService(eServiceType type);
   }

   public class ServicesFactory : IServicesFactory
   {
      private readonly IServiceProvider _serviceProvider;

      public ServicesFactory(IServiceProvider serviceProvider)
      {
         _serviceProvider = serviceProvider;
      }

      public IServices CreateService(eServiceType AType)
      {
         return AType switch
         {
            eServiceType.User => _serviceProvider.GetService<UserService>(),
            eServiceType.Client => _serviceProvider.GetService<ClientService>(),
            eServiceType.Address => _serviceProvider.GetService<AddressService>(),
            _ => throw new ArgumentOutOfRangeException(nameof(AType), AType, null)
         };
      }
   }
}
