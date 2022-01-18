using Lesson1.DAL.DataContext;
using Lesson1.DAL.Interfaces.Base;
using Lesson1.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lesson1.DAL.Repositories
{
    public class RepositoryEf<T> : IRepository<T> where T : class, IEntity  
    {
        private readonly NotesDb _dbContext;
        private readonly ILogger<RepositoryEf<T>> _logger;

        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Entities => Set;

        public RepositoryEf(NotesDb dbContext, ILogger<RepositoryEf<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            Set = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await Entities.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task<T?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await Entities.FirstOrDefaultAsync(x => 
                x.Id == id, cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Added entity: {0}", entity);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Updated entity: {0}", entity);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var toDel = await GetById(id, cancellationToken).ConfigureAwait(false);
            if (toDel is null)
            {
                _logger.LogInformation("Entity not found: {0}", toDel);
                return false;
            }

            _dbContext.Entry(toDel).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted entity: {0}", toDel);

            return true;
        }
    }
}
