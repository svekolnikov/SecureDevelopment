using DebitCardApi.DAL.Interfaces.Base;
using DebitCardApi.DAL.Interfaces.Repositories;
using DebitCardApi.Services.Interfaces;

namespace DebitCardApi.Services
{
    public class DebitCardsManager<T> : IDebitCardsManager<T> where T : class,IEntity

    {
    public DebitCardsManager(IRepositoryEf<T> repositoryEf, IRepositoryDapper repositoryDapper)
    {
        DebitCardsEf = repositoryEf;
        DebitCardsDapper = repositoryDapper;
    }

    public IRepositoryEf<T> DebitCardsEf { get; }
    public IRepositoryDapper DebitCardsDapper { get; }
    }
}
