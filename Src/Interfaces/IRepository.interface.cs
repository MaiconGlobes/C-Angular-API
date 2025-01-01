namespace BaseCodeAPI.Src.Interfaces
{
   public interface IRepository<T>
   {
      public Task<IEnumerable<T>> GetAllRegisterAsync();
      public Task<T> GetOneRegisterByIdAsync(int AId);
      public Task<T> GetOneRegisterByIdUniqueEntityAsync(int AId);
      public Task<T> GetOneRegisterAsync(T AModel);
      public Task<int> CreateRegisterAsync(T AModel);
      public Task<int> CreateSynchronizeRegisterAsync(T AModel);
      public Task<int> UpdateRegisterAsync(T AModel);
      public Task<int> DeleteRegisterAsync(T AModel);
   }
}
