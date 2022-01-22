using DebitCardApi.DAL.Interfaces.Base;

namespace DebitCardApi.DAL.Interfaces.Repositories
{
    public interface IRepositoryEf<T> : IRepository<T> where T : class, IEntity
    {
    }
}
