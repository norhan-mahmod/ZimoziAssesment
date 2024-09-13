using ZimoziAssesment.Repositories.GenericRepo;

namespace ZimoziAssesment.Repositories.UnitOfWorkRepo
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Save();
    }
}
