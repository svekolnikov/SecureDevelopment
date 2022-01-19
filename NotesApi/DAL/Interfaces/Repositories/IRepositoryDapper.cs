using NotesApi.DAL.Interfaces.Base;

namespace NotesApi.DAL.Interfaces.Repositories
{
    public interface IRepositoryDapper<T> : IRepository<T> where T : class, IEntity
    {
    }
}
