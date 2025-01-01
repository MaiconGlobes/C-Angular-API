using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class AddressRepository : IRepository<AddressModel>
   {
      private RepositoryBase FRepositoryBase { get; set; }

      public AddressRepository()
      {
         FRepositoryBase = new RepositoryBase();
      }

      public async Task<IEnumerable<AddressModel>> GetAllRegisterAsync()
      {
         return await FRepositoryBase.GetEntity<AddressModel>().ToListAsync();
      }

      public async Task<AddressModel> GetOneRegisterByIdUniqueEntityAsync(int AId)
      {
         return await FRepositoryBase.GetEntity<AddressModel>().FirstOrDefaultAsync(u => u.Id == AId);
      }

      public async Task<AddressModel> GetOneRegisterAsync(AddressModel AModel)
      {
         return await FRepositoryBase.GetEntity<AddressModel>()
                                     .Where(address => address.Id == AModel.Id)
                                     .SingleOrDefaultAsync();
      }

      public async Task<AddressModel> GetOneRegisterByIdAsync(int AId)
      {
         var result = await FRepositoryBase.GetEntity<AddressModel>()
                                           .Where(address => address.Id == AId)
                                           .SingleOrDefaultAsync();

         return result;
      }

      public async Task<int> CreateRegisterAsync(AddressModel AModel)
      {
         try
         {
            var rowsAffect = await FRepositoryBase.InsertOneAsync(AModel);

            return rowsAffect;
         }
         catch (Exception)
         {
            await FRepositoryBase.RollbackTransactionAsync();
            throw;
         }
      }

      public Task<int> CreateSynchronizeRegisterAsync(AddressModel AModel)
      {
         throw new NotImplementedException();
      }

      public Task<int> DeleteRegisterAsync(AddressModel AModel)
      {
         throw new NotImplementedException();
      }

      public Task<int> UpdateRegisterAsync(AddressModel AModel)
      {
         throw new NotImplementedException();
      }
   }
}
