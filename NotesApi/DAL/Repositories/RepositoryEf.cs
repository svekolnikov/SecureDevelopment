﻿using Lesson1.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;

namespace NotesApi.DAL.Repositories
{
    public class RepositoryEf<T> : IRepositoryEf<T> where T : class, IEntity  
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

        public async Task<int> AddAsync(T note, CancellationToken cancellationToken = default)
        {
            await Set.AddAsync(note, cancellationToken).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Added note: {0}", note);
            return note.Id;
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Updated note: {0}", entity);
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

            _logger.LogInformation("Deleted note: {0}", toDel);

            return true;
        }
    }
}
