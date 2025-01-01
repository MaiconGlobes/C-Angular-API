using AutoMapper;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseCodeAPI.Src.Repositories
{
   public class ClientRepository : IRepository<ClientModel>
   {
      private RepositoryBase FRepositoryBase { get; set; }
      private IMapper FMapper { get; set; }

      public ClientRepository(IMapper AMapper)
      {
         FMapper = AMapper;
         FRepositoryBase = new RepositoryBase();
      }

      public async Task<IEnumerable<ClientModel>> GetAllRegisterAsync()
      {
         var result = await FRepositoryBase.GetEntity<ClientModel>()
                                           .Include(u => u.Person)
                                           .Select(u => new ClientModel
                                           {
                                              Id = u.Id,
                                              CreditLimit = u.CreditLimit,
                                              LastPurchase = u.LastPurchase,
                                              PersonId = u.PersonId,
                                              Person = new PersonModel
                                              {
                                                 Id = u.PersonId,
                                                 Name = u.Person.Name,
                                                 Document = u.Person.Document,
                                                 Phone = u.Person.Phone,
                                                 Addresses = u.Person.Addresses,
                                              },
                                           })
                                           .ToListAsync();

         return result;
      }

      public async Task<ClientModel> GetOneRegisterByIdUniqueEntityAsync(int AId)
      {
         return await FRepositoryBase.GetEntity<ClientModel>().FirstOrDefaultAsync(u => u.Id == AId);
      }

      public async Task<ClientModel> GetOneRegisterAsync(ClientModel AModel)
      {
         return await FRepositoryBase.GetEntity<ClientModel>()
                                     .Include(u => u.Person)
                                     .Where(user => user.Id == AModel.Id)
                                     .SingleOrDefaultAsync();
      }

      public async Task<ClientModel> GetOneRegisterByIdAsync(int AId)
      {
         var result = await FRepositoryBase.GetEntity<ClientModel>()
                                           .Include(u => u.Person)
                                           .Select(u => new ClientModel
                                           {
                                              Id = u.Id,
                                              PersonId = u.PersonId,
                                              Person = new PersonModel
                                              {
                                                 Id = u.PersonId,
                                                 Name = u.Person.Name,
                                                 Document = u.Person.Document,
                                                 Phone = u.Person.Phone,
                                                 Addresses = u.Person.Addresses,
                                              },
                                           })
                                           .Where(user => user.Id == AId)
                                           .SingleOrDefaultAsync();

         return result;
      }

      public async Task<int> CreateRegisterAsync(ClientModel AModel)
      {
         return await FRepositoryBase.InsertOneAsync(AModel);
      }

      public async Task<int> CreateSynchronizeRegisterAsync(ClientModel AModel)
      {
         var person = await FRepositoryBase.GetEntity<PersonModel>()
                                           .Include(p => p.Addresses)
                                           .Where(p => p.Document == AModel.Person.Document)
                                           .SingleOrDefaultAsync();

         if (person != null)
         {
            _ = this.FMapper.Map(AModel.Person, person);
            _ = this.FMapper.Map(AModel.Person.Addresses, person.Addresses);

            await FRepositoryBase.UpdateOneAsync(person);

            AModel.PersonId = person.Id;
            AModel.Person = null;

            var rowsAffect = await FRepositoryBase.InsertOneAsync(AModel);
            return rowsAffect;
         }

         var result = await FRepositoryBase.InsertOneAsync(AModel);

         return result;
      }

      public async Task<int> UpdateRegisterAsync(ClientModel AModel)
      {
         return await FRepositoryBase.UpdateOneAsync(AModel);
      }

      public async Task<int> DeleteRegisterAsync(ClientModel AModel)
      {
         try
         {
            return await FRepositoryBase.RemoveOneAsync(AModel);
         }
         catch
         {
            throw;
         }
      }
   }
}
