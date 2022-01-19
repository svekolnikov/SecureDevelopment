﻿using NotesApi.DAL.Interfaces.Base;

namespace NotesApi.DAL.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<T?> GetById(int id, CancellationToken cancellationToken = default);
        public Task<int> AddAsync(T note, CancellationToken cancellationToken = default);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}