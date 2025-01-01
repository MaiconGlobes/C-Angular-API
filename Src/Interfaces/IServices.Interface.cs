namespace BaseCodeAPI.Src.Interfaces
{
   public interface IServices
   {
      public Task<(byte Status, object Json)> GetAllRegistersAsync();
      public Task<(byte Status, object Json)> GetOneRegisterByIdAsync(int AId);
      public Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel);
      public Task<(byte Status, object Json)> UpdateRegisterAsync<T>(T AModel);
      public Task<(byte Status, object Json)> DeleteRegisterAsync(int AId);
   }
}
