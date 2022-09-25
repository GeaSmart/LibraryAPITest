using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BootcampLibraryAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<List<T>> GetAsync()
        {
            //return await context.Students.ToListAsync(); modo sin repository
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task PostAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task PutAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

        }
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>().Where(filter).ToListAsync();
        }
    }
}
