using System.Collections;
using ZimoziAssesment.Data;
using ZimoziAssesment.Repositories.GenericRepo;

namespace ZimoziAssesment.Repositories.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private Hashtable repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                repositories.Add(type, repository);
            }
            return repositories[type] as IGenericRepository<TEntity>;
        }

        public async Task<int> Save()
            => await context.SaveChangesAsync();
    }
}
