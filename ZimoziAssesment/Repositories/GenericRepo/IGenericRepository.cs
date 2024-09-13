using ZimoziAssesment.Data.Entities;

namespace ZimoziAssesment.Repositories.GenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsyncById(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
