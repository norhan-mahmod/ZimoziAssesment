
using Microsoft.EntityFrameworkCore;
using ZimoziAssesment.Data;

namespace ZimoziAssesment.Repositories.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository( ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
            => await context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => context.Remove(entity);

        public async Task<List<T>> GetAllAsync()
            => await context.Set<T>().ToListAsync();

        public async Task<T> GetAsyncById(int id)
            => await context.Set<T>().FindAsync(id);

        public void Update(T entity)
            => context.Set<T>().Update(entity);
    }
}
