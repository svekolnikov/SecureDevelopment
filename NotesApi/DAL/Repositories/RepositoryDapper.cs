using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;

namespace NotesApi.DAL.Repositories
{
    public class RepositoryDapper<T> : IRepositoryEf<T> where T : class, IEntity
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
