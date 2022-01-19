using NotesApi.DAL.Interfaces.Base;

namespace NotesApi.DAL.Interfaces.Repositories
{
    public interface IRepositoryEf<T> : IRepository<T> where T : class, IEntity
    {
    }
}
