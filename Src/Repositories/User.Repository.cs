using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class UserRepository : IRepository<UserModel>
   {
      private RepositoryBase FRepositoryBase { get; set; }

      public UserRepository()
      {
         FRepositoryBase = new RepositoryBase();
      }

      /// <summary>
      /// Obtém todos os registros de usuários do banco de dados de forma assíncrona, incluindo informações relacionadas à pessoa associada a cada usuário.
      /// </summary>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna uma coleção de objetos UserModel contendo os dados dos usuários e suas respectivas informações de pessoa.</returns>
      public async Task<IEnumerable<UserModel>> GetAllRegisterAsync()
      {
         var result = await FRepositoryBase.GetEntity<UserModel>()
                                           .Include(u => u.Person)
                                           .Select(u => new UserModel
                                           {
                                              Id = u.Id,
                                              Email = u.Email,
                                              Password = u.Password,
                                              PersonId = u.PersonId,
                                              Person = new PersonModel
                                              {
                                                 Name = u.Person.Name,
                                                 Document = u.Person.Document,
                                                 Phone = u.Person.Phone,
                                                 Addresses = u.Person.Addresses,
                                              },
                                           })
                                           .ToListAsync();

         return result;
      }

      public async Task<UserModel> GetOneRegisterByIdUniqueEntityAsync(int AId)
      {
         return await FRepositoryBase.GetEntity<UserModel>().FirstOrDefaultAsync(u => u.Id == AId);
      }

      /// <summary>
      /// Obtém um registro de usuário do banco de dados de forma assíncrona com base no email e Password fornecidos.
      /// </summary>
      /// <param name="AModel">O modelo de usuário contendo o email e Password para a consulta.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna o registro de usuário encontrado.</returns>
      public async Task<UserModel> GetOneRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.GetEntity<UserModel>()
                                     .Where(user => user.Email == AModel.Email && user.Password == AModel.Password)
                                     .SingleOrDefaultAsync();
      }

      /// <summary>
      /// Obtém um registro de usuário pelo ID especificado, incluindo informações relacionadas à pessoa associada.
      /// </summary>
      /// <param name="AModel">O modelo de dados contendo o ID do usuário a ser obtido.</param>
      /// <returns>O objeto UserModel contendo as informações do usuário e da pessoa associada, se encontrado; caso contrário, retorna null.</returns>
      public async Task<UserModel> GetOneRegisterByIdAsync(int AId)
      {
         var result = await FRepositoryBase.GetEntity<UserModel>()
                                           .Include(u => u.Person)
                                           .Select(u => new UserModel
                                           {
                                              Id = u.Id,
                                              Email = u.Email,
                                              Password = u.Password,
                                              PersonId = u.PersonId,
                                              Person = new PersonModel
                                              {
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

      /// <summary>
      /// Cria um novo registro de usuário de forma assíncrona no banco de dados.
      /// </summary>
      /// <param name="AModel">O modelo de usuário a ser criado.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna o número de registros afetados.</returns>
      public async Task<int> CreateRegisterAsync(UserModel AModel)
      {
         try
         {
            return await FRepositoryBase.InsertOneAsync(AModel);
         }
         catch (Exception)
         {
            await FRepositoryBase.RollbackTransactionAsync();
            throw;
         }
      }

      public Task<int> CreateSynchronizeRegisterAsync(UserModel AModel)
      {
         throw new NotImplementedException();
      }

      public Task<int> DeleteRegisterAsync(UserModel AModel)
      {
         throw new NotImplementedException();
      }

      public Task<int> UpdateRegisterAsync(UserModel AModel)
      {
         throw new NotImplementedException();
      }
   }
}
