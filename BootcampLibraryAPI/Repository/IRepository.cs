using System.Linq.Expressions;

namespace BootcampLibraryAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        Task PostAsync(T entity);
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(int id);
        Task PutAsync(T entity);
        Task DeleteAsync(int id);
    }
}
