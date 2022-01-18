using Lesson1.DAL.Interfaces.Base;

namespace Lesson1.DAL.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        public Task<T?> GetById(int id, CancellationToken cancellationToken);
        public Task AddAsync(T entity, CancellationToken cancellationToken);
        public Task UpdateAsync(T entity, CancellationToken cancellationToken);
        public Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
