using Lesson1.DAL.Interfaces;
using Lesson1.DAL.Interfaces.Base;
using Lesson1.DAL.Interfaces.Repositories;

namespace Lesson1.DAL.Repositories
{
    public class RepositoryEf<T> : IRepository<T> where T : class, IEntity  
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
