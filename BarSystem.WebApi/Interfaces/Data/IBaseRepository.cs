using BarSystem.WebApi.Models.Base;

namespace BarSystem.WebApi.Interfaces.Data
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<int?> CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> EntityExistsAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<bool> UpdateAsync(T entity, int id);
    }
}
