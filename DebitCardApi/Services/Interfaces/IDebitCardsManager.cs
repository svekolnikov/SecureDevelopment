using DebitCardApi.DAL.Interfaces.Base;
using DebitCardApi.DAL.Interfaces.Repositories;

namespace DebitCardApi.Services.Interfaces
{
    public interface IDebitCardsManager<T> where T : class, IEntity
    {
        IRepositoryEf<T> DebitCardsEf { get; }
        IRepositoryDapper DebitCardsDapper { get; }
    }
}
