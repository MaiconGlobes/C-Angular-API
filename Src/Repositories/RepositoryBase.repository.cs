using BaseCodeAPI.Src.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseCodeAPI.Src.Repositories
{
   public class RepositoryBase : IRepositoryBase
   {
      private Context FContexto { get; set; }

      public RepositoryBase()
      {
         FContexto = new Context();
      }

      /// <summary>
      /// Método responsável por buscar a entidade do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <returns>Entidade - DbSet [T]</returns>
      public DbSet<T> GetEntity<T>() where T : class
      {
         return FContexto.Set<T>();
      }

      /// <summary>
      /// Método responsável por buscar um registro na tabela do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <returns>Entidade Assíncrona - Task [T]</returns>
      public async Task<T> GetOneAsync<T>() where T : class
      {
         return await FContexto.Set<T>().OfType<T>().SingleOrDefaultAsync();
      }

      /// <summary>
      /// Método responsável por inserção um registro na tabela do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <param name="AModel"></param>
      /// <returns>Status de registros afetados Assíncrono - Task [int]</returns>
      public async Task<int> InsertOneAsync<T>(T AModel) where T : class
      {
         FContexto.Set<T>().Add(AModel);
         return await FContexto.SaveChangesAsync();
      }

      /// <summary>
      /// Método responsável por inserção de vários registro na tabela do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <param name="AListModel">Model da tabela do banco com vários registros</param>
      /// <returns>Status de registros afetados Assíncrono - Task [int]</returns>
      public async Task<int> InsertAllAsync<T>(List<T> AListModel) where T : class
      {
         FContexto.Set<T>().AddRange(AListModel);
         return await FContexto.SaveChangesAsync();
      }

      /// <summary>
      /// Método responsável por inserção um registro na tabela do banco [Sincrono]
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <param name="AModel"></param>
      /// <returns>Status de registros afetados Assíncrono - [int] </returns>
      public int InsertOne<T>(T AModel) where T : class
      {
         FContexto.Set<T>().Add(AModel);
         return FContexto.SaveChanges();
      }

      /// <summary>
      /// Método responsável por atualizção de um registro na tabela do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <param name="AModel">Model da tabela do banco com um registro</param>
      /// <returns>Status de registros afetados Assíncrono - Task [int]</returns>
      public async Task<int> UpdateOneAsync<T>(T AModel) where T : class
      {
         FContexto.Set<T>().Update(AModel); 
         return await FContexto.SaveChangesAsync();
      }

      /// <summary>
      /// Método responsável por exclusão de um registro na tabela do banco
      /// </summary>
      /// <typeparam name="T">Classe genérica</typeparam>
      /// <param name="AModel">Model da tabela do banco com um registro</param>
      /// <returns>Status de registros afetados Assíncrono - Task [int]</returns>
      public async Task<int> RemoveOneAsync<T>(T AModel) where T : class
      {
         FContexto.Set<T>().Remove(AModel);
         return await FContexto.SaveChangesAsync();
      }

      /// <summary>
      /// Método responsável por criar o arquivo do banco de dados
      /// </summary>
      public void EnsureCreated()
      {
         FContexto.Database.EnsureCreated();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação
      /// </summary>
      public IDbContextTransaction BeginTransaction()
      {
         return FContexto.Database.BeginTransaction();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação assíncrona
      /// </summary>
      public async Task<IDbContextTransaction> BeginTransactionAsync()
      {
         return await FContexto.Database.BeginTransactionAsync();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação
      /// </summary>
      public void CommitTransaction()
      {
         FContexto.Database.CommitTransaction();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação assíncrona
      /// </summary>
      public async Task CommitTransactionAsync()
      {
         await FContexto.Database.CommitTransactionAsync();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação
      /// </summary>
      public void RollbackTransaction()
      {
         FContexto.Database.RollbackTransaction();
      }

      /// <summary>
      /// Método responsável por iniciar uma nova transação assíncrona
      /// </summary>
      public async Task RollbackTransactionAsync()
      {
         await FContexto.Database.RollbackTransactionAsync();
      }
   }
}
