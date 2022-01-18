using Lesson1.DAL.Interfaces.Base;

namespace Lesson1.DAL.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        public Task<T?> GetById(int id, CancellationToken cancellationToken);
        public Task<int> AddAsync(T entity, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
