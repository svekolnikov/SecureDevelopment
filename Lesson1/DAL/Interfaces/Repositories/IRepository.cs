using Lesson1.DAL.Interfaces.Base;

namespace Lesson1.DAL.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<T?> GetById(int id, CancellationToken cancellationToken = default);
        public Task<int> AddAsync(T entity, CancellationToken cancellationToken = default);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
